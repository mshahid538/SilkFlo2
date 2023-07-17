using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface ICollaboratorRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Collaborator> GetAsync(string id);

    Task<Collaborator> SingleOrDefaultAsync(Func<Collaborator, bool> predicate);

    Task<bool> AddAsync(Collaborator entity);

    Task<bool> AddRangeAsync(IEnumerable<Collaborator> entities);

    Task<IEnumerable<Collaborator>> GetAllAsync();

    Task<IEnumerable<Collaborator>> FindAsync(Func<Collaborator, bool> predicate);

    Task GetForIdeaAsync(Idea idea);

    Task GetForIdeaAsync(IEnumerable<Idea> ideas);

    Task GetForInvitedByAsync(User invitedBy);

    Task GetForInvitedByAsync(IEnumerable<User> invitedBies);

    Task GetForUserAsync(User user);

    Task GetForUserAsync(IEnumerable<User> users);

    Task GetCollaboratorForAsync(CollaboratorRole collaboratorRole);

    Task GetCollaboratorForAsync(IEnumerable<CollaboratorRole> collaboratorRoles);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Collaborator entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Collaborator> entities);
  }
}
