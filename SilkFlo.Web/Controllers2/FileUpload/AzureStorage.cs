using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Web.Models.Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using static System.Reflection.Metadata.BlobBuilder;

namespace SilkFlo.Web.Controllers2.FileUpload
{
    public class AzureStorage : IAzureStorage
    {
        private readonly ILogger<AzureStorage> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BlobServiceClient _blobServiceClient;
        public AzureStorage(BlobServiceClient blobServiceClient, IConfiguration configuration, ILogger<AzureStorage> logger, Data.Core.IUnitOfWork unitOfWork)
        {
            _blobServiceClient = blobServiceClient;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> DownloadBlobsAsync(string files, string containerName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobPages = blobContainerClient.GetBlobsAsync().AsPages();
            var semaphore = new SemaphoreSlim(4);
            var tasks = new List<Task>();
            byte[] fileData = null;
            string name = null;
            await foreach (var blobPage in blobPages)
            {
                foreach (var blob in blobPage.Values)
                {
                    await semaphore.WaitAsync();
                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            var blobClient = blobContainerClient.GetBlobClient(blob.Name);
                            // fileData = File.Create(blob.Name);
                            //await blobClient.DownloadToAsync(fileData);
                            name = blob.Name;
                            if (name.Equals(files))
                            {
                                //fileData = await blobClient.OpenReadAsync();
                                //fileData =  await DownloadToStream(blobClient, @"C:\Users\alapher.woriayibapri\Desktop\APP");

                                using var file = File.Create(blob.Name);
                                var sample = await blobClient.DownloadToAsync(file);
                                fileData = sample.Content.ToArray();
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    }));
                }
                await Task.WhenAll(tasks);
                tasks.Clear();
            }
            return ""; // await Task.FromResult((fileData, name));
        }
        
