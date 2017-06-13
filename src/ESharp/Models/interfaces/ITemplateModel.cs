using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ESharp.interfaces
{
    [Serializable]
    public class TemplateModel
    {
        public string Chapter { get; private set; }
        public string Title { get; private set; }
        public int TemplateId { get; protected set; }

        public TemplateModel(IFormCollection form)
        {
            Chapter = ((string) form["Chapter"]).Split(',').Last();
            Title = ((string)form["Title"]).Split(',').Last();
        }

        public TemplateModel() { }
    }
}
