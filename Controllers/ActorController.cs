using E_Commerce_Project.DBContext;
using E_Commerce_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Project.Controllers
{
	public class ActorController : Controller
	{

		private readonly DataBaseContext dataBaseContext;
		private readonly IWebHostEnvironment webHostEnvironment;

		public ActorController(DataBaseContext _dataBaseContext, IWebHostEnvironment _webHostEnvironment)
		{
			dataBaseContext = _dataBaseContext;
			webHostEnvironment = _webHostEnvironment;
		}




		[HttpPost]
		public IActionResult CreatNew(Actor actor)
		{
			if (actor.ImageFile == null)
			{
				actor.ImagePath = "/Images/No-Image.png";
			}
			else
			{
				Guid guid = Guid.NewGuid();
				string ImgExtension = Path.GetExtension(actor.ImageFile.FileName);
				string ImgPath = "/Images/" + guid + ImgExtension;
				//Store the Image path into ImagePath Attribute that exist in Student Class 
				actor.ImagePath = ImgPath;
				string ImgFullPath = webHostEnvironment.WebRootPath + ImgPath;
				FileStream fileStream = new FileStream(ImgFullPath, FileMode.Create);
				actor.ImageFile.CopyTo(fileStream);
				fileStream.Dispose();

			}
			dataBaseContext.Actors.Add(actor);
			dataBaseContext.SaveChanges();
			return RedirectToAction("GetHomePage");
		}


		[HttpPost]
		public IActionResult EditCurrent(Actor actor)
		{
			if (dataBaseContext.Actors.Any(actor1 => actor1.Name == actor.Name))


			{
				ModelState.AddModelError(string.Empty, "Duplicate Actor Name");
			}

			if (ModelState.IsValid == true)
			{
				if (actor.ImageFile != null)
				{
					if (actor.ImagePath != "/Images/No-Image.png")
					{

						if (System.IO.File.Exists(webHostEnvironment.WebRootPath + actor.ImagePath) == true)
						{
							System.IO.File.Delete(webHostEnvironment.WebRootPath + actor.ImagePath);
						}
					}
				}

				{
					Guid guid = Guid.NewGuid();
					string ImgExtension = Path.GetExtension(actor.ImageFile.FileName);
					string ImgPath = "/Images/" + guid + ImgExtension;
					//Store the Image path into ImagePath Attribute that exist in Student Class 
					actor.ImagePath = ImgPath;
					string ImgFullPath = webHostEnvironment.WebRootPath + ImgPath;
					FileStream fileStream = new FileStream(ImgFullPath, FileMode.Create);
					actor.ImageFile.CopyTo(fileStream);
					fileStream.Dispose();

				}

				dataBaseContext.Actors.Update(actor);
				dataBaseContext.SaveChanges();
				return RedirectToAction("GetHomePage");
			}
			else
			{
				ViewBag.AllDepartments = dataBaseContext.Movie.ToList();
				return View("ActorEditePage");
			}
		}


		[HttpPost]
		public IActionResult DeleteCurrent(Actor actor1)
		{
			var actor = dataBaseContext.Actors.FirstOrDefault(Actor => Actor.Id == actor1.Id);

			if (actor.ImagePath != "/Images/No-Image.png")
			{
				if (System.IO.File.Exists(webHostEnvironment.WebRootPath + actor.ImagePath) == true)
				{
					System.IO.File.Delete(webHostEnvironment.WebRootPath + actor.ImagePath);
				}
			}
			dataBaseContext.Actors.Remove(actor);
			dataBaseContext.SaveChanges();

			return RedirectToAction("GetHomePage");
		}




		[HttpGet]
		public ActionResult GetHomePage()
		{
			return View("ActorHomePage", dataBaseContext.Actors.ToList());
		}


		[HttpGet]
		public ActionResult GetCreatPage()
		{
			ViewBag.Movies = dataBaseContext.Movie.ToList();
			return View("ActorCreatePage");
		}


		[HttpGet]
		public ActionResult GetDeletePage(int id)
		{
			Actor Actor1 = dataBaseContext.Actors.Find(id);

			if (Actor1 == null)
			{
				return NotFound();
			}
			else
			{
				return View("ActorDeletePage", Actor1);
			}
		}


		[HttpGet]
		public ActionResult GetEditPage(int id)
		{
			Actor actor = dataBaseContext.Actors.Find(id);

			if (actor == null)
			{
				return NotFound();
			}
			else
			{
				ViewBag.data = dataBaseContext.Actors.ToList();
				return View("ActorEditePage");
			}
		}


		[HttpGet]
		public ActionResult GetDetailsPage(int id)
		{

			Actor Actor1 = dataBaseContext.Actors.Include(actor => actor.Movie).FirstOrDefault(actor => actor.Id == id);
			if (Actor1 == null)
			{
				return NotFound();
			}
			else
			{
				return View("ActorDetailsPage", Actor1);
			}
		}

	}
}




