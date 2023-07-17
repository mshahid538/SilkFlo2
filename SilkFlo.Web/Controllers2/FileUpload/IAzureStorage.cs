using Microsoft.AspNetCore.Http;
using SilkFlo.Web.Models.Business;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SilkFlo.Web.Controllers2.FileUpload
{
    public interface IAzureStorage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        Task<List<BlobResponseDto>> UploadAsyncTest(ICollection<IFormFile> files, string id, string ideaId, SilkFlo.Data.Core.Domain.User user, string template);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="files"></param>
		/// <returns></returns>
		Task<List<BlobResponseDto>> UploadTemlateDocumentAsync(ICollection<IFormFile> files, string folderName);
		/// <summary>
		/// This method downloads a file with the specified filename
		/// </summary>
		/// <param name="blobFilename">Filename</param>
		/// <returns>Blob</returns>
		Task<string> DownloadBlobsAsync(string files, string containerName);
        /// <summary>
        /// This method deleted a file with the specified filename
        /// </summary>
        /// <param name="blobFilename">Filename</param>
        /// <returns>Blob with status</returns>
        Task<BlobResponseDto> DeleteAsync(string documentId, string ideaId, bool isTemplate = false);

        Task<List<BlobResponseDto>> DeleteAsync(List<string> templateUris);
		/// <summary>
		/// This method returns a list of all files located in the container
		/// </summary>
		/// <returns>Blobs in a list</returns>
		/// 
		Task<ICollection<DocumentDto>> GetDocuments(string containerName, string tenantId, string id, string userId, string template);
        /// <summary>
        /// Check for valid Upload
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        bool IsValidFileExtension(Stream stream);
        /// <summary>
        /// Get all templates by tenant id
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<ICollection<DocumentDTOMain>> GetTemplateDocuments(string tenantId);

        Task<(byte[], string, string)> DownloadBlobAsync(string documentId, string ideaId, bool isTemplate = false);

	}
}