        public async Task<(byte[], string, string)> DownloadBlobAsync(string documentId, string ideaId, bool isTemplate = false)
        {
            try
            {
                byte[] fileBytes = null;
                string contentType = "";
                string fileName = "";

                if (!isTemplate)
                {
                    var idea = await _unitOfWork.BusinessIdeas.GetAsync(ideaId);
                    await _unitOfWork.BusinessDocuments.GetForIdeaAsync(idea);

                    if (idea.Documents != null && idea.Documents.Count > 0)
                    {
                        var document = idea.Documents.FirstOrDefault(x => x.Id == documentId);
                        var blobContainerClient = _blobServiceClient.GetBlobContainerClient("idea-container");

						Uri uri = new Uri(document.FilenameBackend);
						string result = uri.AbsolutePath.TrimStart('/');
						string[] substrings = result.Split(new string[] { "idea-container/" }, StringSplitOptions.None);
                        string path = HttpUtility.UrlDecode(substrings[1]);

						var blobClient2 = blobContainerClient.GetBlobClient(path);
                        if (await blobClient2.ExistsAsync())
                        {
                            var response2 = await blobClient2.DownloadContentAsync();
                            fileBytes = response2.Value.Content.ToMemory().ToArray();
                            contentType = response2.Value.Details.ContentType;
                        }

                        return (fileBytes, contentType, document.Filename);
                    }
                }
                else
                {
					var blobContainerClient = _blobServiceClient.GetBlobContainerClient("idea-container");
					var blobPages = blobContainerClient.GetBlobsAsync(prefix: documentId).AsPages();

					await foreach (var blobPage in blobPages)
					{
						foreach (var blob in blobPage.Values)
						{
							var blobClient = blobContainerClient.GetBlobClient(blob.Name);
							var response = await blobClient.DownloadContentAsync();
							fileBytes = response.Value.Content.ToMemory().ToArray();
							contentType = response.Value.Details.ContentType;
						}
					}

					return (fileBytes, contentType, System.IO.Path.GetFileName(documentId));
				}
                return (null, null, null);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task DownloadToStream(BlobClient blobClient, string localFilePath)
        {
            FileStream fileStream = File.OpenWrite(localFilePath);
            await blobClient.DownloadToAsync(fileStream);
            fileStream.Close();
        }
        public async Task<ICollection<DocumentDto>> GetDocuments(string containerName, string tenantId, string id, string userId, string template)
        {
            var documents = new List<DocumentDto>();

			var idea = await _unitOfWork.BusinessIdeas.GetAsync(id);
			await _unitOfWork.BusinessDocuments.GetForIdeaAsync(idea);

			if (idea.Documents != null && idea.Documents.Count > 0)
            {
				var blobContainerClient = _blobServiceClient.GetBlobContainerClient("idea-container");
				DocumentDto documentDto;

                foreach(var doc in idea.Documents)
                {
                    documentDto = new DocumentDto();
                    documentDto.FileNameWithExtetnsion = doc.FilenameBackend;
					documentDto.FileNameWithoutExtetnsion = doc.Filename;
                    documentDto.FileNameWithoutExtetnsion = Path.GetFileNameWithoutExtension(doc.FilenameBackend);
                    documentDto.Id = doc.Id;
                    documentDto.IdeaId = idea.Id;

					Uri uri = new Uri(doc.FilenameBackend);
					string result = uri.AbsolutePath.TrimStart('/');
					string[] substrings = result.Split(new string[] { "idea-container/" }, StringSplitOptions.None);
					string path = HttpUtility.UrlDecode(substrings[1]);

					var blobClient2 = blobContainerClient.GetBlobClient(path);
					if (await blobClient2.ExistsAsync())
					{
						long sizeInBytes = blobClient2.GetProperties().Value.ContentLength;

						if (sizeInBytes >= 1024 * 1024)
						{
							// Convert to MB
							double sizeInMB = (double)sizeInBytes / (1024 * 1024);
							documentDto.Size = $"{sizeInMB:0.##} MB";
						}
						else
						{
							// Convert to KB
							double sizeInKB = (double)sizeInBytes / 1024;
							documentDto.Size = $"{sizeInKB:0.##} KB";
						}
					}

					documents.Add(documentDto);
				};

                return documents;
			}

            return documents;
        }

        public async Task<ICollection<DocumentDTOMain>> GetTemplateDocuments(string tenantId)
        {
            List<DocumentDTOMain> documentDTOMains = new List<DocumentDTOMain>();
            List<KeyValuePair<string, string>> templatesConstants = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("DetailedWorkInstructions", "Work Instructions"),
                new KeyValuePair<string, string>("StandardOperatingProcedure", "Standard Operating Procedure"),
                new KeyValuePair<string, string>("ProcessmapsORflowcharts", "Process maps / flowcharts"),
                new KeyValuePair<string, string>("InputFiles(examples)", "Input Files (examples)"),
                new KeyValuePair<string, string>("OutputFiles(examples)", "Output Files (examples)"),
                new KeyValuePair<string, string>("Recordings", "Recordings"),
                new KeyValuePair<string, string>("Misc", "Misc"),
                new KeyValuePair<string, string>("SolutionDesignDocument", "Solution Design Document"),
                new KeyValuePair<string, string>("ProcessDefinitionDocument", "Process Definition Document"),
            };
            
            List<DocumentDto> documents = null;
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient("idea-container");

            foreach (var template in templatesConstants)
            {
                var blobPages = blobContainerClient.GetBlobsAsync(prefix: $"templates/{tenantId}/{template.Key}/").AsPages(); // (prefix: id + '/').AsPages();
                await foreach (var blobPage in blobPages)
                {
                    documents = new List<DocumentDto>();
                    foreach (var blob in blobPage.Values)
                    {
						var blobClient = blobContainerClient.GetBlobClient(blob.Name);
						var doc = new DocumentDto
                        {
                            FileNameWithExtetnsion = blob.Name,
                            FileNameWithoutExtetnsion = Path.GetFileNameWithoutExtension(blob.Name),
                            ThumbNail = GetThumbNailPath(Path.GetExtension(blob.Name)),
                            Text = blobClient.Uri.ToString(),
                            
                        };

						long sizeInBytes = blob.Properties?.ContentLength ?? 0;
						string sizeString;

						if (sizeInBytes >= 1024 * 1024)
						{
							// Convert to MB
							double sizeInMB = (double)sizeInBytes / (1024 * 1024);
							doc.Size = $"{sizeInMB:0.##} MB";
						}
						else
						{
							// Convert to KB
							double sizeInKB = (double)sizeInBytes / 1024;
							doc.Size = $"{sizeInKB:0.##} KB";
						}

						documents.Add(doc);
                    }
                    documentDTOMains.Add(new DocumentDTOMain()
                    {
                        DocumentTemplateName = template.Value,
                        DocumentDtos = documents
                    });
                }
            }
            return documentDTOMains;
        }

        public async Task<List<BlobResponseDto>> UploadAsyncTest(ICollection<IFormFile> files, string folderName, string ideaId, SilkFlo.Data.Core.Domain.User user, string template)
        {
			List<SilkFlo.Data.Core.Domain.Business.Document> documents = null;

			//  Create new upload response object that we can return to the requesting method
			var urls = new Collection<string>();
            List<BlobResponseDto> blobResponseDtos = null;

            try
            {
                if (files == null || files.Count == 0)
                {
                    return blobResponseDtos;
                }

				var idea = await _unitOfWork.BusinessIdeas.GetAsync(ideaId);
				documents = new List<Data.Core.Domain.Business.Document>();

				blobResponseDtos = new List<BlobResponseDto>();
                var containerClient = _blobServiceClient.GetBlobContainerClient("idea-container");

                foreach (var file in files)
                {
                    try
                    {
                        var blobClient = containerClient.GetBlobClient($"userDocuments/{folderName}/{idea.Id}/{user.Id}/{template}/" + file.FileName);
                        await blobClient.UploadAsync(file.OpenReadStream(), new BlobHttpHeaders { ContentType = file.ContentType });
                        
                        //urls.Add(blobClient.Uri.ToString());
                        blobResponseDtos.Add(new BlobResponseDto(new List<string>() { blobClient.Uri.ToString() })
                        {
                            Status = "Uploaded",
                            Error = false
                        });

						//To be Add doc into idea
						documents.Add(new Data.Core.Domain.Business.Document()
						{
							Client = idea.Client,
							CanDelete = true,
							ClientId = idea.ClientId,
							ClientString = idea.ClientString,
							CreatedBy = user,
							CreatedById = user.Id,
							CreatedDate = DateTime.Now,
							Filename = file.FileName,
							FilenameBackend = blobClient.Uri.ToString(),
							Idea = idea,
                            Text = file.FileName,
						});
					}
                    catch (RequestFailedException ex)
                    {
                        blobResponseDtos.Add(new BlobResponseDto()
                        {
                            Status = "Failed",
                            Error = true,
                            Blob = new FileDto()
                            {
                                Name = file.FileName
                            }
                        });
                    }
                }

				if (documents != null && documents.Count > 0)
				{
					foreach (var doc in documents)
						await _unitOfWork.BusinessDocuments.AddAsync(doc);

					await _unitOfWork.CompleteAsync();
				}
			}
            catch (Exception ex)
            {

            }

            return blobResponseDtos;
        }

		public async Task<List<BlobResponseDto>> UploadTemlateDocumentAsync(ICollection<IFormFile> files, string folderName)
		{
			List<SilkFlo.Data.Core.Domain.Business.Document> documents = null;

			//  Create new upload response object that we can return to the requesting method
			var urls = new Collection<string>();
			List<BlobResponseDto> blobResponseDtos = null;

			try
			{
				if (files == null || files.Count == 0)
				{
					return blobResponseDtos;
				}

				blobResponseDtos = new List<BlobResponseDto>();
				var containerClient = _blobServiceClient.GetBlobContainerClient("idea-container");

				foreach (var file in files)
				{
					try
					{
						var blobClient = containerClient.GetBlobClient($"{folderName}/" + file.FileName);
						await blobClient.UploadAsync(file.OpenReadStream(), new BlobHttpHeaders { ContentType = file.ContentType });

						//urls.Add(blobClient.Uri.ToString());
						blobResponseDtos.Add(new BlobResponseDto(new List<string>() { blobClient.Uri.ToString() })
						{
							Status = "Uploaded",
							Error = false
						});
					}
					catch (RequestFailedException ex)
					{
						blobResponseDtos.Add(new BlobResponseDto()
						{
							Status = "Failed",
							Error = true,
							Blob = new FileDto()
							{
								Name = file.FileName
							}
						});
					}
				}
			}
			catch (Exception ex)
			{

			}

			return blobResponseDtos;
		}


		public async Task<List<BlobResponseDto>> DeleteAsync(List<string> templateUris)
        {
            List<BlobResponseDto> blobResponseDtos = new List<BlobResponseDto>();

            if (templateUris != null && templateUris.Count > 0)
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient("idea-container");
                foreach (var uri in templateUris)
                {
    				var blobResponseDto = new BlobResponseDto();
                    var blobClient = blobContainerClient.GetBlobClient(uri);
                    if (await blobClient.ExistsAsync())
                    {
                        var response = await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                        blobResponseDto.Status = "Document Deleted";
                        blobResponseDto.Error = !response.Value;
                    }

                    blobResponseDtos.Add(blobResponseDto);
                }
            }

            return blobResponseDtos;
		}

