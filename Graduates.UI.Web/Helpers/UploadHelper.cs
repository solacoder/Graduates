using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduates.UI.Web.Helpers
{
    public class UploadHelper
    {
        public static string CreateFilePath(string rootPath, string fileType, string name)
        {
            var filePath = string.Empty;

            if (fileType.Equals("image/png"))
            {
                filePath = $"{rootPath}{name}.png";
            }

            else if (fileType.Equals("image/jpeg"))
            {
                filePath = $"{rootPath}{name}.jpeg";
            }

            else if (fileType.Equals("image/gif"))
            {
                filePath = $"{rootPath}{name}.gif";
            }

            else if (fileType.Equals("applicaton/pdf"))
            {
                filePath = $"{rootPath}{name}.pdf";
            }
            return filePath;
        }

        public static string CreateFilePath(string filePath, string fileType)
        {
            var newPath = string.Empty;

            if (fileType.Equals("image/png"))
            {
                newPath = $"{filePath}.png";
            }

            else if (fileType.Equals("image/jpeg"))
            {
                newPath = $"{filePath}.jpeg";
            }

            else if (fileType.Equals("image/gif"))
            {
                newPath = $"{filePath}.gif";
            }

            else if (fileType.Equals("applicaton/pdf"))
            {
                newPath = $"{filePath}.pdf";
            }
            return newPath;
        }

        public static string RemoveBase64StringHeader(string originalValue)
        {
            var result = originalValue.Replace("data:image/gif;base64,", string.Empty)
                        .Replace("data:image/gif;base64,", string.Empty)
                        .Replace("data:image/png;base64,", string.Empty)
                        .Replace("data:image/jpeg;base64,", string.Empty)
                        .Replace("data:application/pdf;base64,", string.Empty);
            return result;
        }

        public static string AddBase64StringHeader(string fileBase64Value, string fileType)
        {
            var file = string.Empty;

            if (fileType.Equals("image/png"))
            {
                file += "data:image/png;base64," + fileBase64Value;
            }

            else if (fileType.Equals("image/jpeg"))
            {
                file += "data:image/jpeg;base64," + fileBase64Value;
            }

            else if (fileType.Equals("image/gif"))
            {
                file += "data:image/gif;base64," + fileBase64Value;
            }

            else if (fileType.Equals("applicaton/pdf"))
            {
                file += "data:application/pdf;base64," + fileBase64Value;
            }
            return file;
        }
    }
}
