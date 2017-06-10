using System.Collections.Generic;

namespace ESharp.Models
{
    public class AdminItem
    {
        public List<string> Articles { get; set; }
        public string Chapter { get; set; }
        public int CurrentPage { get; set; }
        public string Path { get; set; }
    }

    public class AdminModel
    {
        public List<AdminItem> AdminItems { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentArticle { get; set; }

        public AdminModel()
        {
            AdminItems = new List<AdminItem>();
        }
    }
}