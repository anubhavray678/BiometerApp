using BiometerApp.Data;
//using BiometerApp.Migrations.BlogDetailsDb;
using BiometerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiometerApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogDetailsDbContext _bdb;
        public BlogController(BlogDetailsDbContext bdb, IWebHostEnvironment hostEnvironment)
        {
            _bdb = bdb;
            
        }
        public IActionResult Index()
        {
            IEnumerable<BlogDetails> objBlogList = _bdb.BlogData;
            return View(objBlogList);
        }
    }
}
