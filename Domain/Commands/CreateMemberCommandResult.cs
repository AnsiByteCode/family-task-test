using Domain.ViewModel;

namespace Domain.Commands
{
    /// <summary>
    /// CreateMember Command Result
    /// </summary>
    public class CreateMemberCommandResult
    {
        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public MemberVm Payload { get; set; }
    }
}