        public async Task<BlobResponseDto> DeleteAsync(string documentId, string ideaId, bool isTemplate = false)
        {
			BlobResponseDto blobResponseDto = null;

            if (isTemplate)
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient("idea-container");
                var blobClient = blobContainerClient.GetBlobClient(documentId);
                if (await blobClient.ExistsAsync())
                {
					blobResponseDto = new BlobResponseDto();
					var response = await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                    blobResponseDto.Status = "Document Deleted";
                    blobResponseDto.Error = !response.Value;
                }

                return blobResponseDto;
            }


			var idea = await _unitOfWork.BusinessIdeas.GetAsync(ideaId);
			await _unitOfWork.BusinessDocuments.GetForIdeaAsync(idea);

            if(idea.Documents != null && idea.Documents.Count > 0)
            {
                blobResponseDto = new BlobResponseDto();
				var document = idea.Documents.FirstOrDefault(x => x.Id == documentId);
				var blobContainerClient = _blobServiceClient.GetBlobContainerClient("idea-container");

				Uri uri = new Uri(document.FilenameBackend);
				string result = uri.AbsolutePath.TrimStart('/');
				string[] substrings = result.Split(new string[] { "idea-container/" }, StringSplitOptions.None);
				string path = HttpUtility.UrlDecode(substrings[1]);

				var blobClient = blobContainerClient.GetBlobClient(path);
				if (await blobClient.ExistsAsync())
				{
					var response = await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                    blobResponseDto.Status = "Document Deleted";
                    blobResponseDto.Error = !response.Value;
				}

                await _unitOfWork.BusinessDocuments.RemoveAsync(document);
                await _unitOfWork.CompleteAsync();
			}

