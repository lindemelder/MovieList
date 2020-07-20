using MovieList.Database;
using System.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieList.Controllers
{
    public class Movies1Controller : Controller
    {
        private MoviesEntities db = new MoviesEntities();

        // GET: Movies1
        public ActionResult Index()
        {
            List<Movies> movies = new List<Movies>();
            movies = db.Movies.Include(m => m.Genre).OrderBy(x => x.Title).ToList();
            return View(movies);
        }

        public ActionResult Create()
        {
            ViewBag.GenreID = new SelectList(db.Genre, "ID", "Genre1");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Title, GenreID, ReleaseDate")] Movies movies)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genre, "ID", "Genre1", movies.GenreID);
            return View(movies);
        }

        //This won't work as expected, this is webApi where data has been json transformed?
        //public ActionResult Create(string title, Genre genre, DateTime releaseDate)
        //{
        //    Movies newMovie = new Movies({ Title = title, Genre = genre, ReleaseDate = releaseDate});
        //    db.Movies.Add(newMovie);
        //    db.SaveChanges();
        //    return View();
        //}

        public ActionResult Details(int? id)
        {
            //the above param of 'id' must match exactly what is named in the view, renamed here to movieId, and it didn't work, keep in mind
            Movies movieList = db.Movies.FirstOrDefault(x => x.ID == id);
            return View(movieList);
        }
    }
}