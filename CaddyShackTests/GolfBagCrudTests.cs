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
        public void Test1()
        {

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