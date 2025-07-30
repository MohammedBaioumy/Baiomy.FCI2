using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.BLL.Common.AttatchmentService
{
    public interface IAttatchmentService
    {      

        public  Task<string?> UploadFileAsync(IFormFile File,string FolderName);

        public  bool DeleteFile(string FilePath);
       // public IFormFile? GetFile(string? FilePath);
    }
}
