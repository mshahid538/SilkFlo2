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

namespace SilkFlo.Web.Models.Shared
{
    public partial class Input : Abstract
    {
        private SilkFlo.Data.Core.Domain.Shared.Input _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public Input ()
        {
            _core = new SilkFlo.Data.Core.Domain.Shared.Input();
        }
        public Input(SilkFlo.Data.Core.Domain.Shared.Input core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.Shared.Input cannot be null");
        }
        #endregion

        #region Properties
        private string _displayText;
        public string DisplayText
        { 
            get => string.IsNullOrWhiteSpace(_displayText) ? Name : _displayText;
            set => _displayText = value;
        }
        public SilkFlo.Data.Core.Domain.Shared.Input GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.Shared.Input core)
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

        #region Colour
        [Required]
        [StringLength(50,
                      ErrorMessage = "Colour must be between 1 and 50 characters in length.")]
        [DisplayName("Colour")]
        public string Colour
        {
            get => _core.Colour;
            set
            {
                if (_core.Colour == value)
                    return;

                _core.Colour = value;
            }
        }

        public string Colour_ErrorMessage { get; set; } = "Required";
        public bool Colour_IsInValid { get; set; }
        #endregion

        #region Name
        [Required]
        [StringLength(50,
                      ErrorMessage = "Name must be between 1 and 50 characters in length.")]
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

        #region Rating
        [Required]
        [DisplayName("Rating")]
        public int Rating
        {
            get => _core.Rating;
            set
            {
                if (_core.Rating == value)
                    return;

                _core.Rating = value;
            }
        }

        public string Rating_ErrorMessage { get; set; } = "Required";
        public bool Rating_IsInValid { get; set; }
        #endregion

        #region Weighting
        [Required]
        [DisplayName("Weighting")]
        public decimal Weighting
        {
            get => _core.Weighting;
            set
            {
                if (_core.Weighting == value)
                    return;

                _core.Weighting = value;
            }
        }

        public string Weighting_ErrorMessage { get; set; } = "Required";
        public bool Weighting_IsInValid { get; set; }
        #endregion

        private List<Models.Business.Idea> _ideas; 
        public List<Models.Business.Idea> Ideas 
        {
            get
            {
                if (_ideas != null)
                    return _ideas;

                _ideas = new List<Business.Idea>();

                if (_core.Ideas == null)
                    return _ideas;

                foreach (var core in _core.Ideas)
                    _ideas.Add(new Models.Business.Idea(core));

                return _ideas;
            }
            set => _ideas = value;
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
            var message = await Data.Persistence.UnitOfWork.IsUniqueAsync(GetCore());

            if (string.IsNullOrWhiteSpace(message)) 
                return feedback;


             // We have a conflict, give feedback.
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
        public static List<Models.Shared.Input> Create(IEnumerable<Data.Core.Domain.Shared.Input> cores, bool includeEmpty = false)
        {
            if (cores == null)
                return null;

            if(includeEmpty)
            {
                var models = new List<Models.Shared.Input>();
                models.Add(new Models.Shared.Input
                {
                    DisplayText = "<Empty>"
                });

                models.AddRange(cores.Select(core => new Models.Shared.Input(core)));
                return models;
            }

            return cores.Select(core => new Models.Shared.Input(core)).ToList();
        }

        public static Models.Shared.Input[] Create(Data.Core.Domain.Shared.Input[] cores, bool includeEmpty = false)
        {
            if (cores == null)
                return null;

            if(includeEmpty)
                return Create(cores.ToList(), true).ToArray();

            return cores.Select(core => new Models.Shared.Input(core)).ToArray();
        }
    }
}