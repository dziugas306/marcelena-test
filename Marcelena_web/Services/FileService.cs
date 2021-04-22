using Marcelena_web.Domain.Entitites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace Marcelena_web.Services
{
    public static class FileService
    {

        public static void DeletePhotoPaths(List<Photo> photoPaths, string path)
        {
            foreach (var item in photoPaths)
            {
                string filePath = Path.Combine(path, "images", item.PhotoPath);
                File.Delete(filePath);
            }
        }       
        public static void DeletePhotoFile(Photo photoPath, string path)
        {
            string filePath = Path.Combine(path, "images", photoPath.PhotoPath);
            File.Delete(filePath);
        }
        public static List<Photo> GetPhotoPaths(List<IFormFile> Photo, string path)
        {
            var photoPaths = new List<Photo>();
            var count = 0;
            foreach (var item in Photo)
            {
                photoPaths.Add(new Photo(ProcessUploadedFile(item, path)));
                count++;
                if (count == 4) break;
            }
            return photoPaths;
        }
        private static string ProcessUploadedFile(IFormFile file, string webRootPath)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(webRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
