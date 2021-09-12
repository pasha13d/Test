using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;
using Test.Services;

namespace Test.Controllers
{
    public class FileController : Controller
    {
        private readonly IOptions<FileSettingModel> _fileSettings;
        private readonly FileUploadService _fileUploadService;
        public FileController(IOptions<FileSettingModel> fileSettings, FileUploadService fileUploadService)
        {
            _fileSettings = fileSettings;
            _fileUploadService = fileUploadService;
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
            fileUpload.FileType = _fileSettings.Value.FileType;
            fileUpload.File = file;
            var k = file.ContentType;
            var result = _fileUploadService.UploadUserFile(fileUpload);

            if (result.IsSuccess)
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
