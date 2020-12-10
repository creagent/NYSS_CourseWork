using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            string result = File.ReadAllText(path);
            
            // Извините за настолько наглый костыль)))))
            if (result.Contains('�'))
            {
                result = File.ReadAllText(path, Encoding.Default);
            }

            return result;
        }
    }
}