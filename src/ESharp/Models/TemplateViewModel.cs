using System.IO;
using System.Linq;
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

            if (requestForm.ContainsKey("Title")) model.Title = requestForm["Title"].Last();
            if (requestForm.ContainsKey("Description")) model.Description = requestForm["Description"].Last();
            if (requestForm.ContainsKey("Chapter")) model.Chapter = requestForm["Chapter"].Last();
            return model;
        }

        public void Bind(IFormCollection requestForm)
        {
            var model = new Template01Model(requestForm);

            var wrapper = new StorageManager();
            wrapper.WriteModelToDat(model);

            if (requestForm.ContainsKey("OldChapter"))
            {
                var oldChapter = requestForm["OldChapter"].Last();
                var oldArticle = requestForm["OldArticle"].Last();

                wrapper.WriteOldData(oldChapter, oldArticle);
            }
            else
            {
                wrapper.WriteOldData("", "");
            }

            Title = model.Title;
            Description = model.Description;
            Chapter = model.Chapter;

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
            if (requestForm.ContainsKey("Title")) model.Title = requestForm["Title"].Last();
            if (requestForm.ContainsKey("Chapter")) model.Chapter = requestForm["Chapter"].Last();
            return model;
        }

        public void Bind(IFormCollection requestForm)
        {
            var model = new Template02Model(requestForm);

            var wrapper = new StorageManager();
            wrapper.WriteModelToDat(model);

            if (requestForm.ContainsKey("OldChapter"))
            {
                var oldChapter = requestForm["OldChapter"].Last();
                var oldArticle = requestForm["OldArticle"].Last();

                wrapper.WriteOldData(oldChapter, oldArticle);
            }
            else
            {
                wrapper.WriteOldData("", "");
            }

            Title = model.Title;
            Chapter = model.Chapter;

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
            if (requestForm.ContainsKey("Title")) model.Title = requestForm["Title"].Last();
            if (requestForm.ContainsKey("Chapter")) model.Chapter = requestForm["Chapter"].Last();
            return model;
        }

        public void Bind(IFormCollection requestForm)
        {
            var model = new Template03Model(requestForm);

            var wrapper = new StorageManager();
            wrapper.WriteModelToDat(model);

            if (requestForm.ContainsKey("OldChapter"))
            {
                var oldChapter = requestForm["OldChapter"].Last();
                var oldArticle = requestForm["OldArticle"].Last() + "_" + requestForm["template"].Last();

                wrapper.WriteOldData(oldChapter, oldArticle);
            }
            else
            {
                wrapper.WriteOldData("", "");
            }

            Title = model.Title;
            Chapter = model.Chapter;

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

    public class Template04ViewModel : IBaseModel
    {
        public string Title { get; set; }
        public byte[] ImgUrl0 { get; set; }
        public byte[] ImgUrl1 { get; set; }
        public string Chapter { get; set; }

        public IBaseModel GetPartialModel(IFormCollection requestForm)
        {
            var model = new Template04ViewModel();
            if (requestForm.ContainsKey("Title")) model.Title = requestForm["Title"].Last();
            if (requestForm.ContainsKey("Chapter")) model.Chapter = requestForm["Chapter"].Last();
            return model;
        }

        public void Bind(IFormCollection requestForm)
        {
            var model = new Template04Model(requestForm);

            var wrapper = new StorageManager();
            wrapper.WriteModelToDat(model);

            if (requestForm.ContainsKey("OldChapter"))
            {
                var oldChapter = requestForm["OldChapter"].Last();
                var oldArticle = requestForm["OldArticle"].Last() + "_" + requestForm["template"].Last();

                wrapper.WriteOldData(oldChapter, oldArticle);
            }
            else
            {
                wrapper.WriteOldData("", "");
            }

            Title = model.Title;
            Chapter = model.Chapter;

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

    public class Template05ViewModel : IBaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Chapter { get; set; }

        public IBaseModel GetPartialModel(IFormCollection requestForm)
        {
            var model = new Template05ViewModel();
            if (requestForm.ContainsKey("Title")) model.Title = requestForm["Title"].Last();
            if (requestForm.ContainsKey("Description")) model.Description = requestForm["Description"].Last();
            if (requestForm.ContainsKey("Chapter")) model.Chapter = requestForm["Chapter"].Last();
            return model;
        }

        public void Bind(IFormCollection requestForm)
        {
            var model = new Template05Model(requestForm);

            var wrapper = new StorageManager();
            wrapper.WriteModelToDat(model);

            if (requestForm.ContainsKey("OldChapter"))
            {
                var oldChapter = requestForm["OldChapter"].Last();
                var oldArticle = requestForm["OldArticle"].Last() + "_" + requestForm["template"].Last();

                wrapper.WriteOldData(oldChapter, oldArticle);
            }
            else
            {
                wrapper.WriteOldData("", "");
            }

            Title = model.Title;
            Description = model.Description;
            Chapter = model.Chapter;
        }
    }

    public class Template06ViewModel : IBaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Chapter { get; set; }

        public IBaseModel GetPartialModel(IFormCollection requestForm)
        {
            var model = new Template06ViewModel();
            if (requestForm.ContainsKey("Title")) model.Title = requestForm["Title"].Last();
            if (requestForm.ContainsKey("Description")) model.Description = requestForm["Description"].Last();
            if (requestForm.ContainsKey("Chapter")) model.Chapter = requestForm["Chapter"].Last();
            return model;
        }

        public void Bind(IFormCollection requestForm)
        {
            var model = new Template06Model(requestForm);

            var wrapper = new StorageManager();
            wrapper.WriteModelToDat(model);

            if (requestForm.ContainsKey("OldChapter"))
            {
                var oldChapter = requestForm["OldChapter"].Last();
                var oldArticle = requestForm["OldArticle"].Last()+ "_" + requestForm["template"].Last();

                wrapper.WriteOldData(oldChapter, oldArticle);
            }
            else
            {
                wrapper.WriteOldData("", "");
            }

            Title = model.Title;
            Description = model.Description;
            Chapter = model.Chapter;

        }
    }


}
