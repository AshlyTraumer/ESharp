using Microsoft.AspNetCore.Http;

namespace ESharp.Models
{
    public class Template01ViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] ImgUrl0 { get; set; }
        public string Chapter { get; set; }

        public Template01ViewModel GetPartialModel(IFormCollection requestForm)
        {
            var model = new Template01ViewModel();
            if (requestForm.ContainsKey("Title")) model.Title = requestForm["Title"];
            if (requestForm.ContainsKey("Description")) model.Description = requestForm["Description"];
            if (requestForm.ContainsKey("Chapter")) model.Chapter = requestForm["Chapter"];
            return model;
        }
    }
}
