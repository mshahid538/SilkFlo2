using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SilkFlo.Data.Core.Domain.Business
{
  [XmlType(Namespace = "Business")]
  public class Idea : Abstract
  {
    private string _id = "";
    private int? _activityVolumeAverage;
    private string _activityVolumeAverageComment = "";
    private Decimal? _aHTRobot;
    private string _applicationStabilityComment = "";
    private ApplicationStability _applicationStability;
    private string _applicationStabilityId;
    private AutomationGoal _automationGoal;
    private string _automationGoalId;
    private int? _averageEmployeeFullCost;
    private string _averageEmployeeFullCostComment = "";
    private int? _averageErrorRate;
    private string _averageErrorRateComment = "";
    private string _averageNumberOfStepComment = "";
    private AverageNumberOfStep _averageNumberOfStep;
    private string _averageNumberOfStepId;
    private Decimal? _averageProcessingTime;
    private string _averageProcessingTimeComment = "";
    private Decimal? _averageReviewTime;
    private string _averageReviewTimeComment = "";
    private Decimal? _averageReworkTime;
    private string _averageReworkTimeComment = "";
    private int? _averageWorkingDay;
    private string _averageWorkingDayComment = "";
    private Decimal? _averageWorkToBeReviewed;
    private string _averageWorkToBeReviewedComment = "";
    private string _benefitActual = "";
    private string _benefitExpected = "";
    private string _challengeActual = "";
    private string _challengeExpected = "";
    private Client _client;
    private string _clientId;
    private string _dataInputPercentOfStructuredComment = "";
    private DataInputPercentOfStructured _dataInputPercentOfStructured;
    private string _dataInputPercentOfStructuredId;
    private string _dataInputScannedComment = "";
    private string _decisionCountComment = "";
    private DecisionCount _decisionCount;
    private string _decisionCountId;
    private string _decisionDifficultyComment = "";
    private DecisionDifficulty _decisionDifficulty;
    private string _decisionDifficultyId;
    private Department _department;
    private string _departmentId;
    private string _documentationPresentComment = "";
    private DocumentationPresent _documentationPresent;
    private string _documentationPresentId;
    private string _easeOfImplementationFinal = "";
    private int? _employeeCount;
    private string _employeeCountComment = "";
    private string _inputComment = "";
    private InputDataStructure _inputDataStructure;
    private string _inputDataStructureId;
    private Input _input;
    private string _inputId;
    private bool _isAlternative;
    private bool _isDataInputScanned;
    private bool _isDataSensitive;
    private bool _isDraft;
    private bool _isHighRisk;
    private bool _isHostUpgrade;
    private string _lessenLearnt = "";
    private string _name = "";
    private string _negativeImpactComment = "";
    private string _numberOfWaysToCompleteProcessComment = "";
    private NumberOfWaysToCompleteProcess _numberOfWaysToCompleteProcess;
    private string _numberOfWaysToCompleteProcessId;
    private string _painPointComment = "";
    private Decimal? _potentialFineAmount;
    private Decimal? _potentialFineProbability;
    private Process _process;
    private string _processId;
    private User _processOwner;
    private string _processOwnerId;
    private string _processPeakComment = "";
    private ProcessPeak _processPeak;
    private string _processPeakId;
    private string _processStabilityComment = "";
    private ProcessStability _processStability;
    private string _processStabilityId;
    private Decimal? _processVolumetryPerMonth;
    private Decimal? _processVolumetryPerYear;
    private int? _rating;
    private string _ratingComment = "";
    private Decimal? _robotSpeedMultiplier;
    private Decimal? _robotWorkDayYear;
    private Decimal? _robotWorkHourDay;
    private string _ruleComment = "";
    private Rule _rule;
    private string _ruleId;
    private RunningCost _runningCost;
    private string _runningCostId;
    private string _structureComment = "";
    private SubmissionPath _submissionPath;
    private string _submissionPathId;
    private string _subTitle = "";
    private string _summary = "";
    private string _taskFrequencyComment = "";
    private TaskFrequency _taskFrequency;
    private string _taskFrequencyId;
    private Team _team;
    private string _teamId;
    private Decimal? _workingHour;
    private string _workingHourComment = "";
    private Decimal? _workloadSplit;

    public Idea() => this._createdDate = new System.DateTime?(System.DateTime.Now);

    public override bool IsNew => string.IsNullOrWhiteSpace(this.Id);

    public string Id
    {
      get => this._id;
      set
      {
        value = value?.Trim();
        if (this._id == value)
          return;
        this._id = value;
        this.IsSaved = false;
      }
    }

    public int? ActivityVolumeAverage
    {
      get => this._activityVolumeAverage;
      set
      {
        int? activityVolumeAverage = this._activityVolumeAverage;
        int? nullable = value;
        if (activityVolumeAverage.GetValueOrDefault() == nullable.GetValueOrDefault() & activityVolumeAverage.HasValue == nullable.HasValue)
          return;
        this._activityVolumeAverage = value;
        this.IsSaved = false;
      }
    }

    public string ActivityVolumeAverageComment
    {
      get => this._activityVolumeAverageComment;
      set
      {
        value = value?.Trim();
        if (this._activityVolumeAverageComment == value)
          return;
        this._activityVolumeAverageComment = value;
        this.IsSaved = false;
      }
    }

    public Decimal? AHTRobot
    {
      get => this._aHTRobot;
      set
      {
        Decimal? aHtRobot = this._aHTRobot;
        Decimal? nullable = value;
        if (aHtRobot.GetValueOrDefault() == nullable.GetValueOrDefault() & aHtRobot.HasValue == nullable.HasValue)
          return;
        this._aHTRobot = value;
        this.IsSaved = false;
      }
    }

    public string ApplicationStabilityComment
    {
      get => this._applicationStabilityComment;
      set
      {
        value = value?.Trim();
        if (this._applicationStabilityComment == value)
          return;
        this._applicationStabilityComment = value;
        this.IsSaved = false;
      }
    }

    public string ApplicationStabilityString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public ApplicationStability ApplicationStability
    {
      get => this._applicationStability;
      set
      {
        if (this._applicationStability == value)
          return;
        this._applicationStability = value;
        this.ApplicationStabilityString = value == null ? "" : value.ToString();
      }
    }

    public string ApplicationStabilityId
    {
      get => this._applicationStability != null ? this._applicationStability.Id : this._applicationStabilityId;
      set
      {
        value = value?.Trim();
        this._applicationStabilityId = value;
        if (this._applicationStability != null && this._applicationStability.Id != this._applicationStabilityId)
          this._applicationStability = (ApplicationStability) null;
        this.IsSaved = false;
      }
    }

    public string AutomationGoalString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public AutomationGoal AutomationGoal
    {
      get => this._automationGoal;
      set
      {
        if (this._automationGoal == value)
          return;
        this._automationGoal = value;
        this.AutomationGoalString = value == null ? "" : value.ToString();
      }
    }

    public string AutomationGoalId
    {
      get => this._automationGoal != null ? this._automationGoal.Id : this._automationGoalId;
      set
      {
        value = value?.Trim();
        this._automationGoalId = value;
        if (this._automationGoal != null && this._automationGoal.Id != this._automationGoalId)
          this._automationGoal = (AutomationGoal) null;
        this.IsSaved = false;
      }
    }

    public int? AverageEmployeeFullCost
    {
      get => this._averageEmployeeFullCost;
      set
      {
        int? employeeFullCost = this._averageEmployeeFullCost;
        int? nullable = value;
        if (employeeFullCost.GetValueOrDefault() == nullable.GetValueOrDefault() & employeeFullCost.HasValue == nullable.HasValue)
          return;
        this._averageEmployeeFullCost = value;
        this.IsSaved = false;
      }
    }

    public string AverageEmployeeFullCostComment
    {
      get => this._averageEmployeeFullCostComment;
      set
      {
        value = value?.Trim();
        if (this._averageEmployeeFullCostComment == value)
          return;
        this._averageEmployeeFullCostComment = value;
        this.IsSaved = false;
      }
    }

    public int? AverageErrorRate
    {
      get => this._averageErrorRate;
      set
      {
        int? averageErrorRate = this._averageErrorRate;
        int? nullable = value;
        if (averageErrorRate.GetValueOrDefault() == nullable.GetValueOrDefault() & averageErrorRate.HasValue == nullable.HasValue)
          return;
        this._averageErrorRate = value;
        this.IsSaved = false;
      }
    }

    public string AverageErrorRateComment
    {
      get => this._averageErrorRateComment;
      set
      {
        value = value?.Trim();
        if (this._averageErrorRateComment == value)
          return;
        this._averageErrorRateComment = value;
        this.IsSaved = false;
      }
    }

    public string AverageNumberOfStepComment
    {
      get => this._averageNumberOfStepComment;
      set
      {
        value = value?.Trim();
        if (this._averageNumberOfStepComment == value)
          return;
        this._averageNumberOfStepComment = value;
        this.IsSaved = false;
      }
    }

    public string AverageNumberOfStepString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public AverageNumberOfStep AverageNumberOfStep
    {
      get => this._averageNumberOfStep;
      set
      {
        if (this._averageNumberOfStep == value)
          return;
        this._averageNumberOfStep = value;
        this.AverageNumberOfStepString = value == null ? "" : value.ToString();
      }
    }

    public string AverageNumberOfStepId
    {
      get => this._averageNumberOfStep != null ? this._averageNumberOfStep.Id : this._averageNumberOfStepId;
      set
      {
        value = value?.Trim();
        this._averageNumberOfStepId = value;
        if (this._averageNumberOfStep != null && this._averageNumberOfStep.Id != this._averageNumberOfStepId)
          this._averageNumberOfStep = (AverageNumberOfStep) null;
        this.IsSaved = false;
      }
    }

    public Decimal? AverageProcessingTime
    {
      get => this._averageProcessingTime;
      set
      {
        Decimal? averageProcessingTime = this._averageProcessingTime;
        Decimal? nullable = value;
        if (averageProcessingTime.GetValueOrDefault() == nullable.GetValueOrDefault() & averageProcessingTime.HasValue == nullable.HasValue)
          return;
        this._averageProcessingTime = value;
        this.IsSaved = false;
      }
    }

    public string AverageProcessingTimeComment
    {
      get => this._averageProcessingTimeComment;
      set
      {
        value = value?.Trim();
        if (this._averageProcessingTimeComment == value)
          return;
        this._averageProcessingTimeComment = value;
        this.IsSaved = false;
      }
    }

    public Decimal? AverageReviewTime
    {
      get => this._averageReviewTime;
      set
      {
        Decimal? averageReviewTime = this._averageReviewTime;
        Decimal? nullable = value;
        if (averageReviewTime.GetValueOrDefault() == nullable.GetValueOrDefault() & averageReviewTime.HasValue == nullable.HasValue)
          return;
        this._averageReviewTime = value;
        this.IsSaved = false;
      }
    }

    public string AverageReviewTimeComment
    {
      get => this._averageReviewTimeComment;
      set
      {
        value = value?.Trim();
        if (this._averageReviewTimeComment == value)
          return;
        this._averageReviewTimeComment = value;
        this.IsSaved = false;
      }
    }

    public Decimal? AverageReworkTime
    {
      get => this._averageReworkTime;
      set
      {
        Decimal? averageReworkTime = this._averageReworkTime;
        Decimal? nullable = value;
        if (averageReworkTime.GetValueOrDefault() == nullable.GetValueOrDefault() & averageReworkTime.HasValue == nullable.HasValue)
          return;
        this._averageReworkTime = value;
        this.IsSaved = false;
      }
    }

    public string AverageReworkTimeComment
    {
      get => this._averageReworkTimeComment;
      set
      {
        value = value?.Trim();
        if (this._averageReworkTimeComment == value)
          return;
        this._averageReworkTimeComment = value;
        this.IsSaved = false;
      }
    }

    public int? AverageWorkingDay
    {
      get => this._averageWorkingDay;
      set
      {
        int? averageWorkingDay = this._averageWorkingDay;
        int? nullable = value;
        if (averageWorkingDay.GetValueOrDefault() == nullable.GetValueOrDefault() & averageWorkingDay.HasValue == nullable.HasValue)
          return;
        this._averageWorkingDay = value;
        this.IsSaved = false;
      }
    }

    public string AverageWorkingDayComment
    {
      get => this._averageWorkingDayComment;
      set
      {
        value = value?.Trim();
        if (this._averageWorkingDayComment == value)
          return;
        this._averageWorkingDayComment = value;
        this.IsSaved = false;
      }
    }

    public Decimal? AverageWorkToBeReviewed
    {
      get => this._averageWorkToBeReviewed;
      set
      {
        Decimal? workToBeReviewed = this._averageWorkToBeReviewed;
        Decimal? nullable = value;
        if (workToBeReviewed.GetValueOrDefault() == nullable.GetValueOrDefault() & workToBeReviewed.HasValue == nullable.HasValue)
          return;
        this._averageWorkToBeReviewed = value;
        this.IsSaved = false;
      }
    }

    public string AverageWorkToBeReviewedComment
    {
      get => this._averageWorkToBeReviewedComment;
      set
      {
        value = value?.Trim();
        if (this._averageWorkToBeReviewedComment == value)
          return;
        this._averageWorkToBeReviewedComment = value;
        this.IsSaved = false;
      }
    }

    public string BenefitActual
    {
      get => this._benefitActual;
      set
      {
        value = value?.Trim();
        if (this._benefitActual == value)
          return;
        this._benefitActual = value;
        this.IsSaved = false;
      }
    }

    public string BenefitExpected
    {
      get => this._benefitExpected;
      set
      {
        value = value?.Trim();
        if (this._benefitExpected == value)
          return;
        this._benefitExpected = value;
        this.IsSaved = false;
      }
    }

    public string ChallengeActual
    {
      get => this._challengeActual;
      set
      {
        value = value?.Trim();
        if (this._challengeActual == value)
          return;
        this._challengeActual = value;
        this.IsSaved = false;
      }
    }

    public string ChallengeExpected
    {
      get => this._challengeExpected;
      set
      {
        value = value?.Trim();
        if (this._challengeExpected == value)
          return;
        this._challengeExpected = value;
        this.IsSaved = false;
      }
    }

    public string ClientString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Client Client
    {
      get => this._client;
      set
      {
        if (this._client == value)
          return;
        this._client = value;
        this.ClientString = value == null ? "" : value.ToString();
      }
    }

    public string ClientId
    {
      get => this._client != null ? this._client.Id : this._clientId;
      set
      {
        value = value?.Trim();
        this._clientId = value;
        if (this._client != null && this._client.Id != this._clientId)
          this._client = (Client) null;
        this.IsSaved = false;
      }
    }

    public string DataInputPercentOfStructuredComment
    {
      get => this._dataInputPercentOfStructuredComment;
      set
      {
        value = value?.Trim();
        if (this._dataInputPercentOfStructuredComment == value)
          return;
        this._dataInputPercentOfStructuredComment = value;
        this.IsSaved = false;
      }
    }

    public string DataInputPercentOfStructuredString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public DataInputPercentOfStructured DataInputPercentOfStructured
    {
      get => this._dataInputPercentOfStructured;
      set
      {
        if (this._dataInputPercentOfStructured == value)
          return;
        this._dataInputPercentOfStructured = value;
        this.DataInputPercentOfStructuredString = value == null ? "" : value.ToString();
      }
    }

    public string DataInputPercentOfStructuredId
    {
      get => this._dataInputPercentOfStructured != null ? this._dataInputPercentOfStructured.Id : this._dataInputPercentOfStructuredId;
      set
      {
        value = value?.Trim();
        this._dataInputPercentOfStructuredId = value;
        if (this._dataInputPercentOfStructured != null && this._dataInputPercentOfStructured.Id != this._dataInputPercentOfStructuredId)
          this._dataInputPercentOfStructured = (DataInputPercentOfStructured) null;
        this.IsSaved = false;
      }
    }

    public string DataInputScannedComment
    {
      get => this._dataInputScannedComment;
      set
      {
        value = value?.Trim();
        if (this._dataInputScannedComment == value)
          return;
        this._dataInputScannedComment = value;
        this.IsSaved = false;
      }
    }

    public string DecisionCountComment
    {
      get => this._decisionCountComment;
      set
      {
        value = value?.Trim();
        if (this._decisionCountComment == value)
          return;
        this._decisionCountComment = value;
        this.IsSaved = false;
      }
    }

    public string DecisionCountString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public DecisionCount DecisionCount
    {
      get => this._decisionCount;
      set
      {
        if (this._decisionCount == value)
          return;
        this._decisionCount = value;
        this.DecisionCountString = value == null ? "" : value.ToString();
      }
    }

    public string DecisionCountId
    {
      get => this._decisionCount != null ? this._decisionCount.Id : this._decisionCountId;
      set
      {
        value = value?.Trim();
        this._decisionCountId = value;
        if (this._decisionCount != null && this._decisionCount.Id != this._decisionCountId)
          this._decisionCount = (DecisionCount) null;
        this.IsSaved = false;
      }
    }

    public string DecisionDifficultyComment
    {
      get => this._decisionDifficultyComment;
      set
      {
        value = value?.Trim();
        if (this._decisionDifficultyComment == value)
          return;
        this._decisionDifficultyComment = value;
        this.IsSaved = false;
      }
    }

    public string DecisionDifficultyString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public DecisionDifficulty DecisionDifficulty
    {
      get => this._decisionDifficulty;
      set
      {
        if (this._decisionDifficulty == value)
          return;
        this._decisionDifficulty = value;
        this.DecisionDifficultyString = value == null ? "" : value.ToString();
      }
    }

    public string DecisionDifficultyId
    {
      get => this._decisionDifficulty != null ? this._decisionDifficulty.Id : this._decisionDifficultyId;
      set
      {
        value = value?.Trim();
        this._decisionDifficultyId = value;
        if (this._decisionDifficulty != null && this._decisionDifficulty.Id != this._decisionDifficultyId)
          this._decisionDifficulty = (DecisionDifficulty) null;
        this.IsSaved = false;
      }
    }

    public string DepartmentString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Department Department
    {
      get => this._department;
      set
      {
        if (this._department == value)
          return;
        this._department = value;
        this.DepartmentString = value == null ? "" : value.ToString();
      }
    }

    public string DepartmentId
    {
      get => this._department != null ? this._department.Id : this._departmentId;
      set
      {
        value = value?.Trim();
        this._departmentId = value;
        if (this._department != null && this._department.Id != this._departmentId)
          this._department = (Department) null;
        this.IsSaved = false;
      }
    }

    public string DocumentationPresentComment
    {
      get => this._documentationPresentComment;
      set
      {
        value = value?.Trim();
        if (this._documentationPresentComment == value)
          return;
        this._documentationPresentComment = value;
        this.IsSaved = false;
      }
    }

    public string DocumentationPresentString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public DocumentationPresent DocumentationPresent
    {
      get => this._documentationPresent;
      set
      {
        if (this._documentationPresent == value)
          return;
        this._documentationPresent = value;
        this.DocumentationPresentString = value == null ? "" : value.ToString();
      }
    }

    public string DocumentationPresentId
    {
      get => this._documentationPresent != null ? this._documentationPresent.Id : this._documentationPresentId;
      set
      {
        value = value?.Trim();
        this._documentationPresentId = value;
        if (this._documentationPresent != null && this._documentationPresent.Id != this._documentationPresentId)
          this._documentationPresent = (DocumentationPresent) null;
        this.IsSaved = false;
      }
    }

    public string EaseOfImplementationFinal
    {
      get => this._easeOfImplementationFinal;
      set
      {
        value = value?.Trim();
        if (this._easeOfImplementationFinal == value)
          return;
        this._easeOfImplementationFinal = value;
        this.IsSaved = false;
      }
    }

    public int? EmployeeCount
    {
      get => this._employeeCount;
      set
      {
        int? employeeCount = this._employeeCount;
        int? nullable = value;
        if (employeeCount.GetValueOrDefault() == nullable.GetValueOrDefault() & employeeCount.HasValue == nullable.HasValue)
          return;
        this._employeeCount = value;
        this.IsSaved = false;
      }
    }

    public string EmployeeCountComment
    {
      get => this._employeeCountComment;
      set
      {
        value = value?.Trim();
        if (this._employeeCountComment == value)
          return;
        this._employeeCountComment = value;
        this.IsSaved = false;
      }
    }

    public string InputComment
    {
      get => this._inputComment;
      set
      {
        value = value?.Trim();
        if (this._inputComment == value)
          return;
        this._inputComment = value;
        this.IsSaved = false;
      }
    }

    public string InputDataStructureString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public InputDataStructure InputDataStructure
    {
      get => this._inputDataStructure;
      set
      {
        if (this._inputDataStructure == value)
          return;
        this._inputDataStructure = value;
        this.InputDataStructureString = value == null ? "" : value.ToString();
      }
    }

    public string InputDataStructureId
    {
      get => this._inputDataStructure != null ? this._inputDataStructure.Id : this._inputDataStructureId;
      set
      {
        value = value?.Trim();
        this._inputDataStructureId = value;
        if (this._inputDataStructure != null && this._inputDataStructure.Id != this._inputDataStructureId)
          this._inputDataStructure = (InputDataStructure) null;
        this.IsSaved = false;
      }
    }

    public string InputString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Input Input
    {
      get => this._input;
      set
      {
        if (this._input == value)
          return;
        this._input = value;
        this.InputString = value == null ? "" : value.ToString();
      }
    }

    public string InputId
    {
      get => this._input != null ? this._input.Id : this._inputId;
      set
      {
        value = value?.Trim();
        this._inputId = value;
        if (this._input != null && this._input.Id != this._inputId)
          this._input = (Input) null;
        this.IsSaved = false;
      }
    }

    public bool IsAlternative
    {
      get => this._isAlternative;
      set
      {
        if (this._isAlternative == value)
          return;
        this._isAlternative = value;
        this.IsSaved = false;
      }
    }

    public bool IsDataInputScanned
    {
      get => this._isDataInputScanned;
      set
      {
        if (this._isDataInputScanned == value)
          return;
        this._isDataInputScanned = value;
        this.IsSaved = false;
      }
    }

    public bool IsDataSensitive
    {
      get => this._isDataSensitive;
      set
      {
        if (this._isDataSensitive == value)
          return;
        this._isDataSensitive = value;
        this.IsSaved = false;
      }
    }

    public bool IsDraft
    {
      get => this._isDraft;
      set
      {
        if (this._isDraft == value)
          return;
        this._isDraft = value;
        this.IsSaved = false;
      }
    }

    public bool IsHighRisk
    {
      get => this._isHighRisk;
      set
      {
        if (this._isHighRisk == value)
          return;
        this._isHighRisk = value;
        this.IsSaved = false;
      }
    }

    public bool IsHostUpgrade
    {
      get => this._isHostUpgrade;
      set
      {
        if (this._isHostUpgrade == value)
          return;
        this._isHostUpgrade = value;
        this.IsSaved = false;
      }
    }

    public string LessenLearnt
    {
      get => this._lessenLearnt;
      set
      {
        value = value?.Trim();
        if (this._lessenLearnt == value)
          return;
        this._lessenLearnt = value;
        this.IsSaved = false;
      }
    }

    public string Name
    {
      get => this._name;
      set
      {
        value = value?.Trim();
        if (this._name == value)
          return;
        this._name = value;
        this.IsSaved = false;
      }
    }

    public string NegativeImpactComment
    {
      get => this._negativeImpactComment;
      set
      {
        value = value?.Trim();
        if (this._negativeImpactComment == value)
          return;
        this._negativeImpactComment = value;
        this.IsSaved = false;
      }
    }

    public string NumberOfWaysToCompleteProcessComment
    {
      get => this._numberOfWaysToCompleteProcessComment;
      set
      {
        value = value?.Trim();
        if (this._numberOfWaysToCompleteProcessComment == value)
          return;
        this._numberOfWaysToCompleteProcessComment = value;
        this.IsSaved = false;
      }
    }

    public string NumberOfWaysToCompleteProcessString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public NumberOfWaysToCompleteProcess NumberOfWaysToCompleteProcess
    {
      get => this._numberOfWaysToCompleteProcess;
      set
      {
        if (this._numberOfWaysToCompleteProcess == value)
          return;
        this._numberOfWaysToCompleteProcess = value;
        this.NumberOfWaysToCompleteProcessString = value == null ? "" : value.ToString();
      }
    }

    public string NumberOfWaysToCompleteProcessId
    {
      get => this._numberOfWaysToCompleteProcess != null ? this._numberOfWaysToCompleteProcess.Id : this._numberOfWaysToCompleteProcessId;
      set
      {
        value = value?.Trim();
        this._numberOfWaysToCompleteProcessId = value;
        if (this._numberOfWaysToCompleteProcess != null && this._numberOfWaysToCompleteProcess.Id != this._numberOfWaysToCompleteProcessId)
          this._numberOfWaysToCompleteProcess = (NumberOfWaysToCompleteProcess) null;
        this.IsSaved = false;
      }
    }

    public string PainPointComment
    {
      get => this._painPointComment;
      set
      {
        value = value?.Trim();
        if (this._painPointComment == value)
          return;
        this._painPointComment = value;
        this.IsSaved = false;
      }
    }

    public Decimal? PotentialFineAmount
    {
      get => this._potentialFineAmount;
      set
      {
        Decimal? potentialFineAmount = this._potentialFineAmount;
        Decimal? nullable = value;
        if (potentialFineAmount.GetValueOrDefault() == nullable.GetValueOrDefault() & potentialFineAmount.HasValue == nullable.HasValue)
          return;
        this._potentialFineAmount = value;
        this.IsSaved = false;
      }
    }

    public Decimal? PotentialFineProbability
    {
      get => this._potentialFineProbability;
      set
      {
        Decimal? potentialFineProbability = this._potentialFineProbability;
        Decimal? nullable = value;
        if (potentialFineProbability.GetValueOrDefault() == nullable.GetValueOrDefault() & potentialFineProbability.HasValue == nullable.HasValue)
          return;
        this._potentialFineProbability = value;
        this.IsSaved = false;
      }
    }

    public string ProcessString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Process Process
    {
      get => this._process;
      set
      {
        if (this._process == value)
          return;
        this._process = value;
        this.ProcessString = value == null ? "" : value.ToString();
      }
    }

    public string ProcessId
    {
      get => this._process != null ? this._process.Id : this._processId;
      set
      {
        value = value?.Trim();
        this._processId = value;
        if (this._process != null && this._process.Id != this._processId)
          this._process = (Process) null;
        this.IsSaved = false;
      }
    }

    public string ProcessOwnerString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public User ProcessOwner
    {
      get => this._processOwner;
      set
      {
        if (this._processOwner == value)
          return;
        this._processOwner = value;
        this.ProcessOwnerString = value == null ? "" : value.ToString();
      }
    }

    public string ProcessOwnerId
    {
      get => this._processOwner != null ? this._processOwner.Id : this._processOwnerId;
      set
      {
        value = value?.Trim();
        this._processOwnerId = value;
        if (this._processOwner != null && this._processOwner.Id != this._processOwnerId)
          this._processOwner = (User) null;
        this.IsSaved = false;
      }
    }

    public string ProcessPeakComment
    {
      get => this._processPeakComment;
      set
      {
        value = value?.Trim();
        if (this._processPeakComment == value)
          return;
        this._processPeakComment = value;
        this.IsSaved = false;
      }
    }

    public string ProcessPeakString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public ProcessPeak ProcessPeak
    {
      get => this._processPeak;
      set
      {
        if (this._processPeak == value)
          return;
        this._processPeak = value;
        this.ProcessPeakString = value == null ? "" : value.ToString();
      }
    }

    public string ProcessPeakId
    {
      get => this._processPeak != null ? this._processPeak.Id : this._processPeakId;
      set
      {
        value = value?.Trim();
        this._processPeakId = value;
        if (this._processPeak != null && this._processPeak.Id != this._processPeakId)
          this._processPeak = (ProcessPeak) null;
        this.IsSaved = false;
      }
    }

    public string ProcessStabilityComment
    {
      get => this._processStabilityComment;
      set
      {
        value = value?.Trim();
        if (this._processStabilityComment == value)
          return;
        this._processStabilityComment = value;
        this.IsSaved = false;
      }
    }

    public string ProcessStabilityString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public ProcessStability ProcessStability
    {
      get => this._processStability;
      set
      {
        if (this._processStability == value)
          return;
        this._processStability = value;
        this.ProcessStabilityString = value == null ? "" : value.ToString();
      }
    }

    public string ProcessStabilityId
    {
      get => this._processStability != null ? this._processStability.Id : this._processStabilityId;
      set
      {
        value = value?.Trim();
        this._processStabilityId = value;
        if (this._processStability != null && this._processStability.Id != this._processStabilityId)
          this._processStability = (ProcessStability) null;
        this.IsSaved = false;
      }
    }

    public Decimal? ProcessVolumetryPerMonth
    {
      get => this._processVolumetryPerMonth;
      set
      {
        Decimal? volumetryPerMonth = this._processVolumetryPerMonth;
        Decimal? nullable = value;
        if (volumetryPerMonth.GetValueOrDefault() == nullable.GetValueOrDefault() & volumetryPerMonth.HasValue == nullable.HasValue)
          return;
        this._processVolumetryPerMonth = value;
        this.IsSaved = false;
      }
    }

    public Decimal? ProcessVolumetryPerYear
    {
      get => this._processVolumetryPerYear;
      set
      {
        Decimal? volumetryPerYear = this._processVolumetryPerYear;
        Decimal? nullable = value;
        if (volumetryPerYear.GetValueOrDefault() == nullable.GetValueOrDefault() & volumetryPerYear.HasValue == nullable.HasValue)
          return;
        this._processVolumetryPerYear = value;
        this.IsSaved = false;
      }
    }

    public int? Rating
    {
      get => this._rating;
      set
      {
        int? rating = this._rating;
        int? nullable = value;
        if (rating.GetValueOrDefault() == nullable.GetValueOrDefault() & rating.HasValue == nullable.HasValue)
          return;
        this._rating = value;
        this.IsSaved = false;
      }
    }

    public string RatingComment
    {
      get => this._ratingComment;
      set
      {
        value = value?.Trim();
        if (this._ratingComment == value)
          return;
        this._ratingComment = value;
        this.IsSaved = false;
      }
    }

    public Decimal? RobotSpeedMultiplier
    {
      get => this._robotSpeedMultiplier;
      set
      {
        Decimal? robotSpeedMultiplier = this._robotSpeedMultiplier;
        Decimal? nullable = value;
        if (robotSpeedMultiplier.GetValueOrDefault() == nullable.GetValueOrDefault() & robotSpeedMultiplier.HasValue == nullable.HasValue)
          return;
        this._robotSpeedMultiplier = value;
        this.IsSaved = false;
      }
    }

    public Decimal? RobotWorkDayYear
    {
      get => this._robotWorkDayYear;
      set
      {
        Decimal? robotWorkDayYear = this._robotWorkDayYear;
        Decimal? nullable = value;
        if (robotWorkDayYear.GetValueOrDefault() == nullable.GetValueOrDefault() & robotWorkDayYear.HasValue == nullable.HasValue)
          return;
        this._robotWorkDayYear = value;
        this.IsSaved = false;
      }
    }

    public Decimal? RobotWorkHourDay
    {
      get => this._robotWorkHourDay;
      set
      {
        Decimal? robotWorkHourDay = this._robotWorkHourDay;
        Decimal? nullable = value;
        if (robotWorkHourDay.GetValueOrDefault() == nullable.GetValueOrDefault() & robotWorkHourDay.HasValue == nullable.HasValue)
          return;
        this._robotWorkHourDay = value;
        this.IsSaved = false;
      }
    }

    public string RuleComment
    {
      get => this._ruleComment;
      set
      {
        value = value?.Trim();
        if (this._ruleComment == value)
          return;
        this._ruleComment = value;
        this.IsSaved = false;
      }
    }

    public string RuleString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Rule Rule
    {
      get => this._rule;
      set
      {
        if (this._rule == value)
          return;
        this._rule = value;
        this.RuleString = value == null ? "" : value.ToString();
      }
    }

    public string RuleId
    {
      get => this._rule != null ? this._rule.Id : this._ruleId;
      set
      {
        value = value?.Trim();
        this._ruleId = value;
        if (this._rule != null && this._rule.Id != this._ruleId)
          this._rule = (Rule) null;
        this.IsSaved = false;
      }
    }

    public string RunningCostString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public RunningCost RunningCost
    {
      get => this._runningCost;
      set
      {
        if (this._runningCost == value)
          return;
        this._runningCost = value;
        this.RunningCostString = value == null ? "" : value.ToString();
      }
    }

    public string RunningCostId
    {
      get => this._runningCost != null ? this._runningCost.Id : this._runningCostId;
      set
      {
        value = value?.Trim();
        this._runningCostId = value;
        if (this._runningCost != null && this._runningCost.Id != this._runningCostId)
          this._runningCost = (RunningCost) null;
        this.IsSaved = false;
      }
    }

    public string StructureComment
    {
      get => this._structureComment;
      set
      {
        value = value?.Trim();
        if (this._structureComment == value)
          return;
        this._structureComment = value;
        this.IsSaved = false;
      }
    }

    public string SubmissionPathString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public SubmissionPath SubmissionPath
    {
      get => this._submissionPath;
      set
      {
        if (this._submissionPath == value)
          return;
        this._submissionPath = value;
        this.SubmissionPathString = value == null ? "" : value.ToString();
      }
    }

    public string SubmissionPathId
    {
      get => this._submissionPath != null ? this._submissionPath.Id : this._submissionPathId;
      set
      {
        value = value?.Trim();
        this._submissionPathId = value;
        if (this._submissionPath != null && this._submissionPath.Id != this._submissionPathId)
          this._submissionPath = (SubmissionPath) null;
        this.IsSaved = false;
      }
    }

    public string SubTitle
    {
      get => this._subTitle;
      set
      {
        value = value?.Trim();
        if (this._subTitle == value)
          return;
        this._subTitle = value;
        this.IsSaved = false;
      }
    }

    public string Summary
    {
      get => this._summary;
      set
      {
        value = value?.Trim();
        if (this._summary == value)
          return;
        this._summary = value;
        this.IsSaved = false;
      }
    }

    public string TaskFrequencyComment
    {
      get => this._taskFrequencyComment;
      set
      {
        value = value?.Trim();
        if (this._taskFrequencyComment == value)
          return;
        this._taskFrequencyComment = value;
        this.IsSaved = false;
      }
    }

    public string TaskFrequencyString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public TaskFrequency TaskFrequency
    {
      get => this._taskFrequency;
      set
      {
        if (this._taskFrequency == value)
          return;
        this._taskFrequency = value;
        this.TaskFrequencyString = value == null ? "" : value.ToString();
      }
    }

    public string TaskFrequencyId
    {
      get => this._taskFrequency != null ? this._taskFrequency.Id : this._taskFrequencyId;
      set
      {
        value = value?.Trim();
        this._taskFrequencyId = value;
        if (this._taskFrequency != null && this._taskFrequency.Id != this._taskFrequencyId)
          this._taskFrequency = (TaskFrequency) null;
        this.IsSaved = false;
      }
    }

    public string TeamString { get; set; }

    [IgnoreDataMember]
    [XmlIgnore]
    public Team Team
    {
      get => this._team;
      set
      {
        if (this._team == value)
          return;
        this._team = value;
        this.TeamString = value == null ? "" : value.ToString();
      }
    }

    public string TeamId
    {
      get => this._team != null ? this._team.Id : this._teamId;
      set
      {
        value = value?.Trim();
        this._teamId = value;
        if (this._team != null && this._team.Id != this._teamId)
          this._team = (Team) null;
        this.IsSaved = false;
      }
    }

    public Decimal? WorkingHour
    {
      get => this._workingHour;
      set
      {
        Decimal? workingHour = this._workingHour;
        Decimal? nullable = value;
        if (workingHour.GetValueOrDefault() == nullable.GetValueOrDefault() & workingHour.HasValue == nullable.HasValue)
          return;
        this._workingHour = value;
        this.IsSaved = false;
      }
    }

    public string WorkingHourComment
    {
      get => this._workingHourComment;
      set
      {
        value = value?.Trim();
        if (this._workingHourComment == value)
          return;
        this._workingHourComment = value;
        this.IsSaved = false;
      }
    }

    public Decimal? WorkloadSplit
    {
      get => this._workloadSplit;
      set
      {
        Decimal? workloadSplit = this._workloadSplit;
        Decimal? nullable = value;
        if (workloadSplit.GetValueOrDefault() == nullable.GetValueOrDefault() & workloadSplit.HasValue == nullable.HasValue)
          return;
        this._workloadSplit = value;
        this.IsSaved = false;
      }
    }

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Collaborator> Collaborators { get; set; } = new List<Collaborator>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Comment> Comments { get; set; } = new List<Comment>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Document> Documents { get; set; } = new List<Document>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Follow> Follows { get; set; } = new List<Follow>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaApplicationVersion> IdeaApplicationVersions { get; set; } = new List<IdeaApplicationVersion>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaOtherRunningCost> IdeaOtherRunningCosts { get; set; } = new List<IdeaOtherRunningCost>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaRunningCost> IdeaRunningCosts { get; set; } = new List<IdeaRunningCost>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<IdeaStage> IdeaStages { get; set; } = new List<IdeaStage>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<UserAuthorisation> UserAuthorisations { get; set; } = new List<UserAuthorisation>();

    [IgnoreDataMember]
    [XmlIgnore]
    public List<Vote> Votes { get; set; } = new List<Vote>();

    public void Update(Idea idea)
    {
      if (idea.ApplicationStabilityId == null)
        this.ApplicationStability = (ApplicationStability) null;
      if (idea.AutomationGoalId == null)
        this.AutomationGoal = (AutomationGoal) null;
      if (idea.AverageNumberOfStepId == null)
        this.AverageNumberOfStep = (AverageNumberOfStep) null;
      if (idea.ClientId == null)
        this.Client = (Client) null;
      if (idea.DataInputPercentOfStructuredId == null)
        this.DataInputPercentOfStructured = (DataInputPercentOfStructured) null;
      if (idea.DecisionCountId == null)
        this.DecisionCount = (DecisionCount) null;
      if (idea.DecisionDifficultyId == null)
        this.DecisionDifficulty = (DecisionDifficulty) null;
      if (idea.DepartmentId == null)
        this.Department = (Department) null;
      if (idea.DocumentationPresentId == null)
        this.DocumentationPresent = (DocumentationPresent) null;
      if (idea.InputDataStructureId == null)
        this.InputDataStructure = (InputDataStructure) null;
      if (idea.InputId == null)
        this.Input = (Input) null;
      if (idea.NumberOfWaysToCompleteProcessId == null)
        this.NumberOfWaysToCompleteProcess = (NumberOfWaysToCompleteProcess) null;
      if (idea.ProcessId == null)
        this.Process = (Process) null;
      if (idea.ProcessOwnerId == null)
        this.ProcessOwner = (User) null;
      if (idea.ProcessPeakId == null)
        this.ProcessPeak = (ProcessPeak) null;
      if (idea.ProcessStabilityId == null)
        this.ProcessStability = (ProcessStability) null;
      if (idea.RuleId == null)
        this.Rule = (Rule) null;
      if (idea.TaskFrequencyId == null)
        this.TaskFrequency = (TaskFrequency) null;
      if (idea.TeamId == null)
        this.Team = (Team) null;
      this.ActivityVolumeAverage = idea.ActivityVolumeAverage;
      this.ActivityVolumeAverageComment = idea.ActivityVolumeAverageComment;
      this.AHTRobot = idea.AHTRobot;
      this.ApplicationStabilityComment = idea.ApplicationStabilityComment;
      this.ApplicationStabilityId = idea.ApplicationStabilityId;
      this.AutomationGoalId = idea.AutomationGoalId;
      this.AverageEmployeeFullCost = idea.AverageEmployeeFullCost;
      this.AverageEmployeeFullCostComment = idea.AverageEmployeeFullCostComment;
      this.AverageErrorRate = idea.AverageErrorRate;
      this.AverageErrorRateComment = idea.AverageErrorRateComment;
      this.AverageNumberOfStepComment = idea.AverageNumberOfStepComment;
      this.AverageNumberOfStepId = idea.AverageNumberOfStepId;
      this.AverageProcessingTime = idea.AverageProcessingTime;
      this.AverageProcessingTimeComment = idea.AverageProcessingTimeComment;
      this.AverageReviewTime = idea.AverageReviewTime;
      this.AverageReviewTimeComment = idea.AverageReviewTimeComment;
      this.AverageReworkTime = idea.AverageReworkTime;
      this.AverageReworkTimeComment = idea.AverageReworkTimeComment;
      this.AverageWorkingDay = idea.AverageWorkingDay;
      this.AverageWorkingDayComment = idea.AverageWorkingDayComment;
      this.AverageWorkToBeReviewed = idea.AverageWorkToBeReviewed;
      this.AverageWorkToBeReviewedComment = idea.AverageWorkToBeReviewedComment;
      this.BenefitActual = idea.BenefitActual;
      this.BenefitExpected = idea.BenefitExpected;
      this.ChallengeActual = idea.ChallengeActual;
      this.ChallengeExpected = idea.ChallengeExpected;
      this.ClientId = idea.ClientId;
      this.DataInputPercentOfStructuredComment = idea.DataInputPercentOfStructuredComment;
      this.DataInputPercentOfStructuredId = idea.DataInputPercentOfStructuredId;
      this.DataInputScannedComment = idea.DataInputScannedComment;
      this.DecisionCountComment = idea.DecisionCountComment;
      this.DecisionCountId = idea.DecisionCountId;
      this.DecisionDifficultyComment = idea.DecisionDifficultyComment;
      this.DecisionDifficultyId = idea.DecisionDifficultyId;
      this.DepartmentId = idea.DepartmentId;
      this.DocumentationPresentComment = idea.DocumentationPresentComment;
      this.DocumentationPresentId = idea.DocumentationPresentId;
      this.EaseOfImplementationFinal = idea.EaseOfImplementationFinal;
      this.EmployeeCount = idea.EmployeeCount;
      this.EmployeeCountComment = idea.EmployeeCountComment;
      this.InputComment = idea.InputComment;
      this.InputDataStructureId = idea.InputDataStructureId;
      this.InputId = idea.InputId;
      this.IsAlternative = idea.IsAlternative;
      this.IsDataInputScanned = idea.IsDataInputScanned;
      this.IsDataSensitive = idea.IsDataSensitive;
      this.IsDraft = idea.IsDraft;
      this.IsHighRisk = idea.IsHighRisk;
      this.IsHostUpgrade = idea.IsHostUpgrade;
      this.LessenLearnt = idea.LessenLearnt;
      this.Name = idea.Name;
      this.NegativeImpactComment = idea.NegativeImpactComment;
      this.NumberOfWaysToCompleteProcessComment = idea.NumberOfWaysToCompleteProcessComment;
      this.NumberOfWaysToCompleteProcessId = idea.NumberOfWaysToCompleteProcessId;
      this.PainPointComment = idea.PainPointComment;
      this.PotentialFineAmount = idea.PotentialFineAmount;
      this.PotentialFineProbability = idea.PotentialFineProbability;
      this.ProcessId = idea.ProcessId;
      this.ProcessOwnerId = idea.ProcessOwnerId;
      this.ProcessPeakComment = idea.ProcessPeakComment;
      this.ProcessPeakId = idea.ProcessPeakId;
      this.ProcessStabilityComment = idea.ProcessStabilityComment;
      this.ProcessStabilityId = idea.ProcessStabilityId;
      this.ProcessVolumetryPerMonth = idea.ProcessVolumetryPerMonth;
      this.ProcessVolumetryPerYear = idea.ProcessVolumetryPerYear;
      this.Rating = idea.Rating;
      this.RatingComment = idea.RatingComment;
      this.RobotSpeedMultiplier = idea.RobotSpeedMultiplier;
      this.RobotWorkDayYear = idea.RobotWorkDayYear;
      this.RobotWorkHourDay = idea.RobotWorkHourDay;
      this.RuleComment = idea.RuleComment;
      this.RuleId = idea.RuleId;
      this.RunningCostId = idea.RunningCostId;
      this.StructureComment = idea.StructureComment;
      this.SubmissionPathId = idea.SubmissionPathId;
      this.SubTitle = idea.SubTitle;
      this.Summary = idea.Summary;
      this.TaskFrequencyComment = idea.TaskFrequencyComment;
      this.TaskFrequencyId = idea.TaskFrequencyId;
      this.TeamId = idea.TeamId;
      this.WorkingHour = idea.WorkingHour;
      this.WorkingHourComment = idea.WorkingHourComment;
      this.WorkloadSplit = idea.WorkloadSplit;
    }

    public override string ToString() => this.Name;
  }
}
