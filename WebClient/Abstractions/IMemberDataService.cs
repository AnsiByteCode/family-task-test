using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebClient.Abstractions
{
    /// <summary>
    /// IMember Data Service
    /// </summary>
    public interface IMemberDataService
    {
        /// <summary>
        /// Gets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        IEnumerable<MemberVm> Members { get; }
        /// <summary>
        /// Gets the selected member.
        /// </summary>
        /// <value>
        /// The selected member.
        /// </value>
        MemberVm SelectedMember { get; }

        /// <summary>
        /// Occurs when [members changed].
        /// </summary>
        event EventHandler MembersChanged;
        /// <summary>
        /// Occurs when [selected member changed].
        /// </summary>
        event EventHandler SelectedMemberChanged;
        /// <summary>
        /// Occurs when [update member failed].
        /// </summary>
        event EventHandler<string> UpdateMemberFailed;
        /// <summary>
        /// Occurs when [create member failed].
        /// </summary>
        event EventHandler<string> CreateMemberFailed;


        /// <summary>
        /// Updates the member.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task UpdateMember(MemberVm model);
        /// <summary>
        /// Creates the member.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task CreateMember(MemberVm model);
        /// <summary>
        /// Selects the member.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void SelectMember(Guid id);
        /// <summary>
        /// Selects the null member.
        /// </summary>
        void SelectNullMember();

    }
}
