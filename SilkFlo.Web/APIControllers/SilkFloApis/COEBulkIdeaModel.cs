using System;

namespace SilkFlo.Web.APIControllers.SilkFloApis
{
	public class COEBulkIdeaModel
	{
		public string? Name { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string? SubmitterEmailAddress { get; set; }
		public string? ProcessOwnerEmailAddress { get; set; }
		public string? AutomationId { get; set; }
		public string? Description { get; set; }   /////summary
		public string? DepartmentId { get; set; }   /////DepartmentId
		public string? TeamId { get; set; }   /////TeamId
		public string? ProcessId { get; set; }   /////ProcessId confusing
		public string? Stage { get; set; }   /////ProcessId confusing
		public string? Status { get; set; }   /////ProcessId confusing
		public DateTime? DeployeementDate { get; set; }   /////////// confusing
		public string? RuleId {  get; set; }
		public string? InputId {  get; set; }
		public string? InputDataStructureId {  get; set; }
		public string? ProcessStabilityId {  get; set; }
		public string? DocumentationPresentId {  get; set; }
		public string? AutomationGoalId {  get; set; }
		public string? ApplicationStabilityId {  get; set; }
		public string? TaskFrequencyId {  get; set; }
		public string? AverageReviewTimeComment {  get; set; }
		public string? ProcessPeakId {  get; set; }
		public string? AverageNumberOfStepId {  get; set; }
		public string? NumberOfWaysToCompleteProcessId {  get; set; }
		public string? DataInputPercentOfStructuredId {  get; set; }
		public string? DecisionCountId {  get; set; }
		public string? DecisionDifficultyId {  get; set; }
		public int? AverageWorkingDay {  get; set; }
		public int? AverageEmployeeFullCost {  get; set; }
		public int? ActivityVolumeAverage {  get; set; }
		public int? EmployeeCount {  get; set; }
		public int? AverageErrorRate {  get; set; }
		public decimal? WorkingHour {  get; set; }
		public decimal? AverageProcessingTime {  get; set; }
		public decimal? AverageReworkTime {  get; set; }
		public decimal? AverageWorkToBeReviewed {  get; set; }
		public decimal? PotentialFineAmount {  get; set; }
		public decimal? PotentialFineProbability {  get; set; }
		public string? IsHighRisk {  get; set; }
		public string? IsDataSensitive {  get; set; }
		public string? IsAlternative {  get; set; }
		public string? IsHostUpgrade {  get; set; }
		public string? IsDataInputScanned {  get; set; }


	}
}
