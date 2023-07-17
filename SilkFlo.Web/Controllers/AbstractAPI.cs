using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SilkFlo.Web.Controllers2.FileUpload;

namespace SilkFlo.Web.Controllers
{
    public abstract class AbstractAPI : AbstractClient
    {

        #region Constructors
        protected AbstractAPI(Data.Core.IUnitOfWork unitOfWork,
            Services.ViewToString viewToString,
            IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization)
        {

        }

        protected AbstractAPI(Data.Core.IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion


        protected ContentResult NegativeFeedback(string message = "Unauthorised")
        {
            return Content($"<span class=\"text-danger\" >{message}<span>");
        }




        protected ViewModels.Feedback GetFeedback(
            ModelStateDictionary modelState,
            ViewModels.Feedback feedback = null)
        {
            var isValidState = false;
            if (feedback == null)
                feedback = new ViewModels.Feedback
                {
                    Message = ""
                };
            else
                isValidState = feedback.IsValid;


            if (modelState.IsValid)
            {
                feedback.IsValid = true;
                return feedback;
            }


            foreach (var modelStateKey in modelState.Keys)
            {
                var value = ViewData.ModelState[modelStateKey];

                if (value == null)
                    continue;

                foreach (var error in value.Errors)
                    feedback.Elements.Add(modelStateKey, error.ErrorMessage);
            }


            feedback.IsValid = feedback.Elements.Count == 0 && isValidState;

            return feedback;
        }
    }
}