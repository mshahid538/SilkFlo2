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

* GetAutomationType()
  Two overrides to get the parent automationType for the object.
  This is used to display parent in a summary table.

* GetAutomationTypes()
  Two overrides to get a list of alternative parent automationTypes.

* GetClient()
  Two overrides to get the parent client for the object.
  This is used to display parent in a summary table.

* GetClients()
  Two overrides to get a list of alternative parent clients.

* GetFrequency()
  Two overrides to get the parent frequency for the object.
  This is used to display parent in a summary table.



* GetVender()
  Two overrides to get the parent vender for the object.
  This is used to display parent in a summary table.

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
    public partial class RunningCost : Abstract
    {
        private SilkFlo.Data.Core.Domain.Business.RunningCost _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public RunningCost ()
        {
            _core = new SilkFlo.Data.Core.Domain.Business.RunningCost();
        }
        public RunningCost(SilkFlo.Data.Core.Domain.Business.RunningCost core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.Business.RunningCost cannot be null");
        }
        #endregion

        #region Properties
        public string DisplayText { get; set; }
        public SilkFlo.Data.Core.Domain.Business.RunningCost GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.Business.RunningCost core)
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

        #region Cost / Unit
        [Required]
        [DisplayName("Cost / Unit")]
        public Decimal Cost
        {
            get => _core.Cost;
            set
            {
                if (_core.Cost == value)
                    return;

                _core.Cost = value;
            }
        }

        public string Cost_ErrorMessage { get; set; } = "Required";
        public bool Cost_IsInValid { get; set; }
        #endregion

        #region Live
        [Required]
        [DisplayName("Live")]
        public bool IsLive
        {
            get => _core.IsLive;
            set
            {
                if (_core.IsLive == value)
                    return;

                _core.IsLive = value;
            }
        }

        public string IsLive_ErrorMessage { get; set; } = "Required";
        public bool IsLive_IsInValid { get; set; }
        #endregion

        #region Licence Type
        [Required]
        [StringLength(100,
                      ErrorMessage = "Licence Type must be between 1 and 100 characters in length.")]
        [DisplayName("Licence Type")]
        public string LicenceType
        {
            get => _core.LicenceType;
            set
            {
                if (_core.LicenceType == value)
                    return;

                _core.LicenceType = value;
            }
        }

        public string LicenceType_ErrorMessage { get; set; } = "Required";
        public bool LicenceType_IsInValid { get; set; }
        #endregion

        [Required]
        [DisplayName("Automation Type")]
        public string AutomationTypeId
        {
            get => _core.AutomationTypeId;
            set
            {
                if (_core.AutomationTypeId == value)
                    return;

                _core.AutomationTypeId = value;
                _automationType = null;
            }
        }
        private Models.Shared.AutomationType _automationType;
        public Models.Shared.AutomationType AutomationType
        {
            get
            {
                if (_automationType != null)
                    return _automationType;

                if (_core.AutomationType != null)
                    _automationType = new Models.Shared.AutomationType(_core.AutomationType);

                return _automationType;
            }
            set
            {
                if (_automationType == value)
                    return;

                _automationType = value;

                if (_automationType == null)
                    _core.AutomationType = null;
                else
                {
                    if (_core.AutomationTypeId != _automationType.Id)
                        _core.AutomationType = _automationType.GetCore();

                    _core.AutomationTypeId = _automationType.Id;
                }
            }
        }

        [DisplayName("Automation Type")]
        public string AutomationTypeString => AutomationType?.ToString();

        public string AutomationTypeId_ErrorMessage { get; set; } = "Required";
        public bool AutomationTypeId_IsInValid { get; set; }

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
        [DisplayName("Frequency")]
        public string FrequencyId
        {
            get => _core.FrequencyId;
            set
            {
                if (_core.FrequencyId == value)
                    return;

                _core.FrequencyId = value;
                _frequency = null;
            }
        }
        private Models.Shared.Period _frequency;
        public Models.Shared.Period Frequency
        {
            get
            {
                if (_frequency != null)
                    return _frequency;

                if (_core.Frequency != null)
                    _frequency = new Models.Shared.Period(_core.Frequency);

                return _frequency;
            }
            set
            {
                if (_frequency == value)
                    return;

                _frequency = value;

                if (_frequency == null)
                    _core.Frequency = null;
                else
                {
                    if (_core.FrequencyId != _frequency.Id)
                        _core.Frequency = _frequency.GetCore();

                    _core.FrequencyId = _frequency.Id;
                }
            }
        }

        [DisplayName("Frequency")]
        public string FrequencyString => Frequency?.ToString();

        public string FrequencyId_ErrorMessage { get; set; } = "Required";
        public bool FrequencyId_IsInValid { get; set; }

        [Required]
        [DisplayName("Software Vender")]
        public string VenderId
        {
            get => _core.VenderId;
            set
            {
                if (_core.VenderId == value)
                    return;

                _core.VenderId = value;
                _vender = null;
            }
        }
        private Models.Business.SoftwareVender _vender;
        public Models.Business.SoftwareVender Vender
        {
            get
            {
                if (_vender != null)
                    return _vender;

                if (_core.Vender != null)
                    _vender = new Models.Business.SoftwareVender(_core.Vender);

                return _vender;
            }
            set
            {
                if (_vender == value)
                    return;

                _vender = value;

                if (_vender == null)
                    _core.Vender = null;
                else
                {
                    if (_core.VenderId != _vender.Id)
                        _core.Vender = _vender.GetCore();

                    _core.VenderId = _vender.Id;
                }
            }
        }

        [DisplayName("Software Vender")]
        public string VenderString => Vender?.ToString();

        public string VenderId_ErrorMessage { get; set; } = "Required";
        public bool VenderId_IsInValid { get; set; }

        private List<Models.Shared.AutomationType> _automationTypes;
        public List<Models.Shared.AutomationType> AutomationTypes
        {
            get => _automationTypes ??= new List<Models.Shared.AutomationType>();
            set => _automationTypes = value;
        }

        private List<Models.Business.Client> _clients;
        public List<Models.Business.Client> Clients
        {
            get => _clients ??= new List<Models.Business.Client>();
            set => _clients = value;
        }

        private List<Models.Shared.Period> _frequencies;
        public List<Models.Shared.Period> Frequencies
        {
            get => _frequencies ??= new List<Models.Shared.Period>();
            set => _frequencies = value;
        }

        private List<Models.Business.SoftwareVender> _venders;
        public List<Models.Business.SoftwareVender> Venders
        {
            get => _venders ??= new List<Models.Business.SoftwareVender>();
            set => _venders = value;
        }

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

        private List<Models.Business.IdeaRunningCost> _ideaRunningCosts; 
        public List<Models.Business.IdeaRunningCost> IdeaRunningCosts 
        {
            get
            {
                if (_ideaRunningCosts != null)
                    return _ideaRunningCosts;

                _ideaRunningCosts = new List<Business.IdeaRunningCost>();

                if (_core.IdeaRunningCosts == null)
                    return _ideaRunningCosts;

                foreach (var core in _core.IdeaRunningCosts)
                    _ideaRunningCosts.Add(new Models.Business.IdeaRunningCost(core));

                return _ideaRunningCosts;
            }
            set => _ideaRunningCosts = value;
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
            if (feedback.Elements.ContainsKey("AutomationTypeId"))
                feedback.Elements["AutomationTypeId"] = message;
            else
                feedback.Elements.Add("AutomationTypeId", message);

            if (feedback.Elements.ContainsKey("ClientId"))
                feedback.Elements["ClientId"] = message;
            else
                feedback.Elements.Add("ClientId", message);

            if (feedback.Elements.ContainsKey("VenderId"))
                feedback.Elements["VenderId"] = message;
            else
                feedback.Elements.Add("VenderId", message);


            feedback.IsValid = false;

            return feedback;
        }

        public static List<Models.Business.RunningCost> Create(IEnumerable<Data.Core.Domain.Business.RunningCost> cores)
        {
            return cores.Select(core => new Models.Business.RunningCost(core)).ToList();
        }
        
        public static Models.Business.RunningCost[] Create(Data.Core.Domain.Business.RunningCost[] cores)
        {
            return cores.Select(core => new Models.Business.RunningCost(core)).ToArray();
        }
    }
}