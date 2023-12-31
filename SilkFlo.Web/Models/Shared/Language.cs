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

* GetClients()
  Two overrides to get Clients children for this object.

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

namespace SilkFlo.Web.Models.Shared
{
    public partial class Language : Abstract
    {
        private SilkFlo.Data.Core.Domain.Shared.Language _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public Language ()
        {
            _core = new SilkFlo.Data.Core.Domain.Shared.Language();
        }
        public Language(SilkFlo.Data.Core.Domain.Shared.Language core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.Shared.Language cannot be null");
        }
        #endregion

        #region Properties
        private string _displayText;
        public string DisplayText
        { 
            get => string.IsNullOrWhiteSpace(_displayText) ? Name : _displayText;
            set => _displayText = value;
        }
        public SilkFlo.Data.Core.Domain.Shared.Language GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.Shared.Language core)
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

        #region Locale
        [Required]
        [StringLength(10,
                      ErrorMessage = "Locale must be between 1 and 10 characters in length.")]
        [DisplayName("Locale")]
        public string Locale
        {
            get => _core.Locale;
            set
            {
                if (_core.Locale == value)
                    return;

                _core.Locale = value;
            }
        }

        public string Locale_ErrorMessage { get; set; } = "Required";
        public bool Locale_IsInValid { get; set; }
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

        private List<Models.Business.Client> _clients; 
        public List<Models.Business.Client> Clients 
        {
            get
            {
                if (_clients != null)
                    return _clients;

                _clients = new List<Business.Client>();

                if (_core.Clients == null)
                    return _clients;

                foreach (var core in _core.Clients)
                    _clients.Add(new Models.Business.Client(core));

                return _clients;
            }
            set => _clients = value;
        }

        private List<Models.Business.IdeaApplicationVersion> _ideaApplicationVersions; 
        public List<Models.Business.IdeaApplicationVersion> IdeaApplicationVersions 
        {
            get
            {
                if (_ideaApplicationVersions != null)
                    return _ideaApplicationVersions;

                _ideaApplicationVersions = new List<Business.IdeaApplicationVersion>();

                if (_core.IdeaApplicationVersions == null)
                    return _ideaApplicationVersions;

                foreach (var core in _core.IdeaApplicationVersions)
                    _ideaApplicationVersions.Add(new Models.Business.IdeaApplicationVersion(core));

                return _ideaApplicationVersions;
            }
            set => _ideaApplicationVersions = value;
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

            return Name;
        }
        public static List<Models.Shared.Language> Create(IEnumerable<Data.Core.Domain.Shared.Language> cores, bool includeEmpty = false)
        {
            if (cores == null)
                return null;

            if(includeEmpty)
            {
                var models = new List<Models.Shared.Language>();
                models.Add(new Models.Shared.Language
                {
                    DisplayText = "<Empty>"
                });

                models.AddRange(cores.Select(core => new Models.Shared.Language(core)));
                return models;
            }

            return cores.Select(core => new Models.Shared.Language(core)).ToList();
        }

        public static Models.Shared.Language[] Create(Data.Core.Domain.Shared.Language[] cores, bool includeEmpty = false)
        {
            if (cores == null)
                return null;

            if(includeEmpty)
                return Create(cores.ToList(), true).ToArray();

            return cores.Select(core => new Models.Shared.Language(core)).ToArray();
        }
    }
}