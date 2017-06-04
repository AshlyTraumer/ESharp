using Microsoft.AspNetCore.Http;

namespace ESharp.Models
{
    public class ChapterModel
    {
        public ChapterModel(IFormCollection form)
        {
            Chapter = form["Chapter"];
        }

        public ChapterModel()
        { }

        public string Chapter { get; private set; }
    }
}
