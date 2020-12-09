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
        private HttpPostedFileBase _file;
        public TxtFileReader(HttpPostedFileBase file)
        {
            _file = file;
        }

        public override string Read()
        {
            var path = EncodingOperation.Context.Server.MapPath(EncodingOperation.FILES_DIRECTORY + "Uploaded.txt");
            _file.SaveAs(path);

            return File.ReadAllText(path);
        }
    }
}