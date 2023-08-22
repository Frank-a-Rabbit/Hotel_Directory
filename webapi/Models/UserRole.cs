using System;
using Microsoft.AspNetCore.Identity;


namespace HotelDirectory.Models
{
    public class Role : IdentityRole 
    {
        public void ConsoleLog(string message)
        {
            System.Console.WriteLine("Hi there, ", message);
        }
    }
}