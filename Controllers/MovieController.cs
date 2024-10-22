
using E_Commerce_Project.DBContext;
using E_Commerce_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce_Project.Controllers
{
    public class MovieController : Controller
    {

        private readonly DataBaseContext dataBaseContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MovieController(DataBaseContext _dataBaseContext , IWebHostEnvironment _webHostEnvironment) 
        {
            dataBaseContext = _dataBaseContext; 
            webHostEnvironment = _webHostEnvironment;
        }

		[HttpPost]
		public IActionResult CreatNew(Movie MovieParameter)
		{

			Guid guid = Guid.NewGuid();

			string PictureExtension = Path.GetExtension(MovieParameter.ImageFile.FileName);
			string PicturePath = "/Images/" + guid + PictureExtension;

			MovieParameter.ImagePath = PicturePath;
			string PictureFullPath = webHostEnvironment.WebRootPath + PicturePath;

			FileStream fileStream = new FileStream(PictureFullPath, FileMode.Create);
			MovieParameter.ImageFile.CopyTo(fileStream);
			fileStream.Dispose();

			dataBaseContext.Movie.Add(MovieParameter);
			dataBaseContext.SaveChanges();
			return RedirectToAction("GetHomePage");

		}


        [HttpPost]
        public IActionResult DeleteCurrent(Movie MovieParameter)
        {
            var movie = dataBaseContext.Movie.FirstOrDefault(movie => movie.Id == MovieParameter.Id);

            if (movie.ImagePath != "/Images/No-Image.png")
            {
                if (System.IO.File.Exists(webHostEnvironment.WebRootPath + movie.ImagePath) == true)
                {
                    System.IO.File.Delete(webHostEnvironment.WebRootPath + movie.ImagePath);
                }
            }
            dataBaseContext.Movie.Remove(movie);
            dataBaseContext.SaveChanges();

            return RedirectToAction("GetHomePage");
        }


        [HttpGet]
		public ActionResult GetHomePage()
		{
			
			return View("MovieHomePage", dataBaseContext.Movie.ToList());
			
		}


		[HttpGet]
		public ActionResult GetCreatPage()
		{

			ViewBag.Actors = dataBaseContext.Actors.ToList();
			return View("MovieCreatePage");
		}



        [HttpGet]
        public ActionResult GetDeletePage(int ?id)
        {
            Movie movie = dataBaseContext.Movie.Find(id);

            if (movie == null)
            {
				throw new Exception("No data");
            }
            else
            {
                return View("MovieDeletePage", movie );
            }
        }


		[HttpGet]
		public ActionResult GetEditPage(int? id)
		{
			Movie movie = dataBaseContext.Movie.Find(id);

			if (movie == null)
			{
				return NotFound();
			}
			else
			{
				ViewBag.data = dataBaseContext.Movie.ToList();
				return View("MovieEditePage");
			}
		}


		[HttpGet]
		public  IActionResult GetDetailsPage(int id) 
		{
			var movie = dataBaseContext.Movie.Include(movie1 => movie1._Actors).FirstOrDefault(movie1 => movie1.Id == id);

			if (movie == null) 
			{
				throw new Exception("Movie Don't Exist");
			}
            return View("MovieDetailsPage", movie);

        }


    }
}
