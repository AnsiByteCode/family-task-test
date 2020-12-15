using Domain.ViewModel;
using System.Collections.Generic;

namespace Domain.Queries
{
    /// <summary>
    /// GetAllTasks Query Result
    /// </summary>
    public class GetAllTasksQueryResult
    {
        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public IEnumerable<TaskVm> Payload { get; set; }
    }
}
