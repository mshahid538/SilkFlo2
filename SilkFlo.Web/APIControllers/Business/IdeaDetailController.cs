using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SilkFlo.Web.Controllers2.FileUpload;
using Microsoft.AspNetCore.Mvc.Rendering;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using System.IO.Compression;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Azure.Storage.Blobs;

namespace SilkFlo.Web.Controllers.Business
{
    public partial class IdeaController
    {


        [HttpGet("api/business/idea/detail/about/ideaId/{id}")]
        public async Task<IActionResult> GetAbout(string id)
        {
            // Check Authorization
            if (!(await AuthorizeAsync(Policy.Subscriber)).Succeeded)
                return NegativeFeedback("Unauthorised");

            var model = await GetDetailIdeaAsync(id);

            // Guard Clause
            if (model == null)
                return Content("<h1 class\"text-danger\">Error: Idea was not found.</h1>");

            var html = await _viewToString.PartialAsync(
                "Shared/Business/Idea/Detail/_about.cshtml",
                model);

            return Content(html);
        }

        
        [HttpGet("/api/business/idea/detail/Documentation/ideaId/{id}")]
        public async Task<IActionResult> GetDocumentation(string id)
        {
            // Check Authorization
            if (!(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded)
                return NegativeFeedback("Unauthorised");

            var documents = (await _unitOfWork.BusinessDocuments.FindAsync(x => x.IdeaId == id)).ToList();

			//if (documents.Any())
			//    return Content("<h1 class\"text-info\">Documents were not found.</h1>");

			// Get the select tenantId
			var userId = GetUserId();

			// Guard Clause
			if (userId == null)
				return NegativeFeedback("No userId");

			var user = await _unitOfWork.Users.GetAsync(userId);

			// Guard Clause
			if (user == null)
				return NegativeFeedback($"User with id {userId} missing");

			var viewmodel = new ViewModels.Business.Idea.Documentation();
			//  viewmodel.Documents = Models.Business.Document.Create(documents);

			#region TemplatesGet
			//Getting the tenant
			var tenantCore = await GetClientAsync();
			//Getting docs
			var blobstorageTemplateDATA = await AzureStorage.GetTemplateDocuments(tenantCore.Id);
			if (blobstorageTemplateDATA.Any() && blobstorageTemplateDATA.Any(x => x.DocumentDtos.Any()))
			{
				blobstorageTemplateDATA.ToList().ForEach((doc) =>
				{
					doc.DocumentDtos.ForEach((x) =>
					{
						x.User = user.Fullname;
					});
				});
			}
			viewmodel.TDocuments = blobstorageTemplateDATA.ToList();
			#endregion

			var blobstorageDATA = await AzureStorage.GetDocuments("idea-container", tenantCore.Id, id, GetUserId(), "");

			if (blobstorageDATA.Any())
			{
				blobstorageDATA.ToList().ForEach((doc) =>
				{
					doc.User = user.Fullname;
				});
			}

			var blobDocuments = blobstorageDATA.ToList();
            viewmodel.Documents = blobDocuments;
            viewmodel.Id = id;
            viewmodel.AccessAllowed = (await AuthorizeAsync(Policy.Administrator)).Succeeded;
			var html = await _viewToString.PartialAsync(
                    "Shared/Business/Idea/Detail/Documentation/_IdeaFileUpload2.cshtml",
                    viewmodel);

            return Content(html);
        }

        [HttpGet("/api/business/idea/detail/TemplateDocumentation/ideaId/GetTemplates")]
        public async Task<IActionResult> GetTemplateDocumentation()
        {
            // Check Authorization
            if (!(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded)
              return NegativeFeedback("Unauthorised");


			// Get the select tenantId
			var userId = GetUserId();

			// Guard Clause
			if (userId == null)
				return NegativeFeedback("No userId");

			var user = await _unitOfWork.Users.GetAsync(userId);

			// Guard Clause
			if (user == null)
				return NegativeFeedback($"User with id {userId} missing");

			var viewmodel = new ViewModels.Business.Idea.Documentation();

            //Getting the tenant
            var tenantCore = await GetClientAsync();
            //Getting docs
            var blobstorageDATA = await AzureStorage.GetTemplateDocuments(tenantCore.Id);

            if(blobstorageDATA.Any() && blobstorageDATA.Any(x => x.DocumentDtos.Any()))
            {
                blobstorageDATA.ToList().ForEach((doc) =>
                {
                    doc.DocumentDtos.ForEach((x) =>
                    {
                        x.User = user.Fullname;
                    });
                });
			}

			viewmodel.TDocuments = blobstorageDATA.ToList();
            viewmodel.Id = blobstorageDATA.ToList().FirstOrDefault()?.DocumentDtos.FirstOrDefault()?.FileNameWithExtetnsion;
            viewmodel.AccessAllowed = (await AuthorizeAsync(Policy.Administrator)).Succeeded;
            var html = await _viewToString.PartialAsync(
                    "Shared/Business/Idea/Detail/Documentation/_AdminIdeaFileUpload.cshtml",
                    viewmodel); // blobstorageDATA);

            return Content(html);
        }


        [HttpPost("/api/business/idea/detail/Documentation/ideaId/UploadFiles")]
        public async Task<IActionResult> UploadFiles(IList<IFormFile> files, string selectedTemplate, string currItem)
        {
			// Get the select tenantId
			var userId = GetUserId();

			if (!IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditDocumentation, currItem, userId))
				return Content("<h1 class=\"text-warning\">You do not have permission to edit this idea.</h1>");

            if(files == null || files.Count <= 0)
				return Ok(new { Message = "Select files to upload" });

			var fileDtoErrors = new List<string>();
			foreach (var formFile in files)
			{
				if (formFile.Length > 200_000_000)
				{
					// Handle file size limit
					fileDtoErrors.Add($"{formFile.FileName} size exceeds limit of 200 MB");
				}
			}

			if (fileDtoErrors.Count > 0)
				return Ok(new { Message = string.Join(", ", fileDtoErrors) });

			//Getting the tenant
			var tenantCore = await GetClientAsync();

			// Guard Clause
			if (userId == null)
				return NegativeFeedback("No userId");

			var user = await _unitOfWork.Users.GetAsync(userId);

			// Guard Clause
			if (user == null)
				return NegativeFeedback($"User with id {userId} missing");

			var blobReponseDtos = await AzureStorage.UploadAsyncTest(files, tenantCore.Id, currItem, user, selectedTemplate);

            return Ok(new {
                Result = blobReponseDtos,
				Path = $"/Idea/Detail/IdeaId/{currItem}/Documentation",
            });
        }

        [HttpPost("/api/business/idea/detail/Documentation/ideaId/UploadTemplateFile")]
        public async Task<IActionResult> UploadTemplateFile(IList<IFormFile> files, string selectedTemplate, string currItem)
        {
			// Get the select tenantId
			var userId = GetUserId();

			if (!IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditDocumentation, currItem, userId))
				return Content("<h1 class=\"text-warning\">You do not have permission to edit this idea.</h1>");

			if (files == null || files.Count <= 0)
				return Ok(new { Message = "Select files to upload" });

			var fileDtoErrors = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 200_000_000)
                {
                    // Handle file size limit
                    fileDtoErrors.Add($"{formFile.FileName} size exceeds limit of 200 MB");
				}
			}

