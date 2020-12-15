using Core.Extensions.ModelConversion;
using Domain.Commands;
using Domain.Queries;
using Domain.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Abstractions;

namespace WebClient.Services
{
    /// <summary>
    /// Task Data Service
    /// </summary>
    /// <seealso cref="WebClient.Abstractions.ITaskDataService" />
    public class TaskDataService : ITaskDataService
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;
        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <value>
        /// The tasks.
        /// </value>
        public IEnumerable<TaskVm> Tasks { get; private set; }
        /// <summary>
        /// Gets the selected task.
        /// </summary>
        /// <value>
        /// The selected task.
        /// </value>
        public TaskVm SelectedTask { get; private set; }
        /// <summary>
        /// Gets the draged task.
        /// </summary>
        /// <value>
        /// The draged task.
        /// </value>
        public TaskVm DragedTask { get; private set; }

        /// <summary>
        /// Occurs when [create task failed].
        /// </summary>
        public event EventHandler<string> CreateTaskFailed;

        /// <summary>
        /// Occurs when [tasks updated].
        /// </summary>
        public event EventHandler TasksUpdated;
        
        /// <summary>
        /// Occurs when [complete task failed].
        /// </summary>
        public event EventHandler<string> CompleteTaskFailed;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDataService"/> class.
        /// </summary>
        /// <param name="clientFactory">The client factory.</param>
        public TaskDataService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            Tasks = new List<TaskVm>();
            LoadTasks();
        }

        /// <summary>
        /// Selects the draged task.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void SelectDragedTask(Guid id)
        {
            foreach (var taskModel in Tasks)
                DragedTask = Tasks.SingleOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Loads the tasks.
        /// </summary>
        private async void LoadTasks()
        {
            var tasks = (await GetAllTasks()).Payload;
            if (tasks != null)
            {
                Tasks = tasks;
                TasksUpdated?.Invoke(this, null);
                return;
            }
            CreateTaskFailed?.Invoke(this, "Load tasks operation failed");
        }
        /// <summary>
        /// Creates the task.
        /// </summary>
        /// <param name="model">The model.</param>
        public async Task CreateTask(TaskVm model)
        {
            var createdTaskResult = await Create(model.ToCreateTaskCommand());
            if (createdTaskResult != null)
            {
                LoadTasks();
            }
            else
            {
                CreateTaskFailed?.Invoke(this, "Create task operation failed.");
            }
        }

        /// <summary>
        /// Assigns the task.
        /// </summary>
        /// <param name="model">The model.</param>
        public async Task AssignTask(TaskVm model)
        {
            var assignTaskResult = await Assign(model.ToAssignTaskCommand());
            if (assignTaskResult != null && assignTaskResult.Succeed)
            {
                LoadTasks();
            }
            else
            {
                CreateTaskFailed?.Invoke(this, "Update task operation failed.");
            }
        }

        /// <summary>
        /// Toggles the task.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentNullException">Please select task</exception>
        public async Task ToggleTask(Guid id)
        {
            var taskViewModel = Tasks.Where(t => t.Id == id).FirstOrDefault();
            if (taskViewModel == null)
                throw new ArgumentNullException("Please select task");
            var result = await Complete(taskViewModel.ToCompleteTaskCommand());
            if (result != null && result.Succeed)
            {
                taskViewModel.IsComplete = true;
                TasksUpdated?.Invoke(this, null);
            }
            else
            {
                CompleteTaskFailed.Invoke(this, "Unable to complete task.");
            }
        }

        /// <summary>
        /// Creates the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        private async Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
        {
            return await _httpClient.PostJsonAsync<CreateTaskCommandResult>("tasks", command);
        }

        /// <summary>
        /// Assigns the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        private async Task<AssignTaskCommandResult> Assign(AssignTaskCommand command)
        {
            return await _httpClient.PutJsonAsync<AssignTaskCommandResult>("tasks/assign", command);
        }

        /// <summary>
        /// Completes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        private async Task<CompleteTaskCommandResult> Complete(CompleteTaskCommand command)
        {
            return await _httpClient.PutJsonAsync<CompleteTaskCommandResult>("tasks/complete", command);
        }

        /// <summary>
        /// Gets all tasks.
        /// </summary>
        /// <returns></returns>
        private async Task<GetAllTasksQueryResult> GetAllTasks()
        {
            return await _httpClient.GetJsonAsync<GetAllTasksQueryResult>("tasks");
        }
    }
}