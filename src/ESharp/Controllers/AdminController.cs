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

            return PartialView("../Shared/Template1/_TemplateForm1", new Template01ViewModel());
        }

    

        [HttpPost]
        public ActionResult GetTemplatePartial()
        {
            var viewModel = new Template01ViewModel();

            if (Request.Form.IsValid(typeof(Template01ViewModel)))
            {
                var model = new Template01Model(Request.Form);

                var wrapper = new StorageManager();
                wrapper.WriteModelToDat(model);

                if (Request.Form.ContainsKey("OldChapter"))
                {
                    wrapper.WriteOldData(Request.Form["OldChapter"], Request.Form["OldArticle"]);
                }
                else
                {
                    wrapper.WriteOldData("", "");
                }

                viewModel = new Template01ViewModel
                {
                    Title = model.Title,
                    Description = model.Description
                };

                using (var binaryReader = new BinaryReader(model.ImgUrl0.OpenReadStream()))
                {
                    viewModel.ImgUrl0 = binaryReader.ReadBytes((int)model.ImgUrl0.Length);
                }
            }
            else
            {
                if (!Request.Form.ContainsKey("Get"))
                ModelState.AddModelError("Valid", "Ошибка валидации. Не все поля заполнены");
                var wrapper = new StorageManager();
                ViewBag.Chapters = wrapper.GetChapterList();
               
                return PartialView("../Shared/Template1/_TemplateForm1", viewModel.GetPartialModel(Request.Form));
            }

            return PartialView("../Shared/Template1/_TemplateDialog", viewModel);
        }

        [HttpPost]
        public ActionResult GetArticlePartial(int article = 0, int chapter = 0)
        {
            var wrapper = new StorageManager();
            var model = wrapper.GetArticle(chapter, article);
            return PartialView("../Shared/Template1/_Template1", model);
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
            var model = wrapper.GetArticle(chapter, article);
            ViewBag.Chapters = wrapper.GetChapterList();
            return PartialView("../Shared/Template1/_TemplateForm1", model);
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
