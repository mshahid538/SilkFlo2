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
  Two overrides to get a list of alternative parent clients.* GetUsers()
  Two overrides to get Users children for this object.

* GetModels()
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
    public partial class Location : Abstract
    {
        private SilkFlo.Data.Core.Domain.Business.Location _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public Location ()
        {
            _core = new SilkFlo.Data.Core.Domain.Business.Location();
        }
        public Location(SilkFlo.Data.Core.Domain.Business.Location core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.Business.Location cannot be null");
        }
        #endregion

        #region Properties
        private string _displayText;
        public string DisplayText
        { 
            get => string.IsNullOrWhiteSpace(_displayText) ? Name : _displayText;
            set => _displayText = value;
        }
        public SilkFlo.Data.Core.Domain.Business.Location GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.Business.Location core)
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

        #region Name
        [Required]
        [StringLength(100,
                      ErrorMessage = "Name must be between 1 and 100 characters in length.")]
        [DisplayName("Name")]
        public string Name
        {
            get => _core.Name;
            set
            {
                if (_core.Name == value)
                    return;

                _core.Name = value;
            }
        }

        public string Name_ErrorMessage { get; set; } = "Required";
        public bool Name_IsInValid { get; set; }
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

        private List<Models.Business.Client> _clients;
        public List<Models.Business.Client> Clients
        {
            get => _clients ??= new List<Models.Business.Client>();
            set => _clients = value;
        }

        private List<Models.User> _users; 
        public List<Models.User> Users 
        {
            get
            {
                if (_users != null)
                    return _users;

                _users = new List<User>();

                if (_core.Users == null)
                    return _users;

                foreach (var core in _core.Users)
                    _users.Add(new Models.User(core));

                return _users;
            }
            set => _users = value;
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

            if (feedback.Elements.ContainsKey("Name"))
                feedback.Elements["Name"] = message;
            else
                feedback.Elements.Add("Name", message);


            feedback.IsValid = false;

            return feedback;
        }

        public override string ToString()
        {
            if(!string.IsNullOrWhiteSpace(DisplayText))
                return DisplayText;

            return Name;
        }
        public static List<Models.Business.Location> Create(IEnumerable<Data.Core.Domain.Business.Location> cores, bool includeEmpty = false)
        {
            if (cores == null)
                return null;

            if(includeEmpty)
            {
                var models = new List<Models.Business.Location>();
                models.Add(new Models.Business.Location
                {
                    DisplayText = "<Empty>"
                });

                models.AddRange(cores.Select(core => new Models.Business.Location(core)));
                return models;
            }

            return cores.Select(core => new Models.Business.Location(core)).ToList();
        }

        public static Models.Business.Location[] Create(Data.Core.Domain.Business.Location[] cores, bool includeEmpty = false)
        {
            if (cores == null)
                return null;

            if(includeEmpty)
                return Create(cores.ToList(), true).ToArray();

            return cores.Select(core => new Models.Business.Location(core)).ToArray();
        }
    }
}