using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCustomTestBlog.Models;
using CommonLib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCustomTestBlog.Controllers
{
    public class AccountController : Controller
    {
        private IRepository<User> db;
        private IRepository<Post> dbPost;
        private IRepository<Comment> dbCom;

        public AccountController(IRepository<User> _db, IRepository<Post> _dbPost, IRepository<Comment> _dbCom)
        {
            db = _db;
            dbPost = _dbPost;
            dbCom = _dbCom;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(db.GetItems().FirstOrDefault(x => x.E_Mail == User.Identity.Name));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = db.GetItem(id);
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(User newUser)
        {
            db.GetItem(newUser.Id).E_Mail = newUser.E_Mail;
            db.GetItem(newUser.Id).Password = newUser.Password;
            db.GetItem(newUser.Id).UserName = newUser.UserName;
            return RedirectToAction("Index", "Account");
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePost(string Message)
        {
            var UserId = db.GetItems().FirstOrDefault(x => x.E_Mail == User.Identity.Name).Id;
            dbPost.Create(new Post { User = db.GetItem(UserId), Message = Message });
            return RedirectToAction("Index", "Account");
        }

        [Authorize]
        public IActionResult Post() =>
            PartialView("Post", dbPost.GetItems());

        [Authorize]
        public IActionResult AllUsers(string name)
        {
            if(string.IsNullOrEmpty(name))
                return View(db.GetItems().Where(x => x.E_Mail != User.Identity.Name));

            return View(db.GetItems().Where(x => x.UserName.Contains(name)));
        }

        [Authorize]
        public IActionResult Page(int Id)
        {
            return View(db.GetItem(Id));
        }

        [Authorize]
        public IActionResult CreateComment(int PostId)
        {
            ViewBag.PostId = PostId;
            return PartialView("CreateComment");
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateComment(int PostId, string Message)
        {
            var UserId = db.GetItems().FirstOrDefault(x => x.E_Mail == User.Identity.Name).Id;
            dbCom.Create(new Comment { Message = Message, PostId = PostId, User = db.GetItem(UserId)});
            int idThisPage = dbPost.GetItem(PostId).User.Id;
            if(UserId == idThisPage)
            {
                return RedirectToAction("Index", "Account");
            }

            return RedirectToAction("Page", new { Id = idThisPage});
        }

        [Authorize]
        public IActionResult Comment() =>
            PartialView("Comment", dbCom.GetItems());
    }
}
