using CaddyShackMVC.DataAccess;
using CaddyShackMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaddyShackMVC.Controllers
{
	public class GolfBagsController : Controller
	{
		private readonly CaddyShackContext _context;

		public GolfBagsController(CaddyShackContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var bags = _context.GolfBags.ToList();
			return View(bags);
		}

		[Route("/golfbags/{id:int}")]
		public IActionResult Show(int id)
		{
			var bag = _context.GolfBags
				.Where(b => b.Id == id)
				.Include(b => b.Clubs)
				.FirstOrDefault();

			return View(bag);
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var bagToDelete = _context.GolfBags.Find(id);
			_context.GolfBags.Remove(bagToDelete);
			_context.SaveChanges();

			return Redirect("/golfbags");
		}

		public IActionResult New()
		{
			return View();
		}

		[HttpPost]
		[Route("/golfbags")]
		public IActionResult Create(GolfBag bag)
		{
			_context.GolfBags.Add(bag);
			_context.SaveChanges();

			return Redirect($"/golfbags/{bag.Id}");
		}
	}
}
