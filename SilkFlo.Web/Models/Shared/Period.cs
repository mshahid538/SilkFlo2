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
    public partial class Period : Abstract
    {
        private SilkFlo.Data.Core.Domain.Shared.Period _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public Period ()
        {
            _core = new SilkFlo.Data.Core.Domain.Shared.Period();
        }
        public Period(SilkFlo.Data.Core.Domain.Shared.Period core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.Shared.Period cannot be null");
        }
        #endregion

        #region Properties
        private string _displayText;
        public string DisplayText
        { 
            get => string.IsNullOrWhiteSpace(_displayText) ? Name : _displayText;
            set => _displayText = value;
        }
        public SilkFlo.Data.Core.Domain.Shared.Period GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.Shared.Period core)
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

        #region Cancel Period in Days
        [Required]
        [DisplayName("Cancel Period in Days")]
        public int CancelPeriodInDay
        {
            get => _core.CancelPeriodInDay;
            set
            {
                if (_core.CancelPeriodInDay == value)
                    return;

                _core.CancelPeriodInDay = value;
            }
        }

        public string CancelPeriodInDay_ErrorMessage { get; set; } = "Required";
        public bool CancelPeriodInDay_IsInValid { get; set; }
        #endregion

        #region Cancel Period in Days No Renew
        [Required]
        [DisplayName("Cancel Period in Days No Renew")]
        public int CancelPeriodInDayNoRenew
        {
            get => _core.CancelPeriodInDayNoRenew;
            set
            {
                if (_core.CancelPeriodInDayNoRenew == value)
                    return;

                _core.CancelPeriodInDayNoRenew = value;
            }
        }

        public string CancelPeriodInDayNoRenew_ErrorMessage { get; set; } = "Required";
        public bool CancelPeriodInDayNoRenew_IsInValid { get; set; }
        #endregion

        #region Month Count
        [Required]
        [DisplayName("Month Count")]
        public int MonthCount
        {
            get => _core.MonthCount;
            set
            {
                if (_core.MonthCount == value)
                    return;

                _core.MonthCount = value;
            }
        }

        public string MonthCount_ErrorMessage { get; set; } = "Required";
        public bool MonthCount_IsInValid { get; set; }
        #endregion

        #region Name
        [Required]
        [StringLength(255,
                      ErrorMessage = "Name must be between 1 and 255 characters in length.")]
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

        #region Name Plural
        [Required]
        [StringLength(50,
                      ErrorMessage = "Name Plural must be between 1 and 50 characters in length.")]
        [DisplayName("Name Plural")]
        public string NamePlural
        {
            get => _core.NamePlural;
            set
            {
                if (_core.NamePlural == value)
                    return;

                _core.NamePlural = value;
            }
        }

        public string NamePlural_ErrorMessage { get; set; } = "Required";
        public bool NamePlural_IsInValid { get; set; }
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

        private List<Models.Business.OtherRunningCost> _otherRunningCosts; 
        public List<Models.Business.OtherRunningCost> OtherRunningCosts 
        {
            get
            {
                if (_otherRunningCosts != null)
                    return _otherRunningCosts;

                _otherRunningCosts = new List<Business.OtherRunningCost>();

                if (_core.OtherRunningCosts == null)
                    return _otherRunningCosts;

                foreach (var core in _core.OtherRunningCosts)
                    _otherRunningCosts.Add(new Models.Business.OtherRunningCost(core));

                return _otherRunningCosts;
            }
            set => _otherRunningCosts = value;
        }

        private List<Models.Shop.Price> _prices; 
        public List<Models.Shop.Price> Prices 
        {
            get
            {
                if (_prices != null)
                    return _prices;

                _prices = new List<Shop.Price>();

                if (_core.Prices == null)
                    return _prices;

                foreach (var core in _core.Prices)
                    _prices.Add(new Models.Shop.Price(core));

                return _prices;
            }
            set => _prices = value;
        }

        private List<Models.Business.RunningCost> _runningCosts; 
        public List<Models.Business.RunningCost> RunningCosts 
        {
            get
            {
                if (_runningCosts != null)
                    return _runningCosts;

                _runningCosts = new List<Business.RunningCost>();

                if (_core.RunningCosts == null)
                    return _runningCosts;

                foreach (var core in _core.RunningCosts)
                    _runningCosts.Add(new Models.Business.RunningCost(core));

                return _runningCosts;
            }
            set => _runningCosts = value;
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
            if (feedback.Elements.ContainsKey("Name"))
                feedback.Elements["Name"] = message;
            else
                feedback.Elements.Add("Name", message);

            if (feedback.Elements.ContainsKey("NamePlural"))
                feedback.Elements["NamePlural"] = message;
            else
                feedback.Elements.Add("NamePlural", message);


            feedback.IsValid = false;

            return feedback;
        }

        public override string ToString()
        {
            if(!string.IsNullOrWhiteSpace(DisplayText))
                return DisplayText;

            return Name;
        }
        public static List<Models.Shared.Period> Create(IEnumerable<Data.Core.Domain.Shared.Period> cores, bool includeEmpty = false)
        {
            if (cores == null)
                return null;

            if(includeEmpty)
            {
                var models = new List<Models.Shared.Period>();
                models.Add(new Models.Shared.Period
                {
                    DisplayText = "<Empty>"
                });

                models.AddRange(cores.Select(core => new Models.Shared.Period(core)));
                return models;
            }

            return cores.Select(core => new Models.Shared.Period(core)).ToList();
        }

        public static Models.Shared.Period[] Create(Data.Core.Domain.Shared.Period[] cores, bool includeEmpty = false)
        {
            if (cores == null)
                return null;

            if(includeEmpty)
                return Create(cores.ToList(), true).ToArray();

            return cores.Select(core => new Models.Shared.Period(core)).ToArray();
        }
    }
}