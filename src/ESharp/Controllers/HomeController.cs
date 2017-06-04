using System.Collections.Generic;
using System.IO;
using ESharp.interfaces;
using ESharp.Models;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ESharp.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index(int chapterId)
        {
           // long size = 0;
          //  var files = Request.Form.Files;
           // FileDetails fileDetails;
          /*  using (var reader = new StreamReader(files[0].OpenReadStream()))
            {
                var fileContent = reader.ReadToEnd();
                
            }*/

           // var ioProvider = new IoProvider();
          //  var mp = ioProvider.BuildModel("ESharp.Models.Ajax01Model", ioProvider.Unzip(ioProvider.Read("E:\\E.zip")));




            
            // Получить модель для раздела
            return PartialView("_Template01Parial", (Ajax01Model) new Ajax01Model());
        }

        
        /*public ActionResult ProfessorQueryPartial()
        {
            return PartialView("_SomeParial", new ProfessorQuery(HostingApplication.Context).Get());
        }*/
    }
}
