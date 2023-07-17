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

* GetBadge()
  Two overrides to get the parent badge for the object.
  This is used to display parent in a summary table.

* GetBadges()
  Two overrides to get a list of alternative parent badges.

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
    public partial class UserBadge : Abstract
    {
        private SilkFlo.Data.Core.Domain.UserBadge _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public UserBadge ()
        {
            _core = new SilkFlo.Data.Core.Domain.UserBadge();
        }
        public UserBadge(SilkFlo.Data.Core.Domain.UserBadge core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.UserBadge cannot be null");
        }
        #endregion

        #region Properties
        public string DisplayText { get; set; }
        public SilkFlo.Data.Core.Domain.UserBadge GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.UserBadge core)
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
        [DisplayName("Badge")]
        public string BadgeId
        {
            get => _core.BadgeId;
            set
            {
                if (_core.BadgeId == value)
                    return;

                _core.BadgeId = value;
                _badge = null;
            }
        }
        private Models.Shared.Badge _badge;
        public Models.Shared.Badge Badge
        {
            get
            {
                if (_badge != null)
                    return _badge;

                if (_core.Badge != null)
                    _badge = new Models.Shared.Badge(_core.Badge);

                return _badge;
            }
            set
            {
                if (_badge == value)
                    return;

                _badge = value;

                if (_badge == null)
                    _core.Badge = null;
                else
                {
                    if (_core.BadgeId != _badge.Id)
                        _core.Badge = _badge.GetCore();

                    _core.BadgeId = _badge.Id;
                }
            }
        }

        [DisplayName("Badge")]
        public string BadgeString => Badge?.ToString();

        public string BadgeId_ErrorMessage { get; set; } = "Required";
        public bool BadgeId_IsInValid { get; set; }

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

        private List<Models.Shared.Badge> _badges;
        public List<Models.Shared.Badge> Badges
        {
            get => _badges ??= new List<Models.Shared.Badge>();
            set => _badges = value;
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


        public static List<Models.UserBadge> Create(IEnumerable<Data.Core.Domain.UserBadge> cores)
        {
            return cores.Select(core => new Models.UserBadge(core)).ToList();
        }
        
        public static Models.UserBadge[] Create(Data.Core.Domain.UserBadge[] cores)
        {
            return cores.Select(core => new Models.UserBadge(core)).ToArray();
        }
    }
}