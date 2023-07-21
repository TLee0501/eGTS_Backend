using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.FirebaseService
{
    public interface IFirebaseService
    {
        Task<string> UploadAvatarImage(IFormFile imageFile, Guid id);
        Task<string> UploadCertificateImage(IFormFile imageFile, Guid id);
        Task<bool> DeleleAvatar(Guid id);
        Task<bool> DeleleQualification(Guid id);
    }
}
