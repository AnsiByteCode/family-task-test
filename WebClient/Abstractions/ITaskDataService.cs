using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebClient.Abstractions
{
    /// <summary>
    /// ITask Data Service
    /// </summary>
    public interface ITaskDataService
    {
        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <value>
        /// The tasks.
        /// </value>
        IEnumerable<TaskVm> Tasks { get; }
        /// <summary>
        /// Gets the selected task.
        /// </summary>
        /// <value>
        /// The selected task.
        /// </value>
        TaskVm SelectedTask { get; }
        /// <summary>
        /// Gets the draged task.
        /// </summary>
        /// <value>
        /// The draged task.
        /// </value>
        TaskVm DragedTask { get; }

        /// <summary>
        /// Occurs when [create task failed].
        /// </summary>
        event EventHandler<string> CreateTaskFailed;
        /// <summary>
        /// Occurs when [tasks updated].
        /// </summary>
        event EventHandler TasksUpdated;
        /// <summary>
        /// Occurs when [complete task failed].
        /// </summary>
        event EventHandler<string> CompleteTaskFailed;

        /// <summary>
        /// Creates the task.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task CreateTask(TaskVm model);
        /// <summary>
        /// Assigns the task.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task AssignTask(TaskVm model);
        /// <summary>
        /// Toggles the task.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task ToggleTask(Guid id);
        /// <summary>
        /// Selects the draged task.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void SelectDragedTask(Guid id);
    }
}