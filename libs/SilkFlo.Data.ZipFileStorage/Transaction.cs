// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Transaction
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Agency;
using SilkFlo.Data.Core.Domain.Application;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Domain.CRM;
using SilkFlo.Data.Core.Domain.Shared;
using SilkFlo.Data.Core.Domain.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace SilkFlo.Data.Persistence
{
  internal class Transaction
  {
    private readonly 
    
    List<TransactionItem> _items = new List<TransactionItem>();

    internal IReadOnlyList<TransactionItem> Items => (IReadOnlyList<TransactionItem>) this._items;

    internal static Transaction Begin() => new Transaction();

    internal void Add(Abstract entity, Action action) => this._items.Add(new TransactionItem()
    {
      Action = action,
      Entity = entity
    });

    internal Action GetAction(Abstract entity)
    {
      foreach (TransactionItem transactionItem in this._items)
      {
        if (transactionItem.Entity == entity)
          return transactionItem.Action;
      }
      return Action.NoAction;
    }

    internal void Commit(UnitOfWork unitOfWork) => this.CommitAsync(unitOfWork).RunSynchronously();

    internal async Task CommitAsync(UnitOfWork unitOfWork)
    {
      foreach (TransactionItem item in this._items)
      {
        Type type = item.Entity.GetType();
        if (type == typeof (Analytic))
        {
          int num1 = (int) await unitOfWork.DeleteAsync((Analytic) item.Entity);
        }
        if (type == typeof (Log))
        {
          int num2 = (int) await unitOfWork.DeleteAsync((Log) item.Entity);
        }
        if (type == typeof (Message))
        {
          int num3 = (int) await unitOfWork.DeleteAsync((Message) item.Entity);
        }
        if (type == typeof (SilkFlo.Data.Core.Domain.Role))
        {
          int num4 = (int) await unitOfWork.DeleteAsync((SilkFlo.Data.Core.Domain.Role) item.Entity);
        }
        if (type == typeof (User))
        {
          int num5 = (int) await unitOfWork.DeleteAsync((User) item.Entity);
        }
        if (type == typeof (UserAchievement))
        {
          int num6 = (int) await unitOfWork.DeleteAsync((UserAchievement) item.Entity);
        }
        if (type == typeof (UserBadge))
        {
          int num7 = (int) await unitOfWork.DeleteAsync((UserBadge) item.Entity);
        }
        if (type == typeof (UserRole))
        {
          int num8 = (int) await unitOfWork.DeleteAsync((UserRole) item.Entity);
        }
        if (type == typeof (WebHookLog))
        {
          int num9 = (int) await unitOfWork.DeleteAsync((WebHookLog) item.Entity);
        }
        if (type == typeof (ManageTenant))
        {
          int num10 = (int) await unitOfWork.DeleteAsync((ManageTenant) item.Entity);
        }
        if (type == typeof (HotSpot))
        {
          int num11 = (int) await unitOfWork.DeleteAsync((HotSpot) item.Entity);
        }
        if (type == typeof (Page))
        {
          int num12 = (int) await unitOfWork.DeleteAsync((Page) item.Entity);
        }
        if (type == typeof (Setting))
        {
          int num13 = (int) await unitOfWork.DeleteAsync((Setting) item.Entity);
        }
        if (type == typeof (SilkFlo.Data.Core.Domain.Business.Application))
        {
          int num14 = (int) await unitOfWork.DeleteAsync((SilkFlo.Data.Core.Domain.Business.Application) item.Entity);
        }
        if (type == typeof (Client))
        {
          int num15 = (int) await unitOfWork.DeleteAsync((Client) item.Entity);
        }
        if (type == typeof (Collaborator))
        {
          int num16 = (int) await unitOfWork.DeleteAsync((Collaborator) item.Entity);
        }
        if (type == typeof (CollaboratorRole))
        {
          int num17 = (int) await unitOfWork.DeleteAsync((CollaboratorRole) item.Entity);
        }
        if (type == typeof (Comment))
        {
          int num18 = (int) await unitOfWork.DeleteAsync((Comment) item.Entity);
        }
        if (type == typeof (Department))
        {
          int num19 = (int) await unitOfWork.DeleteAsync((Department) item.Entity);
        }
        if (type == typeof (Document))
        {
          int num20 = (int) await unitOfWork.DeleteAsync((Document) item.Entity);
        }
        if (type == typeof (Follow))
        {
          int num21 = (int) await unitOfWork.DeleteAsync((Follow) item.Entity);
        }
        if (type == typeof (Idea))
        {
          int num22 = (int) await unitOfWork.DeleteAsync((Idea) item.Entity);
        }
        if (type == typeof (IdeaApplicationVersion))
        {
          int num23 = (int) await unitOfWork.DeleteAsync((IdeaApplicationVersion) item.Entity);
        }
        if (type == typeof (IdeaOtherRunningCost))
        {
          int num24 = (int) await unitOfWork.DeleteAsync((IdeaOtherRunningCost) item.Entity);
        }
        if (type == typeof (IdeaRunningCost))
        {
          int num25 = (int) await unitOfWork.DeleteAsync((IdeaRunningCost) item.Entity);
        }
        if (type == typeof (IdeaStage))
        {
          int num26 = (int) await unitOfWork.DeleteAsync((IdeaStage) item.Entity);
        }
        if (type == typeof (IdeaStageStatus))
        {
          int num27 = (int) await unitOfWork.DeleteAsync((IdeaStageStatus) item.Entity);
        }
        if (type == typeof (ImplementationCost))
        {
          int num28 = (int) await unitOfWork.DeleteAsync((ImplementationCost) item.Entity);
        }
        if (type == typeof (Location))
        {
          int num29 = (int) await unitOfWork.DeleteAsync((Location) item.Entity);
        }
        if (type == typeof (OtherRunningCost))
        {
          int num30 = (int) await unitOfWork.DeleteAsync((OtherRunningCost) item.Entity);
        }
        if (type == typeof (Process))
        {
          int num31 = (int) await unitOfWork.DeleteAsync((Process) item.Entity);
        }
        if (type == typeof (Recipient))
        {
          int num32 = (int) await unitOfWork.DeleteAsync((Recipient) item.Entity);
        }
        if (type == typeof (SilkFlo.Data.Core.Domain.Business.Role))
        {
          int num33 = (int) await unitOfWork.DeleteAsync((SilkFlo.Data.Core.Domain.Business.Role) item.Entity);
        }
        if (type == typeof (RoleCost))
        {
          int num34 = (int) await unitOfWork.DeleteAsync((RoleCost) item.Entity);
        }
        if (type == typeof (RoleIdeaAuthorisation))
        {
          int num35 = (int) await unitOfWork.DeleteAsync((RoleIdeaAuthorisation) item.Entity);
        }
        if (type == typeof (RunningCost))
        {
          int num36 = (int) await unitOfWork.DeleteAsync((RunningCost) item.Entity);
        }
        if (type == typeof (SoftwareVender))
        {
          int num37 = (int) await unitOfWork.DeleteAsync((SoftwareVender) item.Entity);
        }
        if (type == typeof (Team))
        {
          int num38 = (int) await unitOfWork.DeleteAsync((Team) item.Entity);
        }
        if (type == typeof (UserAuthorisation))
        {
          int num39 = (int) await unitOfWork.DeleteAsync((UserAuthorisation) item.Entity);
        }
        if (type == typeof (SilkFlo.Data.Core.Domain.Business.Version))
        {
          int num40 = (int) await unitOfWork.DeleteAsync((SilkFlo.Data.Core.Domain.Business.Version) item.Entity);
        }
        if (type == typeof (Vote))
        {
          int num41 = (int) await unitOfWork.DeleteAsync((Vote) item.Entity);
        }
        if (type == typeof (CompanySize))
        {
          int num42 = (int) await unitOfWork.DeleteAsync((CompanySize) item.Entity);
        }
        if (type == typeof (JobLevel))
        {
          int num43 = (int) await unitOfWork.DeleteAsync((JobLevel) item.Entity);
        }
        if (type == typeof (Prospect))
        {
          int num44 = (int) await unitOfWork.DeleteAsync((Prospect) item.Entity);
        }
        if (type == typeof (Achievement))
        {
          int num45 = (int) await unitOfWork.DeleteAsync((Achievement) item.Entity);
        }
        if (type == typeof (ApplicationStability))
        {
          int num46 = (int) await unitOfWork.DeleteAsync((ApplicationStability) item.Entity);
        }
        if (type == typeof (AutomationGoal))
        {
          int num47 = (int) await unitOfWork.DeleteAsync((AutomationGoal) item.Entity);
        }
        if (type == typeof (AutomationType))
        {
          int num48 = (int) await unitOfWork.DeleteAsync((AutomationType) item.Entity);
        }
        if (type == typeof (AverageNumberOfStep))
        {
          int num49 = (int) await unitOfWork.DeleteAsync((AverageNumberOfStep) item.Entity);
        }
        if (type == typeof (Badge))
        {
          int num50 = (int) await unitOfWork.DeleteAsync((Badge) item.Entity);
        }
        if (type == typeof (ClientType))
        {
          int num51 = (int) await unitOfWork.DeleteAsync((ClientType) item.Entity);
        }
        if (type == typeof (CostType))
        {
          int num52 = (int) await unitOfWork.DeleteAsync((CostType) item.Entity);
        }
        if (type == typeof (Country))
        {
          int num53 = (int) await unitOfWork.DeleteAsync((Country) item.Entity);
        }
        if (type == typeof (DataInputPercentOfStructured))
        {
          int num54 = (int) await unitOfWork.DeleteAsync((DataInputPercentOfStructured) item.Entity);
        }
        if (type == typeof (DecisionCount))
        {
          int num55 = (int) await unitOfWork.DeleteAsync((DecisionCount) item.Entity);
        }
        if (type == typeof (DecisionDifficulty))
        {
          int num56 = (int) await unitOfWork.DeleteAsync((DecisionDifficulty) item.Entity);
        }
        if (type == typeof (DocumentationPresent))
        {
          int num57 = (int) await unitOfWork.DeleteAsync((DocumentationPresent) item.Entity);
        }
        if (type == typeof (IdeaAuthorisation))
        {
          int num58 = (int) await unitOfWork.DeleteAsync((IdeaAuthorisation) item.Entity);
        }
        if (type == typeof (IdeaStatus))
        {
          int num59 = (int) await unitOfWork.DeleteAsync((IdeaStatus) item.Entity);
        }
        if (type == typeof (Industry))
        {
          int num60 = (int) await unitOfWork.DeleteAsync((Industry) item.Entity);
        }
        if (type == typeof (Input))
        {
          int num61 = (int) await unitOfWork.DeleteAsync((Input) item.Entity);
        }
        if (type == typeof (InputDataStructure))
        {
          int num62 = (int) await unitOfWork.DeleteAsync((InputDataStructure) item.Entity);
        }
        if (type == typeof (Language))
        {
          int num63 = (int) await unitOfWork.DeleteAsync((Language) item.Entity);
        }
        if (type == typeof (NumberOfWaysToCompleteProcess))
        {
          int num64 = (int) await unitOfWork.DeleteAsync((NumberOfWaysToCompleteProcess) item.Entity);
        }
        if (type == typeof (Period))
        {
          int num65 = (int) await unitOfWork.DeleteAsync((Period) item.Entity);
        }
        if (type == typeof (ProcessPeak))
        {
          int num66 = (int) await unitOfWork.DeleteAsync((ProcessPeak) item.Entity);
        }
        if (type == typeof (ProcessStability))
        {
          int num67 = (int) await unitOfWork.DeleteAsync((ProcessStability) item.Entity);
        }
        if (type == typeof (Rule))
        {
          int num68 = (int) await unitOfWork.DeleteAsync((Rule) item.Entity);
        }
        if (type == typeof (Stage))
        {
          int num69 = (int) await unitOfWork.DeleteAsync((Stage) item.Entity);
        }
        if (type == typeof (StageGroup))
        {
          int num70 = (int) await unitOfWork.DeleteAsync((StageGroup) item.Entity);
        }
        if (type == typeof (SubmissionPath))
        {
          int num71 = (int) await unitOfWork.DeleteAsync((SubmissionPath) item.Entity);
        }
        if (type == typeof (TaskFrequency))
        {
          int num72 = (int) await unitOfWork.DeleteAsync((TaskFrequency) item.Entity);
        }
        if (type == typeof (Coupon))
        {
          int num73 = (int) await unitOfWork.DeleteAsync((Coupon) item.Entity);
        }
        if (type == typeof (Currency))
        {
          int num74 = (int) await unitOfWork.DeleteAsync((Currency) item.Entity);
        }
        if (type == typeof (Discount))
        {
          int num75 = (int) await unitOfWork.DeleteAsync((Discount) item.Entity);
        }
        if (type == typeof (Price))
        {
          int num76 = (int) await unitOfWork.DeleteAsync((Price) item.Entity);
        }
        if (type == typeof (Product))
        {
          int num77 = (int) await unitOfWork.DeleteAsync((Product) item.Entity);
        }
        if (type == typeof (Subscription))
        {
          int num78 = (int) await unitOfWork.DeleteAsync((Subscription) item.Entity);
        }
        type = (Type) null;
      }
    }
  }
}
