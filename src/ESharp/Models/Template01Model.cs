﻿using System;
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
            
        }

        public Template01Model() { }

        public string Description { get; private set; }
        public IFormFile ImgUrl0 { get; private set; }
    }
}
