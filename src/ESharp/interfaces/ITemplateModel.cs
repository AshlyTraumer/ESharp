using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace ESharp.interfaces
{
    [Serializable]
    public class TemplateModel
    {
        public string Chapter { get; private set; }
        public string Title { get; private set; }

        public TemplateModel(IFormCollection form)
        {
            Chapter = form["Chapter"];
            Title = form["Title"];
        }

        public TemplateModel() { }
    }
}
