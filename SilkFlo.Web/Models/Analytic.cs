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
    public partial class Analytic : Abstract
    {
        private SilkFlo.Data.Core.Domain.Analytic _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public Analytic ()
        {
            _core = new SilkFlo.Data.Core.Domain.Analytic();
        }
        public Analytic(SilkFlo.Data.Core.Domain.Analytic core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.Analytic cannot be null");
        }
        #endregion

        #region Properties
        public string DisplayText { get; set; }
        public SilkFlo.Data.Core.Domain.Analytic GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.Analytic core)
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

        #region Action
        [StringLength(255,
                      MinimumLength = 0,
                      ErrorMessage = "Action cannot be greater than 255 characters in length.")]
        [DisplayName("Action")]
        public string Action
        {
            get => _core.Action;
            set
            {
                if (_core.Action == value)
                    return;

                _core.Action = value;
            }
        }

        public string Action_ErrorMessage { get; set; }
        public bool Action_IsInValid { get; set; }
        #endregion

        #region Date
        [Required]
        [DisplayName("Date")]
        public DateTime Date
        {
            get => _core.Date;
            set
            {
                if (_core.Date == value)
                    return;

                _core.Date = value;
            }
        }

        public string Date_ErrorMessage { get; set; } = "Required";
        public bool Date_IsInValid { get; set; }
        #endregion

        #region Language
        [StringLength(100,
                      MinimumLength = 0,
                      ErrorMessage = "Language cannot be greater than 100 characters in length.")]
        [DisplayName("Language")]
        public string Language
        {
            get => _core.Language;
            set
            {
                if (_core.Language == value)
                    return;

                _core.Language = value;
            }
        }

        public string Language_ErrorMessage { get; set; }
        public bool Language_IsInValid { get; set; }
        #endregion

        #region Platform
        [StringLength(100,
                      MinimumLength = 0,
                      ErrorMessage = "Platform cannot be greater than 100 characters in length.")]
        [DisplayName("Platform")]
        public string Platform
        {
            get => _core.Platform;
            set
            {
                if (_core.Platform == value)
                    return;

                _core.Platform = value;
            }
        }

        public string Platform_ErrorMessage { get; set; }
        public bool Platform_IsInValid { get; set; }
        #endregion

        #region Session Tracker
        [StringLength(255,
                      MinimumLength = 0,
                      ErrorMessage = "Session Tracker cannot be greater than 255 characters in length.")]
        [DisplayName("Session Tracker")]
        public string SessionTracker
        {
            get => _core.SessionTracker;
            set
            {
                if (_core.SessionTracker == value)
                    return;

                _core.SessionTracker = value;
            }
        }

        public string SessionTracker_ErrorMessage { get; set; }
        public bool SessionTracker_IsInValid { get; set; }
        #endregion

        #region Time Zone
        [StringLength(100,
                      MinimumLength = 0,
                      ErrorMessage = "Time Zone cannot be greater than 100 characters in length.")]
        [DisplayName("Time Zone")]
        public string TimeZone
        {
            get => _core.TimeZone;
            set
            {
                if (_core.TimeZone == value)
                    return;

                _core.TimeZone = value;
            }
        }

        public string TimeZone_ErrorMessage { get; set; }
        public bool TimeZone_IsInValid { get; set; }
        #endregion

        #region URL
        [Required]
        [StringLength(255,
                      ErrorMessage = "URL must be between 1 and 255 characters in length.")]
        [DisplayName("URL")]
        public string URL
        {
            get => _core.URL;
            set
            {
                if (_core.URL == value)
                    return;

                _core.URL = value;
            }
        }

        public string URL_ErrorMessage { get; set; } = "Required";
        public bool URL_IsInValid { get; set; }
        #endregion

        #region Browser
        [StringLength(256,
                      MinimumLength = 0,
                      ErrorMessage = "Browser cannot be greater than 256 characters in length.")]
        [DisplayName("Browser")]
        public string UserAgent
        {
            get => _core.UserAgent;
            set
            {
                if (_core.UserAgent == value)
                    return;

                _core.UserAgent = value;
            }
        }

        public string UserAgent_ErrorMessage { get; set; }
        public bool UserAgent_IsInValid { get; set; }
        #endregion

        #region Colour
        [StringLength(255,
                      MinimumLength = 0,
                      ErrorMessage = "Colour cannot be greater than 255 characters in length.")]
        [DisplayName("Colour")]
        public string UserColour
        {
            get => _core.UserColour;
            set
            {
                if (_core.UserColour == value)
                    return;

                _core.UserColour = value;
            }
        }

        public string UserColour_ErrorMessage { get; set; }
        public bool UserColour_IsInValid { get; set; }
        #endregion

        #region User Tracker
        [StringLength(255,
                      MinimumLength = 0,
                      ErrorMessage = "User Tracker cannot be greater than 255 characters in length.")]
        [DisplayName("User Tracker")]
        public string UserTracker
        {
            get => _core.UserTracker;
            set
            {
                if (_core.UserTracker == value)
                    return;

                _core.UserTracker = value;
            }
        }

        public string UserTracker_ErrorMessage { get; set; }
        public bool UserTracker_IsInValid { get; set; }
        #endregion

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
                        _core.User = null;

                    _core.UserId = _user.Id;
                }
            }
        }

        [DisplayName("User")]
        public string UserString => User?.ToString();

        public string UserId_ErrorMessage { get; set; }
        public bool UserId_IsInValid { get; set; }

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


        public static List<Models.Analytic> Create(IEnumerable<Data.Core.Domain.Analytic> cores)
        {
            return cores.Select(core => new Models.Analytic(core)).ToList();
        }
        
        public static Models.Analytic[] Create(Data.Core.Domain.Analytic[] cores)
        {
            return cores.Select(core => new Models.Analytic(core)).ToArray();
        }
    }
}