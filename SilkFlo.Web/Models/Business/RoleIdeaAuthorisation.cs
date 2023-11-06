/*********************************************************
       Code Generated By  .  .  .  .  Delaney's ScriptBot
       WWW .  .  .  .  .  .  .  .  .  www.scriptbot.io
       Template Name.  .  .  .  .  .  Project Green 3.0
       Template Version.  .  .  .  .  20220411 004
       Author .  .  .  .  .  .  .  .  Delaney

                      ,        ,--,_
                       \ _ ___/ /\|
                       ( )__, )
                      |/_  '--,
                        \ `  / '
 
 Note: Create this object,
       populate from properties from the Core.Domain classes
       and send to a view.

Object Models
-------------
What can this object do.

* GetClient()
  Two overrides to get the parent client for the object.
  This is used to display parent in a summary table.

* GetClients()
  Two overrides to get a list of alternative parent clients.

* GetIdeaAuthorisation()
  Two overrides to get the parent ideaAuthorisation for the object.
  This is used to display parent in a summary table.

* GetIdeaAuthorisations()
  Two overrides to get a list of alternative parent ideaAuthorisations.

* GetRole()
  Two overrides to get the parent role for the object.
  This is used to display parent in a summary table.

* GetRoles()
  Two overrides to get a list of alternative parent roles.* GetModels()
  Return a model containing properties populated with the objects values

* GetCreatedAndUpdated()
  Two overrides to get Users who created updated this object
  and assign them to CreatedBy and UpdatedBy properties.
 
 *********************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Models.Business
{
    public partial class RoleIdeaAuthorisation : Abstract
    {
        private SilkFlo.Data.Core.Domain.Business.RoleIdeaAuthorisation _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public RoleIdeaAuthorisation ()
        {
            _core = new SilkFlo.Data.Core.Domain.Business.RoleIdeaAuthorisation();
        }
        public RoleIdeaAuthorisation(SilkFlo.Data.Core.Domain.Business.RoleIdeaAuthorisation core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.Business.RoleIdeaAuthorisation cannot be null");
        }
        #endregion

        #region Properties
        public string DisplayText { get; set; }
        public SilkFlo.Data.Core.Domain.Business.RoleIdeaAuthorisation GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.Business.RoleIdeaAuthorisation core)
        {
            _core = core;
        }

        public bool IsNew => string.IsNullOrWhiteSpace(Id);

        /// <summary>
        /// Primary Key
        /// </summary>
        #region Id
        [StringLength(255,
                      MinimumLength = 0,
                      ErrorMessage = "Id cannot be greater than 255 characters in length.")]
        [DisplayName("Id")]
        public string Id
        {
            get => _core.Id;
            set
            {
                if (_core.Id == value)
                    return;

                _core.Id = value;
            }
        }

        public string Id_Error { get; set; }
        public string Id_ErrorMessage { get; set; } = "Required";
        public bool Id_IsInValid { get; set; }
        #endregion

        [Required]
        [DisplayName("Client")]
        public string ClientId
        {
            get => _core.ClientId;
            set
            {
                if (_core.ClientId == value)
                    return;

                _core.ClientId = value;
                _client = null;
            }
        }
        private Models.Business.Client _client;
        public Models.Business.Client Client
        {
            get
            {
                if (_client != null)
                    return _client;

                if (_core.Client != null)
                    _client = new Models.Business.Client(_core.Client);

                return _client;
            }
            set
            {
                if (_client == value)
                    return;

                _client = value;

                if (_client == null)
                    _core.Client = null;
                else
                {
                    if (_core.ClientId != _client.Id)
                        _core.Client = _client.GetCore();

                    _core.ClientId = _client.Id;
                }
            }
        }

        [DisplayName("Client")]
        public string ClientString => Client?.ToString();

        public string ClientId_ErrorMessage { get; set; } = "Required";
        public bool ClientId_IsInValid { get; set; }

        [Required]
        [DisplayName("Idea Authorisation")]
        public string IdeaAuthorisationId
        {
            get => _core.IdeaAuthorisationId;
            set
            {
                if (_core.IdeaAuthorisationId == value)
                    return;

                _core.IdeaAuthorisationId = value;
                _ideaAuthorisation = null;
            }
        }
        private Models.Shared.IdeaAuthorisation _ideaAuthorisation;
        public Models.Shared.IdeaAuthorisation IdeaAuthorisation
        {
            get
            {
                if (_ideaAuthorisation != null)
                    return _ideaAuthorisation;

                if (_core.IdeaAuthorisation != null)
                    _ideaAuthorisation = new Models.Shared.IdeaAuthorisation(_core.IdeaAuthorisation);

                return _ideaAuthorisation;
            }
            set
            {
                if (_ideaAuthorisation == value)
                    return;

                _ideaAuthorisation = value;

                if (_ideaAuthorisation == null)
                    _core.IdeaAuthorisation = null;
                else
                {
                    if (_core.IdeaAuthorisationId != _ideaAuthorisation.Id)
                        _core.IdeaAuthorisation = _ideaAuthorisation.GetCore();

                    _core.IdeaAuthorisationId = _ideaAuthorisation.Id;
                }
            }
        }

        [DisplayName("Idea Authorisation")]
        public string IdeaAuthorisationString => IdeaAuthorisation?.ToString();

        public string IdeaAuthorisationId_ErrorMessage { get; set; } = "Required";
        public bool IdeaAuthorisationId_IsInValid { get; set; }

        [Required]
        [DisplayName("Role")]
        public string RoleId
        {
            get => _core.RoleId;
            set
            {
                if (_core.RoleId == value)
                    return;

                _core.RoleId = value;
                _role = null;
            }
        }
        private Models.Business.Role _role;
        public Models.Business.Role Role
        {
            get
            {
                if (_role != null)
                    return _role;

                if (_core.Role != null)
                    _role = new Models.Business.Role(_core.Role);

                return _role;
            }
            set
            {
                if (_role == value)
                    return;

                _role = value;

                if (_role == null)
                    _core.Role = null;
                else
                {
                    if (_core.RoleId != _role.Id)
                        _core.Role = _role.GetCore();

                    _core.RoleId = _role.Id;
                }
            }
        }

        [DisplayName("Role")]
        public string RoleString => Role?.ToString();

        public string RoleId_ErrorMessage { get; set; } = "Required";
        public bool RoleId_IsInValid { get; set; }

        private List<Models.Business.Client> _clients;
        public List<Models.Business.Client> Clients
        {
            get => _clients ??= new List<Models.Business.Client>();
            set => _clients = value;
        }

        private List<Models.Shared.IdeaAuthorisation> _ideaAuthorisations;
        public List<Models.Shared.IdeaAuthorisation> IdeaAuthorisations
        {
            get => _ideaAuthorisations ??= new List<Models.Shared.IdeaAuthorisation>();
            set => _ideaAuthorisations = value;
        }

        private List<Models.Business.Role> _roles;
        public List<Models.Business.Role> Roles
        {
            get => _roles ??= new List<Models.Business.Role>();
            set => _roles = value;
        }

        #region OwnerId
        public string OwnerId => UpdatedById ?? CreatedById;
        #endregion

        #region Owner  
        public string Owner => UpdatedBy ?? CreatedBy;
        #endregion
        

        #region CreatedBy
        [DisplayName("Created By")]
        public string CreatedBy => _core.CreatedBy == null ? "" : _core.CreatedBy.ToString();
        #endregion

        #region CreatedDate
        [DisplayName("Created Date")]
        public DateTime? CreatedDate
        {
            get => _core.CreatedDate;
            set
            {
                if (_core.CreatedDate == value)
                    return;

                _core.CreatedDate = value;
            }
        }
        #endregion

        [DisplayName("Created Date")]
        public string CreatedDateString => CreatedDate?.ToString(Data.Core.Settings.DateFormatShort);

        #region CreatedById
        [DisplayName("Created By Id")]
        public string CreatedById
        {
            get => _core.CreatedById;
            set => _core.CreatedById = value;
        }
        #endregion

        #region UpdatedById
        [DisplayName("Updated By Id")]
        public string UpdatedById
        {
            get => _core.UpdatedById;
            set => _core.UpdatedById = value;
        }
        #endregion

        #region UpdatedBy
        [DisplayName("Updated By")]
        public string UpdatedBy => _core.UpdatedBy == null ? "" : _core.UpdatedBy.ToString();
        #endregion

        #region UpdatedDate
        [DisplayName("Updated Date")]
        public DateTime? UpdatedDate
        {
            get => _core.UpdatedDate;
            set
            {
                if (_core.UpdatedDate == value)
                    return;

                _core.UpdatedDate = value;
            }
        }
        #endregion

        [DisplayName("Updated Date")]
        public string UpdatedDateString => UpdatedDate?.ToString(Data.Core.Settings.DateTimeFormatShort);


        [DisplayName("Date")]
        public string DateDisplayed => UpdatedDate == null ? CreatedDate?.ToString("yyyy-MM-dd") : UpdatedDate?.ToString("yyyy-MM-dd");

        [DisplayName("Date Time")]
        public string DateTimeDisplayed => UpdatedDate == null ? CreatedDate?.ToString("yyyy-MM-dd HH:mm:ss") : UpdatedDate?.ToString("yyyy-MM-dd HH:mm:ss");
        #endregion

        #region IsDeleted
        [DisplayName("Is Deleted")]
        public bool IsDeleted
        {
            get => _core.IsDeleted;
            set
            {
                if (_core.IsDeleted == value)
                    return;

                _core.IsDeleted = value;
            }
        }
        #endregion

        #region IsSaved
        [DisplayName("Is Saved")]
        public bool IsSaved => _core.IsSaved;
        #endregion

        public async Task GetCreatedAndUpdated(Data.Core.IUnitOfWork unitOfWork)
        {
            _core.CreatedBy = await unitOfWork.Users.GetAsync(_core.CreatedById);
            _core.UpdatedBy = await unitOfWork.Users.GetAsync(_core.UpdatedById);
        }

        /// <summary>
        /// Check unique key constraints.
        /// </summary>
        /// <returns>ViewModels.Feedback</returns>
        public async Task<ViewModels.Feedback> CheckUniqueAsync(
        Data.Core.IUnitOfWork unitOfWork,
        ViewModels.Feedback feedback)
        {
            if (unitOfWork == null)
                throw new NullReferenceException("Data.Core.IUnitOfWork cannot be null");

            // Check unique
            var message = await unitOfWork.IsUniqueAsync(GetCore());// Data.Persistence.UnitOfWork.IsUniqueAsync(GetCore());

            if (string.IsNullOrWhiteSpace(message)) 
                return feedback;


             // We have a conflict, give feedback.
            if (feedback.Elements.ContainsKey("ClientId"))
                feedback.Elements["ClientId"] = message;
            else
                feedback.Elements.Add("ClientId", message);

            if (feedback.Elements.ContainsKey("IdeaAuthorisationId"))
                feedback.Elements["IdeaAuthorisationId"] = message;
            else
                feedback.Elements.Add("IdeaAuthorisationId", message);

            if (feedback.Elements.ContainsKey("RoleId"))
                feedback.Elements["RoleId"] = message;
            else
                feedback.Elements.Add("RoleId", message);


            feedback.IsValid = false;

            return feedback;
        }

        public static List<Models.Business.RoleIdeaAuthorisation> Create(IEnumerable<Data.Core.Domain.Business.RoleIdeaAuthorisation> cores)
        {
            return cores.Select(core => new Models.Business.RoleIdeaAuthorisation(core)).ToList();
        }
        
        public static Models.Business.RoleIdeaAuthorisation[] Create(Data.Core.Domain.Business.RoleIdeaAuthorisation[] cores)
        {
            return cores.Select(core => new Models.Business.RoleIdeaAuthorisation(core)).ToArray();
        }
    }
}