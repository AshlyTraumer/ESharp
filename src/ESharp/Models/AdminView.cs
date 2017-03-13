using System.Collections.Generic;

namespace ESharp.Models
{
    public class AdminView
    {
        public List<string> Articles { get; set; }
        public List<string> Chapters { get; set; }
        public int CurrentPage { get; set; }
    }
}