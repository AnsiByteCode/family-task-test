using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Abstractions.Repositories
{
    /// <summary>
    /// ITaskRepository interface
    /// </summary>
    /// <seealso cref="Core.Abstractions.Repositories.IBaseRepository{System.Guid, Domain.DataModels.Task, Core.Abstractions.Repositories.ITaskRepository}" />
    public interface ITaskRepository : IBaseRepository<Guid, Domain.DataModels.Task, ITaskRepository>
    {
        /// <summary>
        /// Gets all tasks with member asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<Domain.DataModels.Task>> GetAllTasksWithMemberAsync(CancellationToken cancellationToken = default);
    }
}
