using System.Collections.Generic;

namespace ESharp.Models
{
    public class AdminView
    {
        public List<string> Articles { get; set; }
        public string Chapter { get; set; }
        public int CurrentPage { get; set; }
    }
}