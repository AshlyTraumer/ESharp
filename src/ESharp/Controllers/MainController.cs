using System.Collections.Generic;
using ESharp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ESharp.Controllers
{
    public class MainController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
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
    }
}
