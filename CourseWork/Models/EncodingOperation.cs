using CourseWork.Controllers;
using CourseWork.Utils;
using System.IO;
using System.Web;
using Xceed.Words.NET;

namespace CourseWork.Models
{
    public class EncodingOperation
    {
        public const string FILES_DIRECTORY = "~/Files/";
        public const string RESULT_TEXT_FILE_NAME = "ResultText.txt";
        public const string DEFAULT_TEXT = "Си шарп - лучший язык программирования";

        private HttpContext _context = HttpContext.Current;
        private string _key;
        private HttpPostedFileBase _uploadedFile;
        private FormAction _buttonAction;
        private string _text;

        public EncodingLanguage Language { get; set; }
        public string Key
        {
            get
            {
                if (IsValidKey && _key != null)
                {
                    return _key;
                }
                else
                {
                    return Encoder.DEFAULT_KEY;
                }
            }
            set
            {
                try
                {
                    IsValidKey = true;
                    EncoderInstance = new Encoder(value, Language);
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
        public string Text
        {
            get 
            {
                if (_text == null || _text == "")
                {
                    return DEFAULT_TEXT;
                }
                else
                {
                    return _text;
                }
            } 
            set => _text = value; 
        }
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