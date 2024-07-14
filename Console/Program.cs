using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VAN.SQLServerCore.SQLServer;
using VAN.SQLServerCore.SQLServer.Models;

public class Program
{
    private static async Task Main(string[] args)
    {
        //var optionsBuilder = new DbContextOptionsBuilder<SQLServerInit>();
        //optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=workdatabase;Persist Security Info=True;User ID=root;Password=123456;Encrypt=False");

        using (var sqlServer = new SQLServerInit("Data Source=localhost;Initial Catalog=workdatabase;Persist Security Info=True;User ID=root;Password=123456;Encrypt=False"))
        {
            string sql = "SELECT * FROM [work].[user]";
            List<User> rs = await sqlServer.Users.FromSqlRaw(sql).ToListAsync();

            foreach (var user in rs)
            {
                Console.WriteLine($"User: {user.Name}, Password: {user.Password}");
            }
        }
    }
}