using System;
using System.Collections.Generic;
using ESharp.Models;
using ESharp.wwwroot.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ESharp.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index(int chapter, int article)
        {
            var wrapper = new StorageManager();
            var model = new AdminModel();
            var chapterList = wrapper.GetChapterList();
            if (chapterList.Count != 0)
            {
                for (int i = 0; i < chapterList.Count; i++)
                {
                    var articlesList = wrapper.GetArticlesTitlesByChapter(chapterList[i]);
                    for (int j= 0; j < articlesList.Count; j++)
                    {
                        articlesList[j] = articlesList[j].Substring(0, articlesList[j].Length - 2);
                    }


                    model.AdminItems.Add(new AdminItem
                    {
                        Chapter = chapterList[i],
                        CurrentPage = i,
                        Articles = articlesList
                    });
                }
            }
            model.CurrentPage = chapter;
            model.CurrentArticle = article;
            return View(model);
        }

        [HttpPost]
        public ActionResult Login()
        {
            LoginModel model;
            if (Request.Form.IsValid(typeof(LoginModel)))
            {
                model = new LoginModel(Request.Form);

            }
            else
            {
                throw new ArgumentNullException("Форма не валидна");
            }

            if (model.Check())
            {
                HttpContext.Session.Set("login", new byte[1] { 1 });
                return RedirectToAction("Index", "Admin", new {page = 0});
            }
            return  RedirectToAction("Index", "Main");
        }
    }
}
