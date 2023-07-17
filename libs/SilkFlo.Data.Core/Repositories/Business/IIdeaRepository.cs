using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IIdeaRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Idea> GetAsync(string id);

    Task<Idea> SingleOrDefaultAsync(Func<Idea, bool> predicate);

    Task<bool> AddAsync(Idea entity);

    Task<bool> AddRangeAsync(IEnumerable<Idea> entities);

    Task<IEnumerable<Idea>> GetAllAsync();

    Task<IEnumerable<Idea>> FindAsync(Func<Idea, bool> predicate);

    Task<Idea> GetUsingNameAsync(string name);

    Task GetForApplicationStabilityAsync(ApplicationStability applicationStability);

    Task GetForApplicationStabilityAsync(
      IEnumerable<ApplicationStability> applicationStabilities);

    Task GetForAutomationGoalAsync(AutomationGoal automationGoal);

    Task GetForAutomationGoalAsync(IEnumerable<AutomationGoal> automationGoals);

    Task GetForAverageNumberOfStepAsync(AverageNumberOfStep averageNumberOfStep);

    Task GetForAverageNumberOfStepAsync(
      IEnumerable<AverageNumberOfStep> averageNumberOfSteps);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForDataInputPercentOfStructuredAsync(
      DataInputPercentOfStructured dataInputPercentOfStructured);

    Task GetForDataInputPercentOfStructuredAsync(
      IEnumerable<DataInputPercentOfStructured> dataInputPercentOfStructureds);

    Task GetForDecisionCountAsync(DecisionCount decisionCount);

    Task GetForDecisionCountAsync(IEnumerable<DecisionCount> decisionCounts);

    Task GetForDecisionDifficultyAsync(DecisionDifficulty decisionDifficulty);

    Task GetForDecisionDifficultyAsync(
      IEnumerable<DecisionDifficulty> decisionDifficulties);

    Task GetForDepartmentAsync(Department department);

    Task GetForDepartmentAsync(IEnumerable<Department> departments);

    Task GetForDocumentationPresentAsync(DocumentationPresent documentationPresent);

    Task GetForDocumentationPresentAsync(
      IEnumerable<DocumentationPresent> documentationPresents);

    Task GetForInputDataStructureAsync(InputDataStructure inputDataStructure);

    Task GetForInputDataStructureAsync(
      IEnumerable<InputDataStructure> inputDataStructures);

    Task GetForInputAsync(Input input);

    Task GetForInputAsync(IEnumerable<Input> inputs);

    Task GetForNumberOfWaysToCompleteProcessAsync(
      NumberOfWaysToCompleteProcess numberOfWaysToCompleteProcess);

    Task GetForNumberOfWaysToCompleteProcessAsync(
      IEnumerable<NumberOfWaysToCompleteProcess> numberOfWaysToCompleteProcesses);

    Task GetForProcessAsync(Process process);

    Task GetForProcessAsync(IEnumerable<Process> processes);

    Task GetForProcessOwnerAsync(User processOwner);

    Task GetForProcessOwnerAsync(IEnumerable<User> processOwners);

    Task GetForProcessPeakAsync(ProcessPeak processPeak);

    Task GetForProcessPeakAsync(IEnumerable<ProcessPeak> processPeaks);

    Task GetForProcessStabilityAsync(ProcessStability processStability);

    Task GetForProcessStabilityAsync(IEnumerable<ProcessStability> processStabilities);

    Task GetForRuleAsync(Rule rule);

    Task GetForRuleAsync(IEnumerable<Rule> rules);

    Task GetForRunningCostAsync(RunningCost runningCost);

    Task GetForRunningCostAsync(IEnumerable<RunningCost> runningCosts);

    Task GetForSubmissionPathAsync(SubmissionPath submissionPath);

    Task GetForSubmissionPathAsync(IEnumerable<SubmissionPath> submissionPaths);

    Task GetForTaskFrequencyAsync(TaskFrequency taskFrequency);

    Task GetForTaskFrequencyAsync(IEnumerable<TaskFrequency> taskFrequencies);

    Task GetForTeamAsync(Team team);

    Task GetForTeamAsync(IEnumerable<Team> teams);

    Task GetIdeaForAsync(Collaborator collaborator);

    Task GetIdeaForAsync(IEnumerable<Collaborator> collaborators);

    Task GetIdeaForAsync(Comment comment);

    Task GetIdeaForAsync(IEnumerable<Comment> comments);

    Task GetIdeaForAsync(Document document);

    Task GetIdeaForAsync(IEnumerable<Document> documents);

    Task GetIdeaForAsync(Follow follow);

    Task GetIdeaForAsync(IEnumerable<Follow> follows);

    Task GetIdeaForAsync(IdeaApplicationVersion ideaApplicationVersion);

    Task GetIdeaForAsync(
      IEnumerable<IdeaApplicationVersion> ideaApplicationVersions);

    Task GetIdeaForAsync(IdeaOtherRunningCost ideaOtherRunningCost);

    Task GetIdeaForAsync(
      IEnumerable<IdeaOtherRunningCost> ideaOtherRunningCosts);

    Task GetIdeaForAsync(IdeaRunningCost ideaRunningCost);

    Task GetIdeaForAsync(IEnumerable<IdeaRunningCost> ideaRunningCosts);

    Task GetIdeaForAsync(IdeaStage ideaStage);

    Task GetIdeaForAsync(IEnumerable<IdeaStage> ideaStages);

    Task GetIdeaForAsync(UserAuthorisation userAuthorisation);

    Task GetIdeaForAsync(IEnumerable<UserAuthorisation> userAuthorisations);

    Task GetIdeaForAsync(Vote vote);

    Task GetIdeaForAsync(IEnumerable<Vote> votes);

    Task<Idea> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Idea entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Idea> entities);
  }
}
