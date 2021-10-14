using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CourseWork
{
    // Что бы создать базу данных и протестировать приложение, необходимо провести следующие операции:
    // Package Manager Console -> Список Default Project -> Пункт DataAccessLayer
    // Package Manager Console -> Команда Add-Migration Initial
    // Package Manager Console -> Команда Update-Database
    // После чего приложение ASP.NET Core можно запускать.
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
