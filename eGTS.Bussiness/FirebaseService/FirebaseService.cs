using eGTS_Backend.Data.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eGTS.Bussiness.FirebaseService
{
    public class FirebaseService : IFirebaseService
    {
        private readonly EGtsContext _context;

        public FirebaseService(EGtsContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleleAvatar(Guid id)
        {
            var credential = GoogleCredential.FromFile("egts-2023-firebase.json");
            var storageClient = StorageClient.Create(credential);
            string bucketName = "egts-2023.appspot.com";

            var account = await _context.Accounts.FindAsync(id);
            string imagePath = account.Image;
            try
            {
                await storageClient.DeleteObjectAsync(bucketName, imagePath);
                account.Image = null;
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleleQualification(Guid id)
        {
            var credential = GoogleCredential.FromFile("egts-2023-firebase.json");
            var storageClient = StorageClient.Create(credential);
            string bucketName = "egts-2023.appspot.com";

            var qualifi = await _context.Qualifications.FindAsync(id);
            string imagePath = qualifi.Certificate;
            try
            {
                await storageClient.DeleteObjectAsync(bucketName, imagePath);
                qualifi.Certificate = null;
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }
        }

        public async Task<string> UploadAvatarImage(IFormFile imageFile, Guid id)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                string result = "No image file provided";
                return result;
            }

            //Add img name
            // Get the original file name
            string originalFileName = imageFile.FileName;

            // Define the new file name
            string newFileName = originalFileName + "-" + id;
            var filePath = "Avatar/" + newFileName;

            using var imageStream = imageFile.OpenReadStream();
            string imageUrl = await UploadImageToFirebaseStorage(imageStream, filePath);

            //Store filePath to DB
            var account = await _context.Accounts.FindAsync(id);
            account.Image = filePath;
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Return the image URL in the API response
            return imageUrl;
        }
        public async Task<string> UploadCertificateImage(IFormFile imageFile, Guid id)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                string result = "No image file provided";
                return result;
            }

            //Add img name
            // Get the original file name
            string originalFileName = imageFile.FileName;

            // Define the new file name
            string newFileName = originalFileName + "-" + id;
            var filePath = "Certificate/" + newFileName;

            using var imageStream = imageFile.OpenReadStream();
            string imageUrl = await UploadImageToFirebaseStorage(imageStream, filePath);

            //Store filePath to DB
            var certificate = new Qualification()
            {
                ExpertId = id,
                Certificate = filePath,
                IsCetifide = false,
                IsDelete = false
            };
            _context.Qualifications.Add(certificate);
            _context.SaveChanges();

            // Return the image URL in the API response
            return imageUrl;
        }
        private async Task<string> UploadImageToFirebaseStorage(Stream imageStream, string filePath)
        {
            var credential = GoogleCredential.FromFile("egts-2023-firebase.json");
            var storage = await StorageClient.CreateAsync(credential);

            var bucketName = "egts-2023.appspot.com"; // Replace with your actual bucket name

            using (var stream = imageStream)
            {
                var imageObject = await storage.UploadObjectAsync(bucketName, filePath, null, stream);
                var url = $"https://storage.googleapis.com/{bucketName}/{filePath}";
                return url;
            }
        }
    }
}
