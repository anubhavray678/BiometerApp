using BiometerApp.Data;
using BiometerApp.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Hosting.Internal;


namespace BiometerApp.Controllers
{
    [Authorize]
    public class BlogDetailController : Controller
    {
        private readonly BlogDetailsDbContext _blogDb;
        private readonly IWebHostEnvironment _hostEnvironment;
  
        public BlogDetailController(BlogDetailsDbContext blogDb, IWebHostEnvironment hostEnvironment)
        {

            _blogDb = blogDb;
            _hostEnvironment = hostEnvironment;
          
        }
        //public IActionResult Index()
        //{
        //   IEnumerable<BlogDetails> objBlogList = _blogDb.BlogData;
        //   return View(objBlogList);
        //}
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(BlogDetails obj)
        {
            //if (obj.CategoryName == obj.BlogTitle.ToString()) 
            //{
            //    ModelState.AddModelError("the CustumError", "Category name and Blog Title Can not be same.");
            
            //}


            if (ModelState.IsValid)
            {
                //if (obj.CoverImage != null)
                //{
                //    string folder = "cover";
                //    folder += Guid.NewGuid().ToString() + "_" + obj.CoverImage.FileName;
                //    string serverfolder = Path.Combine(_hostEnvironment.WebRootPath, folder);
                //    await obj.CoverImage.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                //    var filemodel = new BlogDetails()
                //    {
                //        FirstPara=obj.FirstPara,
                //        SecondPara=obj.SecondPara,
                //        AutherName=obj.AutherName,
                //        BlogTitle=obj.BlogTitle,
                //        CategoryName=obj.CategoryName,

                //        CoverImagePath = serverfolder,
                //    };
                //    _blogDb.BlogData.Add(filemodel);
                //    _blogDb.SaveChanges();
                //}

              
              
                return View(obj);

            }
            _blogDb.BlogData.Add(obj);
            //_blogDb.SaveChanges();
            //TempData["success"] = "message sent successfully";
            //upload
            var imgId = obj.Id;
            //get webrootpath
            string wwrootpath = _hostEnvironment.WebRootPath;
            var files =HttpContext.Request.Form.Files;
            var savefile = _blogDb.BlogData.Find(imgId);
            //upload file
            if(files.Count != 0)
            {
                var ImagePath = @"cover\";
                var Extension = Path.GetExtension(files[imgId].FileName);
                if (Extension.ToLower().Equals(".jpg") || Extension.ToLower().Equals(".jpeg") || Extension.ToLower().Equals(".png"))
                {
                    var RelativeImagePath = ImagePath + Guid.NewGuid().ToString() + Extension;
                    var AbsImagePath = Path.Combine(wwrootpath, RelativeImagePath);
                    //upload on server
                    using (var fileStream = new FileStream(AbsImagePath, FileMode.Create))
                    {
                        files[imgId].CopyTo(fileStream);
                    }
                    //set the image path on database
                    //savefile.CoverImagePath = RelativeImagePath;
                    obj.CoverImagePath = RelativeImagePath;
                    _blogDb.SaveChanges();
                    TempData["success"] = "Blog Uploded Successfully";

                }
               

            }
            else
            {
                ModelState.AddModelError("the CustumError", "only jpg, jpeg and png format are acceptable.");
            }
            
          return RedirectToAction("Index");

        }
        [Authorize]
        public IActionResult Details()
        {
            IEnumerable<BlogDetails> objBlogList = _blogDb.BlogData;
            return View(objBlogList);
        }
        //GET
        public IActionResult Edit(int? id) 
        {
            if(id == null || id == 0)
            {
                return NotFound();  
            }
            var BlogFromDb = _blogDb.BlogData.Find(id);
            //var BlogFromDbFirst = _blogDb.BlogData.FirstOrDefault(u => u.Id == id);
            //var BlogFromDbSingle = _blogDb.BlogData.SingleOrDefault(u => u.Id == id);
            if (BlogFromDb == null)
            {
                return NotFound();
            }
            return View(BlogFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BlogDetails obj)
        {
            //if (obj.CoverImage != null)
            //{
            //    string folder = "image/cover";
            //    folder += Guid.NewGuid().ToString() + "_" + obj.CoverImage.FileName;
            //    string serverfolder = Path.Combine(_hostEnvironment.WebRootPath, folder);
            //    await obj.CoverImage.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            //}
           
            if (obj.CategoryName == obj.BlogTitle.ToString())
            {
                ModelState.AddModelError("the CustumError", "Category name and Blog Title Can not be same.");

            }


            if (ModelState.IsValid)
            {
                _blogDb.BlogData.Update(obj);
                _blogDb.SaveChanges();
                TempData["success"] = "Edited successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }





        //upload function
        //private string UploadedFile(SpeakerViewModel model)
        //{
        //    string uniqueFileName = string.Empty;

        //    if (model.SpeakerPicture != null)
        //    {
        //        string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Uploads");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.SpeakerPicture.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.SpeakerPicture.CopyTo(fileStream);
        //        }
        //    }

        //    return uniqueFileName;
        //}



        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var BlogFromDb = _blogDb.BlogData.Find(id);
            //var BlogFromDbFirst = _blogDb.BlogData.FirstOrDefault(u => u.Id == id);
            //var BlogFromDbSingle = _blogDb.BlogData.SingleOrDefault(u => u.Id == id);
            if (BlogFromDb == null)
            {
                return NotFound();
            }
            return View(BlogFromDb);
        }

        [HttpPost]

        public IActionResult DeletePost(int? id)
        {
            var BlogFromDb = _blogDb.BlogData.Find(id);
            //BlogDetails obj = _blogDb.BlogData.Find(id);
            if (BlogFromDb == null)
            {
                return NotFound();
            }
            _blogDb.Remove(BlogFromDb);
            _blogDb.SaveChanges();
            TempData["success"] = "Blog Deleted Successfully";

            return RedirectToAction("Details");

        }
    }
}
