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

* GetUser()
  Two overrides to get the parent user for the object.
  This is used to display parent in a summary table.

* GetUsers()
  Two overrides to get a list of alternative parent users.* GetModels()
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

namespace SilkFlo.Web.Models
{
    public partial class Message : Abstract
    {
        private SilkFlo.Data.Core.Domain.Message _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public Message ()
        {
            _core = new SilkFlo.Data.Core.Domain.Message();
        }
        public Message(SilkFlo.Data.Core.Domain.Message core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.Message cannot be null");
        }
        #endregion

        #region Properties
        public string DisplayText { get; set; }
        public SilkFlo.Data.Core.Domain.Message GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.Message core)
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

        #region Subject
        [Required]
        [StringLength(256,
                      ErrorMessage = "Subject must be between 1 and 256 characters in length.")]
        [DisplayName("Subject")]
        public string Subject
        {
            get => _core.Subject;
            set
            {
                if (_core.Subject == value)
                    return;

                _core.Subject = value;
            }
        }

        public string Subject_ErrorMessage { get; set; } = "Required";
        public bool Subject_IsInValid { get; set; }
        #endregion

        #region Text
        [Required]
        [StringLength(256,
                      ErrorMessage = "Text must be between 1 and 256 characters in length.")]
        [DisplayName("Text")]
        public string Text
        {
            get => _core.Text;
            set
            {
                if (_core.Text == value)
                    return;

                _core.Text = value;
            }
        }

        public string Text_ErrorMessage { get; set; } = "Required";
        public bool Text_IsInValid { get; set; }
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
        [DisplayName("User")]
        public string UserId
        {
            get => _core.UserId;
            set
            {
                if (_core.UserId == value)
                    return;

                _core.UserId = value;
                _user = null;
            }
        }
        private Models.User _user;
        public Models.User User
        {
            get
            {
                if (_user != null)
                    return _user;

                if (_core.User != null)
                    _user = new Models.User(_core.User);

                return _user;
            }
            set
            {
                if (_user == value)
                    return;

                _user = value;

                if (_user == null)
                    _core.User = null;
                else
                {
                    if (_core.UserId != _user.Id)
                        _core.User = _user.GetCore();

                    _core.UserId = _user.Id;
                }
            }
        }

        [DisplayName("User")]
        public string UserString => User?.ToString();

        public string UserId_ErrorMessage { get; set; } = "Required";
        public bool UserId_IsInValid { get; set; }

        private List<Models.Business.Client> _clients;
        public List<Models.Business.Client> Clients
        {
            get => _clients ??= new List<Models.Business.Client>();
            set => _clients = value;
        }

        private List<Models.User> _users;
        public List<Models.User> Users
        {
            get => _users ??= new List<Models.User>();
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


        public override string ToString()
        {
            if(!string.IsNullOrWhiteSpace(DisplayText))
                return DisplayText;

            return Subject;
        }
        public static List<Models.Message> Create(IEnumerable<Data.Core.Domain.Message> cores)
        {
            return cores.Select(core => new Models.Message(core)).ToList();
        }
        
        public static Models.Message[] Create(Data.Core.Domain.Message[] cores)
        {
            return cores.Select(core => new Models.Message(core)).ToArray();
        }
    }
}