using Microsoft.AspNetCore.Hosting;

namespace backend.Services
{
    public interface IFileService
    {
        Task<string?> SaveImageAsync(IFormFile file);
        void DeleteImage(string path);
    }

    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string?> SaveImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            if (string.IsNullOrEmpty(file.FileName))
                return null;

            var root = _env.WebRootPath;
            if (string.IsNullOrEmpty(root))
                root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var uploadsFolder = Path.Combine(root, "Images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(extension))
                extension = ".png";

            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
            if (!allowed.Contains(extension.ToLowerInvariant()))
                return null;

            var fileName = Guid.NewGuid() + extension;
            var fullPath = Path.Combine(uploadsFolder, fileName);

            try
            {
                using var stream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(stream);
            }
            catch
            {
                return null;
            }

            return "/Images/" + fileName;
        }

        public void DeleteImage(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            var root = _env.WebRootPath;
            if (string.IsNullOrEmpty(root))
                root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var fullPath = Path.Combine(root, path.TrimStart('/'));
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}