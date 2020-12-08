using CourseWork.Controllers;
using CourseWork.Utils;
using System.IO;
using System.Web;
using Xceed.Words.NET;

namespace CourseWork.Models
{
    public class EncodingOperation
    {
        public static readonly string FILES_DIRECTORY = "~/Files/";
        public static readonly string RESULT_TEXT_FILE_NAME = "ResultText.txt";

        private HttpContext _context = HttpContext.Current;
        private string _key;
        private HttpPostedFileBase _uploadedFile;
        private FormAction _buttonAction;

        public EncodingLanguage Language { get; set; }
        public string Key
        {
            get => _key;
            set
            {
                try
                {
                    EncoderInstance = new Encoder(value, Language);
                    IsValidKey = true;
                    _key = value;
                }
                catch
                {
                    IsValidKey = false;
                }
            }
        }
        public Encoder EncoderInstance { get; private set; }
        public EncodingAction EncAction { get; set; }
        public string Text { get; set; }
        public string ResultText { get; set; }
        public bool UploadButtonPressed { get; set; }
        public bool IsValidKey { get; set; } = true;
        public bool FileIsRead { get; set; }
        public FormAction ButtonAction
        {
            get => _buttonAction;
            set
            {
                _buttonAction = value;
                if (_buttonAction == FormAction.Upload)
                {
                    UploadButtonPressed = true;
                }
                else
                {
                    UploadButtonPressed = false;

                    try
                    {
                        if (EncAction == EncodingAction.Encode)
                        {
                            ResultText = EncoderInstance.Encode(Text);
                        }
                        else
                        {
                            ResultText = EncoderInstance.Decode(Text);
                        }
                        var path = _context.Server.MapPath(FILES_DIRECTORY + RESULT_TEXT_FILE_NAME);
                        File.WriteAllText(path, ResultText);
                    }
                    catch
                    {
                        ResultText = "";
                    }
                }
            }
        }

        public HttpPostedFileBase UploadedFile
        {
            get => _uploadedFile;
            set
            {
                if (value != null && UploadButtonPressed)
                {
                    _uploadedFile = value;

                    string uploadedFileText = "";
                    
                    if (_uploadedFile.FileName.EndsWith(".txt"))
                    {
                        var path = _context.Server.MapPath(FILES_DIRECTORY + "Uploaded.txt");
                        _uploadedFile.SaveAs(path);
                        uploadedFileText = File.ReadAllText(path);
                        FileIsRead = true;
                    }
                    else if (_uploadedFile.FileName.EndsWith(".docx"))
                    {
                        var path = _context.Server.MapPath(FILES_DIRECTORY + "Uploaded.docx");
                        _uploadedFile.SaveAs(path);

                        using (var fs = new FileStream(path, FileMode.Open))
                        {
                            var doc = DocX.Load(fs);

                            foreach (var paragraph in doc.Paragraphs)
                            {
                                uploadedFileText += paragraph.Text;
                            }
                        }

                        FileIsRead = true;
                    }

                    if (FileIsRead)
                    {
                        Text = uploadedFileText;
                        ResultText = "";
                    }
                }
            }
        }
    }
}