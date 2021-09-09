using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Controllers
{
    public class FileController : Controller
    {
        //private IHostingEnvironment environment;
        //public FileController(IHostingEnvironment _environment)
        //{
        //    environment = _environment;
        //}
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Fileupload()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Fileupload(IFormFile file)
        {
            FileUploadViewModel fileUpload = new FileUploadViewModel();
            //Filesize
            fileUpload.FileSize = 550;
            bool result = fileUpload.UploadUserFile(file);

            if (result)
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadFile"));
                //Declare path to save uploaded file
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "UploadFile",
                            file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            ViewBag.ResultErrorMessage = fileUpload.Message;

            return View();
        }

        #region Private Methods

        #endregion
    }
}
