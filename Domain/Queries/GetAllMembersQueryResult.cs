using Domain.ViewModel;
using System.Collections.Generic;

namespace Domain.Queries
{
    /// <summary>
    /// GetAllMembers Query Result
    /// </summary>
    public class GetAllMembersQueryResult
    {
        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public IEnumerable<MemberVm> Payload { get; set; }        
    }

}
