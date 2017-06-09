using System;
using Microsoft.AspNetCore.Http;

namespace ESharp.wwwroot.Extensions
{
    public static class FormExtensions
    {
        public static bool IsValid(this IFormCollection collection, Type type)
        {
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name.Contains("TemplateId"))
                {
                    continue;
                }

                if (property.Name.Contains("ImgUrl") && (collection.Files.Count > 0))
                {
                    if (collection.Files[int.Parse(property.Name.Substring(6, 1))] == null)
                    {
                        return false;
                    }
                }
                else
                {
                    if (!collection.ContainsKey(property.Name))
                    {
                        return false;
                    }
                }

            }
            return true;
        }
    }
}
