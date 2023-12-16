using FileOperations.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileOperations.Controllers
{
    [ApiController]
    public class FileManager : ControllerBase
    {
 
            private readonly IManageImage _iManageImage;
            public FileManager(IManageImage iManageImage)
            {
                _iManageImage = iManageImage;
            }

            [HttpPost]
            [Route("uploadfile")]
            public async Task<IActionResult> UploadFile(IFormFile _IFormFile)
            {
                var result = await _iManageImage.UploadFile(_IFormFile);
                return Ok(result);
            }

            [HttpGet]
            [Route("downloadfile")]
            public async Task<IActionResult> DownloadFile(string FileName)
            {
                var result = await _iManageImage.DownloadFile(FileName);
                return File(result.Item1, result.Item2, result.Item2);//byte, string string
            }
    }
}
