using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Xceed.Words.NET;

namespace CourseWork.Utils
{
    public class DocxFileReader : FileReader
    {
        private HttpPostedFileBase _file;
        public DocxFileReader(HttpPostedFileBase file)
        {
            _file = file;
        }
        public override string Read()
        {
            var path = EncodingOperation.Context.Server.MapPath(EncodingOperation.FILES_DIRECTORY + "Uploaded.docx");
            _file.SaveAs(path);


            string uploadedFileText = "";
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var doc = DocX.Load(fs);

                foreach (var paragraph in doc.Paragraphs)
                {
                    uploadedFileText += paragraph.Text;
                }
            }

            return uploadedFileText;
        }
    }
}