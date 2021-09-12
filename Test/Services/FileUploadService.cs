using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Services
{
    public class FileUploadService
    {

        private byte[] fileByte;
        //public List<KeyValuePair<string, string>> MyProperty { get; set; }
        //private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };
        //private static readonly byte[] DOC = { 208, 207, 17, 224, 161, 177, 26, 225 };
        //private static readonly byte[] TEXT = { 71, 70, 71, 10 };
        public FileUploadViewModel UploadUserFile(FileUploadViewModel fileUpload)
        {
            //byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            //FileUploadViewModel fileUpload = new FileUploadViewModel();
            if (fileUpload.File.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileUpload.File.CopyTo(ms);
                    fileByte = ms.ToArray();
                }
            }
            //bool result = false;
            fileUpload.IsSuccess = false;
            try
            {
                //var supportedTypes = new[] { "txt", "doc", "docx", "pdf", "xls", "xlsx" };
                //var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                //if (!supportedTypes.Contains(fileExt))
                //{
                //    Message = "Invalid file extension - uploads word/pdf/excel/txt file only";  
                //    return Message;
                //}
                //Mime content type for supported files -- pdf, word, excel, text
                //bool isValidFileType = (string.Equals(fileUpload.File.ContentType, "application/pdf", StringComparison.OrdinalIgnoreCase) && fileByte.Take(7).SequenceEqual(PDF)) ||
                //                    string.Equals(fileUpload.File.ContentType, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", StringComparison.OrdinalIgnoreCase) ||
                //                    string.Equals(fileUpload.File.ContentType, "application/vnd.ms-excel", StringComparison.OrdinalIgnoreCase) ||
                //                    (string.Equals(fileUpload.File.ContentType, "text/plain", StringComparison.OrdinalIgnoreCase) && fileByte.Take(4).SequenceEqual(TEXT)) ||
                //                    (string.Equals(fileUpload.File.ContentType, "application/msword", StringComparison.OrdinalIgnoreCase) && fileByte.Take(8).SequenceEqual(DOC)) ||
                //                    string.Equals(fileUpload.File.ContentType, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", StringComparison.OrdinalIgnoreCase);

                var contentType = fileUpload.FileType
                    .Where(m => m.Key.Equals(fileUpload.File.ContentType, StringComparison.OrdinalIgnoreCase) 
                    && fileByte.Take(7).ToArray().SequenceEqual(m.Value)
                    )
                    .FirstOrDefault();

                if (contentType == null)
                {
                    fileUpload.Message = "Invalid file extension - uploads word/pdf/excel/txt/jpg/png file only";
                }
                else
                {

                    if (fileUpload.File.Length > (fileUpload.FileSize * 1024))
                    {
                        fileUpload.Message = "File size should be upto " + fileUpload.FileSize + "KB";
                    }
                    else
                    {
                        fileUpload.Message = "Successfully Uploaded";
                        fileUpload.IsSuccess = true;
                    }
                }                
            }
            catch (Exception ex)
            {
                fileUpload.Message = "Upload Container Should Not Be Empty";
            }
            return fileUpload;
        }
    }
}
