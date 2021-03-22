using IoC.WPF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC.WPF.Data
{
    internal static class DbRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
           .AddDbContext<BookinistDB>(opt =>
           {
               var type = configuration["Type"];
               switch (type)
               {
                   case null: throw new InvalidOperationException("Не определён тип БД");
                   default:  throw new InvalidOperationException($"Тип подключения {type} не поддерживается");
                   case "MSSQL":
                       opt.UseSqlServer(configuration.GetConnectionString(type));
                       break;
                   case "InMemory":
                       opt.UseInMemoryDatabase("Bookinist.db");
                       break;
               }
           })
           ;
    }
}
