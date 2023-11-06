using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Models.Business
{
    public partial class Collaborator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collaborators"></param>
        /// <param name="ideaId"></param>
        /// <param name="invitedById"></param>
        /// <param name="unitOfWork"></param>
        /// <returns>true = save me</returns>
        public static async Task UpdateAsync(
            Data.Core.IUnitOfWork unitOfWork,
            List<Collaborator> collaborators,
            string ideaId,
            string invitedById = "")
        {
            if (collaborators == null || collaborators.Count <= 0)
                return;

            // Get the existing.
            // We need some field content, before deleting them
            var cores = (await unitOfWork
                    .BusinessCollaborators
                    .FindAsync(x => x.IdeaId == ideaId))
                    .ToArray();



            // Prepare
            foreach (var model in collaborators)
            {
                var core = cores.SingleOrDefault(x => x.UserId == model.UserId
                                                      && x.IdeaId == ideaId);
                if (core == null)
                    continue;

                model.IsInvitationConfirmed = core.IsInvitationConfirmed;
                model.InvitedById = core.InvitedById;
            }



            // Remove existing
            // This will also remove connected Business.CollaboratorRole rows
            await unitOfWork.BusinessCollaborators.RemoveRangeAsync(cores);


            // The content of this will be used to populate the Business.UserAuthorisation table.
            var newUserAuthorisation = new List<Data.Core.Domain.Business.UserAuthorisation>();



            foreach (var collaborator in collaborators)
            {
                if (collaborator.CollaboratorRoles == null)
                    continue;

                var collaboratorRoles = new List<CollaboratorRole>();

                var core = collaborator.GetCore();
                core.IdeaId = ideaId;

                if (string.IsNullOrWhiteSpace(core.InvitedById))
                    core.InvitedById = invitedById;

                await unitOfWork.AddAsync(core);
                foreach (var collaboratorRole in collaborator.CollaboratorRoles)
                {
                    var collaboratorRoleCore = collaboratorRole.GetCore();
                    collaboratorRoleCore.Collaborator = collaborator.GetCore();
                    await unitOfWork.AddAsync(collaboratorRoleCore);

                    // This will be used to add userAuthorisations
                    if (collaboratorRoles.All(x => x.RoleId != collaboratorRole.RoleId))
                        collaboratorRoles.Add(collaboratorRole);
                }



                // Remove the userAuthorisation from the de-normalized table
                var userAuthorisations =
                    (await unitOfWork.BusinessUserAuthorisations
                        .FindAsync(x => x.UserId == collaborator.UserId && x.IdeaId == ideaId)).ToList();

                await unitOfWork.BusinessUserAuthorisations.RemoveRangeAsync(userAuthorisations);


                // Create Business.UserAuthorisation records
                foreach (var collaboratorRole in collaboratorRoles)
                {
                    var roleIdeaAuthorisation =
                        await unitOfWork
                            .BusinessRoleIdeaAuthorisations
                            .SingleOrDefaultAsync(x => x.RoleId == collaboratorRole.RoleId);

                    if (newUserAuthorisation
                        .Any(x => x.UserId == collaborator.UserId
                                  && x.IdeaId == ideaId
                                  && x.IdeaAuthorisationId == roleIdeaAuthorisation.IdeaAuthorisationId))
                        continue;

                    var userAuthorisation = new Data.Core.Domain.Business.UserAuthorisation
                    {
                        UserId = collaborator.UserId,
                        IdeaId = ideaId,
                        CollaboratorRoleId = collaboratorRole.Id,
                        IdeaAuthorisationId = roleIdeaAuthorisation.IdeaAuthorisationId
                    };

                    // Add the userAuthorisation to the de-normalized table
                    newUserAuthorisation.Add(userAuthorisation);
                }
            }

            // Add the userAuthorisations to the de-normalized table
            await unitOfWork.AddAsync(newUserAuthorisation);
        }




        public static async Task<List<User>> GetUsersAsync(
            Data.Core.IUnitOfWork unitOfWork,
            string ideaId)
        {
            // Get Collaborating Users
            var collaborators = 
                (await unitOfWork.BusinessCollaborators
                    .FindAsync(x => x.IdeaId == ideaId))
                .ToArray();

            await unitOfWork.Users.GetUserForAsync(collaborators);
            var models = Collaborator.Create(collaborators);

            var users = new List<User>();

            foreach (var collaborator in models)
            {
                await unitOfWork.BusinessCollaboratorRoles.GetForCollaboratorAsync(collaborator.GetCore());
                await unitOfWork.BusinessRoles.GetRoleForAsync(collaborator.GetCore().CollaboratorRoles);

                foreach (var collaboratorRole in collaborator.CollaboratorRoles)
                    collaborator.User.BusinessRoles.Add(collaboratorRole.Role);

                await unitOfWork.UserRoles.GetForUserAsync(collaborator.GetCore().User);

                users.Add(collaborator.User);
            }

            return users;
        }
    }
}