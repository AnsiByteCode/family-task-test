using Domain.Commands;
using Domain.Queries;
using System.Threading.Tasks;

namespace Core.Abstractions.Services
{
    /// <summary>
    /// ITaskService interface
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Gets all tasks query handler.
        /// </summary>
        /// <returns></returns>
        Task<GetAllTasksQueryResult> GetAllTasksQueryHandler();
        /// <summary>
        /// Creates the task command handler.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command);
        /// <summary>
        /// Assigns the task command handler.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        Task<AssignTaskCommandResult> AssignTaskCommandHandler(AssignTaskCommand command);
        /// <summary>
        /// Completes the task command handler.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        Task<CompleteTaskCommandResult> CompleteTaskCommandHandler(CompleteTaskCommand command);

    }
}
