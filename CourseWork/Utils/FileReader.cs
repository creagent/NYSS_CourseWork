using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseWork.Utils
{
    public abstract class FileReader
    {
        internal string path;
        public const string FILE_NAME = "Upload";
        public abstract string Read();

        public FileReader(HttpPostedFileBase file)
        {
            var uploadFileName = file.FileName.Split('.');
            var extention = uploadFileName[uploadFileName.Length - 1];
            var fullName = FILE_NAME + "." + extention;

            path = EncodingOperation.Context.Server.MapPath(EncodingOperation.FILES_DIRECTORY + fullName);

            file.SaveAs(path);
        }

        public static FileReader GetFileReader(HttpPostedFileBase file)
        {
            var fullName = file.FileName.Split('.');
            var extention = fullName[fullName.Length - 1];

            if (extention == "txt")
            {
                return new TxtFileReader(file);
            }
            else if (extention == "docx")
            {
                return new DocxFileReader(file);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}