using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class FileUploadViewModel
    {
        public string Message { get; set; }
        public decimal FileSize { get; set; }
        public IFormFile File { get; set; }
        public FileTypeMap[] FileType { get; set; }
        public bool IsSuccess { get; set; }
    }
}
