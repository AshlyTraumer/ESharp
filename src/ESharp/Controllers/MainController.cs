using System.Collections.Generic;
using ESharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESharp.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            var wrapper = new StorageManager();
            var model = new List<AdminItem>();
            var chapterList = wrapper.GetChapterList();
            if (chapterList.Count != 0)
            {
                for (int i = 0; i < chapterList.Count; i++)
                {
                    var articlesList = wrapper.GetArticlesTitlesByChapter(chapterList[i]);

                    model.Add(new AdminItem
                    {
                        Chapter = chapterList[i],
                        CurrentPage = i,
                        Articles = articlesList
                    });
                }
            }
            return View(model);
        }
    }
}
