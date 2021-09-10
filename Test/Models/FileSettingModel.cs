using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class FileSettingModel
    {
        public int FileSize { get; set; }
        public string Location { get; set; }
        public string[] FileType { get; set; }
    }
}
