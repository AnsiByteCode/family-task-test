using Domain.Commands;
using Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebClient.Abstractions;
using Microsoft.AspNetCore.Components;
using Domain.ViewModel;
using Core.Extensions.ModelConversion;

namespace WebClient.Services
{
    /// <summary>
    /// Member Data Service
    /// </summary>
    /// <seealso cref="WebClient.Abstractions.IMemberDataService" />
    public class MemberDataService : IMemberDataService
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient httpClient;
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberDataService"/> class.
        /// </summary>
        /// <param name="clientFactory">The client factory.</param>
        public MemberDataService(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            members = new List<MemberVm>();
            LoadMembers();
        }
        /// <summary>
        /// The members
        /// </summary>
        private IEnumerable<MemberVm> members;

        /// <summary>
        /// Gets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        public IEnumerable<MemberVm> Members => members;

        /// <summary>
        /// Gets the selected member.
        /// </summary>
        /// <value>
        /// The selected member.
        /// </value>
        public MemberVm SelectedMember { get; private set; }

        /// <summary>
        /// Occurs when [members changed].
        /// </summary>
        public event EventHandler MembersChanged;
        /// <summary>
        /// Occurs when [update member failed].
        /// </summary>
        public event EventHandler<string> UpdateMemberFailed;
        /// <summary>
        /// Occurs when [create member failed].
        /// </summary>
        public event EventHandler<string> CreateMemberFailed;
        /// <summary>
        /// Occurs when [selected member changed].
        /// </summary>
        public event EventHandler SelectedMemberChanged;

        /// <summary>
        /// Loads the members.
        /// </summary>
        private async void LoadMembers()
        {
            members = (await GetAllMembers()).Payload;
            MembersChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Creates the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        private async Task<CreateMemberCommandResult> Create(CreateMemberCommand command)
        {
            return await httpClient.PostJsonAsync<CreateMemberCommandResult>("members", command);
        }

        /// <summary>
        /// Gets all members.
        /// </summary>
        /// <returns></returns>
        private async Task<GetAllMembersQueryResult> GetAllMembers()
        {
            return await httpClient.GetJsonAsync<GetAllMembersQueryResult>("members");
        }

        /// <summary>
        /// Updates the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        private async Task<UpdateMemberCommandResult> Update(UpdateMemberCommand command)
        {
            return await httpClient.PutJsonAsync<UpdateMemberCommandResult>($"members/{command.Id}", command);
        }

        /// <summary>
        /// Updates the member.
        /// </summary>
        /// <param name="model">The model.</param>
        public async Task UpdateMember(MemberVm model)
        {
            var result = await Update(model.ToUpdateMemberCommand());

            Console.WriteLine(JsonSerializer.Serialize(result));

            if (result != null)
            {
                var updatedList = (await GetAllMembers()).Payload;

                if (updatedList != null)
                {
                    members = updatedList;
                    MembersChanged?.Invoke(this, null);
                    return;
                }
                UpdateMemberFailed?.Invoke(this, "The save was successful, but we can no longer get an updated list of members from the server.");
            }

            UpdateMemberFailed?.Invoke(this, "Unable to save changes.");
        }

        /// <summary>
        /// Creates the member.
        /// </summary>
        /// <param name="model">The model.</param>
        public async Task CreateMember(MemberVm model)
        {
            var result = await Create(model.ToCreateMemberCommand());
            if (result != null)
            {
                var updatedList = (await GetAllMembers()).Payload;

                if (updatedList != null)
                {
                    members = updatedList;
                    MembersChanged?.Invoke(this, null);
                    return;
                }
                UpdateMemberFailed?.Invoke(this, "The creation was successful, but we can no longer get an updated list of members from the server.");
            }

            UpdateMemberFailed?.Invoke(this, "Unable to create record.");
        }

        /// <summary>
        /// Selects the member.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void SelectMember(Guid id)
        {
            if (members.All(memberVm => memberVm.Id != id && id != Guid.Empty)) return;
            {
                if (id == Guid.Empty)
                {
                    SelectedMember = new MemberVm { Id = id };
                }
                else
                {
                    SelectedMember = members.SingleOrDefault(memberVm => memberVm.Id == id);
                }
                SelectedMemberChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Selects the null member.
        /// </summary>
        public void SelectNullMember()
        {
            SelectedMember = null;
            SelectedMemberChanged?.Invoke(this, null);
        }
    }
}
