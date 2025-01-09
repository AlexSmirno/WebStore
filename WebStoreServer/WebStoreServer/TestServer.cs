﻿using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebStoreServer.DAL;

namespace WebStoreServer
{
    public class TestServer : WebApplicationFactory<Program>
    {

        public TestServer() { }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSetting("https_port", "443");

            builder.ConfigureAppConfiguration((_, config) =>
            {
                var root = Directory.GetCurrentDirectory();
                var fileProvider = new PhysicalFileProvider(root);
                config.AddJsonFile(fileProvider, "apptestsettings.json", false, false);
            });
        }
    }
}
