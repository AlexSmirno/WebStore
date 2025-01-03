﻿

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Domain.Clients;
using WebStore.Domain.Orders;
using WebStore.Domain.Products;
using WebStoreServer;
using WebStoreServer.DAL;

namespace Tests
{
    [Collection("Integration Tests")]
    public abstract class TestBase : IAsyncLifetime
    {
        private readonly TestServer _server;
        private AsyncServiceScope _scope;
        protected IServiceProvider _services;
        protected HttpClient _client;

        public TestBase(TestServer server)
        {
            _server = server;
        }

        public async Task InitializeAsync()
        {
            _client = _server.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost/")
            });

            _scope = _server.Services.CreateAsyncScope();
            _services = _scope.ServiceProvider;

            await ClearDatabaseAsync();
            await SeedDataAsync();
        }

        private async Task ClearDatabaseAsync()
        {
            var context = _services.GetRequiredService<StoreContext>();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }

        private async Task SeedDataAsync()
        {
            var context = _services.GetRequiredService<StoreContext>();

            await context.OrderTypes.AddAsync(new OrderType() { Description = "Import" });
            await context.OrderTypes.AddAsync(new OrderType() { Description = "Export" });

            await context.Clients.AddAsync(new Client() { Mail = "first@mail.ru", Password = "123", FullName = "Name1" });
            await context.Clients.AddAsync(new Client() { Mail = "second@mail.ru", Password = "321", FullName = "Name2" });
            await context.Clients.AddAsync(new Client() { Mail = "third@mail.ru", Password = "213", FullName = "Name3" });

            await context.Products.AddAsync(new Product() { ProductName = "first", Description = "text", Count = 0, Size = 1, Price = 100.1m });
            await context.Products.AddAsync(new Product() { ProductName = "second", Description = "text", Count = 0, Size = 1, Price = 100.1m });
            await context.Products.AddAsync(new Product() { ProductName = "third", Description = "text", Count = 0, Size = 1, Price = 100.1m });

            await context.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await _scope.DisposeAsync();
        }
    }
}