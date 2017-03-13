﻿using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using ESharp.interfaces;
using ESharp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ESharp.Controllers
{
    public class Zipper
    {
        public byte[] Zip(TemplateModel model)
        {

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var type = model.GetType();
                    var properties = type.GetProperties();

                    foreach (var propery in properties)
                    {
                        var name = propery.Name;

                        if (name == "Chapter") continue;

                        name = name.Contains("ImgUrl") ? $"{name}.jpg" : $"{name}.txt";

                        var entry = archive.CreateEntry(name);

                        if (!name.Contains("ImgUrl"))
                        {
                            using (var streamWriter = new StreamWriter(entry.Open(), Encoding.UTF8))
                            {
                                var obj = model.GetType()
                                    .GetProperty(propery.Name)
                                    .GetValue(model);

                                streamWriter.Write(obj);
                            }
                        }
                        else
                        {
                            using (var e = entry.Open())
                            {
                                ((IFormFile)model.GetType()
                                      .GetProperty(propery.Name)
                                      .GetValue(model))
                                  .CopyTo(e);
                            }
                        }

                    }

                    return memoryStream.GetBuffer();

                }

            }
        }

        public Template01ViewModel Unzip(byte[] zipContent)
        {

            var model = new Template01ViewModel();
            var type = model.GetType();
            var stream = new MemoryStream();

            stream.Write(zipContent, 0, zipContent.Length);

            using (var archive = new ZipArchive(stream, ZipArchiveMode.Read, true))
            {
                foreach (var entry in archive.Entries)
                {
                    using (var reader = new StreamReader(entry.Open()))
                    {

                        var name = entry.FullName.Substring(0, entry.FullName.IndexOf('.'));
                        var property = type.GetProperty(name);

                        if (!name.Contains("ImgUrl"))
                        {
                            property.SetValue(model,reader.ReadToEnd());
                        }
                        else
                        {
                            using (var binaryReader = new BinaryReader(entry.Open()))
                            {
                               
                                property.SetValue(model, binaryReader.ReadBytes((int)entry.Length));
                            }
                        }
                        
                       




                        //  csvList.Add(new CsvFile(entry.FullName, list));

                    }

                }

            }



            return model;
        }
    }
}