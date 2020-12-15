using Core.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataLayer
{
    /// <summary>
    /// Task Repository
    /// </summary>
    /// <seealso cref="DataLayer.BaseRepository{System.Guid, Domain.DataModels.Task, DataLayer.Repositories.TaskRepository}" />
    /// <seealso cref="Core.Abstractions.Repositories.ITaskRepository" />
    public class TaskRepository : BaseRepository<Guid, Domain.DataModels.Task, TaskRepository>, ITaskRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public TaskRepository(FamilyTaskContext context) : base(context)
        { }

        /// <summary>
        /// Make the query Non Tracking
        /// </summary>
        /// <returns>
        /// <see cref="T:Core.Abstractions.Repositories.IBaseRepository`3" />
        /// </returns>
        ITaskRepository IBaseRepository<Guid, Domain.DataModels.Task, ITaskRepository>.NoTrack()
        {
            return base.NoTrack();
        }

        /// <summary>
        /// Used to reset the Query for QueryBuilder.
        /// </summary>
        /// <returns>
        /// <see cref="!:TRepository" />
        /// </returns>
        ITaskRepository IBaseRepository<Guid, Domain.DataModels.Task, ITaskRepository>.Reset()
        {
            return base.Reset();
        }
        /// <summary>
        /// Gets all tasks with member asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Domain.DataModels.Task>> GetAllTasksWithMemberAsync(CancellationToken cancellationToken = default)
        {
            var result = await Query.Include(t => t.AssignedMember).ToListAsync(cancellationToken);
            return result;
        }
    }
}
