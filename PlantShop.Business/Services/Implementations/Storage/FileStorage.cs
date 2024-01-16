using PlantShop.Business.Services.Abstractions.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace PlantShop.Business.Services.Implementations.Storage
{
    public class FileStorage : IStorage
    {
        private readonly IHostingEnvironment _webHostEnvironment;

        public FileStorage(IHostingEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            throw new NotImplementedException();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            List<(string fileName, string pathOrContainerName)> datas = new();

            string upload = Path.Combine($"wwwroot/resurce/{pathOrContainerName}");
            if (!Directory.Exists(upload))
            {
                Directory.CreateDirectory(upload);
            }
            Random r = new Random();

            foreach (IFormFile file in files)
            {
                string fullPath = Path.Combine(upload, $"{file.FileName}");
                using FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                datas.Add((file.FileName, $@"{upload.Remove(0, 8)}/{file.FileName}"));
            }
            return datas;
        }
    }
}
