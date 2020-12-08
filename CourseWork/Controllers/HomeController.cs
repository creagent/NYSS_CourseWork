using CourseWork.Models;
using CourseWork.Utils;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Xceed.Words.NET;

namespace CourseWork.Controllers
{
    public class HomeController : Controller
    {
        private string _resultText;
        private const string _TITLE = "Шифрование Виженера";

        [HttpGet]
        public ViewResult Main()
        {
            ViewBag.Title = _TITLE;
            var operation = new EncodingOperation();
            ViewBag.EncodingOperation = operation;
            return View("Main");
        }

        [HttpPost]
        public ViewResult Main(EncodingOperation unit)
        {
            ViewBag.Title = _TITLE;
            ViewBag.EncodingOperation = unit;

            return View("Main");
        }

        public FileResult DownloadFile()
        {
            string docxPath = Server.MapPath(EncodingOperation.FILES_DIRECTORY + "Template.docx");
            string fileType = "application/docx";

            var resultTextPath = Server.MapPath(EncodingOperation.FILES_DIRECTORY + EncodingOperation.RESULT_TEXT_FILE_NAME);
            _resultText = System.IO.File.ReadAllText(resultTextPath);

            if (!string.IsNullOrEmpty(_resultText))
            {
                var doc = DocX.Create(docxPath);
                doc.InsertParagraph(_resultText);
                doc.Save();

                Redirect("/./Main/");
                return File(docxPath, fileType, "result.docx");
            }
            else
            {
                // Бросать 404 или что-то такое
                throw new Exception();
            }
        }
    }
}