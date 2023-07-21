﻿using System.IO;

namespace SilkFlo.Web.Controllers2.FileUpload
{
    public class FileDto
    {
        public string Uri { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public Stream Content { get; set; }
        public string GetPathWithFileName(string folderName)
        {
            // string uniqueAutoGeneratedFileName = Path.GetRandomFileName();
            string shortClientSideFileNameWithoutExt = Path.GetFileNameWithoutExtension(Name).TruncateLongString(20);  //Trimming to max 10 as client side file name can be too long
            string ext = Path.GetExtension(Name);
            // string basePath = $"{containerName}/{folderName}/";
            string basePath = $"{folderName}/";

            //return basePath + uniqueAutoGeneratedFileName + "_" + shortClientSideFileNameWithoutExt + ext;
            return basePath + shortClientSideFileNameWithoutExt + ext;
        }
    }

}