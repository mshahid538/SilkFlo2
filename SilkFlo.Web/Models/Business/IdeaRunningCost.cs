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

* GetIdea()
  Two overrides to get the parent idea for the object.
  This is used to display parent in a summary table.



* GetRunningCost()
  Two overrides to get the parent runningCost for the object.
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
    public partial class IdeaRunningCost : Abstract
    {
        private SilkFlo.Data.Core.Domain.Business.IdeaRunningCost _core;
        #region Constructors
        
        // This constructor is used by HTTP posts 
        public IdeaRunningCost ()
        {
            _core = new SilkFlo.Data.Core.Domain.Business.IdeaRunningCost();
        }
        public IdeaRunningCost(SilkFlo.Data.Core.Domain.Business.IdeaRunningCost core)
        {
            _core = core ??   
	        throw new NullReferenceException("The SilkFlo.Data.Core.Domain.Business.IdeaRunningCost cannot be null");
        }
        #endregion

        #region Properties
        public string DisplayText { get; set; }
        public SilkFlo.Data.Core.Domain.Business.IdeaRunningCost GetCore()
        {
            return _core;
        }
        public void SetCore(SilkFlo.Data.Core.Domain.Business.IdeaRunningCost core)
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

        #region LicenceCount
        [Required]
        [DisplayName("LicenceCount")]
        public int LicenceCount
        {
            get => _core.LicenceCount;
            set
            {
                if (_core.LicenceCount == value)
                    return;

                _core.LicenceCount = value;
            }
        }

        public string LicenceCount_ErrorMessage { get; set; } = "Required";
        public bool LicenceCount_IsInValid { get; set; }
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
        [DisplayName("Idea")]
        public string IdeaId
        {
            get => _core.IdeaId;
            set
            {
                if (_core.IdeaId == value)
                    return;

                _core.IdeaId = value;
                _idea = null;
            }
        }
        private Models.Business.Idea _idea;
        public Models.Business.Idea Idea
        {
            get
            {
                if (_idea != null)
                    return _idea;

                if (_core.Idea != null)
                    _idea = new Models.Business.Idea(_core.Idea);

                return _idea;
            }
            set
            {
                if (_idea == value)
                    return;

                _idea = value;

                if (_idea == null)
                    _core.Idea = null;
                else
                {
                    if (_core.IdeaId != _idea.Id)
                        _core.Idea = _idea.GetCore();

                    _core.IdeaId = _idea.Id;
                }
            }
        }

        [DisplayName("Idea")]
        public string IdeaString => Idea?.ToString();

        public string IdeaId_ErrorMessage { get; set; } = "Required";
        public bool IdeaId_IsInValid { get; set; }

        [Required]
        [DisplayName("Running Cost")]
        public string RunningCostId
        {
            get => _core.RunningCostId;
            set
            {
                if (_core.RunningCostId == value)
                    return;

                _core.RunningCostId = value;
                _runningCost = null;
            }
        }
        private Models.Business.RunningCost _runningCost;
        public Models.Business.RunningCost RunningCost
        {
            get
            {
                if (_runningCost != null)
                    return _runningCost;

                if (_core.RunningCost != null)
                    _runningCost = new Models.Business.RunningCost(_core.RunningCost);

                return _runningCost;
            }
            set
            {
                if (_runningCost == value)
                    return;

                _runningCost = value;

                if (_runningCost == null)
                    _core.RunningCost = null;
                else
                {
                    if (_core.RunningCostId != _runningCost.Id)
                        _core.RunningCost = _runningCost.GetCore();

                    _core.RunningCostId = _runningCost.Id;
                }
            }
        }

        [DisplayName("Running Cost")]
        public string RunningCostString => RunningCost?.ToString();

        public string RunningCostId_ErrorMessage { get; set; } = "Required";
        public bool RunningCostId_IsInValid { get; set; }

        private List<Models.Business.Client> _clients;
        public List<Models.Business.Client> Clients
        {
            get => _clients ??= new List<Models.Business.Client>();
            set => _clients = value;
        }

        private List<Models.Business.Idea> _ideas;
        public List<Models.Business.Idea> Ideas
        {
            get => _ideas ??= new List<Models.Business.Idea>();
            set => _ideas = value;
        }

        private List<Models.Business.RunningCost> _runningCosts;
        public List<Models.Business.RunningCost> RunningCosts
        {
            get => _runningCosts ??= new List<Models.Business.RunningCost>();
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
            var message = await Data.Persistence.UnitOfWork.IsUniqueAsync(GetCore());

            if (string.IsNullOrWhiteSpace(message)) 
                return feedback;


             // We have a conflict, give feedback.
            if (feedback.Elements.ContainsKey("ClientId"))
                feedback.Elements["ClientId"] = message;
            else
                feedback.Elements.Add("ClientId", message);

            if (feedback.Elements.ContainsKey("IdeaId"))
                feedback.Elements["IdeaId"] = message;
            else
                feedback.Elements.Add("IdeaId", message);

            if (feedback.Elements.ContainsKey("RunningCostId"))
                feedback.Elements["RunningCostId"] = message;
            else
                feedback.Elements.Add("RunningCostId", message);


            feedback.IsValid = false;

            return feedback;
        }

        public static List<Models.Business.IdeaRunningCost> Create(IEnumerable<Data.Core.Domain.Business.IdeaRunningCost> cores)
        {
            return cores.Select(core => new Models.Business.IdeaRunningCost(core)).ToList();
        }
        
        public static Models.Business.IdeaRunningCost[] Create(Data.Core.Domain.Business.IdeaRunningCost[] cores)
        {
            return cores.Select(core => new Models.Business.IdeaRunningCost(core)).ToArray();
        }
    }
}