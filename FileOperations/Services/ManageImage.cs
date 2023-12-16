﻿using FileOperations;
using FileOperations.Services;
using Microsoft.AspNetCore.StaticFiles;

namespace API.FileProcessing.Service
{
    public class ManageImage : IManageImage
    {
        public async Task<string> UploadFile(IFormFile _IFormFile)
        {
            string FileName = "";
            try
            {
                FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
                FileName = _IFormFile.FileName + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Extension;
                var _GetFilePath = Common.GetFilePath(FileName);
                using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
                {
                    await _IFormFile.CopyToAsync(_FileStream);
                }
                return FileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(byte[], string, string)> DownloadFile(string FileName)
        {
            try
            {
                var _GetFilePath = Common.GetFilePath(FileName);
                var provider = new FileExtensionContentTypeProvider();//file in extencion na gore ne filei oldugunu bilir
                if (!provider.TryGetContentType(_GetFilePath, out var _ContentType))//path veririk, filein path ni geri verir, true or false
                {
                    _ContentType = "application/octet-stream";//false olsa, onda sonu olmayan bilinmeyen qarisiq bir file qaytarir
                }
                var _ReadAllBytesAsync = await File.ReadAllBytesAsync(_GetFilePath);
                return (_ReadAllBytesAsync, _ContentType, Path.GetFileName(_GetFilePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}