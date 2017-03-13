using Microsoft.AspNetCore.Http;

namespace ESharp.Models
{
    public class ChapterModel
    {
        public ChapterModel(IFormCollection form)
        {
            Chapter = form["Chapter"];
        }

        public string Chapter { get; private set; }
    }
}
