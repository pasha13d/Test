using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<FileSettingModel> _fileSettings;
        public FileController(IOptions<FileSettingModel> fileSettings)
        {
            _fileSettings = fileSettings;
        }
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
            fileUpload.FileSize = _fileSettings.Value.FileSize;
            bool result = fileUpload.UploadUserFile(file);

            if (result)
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), _fileSettings.Value.Location, "UploadFile"));
                //Declare path to save uploaded file
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), _fileSettings.Value.Location, "UploadFile",
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
