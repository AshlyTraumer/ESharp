using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ESharp.Models
{
    public class ChapterModel
    {
        public ChapterModel(IFormCollection form)
        {
            Chapter = form["Chapter"];
            ChapterSelect = form["ChapterSelect"];
        }

        public ChapterModel()
        { }

        public string Chapter { get; private set; }
        public string ChapterSelect { get; private set; }
    }

    public class LoginModel
    {
        public LoginModel(IFormCollection form)
        {
            Login = form["Login"];
            Password = form["Password"];
        }

        public LoginModel()
        { }

        public string Login { get; private set; }
        public string Password { get; private set; }

        public bool Check()
        {
            return (Login == "Admin" && Password == "1234");
        }
    }
}
