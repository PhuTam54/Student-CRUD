using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Student_CRUD.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileUploadController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("FileUpload")]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var allowedExtenstions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                // Check if the file has a valid extensions
                var fileExtension = Path.GetExtension(formFile.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(fileExtension) || !allowedExtenstions.Contains(fileExtension))
                {
                    return BadRequest("Invalid file extension. Allowed extensions are: " + string.Join(", ", allowedExtenstions));
                };

                if (formFile.Length > 0)
                {
                    // Change the folder path
                    var uploadFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadFolderPath);

                    var fileName = Path.GetRandomFileName() + fileExtension;
                    var filePath = Path.Combine(uploadFolderPath, fileName);
                    filePaths.Add(filePath);

                    using ( var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }

            }

            return Ok(new { count = files.Count, size, filePaths });
        }
    }
}
