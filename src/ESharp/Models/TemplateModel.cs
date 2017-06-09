using System;
using ESharp.interfaces;
using Microsoft.AspNetCore.Http;

namespace ESharp.Models
{
    [Serializable]
    public class Template01Model : TemplateModel
    {
        public Template01Model(IFormCollection form) : base(form)
        {
            Description = ((string) form["Description"]);
            Description = ((string) form["Description"]).Replace("\r\n","<br>");
            ImgUrl0 = form.Files[0];
            TemplateId = 1;
        }

        public Template01Model() { }

        public string Description { get; private set; }
        public IFormFile ImgUrl0 { get; private set; }
    }

    [Serializable]
    public class Template02Model : TemplateModel
    {
        public Template02Model(IFormCollection form) : base(form)
        {
            ImgUrl0 = form.Files[0];
            TemplateId = 2;
        }

        public Template02Model() { }
        public IFormFile ImgUrl0 { get; private set; }
    }

    [Serializable]
    public class Template03Model : TemplateModel
    {
        public Template03Model(IFormCollection form) : base(form)
        {
            ImgUrl0 = form.Files[0];
            ImgUrl1 = form.Files[1];
            TemplateId = 3;
        }

        public Template03Model() { }

        public IFormFile ImgUrl0 { get; private set; }
        public IFormFile ImgUrl1 { get; private set; }
    }
}
