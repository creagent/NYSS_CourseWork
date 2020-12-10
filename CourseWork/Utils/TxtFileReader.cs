using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CourseWork.Utils
{
    public class TxtFileReader : FileReader
    {
        public TxtFileReader(HttpPostedFileBase file) : base(file)
        {

        }

        public override string Read()
        {
            return File.ReadAllText(path);
        }
    }
}