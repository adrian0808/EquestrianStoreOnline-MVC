using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Tests.AutomaticTests.Login
{
    public static class DataHelper
    {
        public static string Email { get; } = "test@wp.pl";
        public static string Password { get; } = "3kr39uvK!";
        public static string ConfirmPassword { get; } = "3kr39uvK!";
        public static string ServerUrl { get; } = "https://localhost:44367/";
        public static string IncorrectEmail { get; } = "test2@wp.pl";
        public static string IncorrectPassword { get; } = "123456";
    }
}
