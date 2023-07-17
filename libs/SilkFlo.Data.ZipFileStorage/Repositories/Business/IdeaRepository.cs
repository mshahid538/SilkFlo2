// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.IdeaRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Repositories.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Data.Persistence.Repositories.Business
{
  public class IdeaRepository : IIdeaRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public IdeaRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Idea Get(string id) => this.GetAsync(id).Result;

    public async Task<Idea> GetAsync(string id)
    {
      if (id == null)
        return (Idea) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeas.SingleOrDefault<Idea>((Func<Idea, bool>) (x => x.Id == id));
    }

    public Idea SingleOrDefault(Func<Idea, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Idea> SingleOrDefaultAsync(Func<Idea, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeas.Where<Idea>(predicate).FirstOrDefault<Idea>();
    }

    public bool Add(Idea entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Idea entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Idea> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Idea> entities)
    {
      if (entities == null)
        return false;
      foreach (Idea entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Idea> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Idea>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Idea>) dataSetAsync.BusinessIdeas.OrderBy<Idea, string>((Func<Idea, string>) (m => m.Name)).ThenBy<Idea, string>((Func<Idea, string>) (m => m.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (m => m.Summary));
    }

    public IEnumerable<Idea> Find(Func<Idea, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Idea>> FindAsync(Func<Idea, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Idea>) dataSetAsync.BusinessIdeas.Where<Idea>(predicate).OrderBy<Idea, string>((Func<Idea, string>) (m => m.Name)).ThenBy<Idea, string>((Func<Idea, string>) (m => m.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (m => m.Summary));
    }

    public Idea GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Idea> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Idea) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeas.SingleOrDefault<Idea>((Func<Idea, bool>) (x => x.Name == name));
    }

    public void GetForApplicationStability(ApplicationStability applicationStability) => this.GetForApplicationStabilityAsync(applicationStability).RunSynchronously();

    public async Task GetForApplicationStabilityAsync(ApplicationStability applicationStability)
    {
      List<Idea> lst;
      if (applicationStability == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(applicationStability.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.ApplicationStabilityId == applicationStability.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.ApplicationStabilityId = applicationStability.Id;
          item.ApplicationStability = applicationStability;
        }
        applicationStability.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForApplicationStability(
      IEnumerable<ApplicationStability> applicationStabilities)
    {
      this.GetForApplicationStabilityAsync(applicationStabilities).RunSynchronously();
    }

    public async Task GetForApplicationStabilityAsync(
      IEnumerable<ApplicationStability> applicationStabilities)
    {
      if (applicationStabilities == null)
        return;
      foreach (ApplicationStability applicationStability in applicationStabilities)
        await this.GetForApplicationStabilityAsync(applicationStability);
    }

    public void GetForAutomationGoal(AutomationGoal automationGoal) => this.GetForAutomationGoalAsync(automationGoal).RunSynchronously();

    public async Task GetForAutomationGoalAsync(AutomationGoal automationGoal)
    {
      List<Idea> lst;
      if (automationGoal == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(automationGoal.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.AutomationGoalId == automationGoal.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.AutomationGoalId = automationGoal.Id;
          item.AutomationGoal = automationGoal;
        }
        automationGoal.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForAutomationGoal(IEnumerable<AutomationGoal> automationGoals) => this.GetForAutomationGoalAsync(automationGoals).RunSynchronously();

    public async Task GetForAutomationGoalAsync(IEnumerable<AutomationGoal> automationGoals)
    {
      if (automationGoals == null)
        return;
      foreach (AutomationGoal automationGoal in automationGoals)
        await this.GetForAutomationGoalAsync(automationGoal);
    }

    public void GetForAverageNumberOfStep(AverageNumberOfStep averageNumberOfStep) => this.GetForAverageNumberOfStepAsync(averageNumberOfStep).RunSynchronously();

    public async Task GetForAverageNumberOfStepAsync(AverageNumberOfStep averageNumberOfStep)
    {
      List<Idea> lst;
      if (averageNumberOfStep == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(averageNumberOfStep.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.AverageNumberOfStepId == averageNumberOfStep.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.AverageNumberOfStepId = averageNumberOfStep.Id;
          item.AverageNumberOfStep = averageNumberOfStep;
        }
        averageNumberOfStep.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForAverageNumberOfStep(
      IEnumerable<AverageNumberOfStep> averageNumberOfSteps)
    {
      this.GetForAverageNumberOfStepAsync(averageNumberOfSteps).RunSynchronously();
    }

    public async Task GetForAverageNumberOfStepAsync(
      IEnumerable<AverageNumberOfStep> averageNumberOfSteps)
    {
      if (averageNumberOfSteps == null)
        return;
      foreach (AverageNumberOfStep averageNumberOfStep in averageNumberOfSteps)
        await this.GetForAverageNumberOfStepAsync(averageNumberOfStep);
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<Idea> lst;
      if (client == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.ClientId == client.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForClient(IEnumerable<Client> clients) => this.GetForClientAsync(clients).RunSynchronously();

    public async Task GetForClientAsync(IEnumerable<Client> clients)
    {
      if (clients == null)
        return;
      foreach (Client client in clients)
        await this.GetForClientAsync(client);
    }

    public void GetForDataInputPercentOfStructured(
      DataInputPercentOfStructured dataInputPercentOfStructured)
    {
      this.GetForDataInputPercentOfStructuredAsync(dataInputPercentOfStructured).RunSynchronously();
    }

    public async Task GetForDataInputPercentOfStructuredAsync(
      DataInputPercentOfStructured dataInputPercentOfStructured)
    {
      List<Idea> lst;
      if (dataInputPercentOfStructured == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(dataInputPercentOfStructured.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.DataInputPercentOfStructuredId == dataInputPercentOfStructured.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.DataInputPercentOfStructuredId = dataInputPercentOfStructured.Id;
          item.DataInputPercentOfStructured = dataInputPercentOfStructured;
        }
        dataInputPercentOfStructured.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForDataInputPercentOfStructured(
      IEnumerable<DataInputPercentOfStructured> dataInputPercentOfStructureds)
    {
      this.GetForDataInputPercentOfStructuredAsync(dataInputPercentOfStructureds).RunSynchronously();
    }

    public async Task GetForDataInputPercentOfStructuredAsync(
      IEnumerable<DataInputPercentOfStructured> dataInputPercentOfStructureds)
    {
      if (dataInputPercentOfStructureds == null)
        return;
      foreach (DataInputPercentOfStructured dataInputPercentOfStructured in dataInputPercentOfStructureds)
        await this.GetForDataInputPercentOfStructuredAsync(dataInputPercentOfStructured);
    }

    public void GetForDecisionCount(DecisionCount decisionCount) => this.GetForDecisionCountAsync(decisionCount).RunSynchronously();

    public async Task GetForDecisionCountAsync(DecisionCount decisionCount)
    {
      List<Idea> lst;
      if (decisionCount == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(decisionCount.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.DecisionCountId == decisionCount.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.DecisionCountId = decisionCount.Id;
          item.DecisionCount = decisionCount;
        }
        decisionCount.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForDecisionCount(IEnumerable<DecisionCount> decisionCounts) => this.GetForDecisionCountAsync(decisionCounts).RunSynchronously();

    public async Task GetForDecisionCountAsync(IEnumerable<DecisionCount> decisionCounts)
    {
      if (decisionCounts == null)
        return;
      foreach (DecisionCount decisionCount in decisionCounts)
        await this.GetForDecisionCountAsync(decisionCount);
    }

    public void GetForDecisionDifficulty(DecisionDifficulty decisionDifficulty) => this.GetForDecisionDifficultyAsync(decisionDifficulty).RunSynchronously();

    public async Task GetForDecisionDifficultyAsync(DecisionDifficulty decisionDifficulty)
    {
      List<Idea> lst;
      if (decisionDifficulty == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(decisionDifficulty.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.DecisionDifficultyId == decisionDifficulty.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.DecisionDifficultyId = decisionDifficulty.Id;
          item.DecisionDifficulty = decisionDifficulty;
        }
        decisionDifficulty.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForDecisionDifficulty(
      IEnumerable<DecisionDifficulty> decisionDifficulties)
    {
      this.GetForDecisionDifficultyAsync(decisionDifficulties).RunSynchronously();
    }

    public async Task GetForDecisionDifficultyAsync(
      IEnumerable<DecisionDifficulty> decisionDifficulties)
    {
      if (decisionDifficulties == null)
        return;
      foreach (DecisionDifficulty decisionDifficulty in decisionDifficulties)
        await this.GetForDecisionDifficultyAsync(decisionDifficulty);
    }

    public void GetForDepartment(Department department) => this.GetForDepartmentAsync(department).RunSynchronously();

    public async Task GetForDepartmentAsync(Department department)
    {
      List<Idea> lst;
      if (department == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(department.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.DepartmentId == department.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.DepartmentId = department.Id;
          item.Department = department;
        }
        department.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForDepartment(IEnumerable<Department> departments) => this.GetForDepartmentAsync(departments).RunSynchronously();

    public async Task GetForDepartmentAsync(IEnumerable<Department> departments)
    {
      if (departments == null)
        return;
      foreach (Department department in departments)
        await this.GetForDepartmentAsync(department);
    }

    public void GetForDocumentationPresent(DocumentationPresent documentationPresent) => this.GetForDocumentationPresentAsync(documentationPresent).RunSynchronously();

    public async Task GetForDocumentationPresentAsync(DocumentationPresent documentationPresent)
    {
      List<Idea> lst;
      if (documentationPresent == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(documentationPresent.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.DocumentationPresentId == documentationPresent.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.DocumentationPresentId = documentationPresent.Id;
          item.DocumentationPresent = documentationPresent;
        }
        documentationPresent.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForDocumentationPresent(
      IEnumerable<DocumentationPresent> documentationPresents)
    {
      this.GetForDocumentationPresentAsync(documentationPresents).RunSynchronously();
    }

    public async Task GetForDocumentationPresentAsync(
      IEnumerable<DocumentationPresent> documentationPresents)
    {
      if (documentationPresents == null)
        return;
      foreach (DocumentationPresent documentationPresent in documentationPresents)
        await this.GetForDocumentationPresentAsync(documentationPresent);
    }

    public void GetForInputDataStructure(InputDataStructure inputDataStructure) => this.GetForInputDataStructureAsync(inputDataStructure).RunSynchronously();

    public async Task GetForInputDataStructureAsync(InputDataStructure inputDataStructure)
    {
      List<Idea> lst;
      if (inputDataStructure == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(inputDataStructure.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.InputDataStructureId == inputDataStructure.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.InputDataStructureId = inputDataStructure.Id;
          item.InputDataStructure = inputDataStructure;
        }
        inputDataStructure.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForInputDataStructure(
      IEnumerable<InputDataStructure> inputDataStructures)
    {
      this.GetForInputDataStructureAsync(inputDataStructures).RunSynchronously();
    }

    public async Task GetForInputDataStructureAsync(
      IEnumerable<InputDataStructure> inputDataStructures)
    {
      if (inputDataStructures == null)
        return;
      foreach (InputDataStructure inputDataStructure in inputDataStructures)
        await this.GetForInputDataStructureAsync(inputDataStructure);
    }

    public void GetForInput(Input input) => this.GetForInputAsync(input).RunSynchronously();

    public async Task GetForInputAsync(Input input)
    {
      List<Idea> lst;
      if (input == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(input.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.InputId == input.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.InputId = input.Id;
          item.Input = input;
        }
        input.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForInput(IEnumerable<Input> inputs) => this.GetForInputAsync(inputs).RunSynchronously();

    public async Task GetForInputAsync(IEnumerable<Input> inputs)
    {
      if (inputs == null)
        return;
      foreach (Input input in inputs)
        await this.GetForInputAsync(input);
    }

    public void GetForNumberOfWaysToCompleteProcess(
      NumberOfWaysToCompleteProcess numberOfWaysToCompleteProcess)
    {
      this.GetForNumberOfWaysToCompleteProcessAsync(numberOfWaysToCompleteProcess).RunSynchronously();
    }

    public async Task GetForNumberOfWaysToCompleteProcessAsync(
      NumberOfWaysToCompleteProcess numberOfWaysToCompleteProcess)
    {
      List<Idea> lst;
      if (numberOfWaysToCompleteProcess == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(numberOfWaysToCompleteProcess.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.NumberOfWaysToCompleteProcessId == numberOfWaysToCompleteProcess.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.NumberOfWaysToCompleteProcessId = numberOfWaysToCompleteProcess.Id;
          item.NumberOfWaysToCompleteProcess = numberOfWaysToCompleteProcess;
        }
        numberOfWaysToCompleteProcess.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForNumberOfWaysToCompleteProcess(
      IEnumerable<NumberOfWaysToCompleteProcess> numberOfWaysToCompleteProcesses)
    {
      this.GetForNumberOfWaysToCompleteProcessAsync(numberOfWaysToCompleteProcesses).RunSynchronously();
    }

    public async Task GetForNumberOfWaysToCompleteProcessAsync(
      IEnumerable<NumberOfWaysToCompleteProcess> numberOfWaysToCompleteProcesses)
    {
      if (numberOfWaysToCompleteProcesses == null)
        return;
      foreach (NumberOfWaysToCompleteProcess numberOfWaysToCompleteProcess in numberOfWaysToCompleteProcesses)
        await this.GetForNumberOfWaysToCompleteProcessAsync(numberOfWaysToCompleteProcess);
    }

    public void GetForProcess(Process process) => this.GetForProcessAsync(process).RunSynchronously();

    public async Task GetForProcessAsync(Process process)
    {
      List<Idea> lst;
      if (process == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(process.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.ProcessId == process.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.ProcessId = process.Id;
          item.Process = process;
        }
        process.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForProcess(IEnumerable<Process> processes) => this.GetForProcessAsync(processes).RunSynchronously();

    public async Task GetForProcessAsync(IEnumerable<Process> processes)
    {
      if (processes == null)
        return;
      foreach (Process process in processes)
        await this.GetForProcessAsync(process);
    }

    public void GetForProcessOwner(User processOwner) => this.GetForProcessOwnerAsync(processOwner).RunSynchronously();

    public async Task GetForProcessOwnerAsync(User processOwner)
    {
      List<Idea> lst;
      if (processOwner == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(processOwner.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.ProcessOwnerId == processOwner.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.ProcessOwnerId = processOwner.Id;
          item.ProcessOwner = processOwner;
        }
        processOwner.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForProcessOwner(IEnumerable<User> processOwners) => this.GetForProcessOwnerAsync(processOwners).RunSynchronously();

    public async Task GetForProcessOwnerAsync(IEnumerable<User> processOwners)
    {
      if (processOwners == null)
        return;
      foreach (User processOwner in processOwners)
        await this.GetForProcessOwnerAsync(processOwner);
    }

    public void GetForProcessPeak(ProcessPeak processPeak) => this.GetForProcessPeakAsync(processPeak).RunSynchronously();

    public async Task GetForProcessPeakAsync(ProcessPeak processPeak)
    {
      List<Idea> lst;
      if (processPeak == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(processPeak.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.ProcessPeakId == processPeak.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.ProcessPeakId = processPeak.Id;
          item.ProcessPeak = processPeak;
        }
        processPeak.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForProcessPeak(IEnumerable<ProcessPeak> processPeaks) => this.GetForProcessPeakAsync(processPeaks).RunSynchronously();

    public async Task GetForProcessPeakAsync(IEnumerable<ProcessPeak> processPeaks)
    {
      if (processPeaks == null)
        return;
      foreach (ProcessPeak processPeak in processPeaks)
        await this.GetForProcessPeakAsync(processPeak);
    }

    public void GetForProcessStability(ProcessStability processStability) => this.GetForProcessStabilityAsync(processStability).RunSynchronously();

    public async Task GetForProcessStabilityAsync(ProcessStability processStability)
    {
      List<Idea> lst;
      if (processStability == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(processStability.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.ProcessStabilityId == processStability.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.ProcessStabilityId = processStability.Id;
          item.ProcessStability = processStability;
        }
        processStability.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForProcessStability(IEnumerable<ProcessStability> processStabilities) => this.GetForProcessStabilityAsync(processStabilities).RunSynchronously();

    public async Task GetForProcessStabilityAsync(IEnumerable<ProcessStability> processStabilities)
    {
      if (processStabilities == null)
        return;
      foreach (ProcessStability processStability in processStabilities)
        await this.GetForProcessStabilityAsync(processStability);
    }

    public void GetForRule(Rule rule) => this.GetForRuleAsync(rule).RunSynchronously();

    public async Task GetForRuleAsync(Rule rule)
    {
      List<Idea> lst;
      if (rule == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(rule.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.RuleId == rule.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.RuleId = rule.Id;
          item.Rule = rule;
        }
        rule.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForRule(IEnumerable<Rule> rules) => this.GetForRuleAsync(rules).RunSynchronously();

    public async Task GetForRuleAsync(IEnumerable<Rule> rules)
    {
      if (rules == null)
        return;
      foreach (Rule rule in rules)
        await this.GetForRuleAsync(rule);
    }

    public void GetForRunningCost(RunningCost runningCost) => this.GetForRunningCostAsync(runningCost).RunSynchronously();

    public async Task GetForRunningCostAsync(RunningCost runningCost)
    {
      List<Idea> lst;
      if (runningCost == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(runningCost.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.RunningCostId == runningCost.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.RunningCostId = runningCost.Id;
          item.RunningCost = runningCost;
        }
        runningCost.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForRunningCost(IEnumerable<RunningCost> runningCosts) => this.GetForRunningCostAsync(runningCosts).RunSynchronously();

    public async Task GetForRunningCostAsync(IEnumerable<RunningCost> runningCosts)
    {
      if (runningCosts == null)
        return;
      foreach (RunningCost runningCost in runningCosts)
        await this.GetForRunningCostAsync(runningCost);
    }

    public void GetForSubmissionPath(SubmissionPath submissionPath) => this.GetForSubmissionPathAsync(submissionPath).RunSynchronously();

    public async Task GetForSubmissionPathAsync(SubmissionPath submissionPath)
    {
      List<Idea> lst;
      if (submissionPath == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(submissionPath.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.SubmissionPathId == submissionPath.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.SubmissionPathId = submissionPath.Id;
          item.SubmissionPath = submissionPath;
        }
        submissionPath.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForSubmissionPath(IEnumerable<SubmissionPath> submissionPaths) => this.GetForSubmissionPathAsync(submissionPaths).RunSynchronously();

    public async Task GetForSubmissionPathAsync(IEnumerable<SubmissionPath> submissionPaths)
    {
      if (submissionPaths == null)
        return;
      foreach (SubmissionPath submissionPath in submissionPaths)
        await this.GetForSubmissionPathAsync(submissionPath);
    }

    public void GetForTaskFrequency(TaskFrequency taskFrequency) => this.GetForTaskFrequencyAsync(taskFrequency).RunSynchronously();

    public async Task GetForTaskFrequencyAsync(TaskFrequency taskFrequency)
    {
      List<Idea> lst;
      if (taskFrequency == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(taskFrequency.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.TaskFrequencyId == taskFrequency.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.TaskFrequencyId = taskFrequency.Id;
          item.TaskFrequency = taskFrequency;
        }
        taskFrequency.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForTaskFrequency(IEnumerable<TaskFrequency> taskFrequencies) => this.GetForTaskFrequencyAsync(taskFrequencies).RunSynchronously();

    public async Task GetForTaskFrequencyAsync(IEnumerable<TaskFrequency> taskFrequencies)
    {
      if (taskFrequencies == null)
        return;
      foreach (TaskFrequency taskFrequency in taskFrequencies)
        await this.GetForTaskFrequencyAsync(taskFrequency);
    }

    public void GetForTeam(Team team) => this.GetForTeamAsync(team).RunSynchronously();

    public async Task GetForTeamAsync(Team team)
    {
      List<Idea> lst;
      if (team == null)
        lst = (List<Idea>) null;
      else if (string.IsNullOrWhiteSpace(team.Id))
      {
        lst = (List<Idea>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessIdeas.Where<Idea>((Func<Idea, bool>) (x => x.TeamId == team.Id)).OrderBy<Idea, string>((Func<Idea, string>) (x => x.Name)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.SubTitle)).ThenBy<Idea, string>((Func<Idea, string>) (x => x.Summary)).ToList<Idea>();
        dataSet = (DataSet) null;
        foreach (Idea item in lst)
        {
          item.TeamId = team.Id;
          item.Team = team;
        }
        team.Ideas = lst;
        lst = (List<Idea>) null;
      }
    }

    public void GetForTeam(IEnumerable<Team> teams) => this.GetForTeamAsync(teams).RunSynchronously();

    public async Task GetForTeamAsync(IEnumerable<Team> teams)
    {
      if (teams == null)
        return;
      foreach (Team team in teams)
        await this.GetForTeamAsync(team);
    }

    public void GetIdeaFor(IEnumerable<Collaborator> collaborators) => this.GetIdeaForAsync(collaborators).RunSynchronously();

    public async Task GetIdeaForAsync(IEnumerable<Collaborator> collaborators)
    {
      if (collaborators == null)
        return;
      foreach (Collaborator collaborator in collaborators)
        await this.GetIdeaForAsync(collaborator);
    }

    public void GetIdeaFor(Collaborator collaborator) => this.GetIdeaForAsync(collaborator).RunSynchronously();

    public async Task GetIdeaForAsync(Collaborator collaborator)
    {
      if (collaborator == null)
        ;
      else
      {
        Collaborator collaborator1 = collaborator;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        collaborator1.Idea = dataSet.BusinessIdeas.SingleOrDefault<Idea>((Func<Idea, bool>) (x => x.Id == collaborator.IdeaId));
        collaborator1 = (Collaborator) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetIdeaFor(IEnumerable<Comment> comments) => this.GetIdeaForAsync(comments).RunSynchronously();

    public async Task GetIdeaForAsync(IEnumerable<Comment> comments)
    {
      if (comments == null)
        return;
      foreach (Comment comment in comments)
        await this.GetIdeaForAsync(comment);
    }

    public void GetIdeaFor(Comment comment) => this.GetIdeaForAsync(comment).RunSynchronously();

    public async Task GetIdeaForAsync(Comment comment)
    {
      if (comment == null)
        ;
      else
      {
        Comment comment1 = comment;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        comment1.Idea = dataSet.BusinessIdeas.SingleOrDefault<Idea>((Func<Idea, bool>) (x => x.Id == comment.IdeaId));
        comment1 = (Comment) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetIdeaFor(IEnumerable<Document> documents) => this.GetIdeaForAsync(documents).RunSynchronously();

    public async Task GetIdeaForAsync(IEnumerable<Document> documents)
    {
      if (documents == null)
        return;
      foreach (Document document in documents)
        await this.GetIdeaForAsync(document);
    }

    public void GetIdeaFor(Document document) => this.GetIdeaForAsync(document).RunSynchronously();

    public async Task GetIdeaForAsync(Document document)
    {
      if (document == null)
        ;
      else
      {
        Document document1 = document;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        document1.Idea = dataSet.BusinessIdeas.SingleOrDefault<Idea>((Func<Idea, bool>) (x => x.Id == document.IdeaId));
        document1 = (Document) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetIdeaFor(IEnumerable<Follow> follows) => this.GetIdeaForAsync(follows).RunSynchronously();

    public async Task GetIdeaForAsync(IEnumerable<Follow> follows)
    {
      if (follows == null)
        return;
      foreach (Follow follow in follows)
        await this.GetIdeaForAsync(follow);
    }

    public void GetIdeaFor(Follow follow) => this.GetIdeaForAsync(follow).RunSynchronously();

    public async Task GetIdeaForAsync(Follow follow)
    {
      if (follow == null)
        ;
      else
      {
        Follow follow1 = follow;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        follow1.Idea = dataSet.BusinessIdeas.SingleOrDefault<Idea>((Func<Idea, bool>) (x => x.Id == follow.IdeaId));
        follow1 = (Follow) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetIdeaFor(
      IEnumerable<IdeaApplicationVersion> ideaApplicationVersions)
    {
      this.GetIdeaForAsync(ideaApplicationVersions).RunSynchronously();
    }

    public async Task GetIdeaForAsync(
      IEnumerable<IdeaApplicationVersion> ideaApplicationVersions)
    {
      if (ideaApplicationVersions == null)
        return;
      foreach (IdeaApplicationVersion ideaApplicationVersion in ideaApplicationVersions)
        await this.GetIdeaForAsync(ideaApplicationVersion);
    }

    public void GetIdeaFor(IdeaApplicationVersion ideaApplicationVersion) => this.GetIdeaForAsync(ideaApplicationVersion).RunSynchronously();

    public async Task GetIdeaForAsync(IdeaApplicationVersion ideaApplicationVersion)
    {
      if (ideaApplicationVersion == null)
        ;
      else
      {
        IdeaApplicationVersion applicationVersion = ideaApplicationVersion;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        applicationVersion.Idea = dataSet.BusinessIdeas.SingleOrDefault<Idea>((Func<Idea, bool>) (x => x.Id == ideaApplicationVersion.IdeaId));
        applicationVersion = (IdeaApplicationVersion) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetIdeaFor(
      IEnumerable<IdeaOtherRunningCost> ideaOtherRunningCosts)
    {
      this.GetIdeaForAsync(ideaOtherRunningCosts).RunSynchronously();
    }

    public async Task GetIdeaForAsync(
      IEnumerable<IdeaOtherRunningCost> ideaOtherRunningCosts)
    {
      if (ideaOtherRunningCosts == null)
        return;
      foreach (IdeaOtherRunningCost ideaOtherRunningCost in ideaOtherRunningCosts)
        await this.GetIdeaForAsync(ideaOtherRunningCost);
    }

    public void GetIdeaFor(IdeaOtherRunningCost ideaOtherRunningCost) => this.GetIdeaForAsync(ideaOtherRunningCost).RunSynchronously();

    public async Task GetIdeaForAsync(IdeaOtherRunningCost ideaOtherRunningCost)
    {
      if (ideaOtherRunningCost == null)
        ;
      else
      {
        IdeaOtherRunningCost otherRunningCost = ideaOtherRunningCost;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        otherRunningCost.Idea = dataSet.BusinessIdeas.SingleOrDefault<Idea>((Func<Idea, bool>) (x => x.Id == ideaOtherRunningCost.IdeaId));
        otherRunningCost = (IdeaOtherRunningCost) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetIdeaFor(IEnumerable<IdeaRunningCost> ideaRunningCosts) => this.GetIdeaForAsync(ideaRunningCosts).RunSynchronously();

    public async Task GetIdeaForAsync(IEnumerable<IdeaRunningCost> ideaRunningCosts)
    {
      if (ideaRunningCosts == null)
        return;
      foreach (IdeaRunningCost ideaRunningCost in ideaRunningCosts)
        await this.GetIdeaForAsync(ideaRunningCost);
    }

    public void GetIdeaFor(IdeaRunningCost ideaRunningCost) => this.GetIdeaForAsync(ideaRunningCost).RunSynchronously();

    public async Task GetIdeaForAsync(IdeaRunningCost ideaRunningCost)
    {
      if (ideaRunningCost == null)
        ;
      else
      {
        IdeaRunningCost ideaRunningCost1 = ideaRunningCost;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        ideaRunningCost1.Idea = dataSet.BusinessIdeas.SingleOrDefault<Idea>((Func<Idea, bool>) (x => x.Id == ideaRunningCost.IdeaId));
        ideaRunningCost1 = (IdeaRunningCost) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetIdeaFor(IEnumerable<IdeaStage> ideaStages) => this.GetIdeaForAsync(ideaStages).RunSynchronously();

    public async Task GetIdeaForAsync(IEnumerable<IdeaStage> ideaStages)
    {
      if (ideaStages == null)
        return;
      foreach (IdeaStage ideaStage in ideaStages)
        await this.GetIdeaForAsync(ideaStage);
    }

    public void GetIdeaFor(IdeaStage ideaStage) => this.GetIdeaForAsync(ideaStage).RunSynchronously();

    public async Task GetIdeaForAsync(IdeaStage ideaStage)
    {
      if (ideaStage == null)
        ;
      else
      {
        IdeaStage ideaStage1 = ideaStage;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        ideaStage1.Idea = dataSet.BusinessIdeas.SingleOrDefault<Idea>((Func<Idea, bool>) (x => x.Id == ideaStage.IdeaId));
        ideaStage1 = (IdeaStage) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetIdeaFor(IEnumerable<UserAuthorisation> userAuthorisations) => this.GetIdeaForAsync(userAuthorisations).RunSynchronously();

    public async Task GetIdeaForAsync(IEnumerable<UserAuthorisation> userAuthorisations)
    {
      if (userAuthorisations == null)
        return;
      foreach (UserAuthorisation userAuthorisation in userAuthorisations)
        await this.GetIdeaForAsync(userAuthorisation);
    }

    public void GetIdeaFor(UserAuthorisation userAuthorisation) => this.GetIdeaForAsync(userAuthorisation).RunSynchronously();

    public async Task GetIdeaForAsync(UserAuthorisation userAuthorisation)
    {
      if (userAuthorisation == null)
        ;
      else
      {
        UserAuthorisation userAuthorisation1 = userAuthorisation;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        userAuthorisation1.Idea = dataSet.BusinessIdeas.SingleOrDefault<Idea>((Func<Idea, bool>) (x => x.Id == userAuthorisation.IdeaId));
        userAuthorisation1 = (UserAuthorisation) null;
        dataSet = (DataSet) null;
      }
    }

    public void GetIdeaFor(IEnumerable<Vote> votes) => this.GetIdeaForAsync(votes).RunSynchronously();

    public async Task GetIdeaForAsync(IEnumerable<Vote> votes)
    {
      if (votes == null)
        return;
      foreach (Vote vote in votes)
        await this.GetIdeaForAsync(vote);
    }

    public void GetIdeaFor(Vote vote) => this.GetIdeaForAsync(vote).RunSynchronously();

    public async Task GetIdeaForAsync(Vote vote)
    {
      if (vote == null)
        ;
      else
      {
        Vote vote1 = vote;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        vote1.Idea = dataSet.BusinessIdeas.SingleOrDefault<Idea>((Func<Idea, bool>) (x => x.Id == vote.IdeaId));
        vote1 = (Vote) null;
        dataSet = (DataSet) null;
      }
    }

    public Idea GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Idea> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Idea) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessIdeas.SingleOrDefault<Idea>((Func<Idea, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Idea entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Idea entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Idea entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Idea> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Idea> entities)
    {
      if (entities == null)
        throw new DuplicateException("The ideas are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
