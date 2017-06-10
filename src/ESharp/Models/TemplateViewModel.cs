using System.IO;
using Microsoft.AspNetCore.Http;

namespace ESharp.Models
{
    public interface IBaseModel
    {
        string Chapter { get; set; }
        IBaseModel GetPartialModel(IFormCollection requestForm);
        void Bind(IFormCollection requestForm);

    }

    public class Template01ViewModel : IBaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] ImgUrl0 { get; set; }
        public string Chapter { get; set; }

        public IBaseModel GetPartialModel(IFormCollection requestForm)
        {
            var model = new Template01ViewModel();
            if (requestForm.ContainsKey("Title")) model.Title = requestForm["Title"];
            if (requestForm.ContainsKey("Description")) model.Description = requestForm["Description"];
            if (requestForm.ContainsKey("Chapter")) model.Chapter = requestForm["Chapter"];
            return model;
        }

        public void Bind(IFormCollection requestForm)
        {
            var model = new Template01Model(requestForm);

            var wrapper = new StorageManager();
            wrapper.WriteModelToDat(model);

            if (requestForm.ContainsKey("OldChapter"))
            {
                wrapper.WriteOldData(requestForm["OldChapter"], requestForm["OldArticle"]);
            }
            else
            {
                wrapper.WriteOldData("", "");
            }

            Title = model.Title;
            Description = model.Description;
            

            using (var binaryReader = new BinaryReader(model.ImgUrl0.OpenReadStream()))
            {
                ImgUrl0 = binaryReader.ReadBytes((int)model.ImgUrl0.Length);
            }
        }
    }

    public class Template02ViewModel : IBaseModel
    {
        public string Title { get; set; }
        public byte[] ImgUrl0 { get; set; }
        public string Chapter { get; set; }

        public IBaseModel GetPartialModel(IFormCollection requestForm)
        {
            var model = new Template02ViewModel();
            if (requestForm.ContainsKey("Title")) model.Title = requestForm["Title"];
            if (requestForm.ContainsKey("Chapter")) model.Chapter = requestForm["Chapter"];
            return model;
        }

        public void Bind(IFormCollection requestForm)
        {
            var model = new Template02Model(requestForm);

            var wrapper = new StorageManager();
            wrapper.WriteModelToDat(model);

            if (requestForm.ContainsKey("OldChapter"))
            {
                wrapper.WriteOldData(requestForm["OldChapter"], requestForm["OldArticle"]);
            }
            else
            {
                wrapper.WriteOldData("", "");
            }

            Title = model.Title;

            using (var binaryReader = new BinaryReader(model.ImgUrl0.OpenReadStream()))
            {
                ImgUrl0 = binaryReader.ReadBytes((int)model.ImgUrl0.Length);
            }
        }
    }

    public class Template03ViewModel : IBaseModel
    {
        public string Title { get; set; }
        public byte[] ImgUrl0 { get; set; }
        public byte[] ImgUrl1 { get; set; }
        public string Chapter { get; set; }

        public IBaseModel GetPartialModel(IFormCollection requestForm)
        {
            var model = new Template03ViewModel();
            if (requestForm.ContainsKey("Title")) model.Title = requestForm["Title"];
            if (requestForm.ContainsKey("Chapter")) model.Chapter = requestForm["Chapter"];
            return model;
        }

        public void Bind(IFormCollection requestForm)
        {
            var model = new Template03Model(requestForm);

            var wrapper = new StorageManager();
            wrapper.WriteModelToDat(model);

            if (requestForm.ContainsKey("OldChapter"))
            {
                wrapper.WriteOldData(requestForm["OldChapter"], requestForm["OldArticle"]+"_"+requestForm["template"]);
            }
            else
            {
                wrapper.WriteOldData("", "");
            }

            Title = model.Title;

            using (var binaryReader = new BinaryReader(model.ImgUrl0.OpenReadStream()))
            {
                ImgUrl0 = binaryReader.ReadBytes((int)model.ImgUrl0.Length);
            }

            using (var binaryReader = new BinaryReader(model.ImgUrl1.OpenReadStream()))
            {
                ImgUrl1 = binaryReader.ReadBytes((int)model.ImgUrl1.Length);
            }
        }
    }
}
