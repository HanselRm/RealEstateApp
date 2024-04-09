

using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace RealStateAppProg3.Core.Application.Helpers
{
    public class UploadFiles
    {
        public static string UploadFile(IFormFile file, string Chapter, string id)
        {
           
            string basePath = $"/Img/{Chapter}/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string filename = Path.Combine(path, fileName);

            using (var stream = new FileStream(filename, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return $"{basePath}/{fileName}";
        }
    }
}
