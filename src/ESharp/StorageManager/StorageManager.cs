using System;
using System.Collections.Generic;
using System.IO;
using ESharp.Controllers;
using ESharp.interfaces;
using ESharp.Models;
using Microsoft.Extensions.Primitives;

namespace ESharp
{
    public class StorageManager
    {
        private readonly Zipper _zipper;
        public string Path { get; private set; }

        public StorageManager()
        {
            _zipper = new Zipper();
        }

        public void WriteModelToDat(TemplateModel model)
        {
            var data = _zipper.Zip(model);

            var serializer = new Serializer();
            serializer.SerializeData(data);
            var title = model.Title.Replace('.', ',');

            Path = $"{Directory.GetCurrentDirectory()}\\Articles\\{model.Chapter}\\{title}_{model.TemplateId}.zip";
            serializer.SerializePath(Path);
        }

        public void WriteModelToStorage(byte[] model, string path)
        {
            using (var writer = new BinaryWriter(File.OpenWrite(path)))
            {
                writer.Write(model);
            }
        }

        public void WriteChapterToStorage(ChapterModel model)
        {
            var path = $"{Directory.GetCurrentDirectory()}\\Articles\\Chapters.txt";
            var pathDir = $"{Directory.GetCurrentDirectory()}\\Articles\\{model.Chapter}";

            using (var writer = File.AppendText(path))
            {
                writer.Write(Environment.NewLine + model.Chapter);
            }

            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
        }

        public List<string> GetChapterList()
        {
            var path = $"{Directory.GetCurrentDirectory()}\\Articles\\Chapters.txt";
            var list = new List<string>();

            using (var reader = File.OpenText(path))
            {
                while (!reader.EndOfStream)
                {
                    list.Add(reader.ReadLine());
                }
            }

            return list;
        }

        public List<string> GetArticlesTitlesByChapter(string chapter)
        {
            var path = $"{Directory.GetCurrentDirectory()}\\Articles\\{chapter}";
            var stream = Directory.GetFiles(path);
            var list = new List<string>();

            foreach (var item in stream)
            {
                var index = item.LastIndexOf("\\");
                list.Add(item.Remove(item.Length - 4, 4).Substring(index + 1, item.Length - index - 5));
            }

            return list;
        }

        public void RemoveArticle(int chapter, int article)
        {
            var chapters = GetChapterList();
            var chapterTitle = chapters[chapter];
            var articles = GetArticlesTitlesByChapter(chapterTitle);

            var path = $"{Directory.GetCurrentDirectory()}\\Articles\\{chapterTitle}\\{articles[article]}.zip";
            File.Delete(path);
        }

        public void RemoveArticleByString(string chapter, string article)
        {
            var path = $"{Directory.GetCurrentDirectory()}\\Articles\\{chapter}\\{article}.zip";
            File.Delete(path);
        }

        public void RemoveChapterByTitle(StringValues stringValues)
        {
            var path = $"{Directory.GetCurrentDirectory()}\\Articles\\{stringValues}";

            var di = new DirectoryInfo(path);
            foreach (var fi in di.GetFiles())
            {
                fi.Delete();
            }

            Directory.Delete(path);

            var list = GetChapterList();
            list.Remove(stringValues);

            path = $"{Directory.GetCurrentDirectory()}\\Articles\\Chapters.txt";
            File.Delete(path);

            using (var writer = File.AppendText(path))
            {
                foreach (var listItem in list)
                {
                    writer.WriteLine(listItem);
                }
            }
        }

        public IBaseModel GetArticle(int chapter, int article, out string template)
        {
            var chapters = GetChapterList();
            var chapterTitle = chapters[chapter];
            var articles = GetArticlesTitlesByChapter(chapterTitle);

            var path = $"{Directory.GetCurrentDirectory()}\\Articles\\{chapterTitle}\\{articles[article]}.zip";

            var array = File.ReadAllBytes(path);
            template = articles[article].Substring(articles[article].Length - 1);

            var model = _zipper.Unzip(array, template);
            model.Chapter = chapterTitle;

            return model;
        }


        public string GetPath(int chapter, int article)
        {
            var chapters = GetChapterList();
            var chapterTitle = chapters[chapter];
            var articles = GetArticlesTitlesByChapter(chapterTitle);

            var path = $"... \\ {chapterTitle} \\ {articles[article].Substring(0, articles[article].Length - 2)}";

            return path;
        }

        public void WriteOldData(StringValues stringValues, StringValues stringValues1)
        {
            var serializer = new Serializer();
            serializer.SerializeOldChapter(stringValues);
            serializer.SerializeOldArticle(stringValues1);
        }

        public void WriteNewModelToStorage(byte[] data, string path, string oldChapter, string oldArticle)
        {
            RemoveArticleByString(oldChapter, oldArticle);
            WriteModelToStorage(data, path);
        }
    }
}