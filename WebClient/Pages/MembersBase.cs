using Domain.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Abstractions;

namespace WebClient.Pages
{
    /// <summary>
    /// Members Base
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public class MembersBase: ComponentBase
    {
        /// <summary>
        /// The members
        /// </summary>
        protected List<MemberVm> members = new List<MemberVm>();
        /// <summary>
        /// The left menu item
        /// </summary>
        protected List<MenuItem> leftMenuItem = new List<MenuItem>();

        /// <summary>
        /// The show creator
        /// </summary>
        protected bool showCreator;
        /// <summary>
        /// The is loaded
        /// </summary>
        protected bool isLoaded;

        /// <summary>
        /// Gets or sets the member data service.
        /// </summary>
        /// <value>
        /// The member data service.
        /// </value>
        [Inject]
        public IMemberDataService MemberDataService { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            UpdateMembers();
            ReloadMenu();

            MemberDataService.MembersChanged += MemberDataService_MembersChanged;
            showCreator = true;
            isLoaded = true;
        }

        /// <summary>
        /// Handles the MembersChanged event of the MemberDataService control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MemberDataService_MembersChanged(object sender, EventArgs e)
        {
            UpdateMembers();
            ReloadMenu();

            showCreator = true;
            isLoaded = true;

            StateHasChanged();
        }

        /// <summary>
        /// Updates the members.
        /// </summary>
        void UpdateMembers()
        {
            var result = MemberDataService.Members;

            if (result.Any())
            {
                members = result.ToList();
            }
        }

        /// <summary>
        /// Reloads the menu.
        /// </summary>
        void ReloadMenu()
        {
            for (int i = 0; i < members.Count; i++)
            {
                leftMenuItem.Add(new MenuItem
                {
                    iconColor = members[i].Avatar,
                    label = members[i].FirstName,
                    referenceId = members[i].Id
                });
            }
        }

        /// <summary>
        /// Ons the add item.
        /// </summary>
        protected void onAddItem()
        {
            showCreator = true;
            StateHasChanged();
        }

        /// <summary>
        /// Ons the member add.
        /// </summary>
        /// <param name="familyMember">The family member.</param>
        protected void onMemberAdd(MemberVm familyMember)
        {
            MemberDataService.CreateMember(familyMember);            
        }

    }
}
