using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduates.UI.Web.Helpers
{
    public class FileReaderHelper
    {
        public static string GetDocumentFromFile(string path, string fileType)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string file = Convert.ToBase64String(fileBytes);
            return UploadHelper.AddBase64StringHeader(file, fileType);
        }
    }
}
