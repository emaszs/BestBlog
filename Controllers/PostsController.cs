using BestBlog.DAL;
using BestBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BestBlog.Controllers
{
    public class PostsController : Controller
    {
        private BlogContext model = new BlogContext();

        //
        // GET: /Posts/

        public ActionResult Index()
        {
            return View();
        }

        // GET: /Posts/Edit/5
        public ActionResult Edit(int id)
        {
            if (!isAdmin)
            {
                return RedirectToAction("Index");
            }

            Post post = model.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            StringBuilder tagList = new StringBuilder();
            foreach (Tag tag in post.Tags)
            {
                tagList.AppendFormat("{0} ", tag.Name);
            }
            ViewBag.Tags = tagList.ToString();
            CreatePostViewModel createPostViewModel = new CreatePostViewModel();
            createPostViewModel.Tags = tagList.ToString();
            createPostViewModel.Title = post.Title;
            createPostViewModel.Body = post.Body;
            createPostViewModel.DateCreated = post.DateCreated;
            createPostViewModel.Id = id;
            return View(createPostViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CreatePostViewModel createPostViewModel)
        {
            if (!isAdmin)
            {
                return RedirectToAction("Index");
            }

            Post post = model.Posts.Find(createPostViewModel.Id);
            post.Title = createPostViewModel.Title;
            post.Body = createPostViewModel.Body;
            post.DateCreated = createPostViewModel.DateCreated;
            post.Tags.Clear();
            var tags = createPostViewModel.Tags ?? string.Empty;
            string[] tagNames = tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string tagName in tagNames)
            {
                Tag tag = model.Tags.Where(x => x.Name == tagName).FirstOrDefault() ?? new Tag() { Name = tagName };
                post.Tags.Add(tag);
            }
            model.Entry(post).State = EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("Edit", new { id = post.ID });
        }

        public ActionResult Create()
        {
            if (!isAdmin)
            {
                return RedirectToAction("Index");
            }

            var createPostViewModel = new CreatePostViewModel() { DateCreated = System.DateTime.Now };
            return View(createPostViewModel);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostViewModel createPostViewModel)
        {
            if (!isAdmin)
            {
                return RedirectToAction("Index");
            }

            Post post = new Post();
            post.Title = createPostViewModel.Title;
            post.Body = createPostViewModel.Body;
            post.DateCreated = createPostViewModel.DateCreated;

            var tags = createPostViewModel.Tags ?? string.Empty;
            string[] tagNames = tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string tagName in tagNames)
            {
                Tag tag = model.Tags.Where(x => x.Name == tagName).FirstOrDefault() ?? new Tag() { Name = tagName };
                post.Tags.Add(tag);
            }
            model.Posts.Add(post);
            model.SaveChanges();

            return RedirectToAction("Edit", new { id = post.ID });
        }

        // TODO: Don't just return true
        public bool isAdmin { get { return true; } }
    }
}
