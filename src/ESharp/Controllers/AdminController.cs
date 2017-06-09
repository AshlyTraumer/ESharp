using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ESharp.interfaces;
using ESharp.Models;
using ESharp.wwwroot.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ESharp.Controllers
{
    public class AdminController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(int page = 0)
        {
            var wrapper = new StorageManager();
            var model = new List<AdminView>();
            var chapterList = wrapper.GetChapterList();
            if (chapterList.Count != 0)
            {
                for (int i = 0; i < chapterList.Count; i++)
                {
                    var articlesList = wrapper.GetArticlesTitlesByChapter(chapterList[i]);

                    model.Add(new AdminView
                    {
                        Chapter = chapterList[i],
                        CurrentPage = i,
                        Articles = articlesList
                    });
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult GetFormPartial()
        {
            var wrapper = new StorageManager();
            ViewBag.Chapters = wrapper.GetChapterList();

            return PartialView("../Shared/Template1/_Form", new Template01ViewModel());
        }

    

        [HttpPost]
        public ActionResult GetTemplatePartial()
        {
            IBaseModel viewModel;


            switch (Request.Form["template"])
            {
                case "1": viewModel = new Template01ViewModel();
                    break;

                case "2":
                    viewModel = new Template02ViewModel();
                    break;

                case "3":
                    viewModel = new Template03ViewModel();
                    break;

                default: viewModel = new Template03ViewModel();
                    break;
            }

            if (Request.Form.IsValid(viewModel.GetType()))
            {
                viewModel.Bind(Request.Form);
            }
            else
            {
                if (!Request.Form.ContainsKey("Get"))
                ModelState.AddModelError("Valid", "Ошибка валидации. Не все поля заполнены");
                var wrapper = new StorageManager();
                ViewBag.Chapters = wrapper.GetChapterList();


                return PartialView($"../Shared/Template{Request.Form["template"]}/_Form", viewModel.GetPartialModel(Request.Form));
            }

            return PartialView($"../Shared/Template{Request.Form["template"]}/_Preview", viewModel);
        }

        [HttpPost]
        public ActionResult GetArticlePartial(int article = 0, int chapter = 0)
        {
            var template = "";
            var wrapper = new StorageManager();
            var model = wrapper.GetArticle(chapter, article, out template);
            return PartialView($"../Shared/Template{template}/_View", model);
        }

        [HttpGet]
        public ActionResult GetChapterPartial()
        {
            return PartialView("../Shared/Chapter/_ChapterForm", new ChapterModel());
        }

        [HttpGet]
        public ActionResult AcceptForm()
        {
            var serializer = new Serializer();
            var path = serializer.DeserializePath();
            var data = serializer.DeserializeData();
            var oldChapter = serializer.DeserializeOldChapterPath();
            var oldArticle = serializer.DeserializeOldArticlePath();
            var wrapper = new StorageManager();

            if (oldChapter == "")
            {
                wrapper.WriteModelToStorage(data, path);
            }
            else
            {
                wrapper.WriteNewModelToStorage(data, path, oldChapter, oldArticle);
            }



            return Redirect(Url.Action("Index","Admin", new {pages = 0}));
        }

        [HttpPost]
        public ActionResult AddChapter()
        {
            ChapterModel model;
            if (Request.Form.IsValid(typeof(ChapterModel)))
            {
                model = new ChapterModel(Request.Form);
            }
            else
            {
                throw new ArgumentNullException("Форма не валидна");
            }

            var wrapper = new StorageManager();
            wrapper.WriteChapterToStorage(model);

            return RedirectToAction("Index","Admin", new {page = 0});
        }

        [HttpPost]
        public ActionResult ChangeChapter()
        {
            ChapterModel model;
            if (Request.Form.IsValid(typeof(ChapterModel)))
            {
                model = new ChapterModel(Request.Form);
            }
            else
            {
                throw new ArgumentNullException("Форма не валидна");
            }

            var wrapper = new StorageManager();
            wrapper.WriteChapterToStorage(model);

            return RedirectToAction("Index", "Admin", new { page = 0 });
        }

        public IActionResult DeleteArticle(int article = 0, int chapter = 0)
        {
            var wrapper = new StorageManager();
            wrapper.RemoveArticle(chapter, article);
            return RedirectToAction("Index", "Admin", new {page = chapter});
        }

        [HttpGet]
        public IActionResult ChangeArticleView(int article = 0, int chapter = 0)
        {
            var wrapper = new StorageManager();
            var template = "";
            var model = wrapper.GetArticle(chapter, article, out template);
            ViewBag.Chapters = wrapper.GetChapterList();
            return PartialView($"../Shared/Template{template}/_Form", model);
        }

        

        public IActionResult DeleteChapter(int article = 0, int chapter = 0)
        {
            var wrapper = new StorageManager();
            var request = Request.Form;
            wrapper.RemoveChapterByTitle(request["Chapter"]);
            return RedirectToAction("Index","Admin");
        }
    }
}