            if (fileDtoErrors.Count > 0)
				return Ok(new { Message = string.Join(", ", fileDtoErrors) });

			//Getting the tenant
			var tenantCore = await GetClientAsync();
            var blobResponseDto = await AzureStorage.UploadTemlateDocumentAsync(files, $"templates/{tenantCore.Id}/{selectedTemplate}");

            return Ok(blobResponseDto);
        }


        [HttpPost("/api/business/idea/detail/Documentation/ideaId/DownloadBlob")]
        public async Task<IActionResult> DownloadBlob(string selectedDocs, string ideaId)
        {
			// Get the select tenantId
			var userId = GetUserId();

			if (!IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditDocumentation, ideaId, userId))
				return Content("<h1 class=\"text-warning\">You do not have permission to edit this idea.</h1>");


			var idea = await _unitOfWork.BusinessIdeas.GetAsync(ideaId);
			await _unitOfWork.BusinessDocuments.GetForIdeaAsync(idea);
			// Create a memory stream to hold the zip file
			var memoryStream = new MemoryStream();

			if (idea.Documents != null && idea.Documents.Count > 0)
            {
                // Create a zip archive
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var document in idea.Documents)
                    {
                        if (selectedDocs.Split(",").Contains(document.Id))
                        {
                            Uri uri = new Uri(document.FilenameBackend);
                            string result = uri.AbsolutePath.TrimStart('/');
                            string[] substrings = result.Split(new string[] { "idea-container/" }, StringSplitOptions.None);
                            string path = System.Web.HttpUtility.UrlDecode(substrings[1]);

                            var response = await AzureStorage.DownloadBlobAsync(path, ideaId, true);

                            // Add the first file to the archive
                            var fileEntry = archive.CreateEntry(response.Item3);
                            using (Stream fs = fileEntry.Open())
                            {
                                fs.Write(response.Item1, 0, response.Item1.Length);
                            }
                        }
					}
                }

				// Reset the position of the memory stream to the beginning
				memoryStream.Position = 0;

				// Return the zip file as a FileContentResult
				return File(memoryStream, "application/zip", "IdeaDocuments.zip");
			}

			return File(memoryStream, "application/zip", "IdeaDocuments-empty.zip");
		}

		[HttpGet("/api/business/idea/detail/Documentation/ideaId/DownloadTBlob")]
		public async Task<IActionResult> DownloadTBlob(string docUri, string ideaId)
		{
			// Get the select tenantId
			var userId = GetUserId();

			if (!IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditDocumentation, ideaId, userId))
				return Content("<h1 class=\"text-warning\">You do not have permission to edit this idea.</h1>");

			var response = await AzureStorage.DownloadBlobAsync(docUri, ideaId, true);

            if (response.Item1 != null && !String.IsNullOrEmpty(response.Item2) && !String.IsNullOrEmpty(response.Item3))
                return File(response.Item1, response.Item2, response.Item3);

			return NoContent();
		}

		[HttpGet("/api/business/idea/detail/Documentation/ideaId/Download")]
		public async Task<IActionResult> DownloadSingleBlob(string docUri, string ideaId)
		{
			// Get the select tenantId
			var userId = GetUserId();

			if (!IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditDocumentation, ideaId, userId))
				return Content("<h1 class=\"text-warning\">You do not have permission to edit this idea.</h1>");

			var response = await AzureStorage.DownloadBlobAsync(docUri, ideaId);

            if (response.Item1 != null && !String.IsNullOrEmpty(response.Item2) && !String.IsNullOrEmpty(response.Item3))
            {
				var contentDisposition = new System.Net.Mime.ContentDisposition
				{
					FileName = response.Item3,
					Inline = false // Set this to false to force the browser to download the file
				};

				Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

				return File(response.Item1, response.Item2, response.Item3);
            }
            
			return NoContent();
		}

        [HttpPost("/api/business/idea/detail/Documentation/ideaId/Downloads")]
        public async Task<IActionResult> DownloadMultipleBlobs([FromForm] string docUris, [FromForm] string ideaId, [FromForm] bool isTemplate)
        {
            // Get the select tenantId
            var userId = GetUserId();

			// Create a memory stream to hold the zip file
			var memoryStream = new MemoryStream();

			//if (!IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditDocumentation, ideaId, userId))
                //return Content("<h1 class=\"text-warning\">You do not have permission to edit this idea.</h1>");

            if (isTemplate)
            {
                if (!String.IsNullOrWhiteSpace(docUris))
                {
					var uris = docUris.Split(",");
					// Create a zip archive
					using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var uri in uris)
                        {
                            //Uri uri = new Uri(document.FilenameBackend);
                            //string result = uri.AbsolutePath.TrimStart('/');
                            //string[] substrings = result.Split(new string[] { "idea-container/" }, StringSplitOptions.None);
                            //string path = System.Web.HttpUtility.UrlDecode(substrings[1]);

                            var response = await AzureStorage.DownloadBlobAsync(uri, ideaId, true);

                            // Add the first file to the archive
                            var fileEntry = archive.CreateEntry(response.Item3);
                            using (Stream fs = fileEntry.Open())
                            {
                                fs.Write(response.Item1, 0, response.Item1.Length);
                            }
                        }
                    }
                }
			}
            else
            {
				var idea = await _unitOfWork.BusinessIdeas.GetAsync(ideaId);
				await _unitOfWork.BusinessDocuments.GetForIdeaAsync(idea);

				if (idea.Documents != null && idea.Documents.Count > 0)
				{
					// Create a zip archive
					using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
					{
						foreach (var document in idea.Documents)
						{
							Uri uri = new Uri(document.FilenameBackend);
							string result = uri.AbsolutePath.TrimStart('/');
							string[] substrings = result.Split(new string[] { "idea-container/" }, StringSplitOptions.None);
							string path = System.Web.HttpUtility.UrlDecode(substrings[1]);

							var response = await AzureStorage.DownloadBlobAsync(path, ideaId, true);

							// Add the first file to the archive
							var fileEntry = archive.CreateEntry(response.Item3);
							using (Stream fs = fileEntry.Open())
							{
								fs.Write(response.Item1, 0, response.Item1.Length);
							}
						}
					}
				}
			}


			//memoryStream.Position = 0;

			//// Create a response message with the file data
			//var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
			//httpResponseMessage.Content = new StreamContent(memoryStream);

			//// Set the content type header to the correct MIME type
			//httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");

			//// Set the content disposition header to specify the filename
			//httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
			//httpResponseMessage.Content.Headers.ContentDisposition.FileName = "IdeaDocuments.zip";

			//return httpResponseMessage;

			// Reset the position of the memory stream to the beginning
			memoryStream.Position = 0;

			// Return the zip file as a FileContentResult
			return File(memoryStream, "application/zip", "IdeaDocuments.zip");
		}

        [HttpPost("/api/business/idea/detail/Documentation/ideaId/DeleteBlobs")]
        public async Task<IActionResult> DeleteMultipleBlobs(string docUris, string ideaId, bool isTemplate)
        {
            // Get the select tenantId
            var userId = GetUserId();

            if (!IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditDocumentation, ideaId, userId))
                return Content("<h1 class=\"text-warning\">You do not have permission to edit this idea.</h1>");

            var uris = docUris.Split(",").ToList();
            var response = await AzureStorage.DeleteAsync(uris);

            if (response.Count > 0)
            {
                var success = response.Where(x => x.Error == false).Count();
				var error = response.Where(x => x.Error == true).Count();

				return Ok(new
                {
                    Message = $"{success} files are deleted, {error} files failed to delete.",
                });
            }

            return NoContent();
        }


        [HttpGet("/api/business/idea/detail/Documentation/ideaId/DownloadAd")]
        public async Task<IActionResult> DownloadSingleTemplateBlobs(string docUri)
        {
            if (!String.IsNullOrWhiteSpace(docUri))
            {
                var response = await AzureStorage.DownloadBlobAsync(docUri, "", true);
                return File(response.Item1, response.Item2, response.Item3);
            }

            return NoContent();
        }

        [HttpDelete("/api/business/idea/detail/Documentation/ideaId/DeleteBlobAd")]
        public async Task<IActionResult> DeleteSingleTemplateBlobs(string docUri)
        {
            var response = await AzureStorage.DeleteAsync(docUri, "", true);

            return Ok(new
            {
                Message = !response.Error ? "File is deleted" : "Some error occurred while deleting file",
            });
        }


        [HttpDelete("/api/business/idea/detail/Documentation/ideaId/DeleteBlob")]
        public async Task<IActionResult> DeleteBlob(string docUri, string ideaId)
        {
			// Get the select tenantId
			var userId = GetUserId();

			if (!IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditDocumentation, ideaId, userId))
				return Content("<h1 class=\"text-warning\">You do not have permission to edit this idea.</h1>");

			var response = await AzureStorage.DeleteAsync(docUri, ideaId);

            if(response != null)
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpDelete("/api/business/idea/detail/Documentation/ideaId/DeleteUserBlobs")]
        public async Task<IActionResult> DeleteUserDocs(string selectedDocs, string ideaId)
        {
            // Get the select tenantId
            var userId = GetUserId();

            if (!IsIdeaAuthorisationMember(_unitOfWork, Data.Core.Enumerators.IdeaAuthorization.EditDocumentation, ideaId, userId))
                return Content("<h1 class=\"text-warning\">You do not have permission to edit this idea.</h1>");

            if (!String.IsNullOrWhiteSpace(selectedDocs))
            {
                foreach (var doc in selectedDocs.Split(","))
                {
                    await AzureStorage.DeleteAsync(doc, ideaId);
                }
            }

            return Ok(new
            {
                Message = "Files are deleted."
            });
        }


		[HttpGet("api/business/idea/detail/Collaborators/ideaId/{id}")]
        public async Task<IActionResult> GetCollaboratorSummary(string id)
        {
            try
            {
                // Check Authorization
                if (!(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded)
                    return NegativeFeedback("Error: Unauthorised");

                var ideaCore = await _unitOfWork.BusinessIdeas.GetAsync(id);

                // Guard Clause
                if (ideaCore == null)
                    return NegativeFeedback("Error: Invalid idea Id.");

                var collaborators =
                    (await _unitOfWork.BusinessCollaborators.FindAsync(x => x.IdeaId == id))
                    .ToArray();

                await _unitOfWork.Users.GetUserForAsync(collaborators);
                var collaboratingUsers = new List<Models.User>();
                foreach (var collaborator in collaborators)
                    collaboratingUsers.Add(new Models.User(collaborator.User));


                var idea = new Models.Business.Idea(ideaCore);

                var model = new ViewModels.Business.Idea.ManageCollaborator.CollaboratorList
                {
                    Idea = idea,
                    CollaboratingUsers = collaboratingUsers,
                    CanEditCollaborators = (await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded
                };

                var html = await _viewToString.PartialAsync(
                    "Shared/Business/Idea/Detail/Collaborators/_Page.cshtml",
                    model);

                return Content(html);
            }
            catch (Exception e)
            {
                _unitOfWork.Log(e);
                return NegativeFeedback("Error getting Collaborators from data store.");
            }
        }


        [HttpGet("api/business/idea/detail/IdeaStageGantt/ideaId/{id}")]
        public async Task<IActionResult> GetIdeaStageGantt(string id)
        {
            // Check Authorization
            if (!(await AuthorizeAsync(Policy.EditAllIdeaFields)).Succeeded)
                return NegativeFeedback("Unauthorised");

            var model = new ViewModels.Business.Idea.IdeaStageGantt
            {
                GanttIdeaStages = await GetGanttChartStages(id),
                IsReadOnly = true
            };

            var html = await _viewToString.PartialAsync(
                "Shared/Business/Idea/_IdeaStageGantt.cshtml",
                model);

            return Content(html);
        }
    }

}