            return blobResponseDto;
        }

        private string GetThumbNailPath(string extension)
        {
            string result = string.Empty;
            switch (extension.ToLower())
            {
                case ".jpg":
                case ".png":
                case ".jpeg":
                    result = "image.png"; break;
                case ".xls":
                case ".xlsx":
                    result = "excel.png"; break;
                case ".doc":
                case ".docx":
                    result = "word.png"; break;
                case ".ppt":
                case ".pptx":
                    result = "powerpoint.png"; break;
                case ".pdf": result = "pdf.png"; break;
                case ".zip":
                    result = "zip.jpg"; break;
                case ".txt":
                case ".jfif":
                    result = "text-file-plain.png"; break;

                default: result = "text-file-plain.png"; break;
            }
            string path = $"Icons/fileuploadicons/{result}";

            return path;
        }

        public bool IsValidFileExtension(Stream stream)
        {
            try
            {
                var flag = false;
                var fileData = GetBytes(stream);

                foreach (var b in _fileSignatures.Values.SelectMany(x => x))
                {
                    if (fileData == null || fileData.Length < b.Length)
                    {
                        continue;
                    }

                    var curFileSig = new byte[b.Length];
                    Array.Copy(fileData, curFileSig, b.Length);
                    if (curFileSig.SequenceEqual(b))
                    {
                        flag = true;
                        break;
                    }
                }

                return flag;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        private static byte[] GetBytes(Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.ReadAsync(bytes, 0, bytes.Length);
            stream.Dispose();
            return bytes;
        }
        private static readonly Dictionary<string, List<byte[]>> _fileSignatures = new()
        {
            { ".DOC", new List<byte[]>
                {
                    new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }
                }
            },
            { ".DOCX", new List<byte[]>
                {
                    new byte[] { 0x50, 0x4B, 0x03, 0x04 }
                }
            },
            { ".PDF", new List<byte[]>
                {
                    new byte[] { 0x25, 0x50, 0x44, 0x46 }
                }
            },
            {
                ".XLS",new List<byte[]>
                                {
                                    new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                                    new byte[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 },
                                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF }
                                }
            },
            { ".XLSX", new List<byte[]>
                {
                    new byte[] { 0x50, 0x4B, 0x03, 0x04 }
                }
            },
            { ".gif", new List<byte[]>
                {
                    new byte[] { 0x47, 0x49, 0x46, 0x38 }
                }
            },
            { ".png", new List<byte[]>
                {
                    new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
                }
            },
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                }
            },
            { ".jpeg2000", new List<byte[]>
                {
                    new byte[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A }
                }
            },

            { ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                }
            },
            {
                ".zip", new List<byte[]> //also docx, xlsx, pptx, ...
                {
                    new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                    new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x45 },
                    new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 },
                    new byte[] { 0x50, 0x4B, 0x05, 0x06 },
                    new byte[] { 0x50, 0x4B, 0x07, 0x08 },
                    new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 },
                }
            },

            {
                ".pdf", new List<byte[]>
                {
                    new byte[] { 0x25, 0x50, 0x44, 0x46 }
                }
            },
            { ".z", new List<byte[]>
                {
                    new byte[] { 0x1F, 0x9D },
                    new byte[] { 0x1F, 0xA0 }
                }
            },
            { ".tar", new List<byte[]>
                {
                    new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72, 0x00, 0x30 , 0x30 },
                    new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72, 0x20, 0x20 , 0x00 },
                }
            },
            { ".tar.z", new List<byte[]>
                {
                    new byte[] { 0x1F, 0x9D },
                    new byte[] { 0x1F, 0xA0 }
                }
            },
            { ".tif", new List<byte[]>
                {
                    new byte[] { 0x49, 0x49, 0x2A, 0x00 },
                    new byte[] { 0x4D, 0x4D, 0x00, 0x2A }
                }
            },
            { ".tiff", new List<byte[]>
                {
                    new byte[] { 0x49, 0x49, 0x2A, 0x00 },
                    new byte[] { 0x4D, 0x4D, 0x00, 0x2A }
                }
            },
            { ".rar", new List<byte[]>
                {
                    new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07 , 0x00 },
                    new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07 , 0x01, 0x00 },
                }
            },
            { ".7z", new List<byte[]>
                {
                    new byte[] { 0x37, 0x7A, 0xBC, 0xAF, 0x27 , 0x1C },
                }
            },
            { ".txt", new List<byte[]>
                {
                    new byte[] { 0xEF, 0xBB , 0xBF },
                    new byte[] { 0xFF, 0xFE},
                    new byte[] { 0xFE, 0xFF },
                    new byte[] { 0x00, 0x00, 0xFE, 0xFF },
                }
            },
            { ".mp3", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xFB },
                    new byte[] { 0xFF, 0xF3},
                    new byte[] { 0xFF, 0xF2},
                    new byte[] { 0x49, 0x44, 0x43},
                }
            },
        };

    }
}