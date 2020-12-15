using Domain.Commands;
using Domain.Queries;
using System.Threading.Tasks;

namespace Core.Abstractions.Services
{
    /// <summary>
    /// IMemberService interface
    /// </summary>
    public interface IMemberService
    {
        /// <summary>
        /// Creates the member command handler.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        Task<CreateMemberCommandResult> CreateMemberCommandHandler(CreateMemberCommand command);
        /// <summary>
        /// Updates the member command handler.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        Task<UpdateMemberCommandResult> UpdateMemberCommandHandler(UpdateMemberCommand command);
        /// <summary>
        /// Gets all members query handler.
        /// </summary>
        /// <returns></returns>
        Task<GetAllMembersQueryResult> GetAllMembersQueryHandler();
    }
}
