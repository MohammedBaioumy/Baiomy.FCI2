using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;


namespace Baiomy.FCI2.BLL.Common.AttatchmentService
{
    public class AttatchmentService : IAttatchmentService
    {
        private  readonly List<string> AllowedExtention =new List<string>() { ".jpg",".npj", ".jpeg" }; 
        public const int MaxSize = 4_194_304;

        public  async Task<string?> UploadFileAsync(IFormFile file,string FolderName)
        {
         
            var extention= Path.GetExtension(file.FileName.ToLower());

            if (!AllowedExtention.Contains(extention))
                return null;

            if (file.Length > MaxSize)
                return null;


            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\AttatchmentFiles", FolderName);

            if(!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            var FileName = $"{Guid.NewGuid()}{extention}";

            var FilePath = Path.Combine(FolderPath, FileName);

            using var FileStream= new FileStream(FilePath, FileMode.Create);
            await file.CopyToAsync(FileStream);

            return FileName;
        }
        public  bool DeleteFile(string FilePath)
        {
             
            if(FilePath is not null && File.Exists(FilePath))
            {
                File.Delete(FilePath);
                return true;
            }
            return false;
        }
        //public IFormFile? GetFile(string? FilePath)
        //{
        //    if (String.IsNullOrEmpty(FilePath))
        //        return null;
        //    using var x = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
        //    var formFile = new FormFile(x, 0, x.Length, "path", Path.GetFileName(FilePath));
        //    return formFile;
        //}

    }
}
