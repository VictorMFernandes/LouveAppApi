﻿using LouveApp.Dal.Integracao;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace LouveApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build().Seed().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .ConfigureKestrel(options => options.AddServerHeader = false)
                    .UseStartup<Startup>();
    }
}
