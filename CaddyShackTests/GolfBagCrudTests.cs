using Microsoft.AspNetCore.Mvc.Testing;
using CaddyShackMVC.Models;
using CaddyShackMVC.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CaddyShackTests
{
	[Collection("Controller Tests")]
	public class GolfBagCrudTests : IClassFixture<WebApplicationFactory<Program>>
	{
		private readonly WebApplicationFactory<Program> _factory;

		public GolfBagCrudTests(WebApplicationFactory<Program> factory)
		{
			_factory = factory;
		}

		[Fact]
        public async void Index_ReturnsViewOfAllBags()
        {
			var context = GetDbContext();
			var client = _factory.CreateClient();

			var bag1 = new GolfBag { Player = "Ricky Fowler", Capacity = 11 };
			var bag2 = new GolfBag { Player = "Tiger Woods", Capacity = 2 };

			context.GolfBags.Add(bag1);
			context.GolfBags.Add(bag2);

			var response = await client.GetAsync("/golfbags");
			var html = await response.Content.ReadAsStringAsync();

			response.EnsureSuccessStatusCode();
			Assert.Contains("Ricky Fowler", html);
			Assert.Contains("Tiget Woods", html);
		}

		private CaddyShackContext GetDbContext()
		{
			var optionsBuilder = new DbContextOptionsBuilder<CaddyShackContext>();
			optionsBuilder.UseInMemoryDatabase("TestDatabase");

			var context = new CaddyShackContext(optionsBuilder.Options);
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();

			return context;
		}
	}
}