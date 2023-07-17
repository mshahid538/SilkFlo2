using System.Collections.Generic;

namespace SilkFlo.Web.Controllers2.FileUpload
{
    public class BlobResponseDto
    {
        public string Status { get; set; }
        public bool Error { get; set; }
        public FileDto Blob { get; set; }
        public ICollection<string> Urls { get; }
        public BlobResponseDto()
        {
            Blob = new FileDto();
        }
        public BlobResponseDto(ICollection<string> urls) : this()
        {
            Urls = urls;
        }
    }
}