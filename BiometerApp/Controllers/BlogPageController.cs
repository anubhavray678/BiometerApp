using BiometerApp.Data;
using BiometerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiometerApp.Controllers
{
    public class BlogPageController : Controller
    {
        private readonly BlogDetailsDbContext _blogPageDb;
       
        public BlogPageController(BlogDetailsDbContext blogPageDb)
        {
            _blogPageDb = blogPageDb;

        }
     
        public IActionResult Index(int id)

        {
            ViewBag.Id = id;
           IEnumerable<BlogDetails> objBlogList = _blogPageDb.BlogData;
           return View(objBlogList);
        }
        //public IActionResult Index(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var BlogFromDb = _blogPageDb.BlogData.Find(id);
        //    if (BlogFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(BlogFromDb);
        //}

    }
}
