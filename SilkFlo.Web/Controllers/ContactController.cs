/*********************************************************
       Code Generated By  .  .  .  .  Delaney's ScriptBot
       WWW .  .  .  .  .  .  .  .  .  www.scriptbot.io
       Template Name.  .  .  .  .  .  Project Green 3.0
       Template Version.  .  .  .  .  20210318 011
       Author .  .  .  .  .  .  .  .  Delaney

                      ,        ,--,_
                       \ _ ___/ /\|
                       ( )__, )
                      |/_  '--,
                        \ `  / '

 *********************************************************/

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SilkFlo.Web.Controllers
{
    public partial class ContactController : AbstractAPI
    {
        public ContactController(Data.Core.IUnitOfWork unitOfWork,
                                 Services.ViewToString viewToString,
                                 IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }


        #region HTTP Posters
        [HttpPost("api/contact/contact")]
        public async Task<IActionResult> Contact(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                _unitOfWork.Log("Json is empty.",
                                Data.Core.Severity.Warning);

                return BadRequest();
            }

            var jsonResultSuccess = new JsonResult(new { Message = "Your message was sent at " + DateTime.Now.ToString("HH:mm.ss") });

            string jsonSample = "{\"FirstName\":\"\",\"LastName\":\"\",\"Email\":\"\",\"Subject\":\"\",\"Text\":\"\"}";
            int maxLength = jsonSample.Length + 50 + 50 + 50 + 256 + 256;

            if (json.Length > maxLength)
            {
                // Fail silently
                return jsonResultSuccess;
            }


            var contact = JsonConvert.DeserializeObject<Services.Models.Account.Contact>(json);

            if (contact == null)
                return BadRequest();

            if(!TryValidateModel(contact))
                return BadRequest(ModelState);

            // Is Email valid?
            if (string.IsNullOrEmpty(contact.Email))
            {
                string JSON = "{\"message\":\"Please add an email address\"}";
                return StatusCode(400, JSON);
            }


            // Is there and subject or text?
            if (string.IsNullOrEmpty(contact.Subject)
            || string.IsNullOrEmpty(contact.Text))
            {
                string JSON = "{\"message\":\"Please provide a subject line and message\"}";
                return StatusCode(400, JSON);
            }

            contact.Replace();


            var user =
                await
                _unitOfWork.Users
                           .SingleOrDefaultAsync(m => m.Email.ToLower() == contact.Email.ToLower());


            // Create the user if not present
            if (user == null)
            {
                user = new Data.Core.Domain.User
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email
                };
                await _unitOfWork.Users.AddAsync(user);
            }

            // Create the message
            var message = new Data.Core.Domain.Message
            {
                User = user,
                Subject = contact.Subject,
                Text = contact.Text
            };

            await _unitOfWork.Messages.AddAsync(message);

            // Save
            try
            {
                _unitOfWork.Complete();
            }
            catch
            {
                string JSON = "{\"message\":\"Failed to saved message\"}";
                return StatusCode(400, JSON);
            }


            // Send
            try
            {
                var result = await Email.Service.SendEmailContactAsync(message.User,
                                                                       message.Subject,
                                                                       message.Text,
                                                                       Domain);
                if (!result.Item1)
                {
                    string JSON = "{\"message\":\""+ result.Item2 + "\"}";
                    return StatusCode(400, JSON);
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Log(ex);

                return StatusCode(400, "");
            }

            // Report success
            return jsonResultSuccess;
        }
        #endregion
    }
}