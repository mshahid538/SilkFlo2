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

namespace SilkFlo.Web.Models.Application
{
    public partial class Page : Abstract
    {
        private SilkFlo.Data.Core.Domain.Application.Page _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public Page ()
        {
            _core = new SilkFlo.Data.Core.Domain.Application.Page();
        }
        public Page(SilkFlo.Data.Core.Domain.Application.Page core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.Application.Page cannot be null");
        }
        #endregion

        #region Properties
        private string _displayText;
        public string DisplayText
        { 
            get => string.IsNullOrWhiteSpace(_displayText) ? Name : _displayText;
            set => _displayText = value;
        }
        public SilkFlo.Data.Core.Domain.Application.Page GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.Application.Page core)
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

        #region Can Delete
        [Required]
        [DisplayName("Can Delete")]
        public bool CanDelete
        {
            get => _core.CanDelete;
            set
            {
                if (_core.CanDelete == value)
                    return;

                _core.CanDelete = value;
            }
        }

        public string CanDelete_ErrorMessage { get; set; } = "Required";
        public bool CanDelete_IsInValid { get; set; }
        #endregion

        #region Is Menu Item
        [Required]
        [DisplayName("Is Menu Item")]
        public bool IsMenuItem
        {
            get => _core.IsMenuItem;
            set
            {
                if (_core.IsMenuItem == value)
                    return;

                _core.IsMenuItem = value;
            }
        }

        public string IsMenuItem_ErrorMessage { get; set; } = "Required";
        public bool IsMenuItem_IsInValid { get; set; }
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

        #region Sort
        [Required]
        [DisplayName("Sort")]
        public int Sort
        {
            get => _core.Sort;
            set
            {
                if (_core.Sort == value)
                    return;

                _core.Sort = value;
            }
        }

        public string Sort_ErrorMessage { get; set; } = "Required";
        public bool Sort_IsInValid { get; set; }
        #endregion

        #region Text (HTML)
        [Required]
        [StringLength(2147483647,
                      ErrorMessage = "Text (HTML) must be between 1 and 2147483647 characters in length.")]
        [DisplayName("Text (HTML)")]
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

        #region Text Height
        [DisplayName("Text Height")]
        public decimal? TextHeight
        {
            get => _core.TextHeight;
            set
            {
                if (_core.TextHeight == value)
                    return;

                _core.TextHeight = value;
            }
        }

        public string TextHeight_ErrorMessage { get; set; }
        public bool TextHeight_IsInValid { get; set; }
        #endregion

        #region URL
        [Required]
        [StringLength(256,
                      ErrorMessage = "URL must be between 1 and 256 characters in length.")]
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
        public static List<Models.Application.Page> Create(IEnumerable<Data.Core.Domain.Application.Page> cores, bool includeEmpty = false)
        {
            if (cores == null)
                return null;

            if(includeEmpty)
            {
                var models = new List<Models.Application.Page>();
                models.Add(new Models.Application.Page
                {
                    DisplayText = "<Empty>"
                });

                models.AddRange(cores.Select(core => new Models.Application.Page(core)));
                return models;
            }

            return cores.Select(core => new Models.Application.Page(core)).ToList();
        }

        public static Models.Application.Page[] Create(Data.Core.Domain.Application.Page[] cores, bool includeEmpty = false)
        {
            if (cores == null)
                return null;

            if(includeEmpty)
                return Create(cores.ToList(), true).ToArray();

            return cores.Select(core => new Models.Application.Page(core)).ToArray();
        }
    }
}