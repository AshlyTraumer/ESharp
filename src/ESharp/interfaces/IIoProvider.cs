using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using ESharp.Models;

namespace ESharp.interfaces
{
    interface IIoProvider
    {
        byte[] Read(string path);
        List<GeneralFile> Unzip(byte[] zipContent);
        IAjaxModel BuildModel(string template, List<GeneralFile> objects);
    }

    public class IoProvider : IIoProvider
    {
        public byte[] Read(string path)
        {
            return File.ReadAllBytes(path);
        }

        public List<GeneralFile> Unzip(byte[] zipContent)
        {
            var csvList = new List<GeneralFile>();

            var stream = new MemoryStream();
            stream.Write(zipContent, 0, zipContent.Length);

            using (var archive = new ZipArchive(stream, ZipArchiveMode.Read, true))
            {
                foreach (var entry in archive.Entries)
                {
                    using (var reader = new StreamReader(entry.Open()))
                    {
                        var list = reader.ReadToEnd();
                           // .Split('\n')
                           // .ToList();

                        csvList.Add(new GeneralFile(entry.FullName, list));
                    }
                }
            }

            return csvList;
        }

        public IAjaxModel BuildModel(string template, List<GeneralFile> objects)
        {
            var type = Type.GetType(template);
            var i = 0;
            var obj = Activator.CreateInstance(type);
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                property.SetValue(obj, objects[i].list);
                i++;
            }

            return (IAjaxModel) obj;
        }

        
    }

    
}
