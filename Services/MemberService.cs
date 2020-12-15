using AutoMapper;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.DataModels;
using Domain.Queries;
using Domain.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Member Service
    /// </summary>
    /// <seealso cref="Core.Abstractions.Services.IMemberService" />
    public class MemberService : IMemberService
    {
        /// <summary>
        /// The member repository
        /// </summary>
        private readonly IMemberRepository _memberRepository;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberService"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="memberRepository">The member repository.</param>
        public MemberService(IMapper mapper, IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
        }

        /// <summary>
        /// Creates the member command handler.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public async Task<CreateMemberCommandResult> CreateMemberCommandHandler(CreateMemberCommand command)
        {
            var member = _mapper.Map<Member>(command);
            var persistedMember = await _memberRepository.CreateRecordAsync(member);

            var vm = _mapper.Map<MemberVm>(persistedMember);

            return new CreateMemberCommandResult()
            {
                Payload = vm
            };
        }

        /// <summary>
        /// Updates the member command handler.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public async Task<UpdateMemberCommandResult> UpdateMemberCommandHandler(UpdateMemberCommand command)
        {
            var isSucceed = true;
            var member = await _memberRepository.ByIdAsync(command.Id);

            _mapper.Map<UpdateMemberCommand,Member>(command, member);
            
            var affectedRecordsCount = await _memberRepository.UpdateRecordAsync(member);

            if (affectedRecordsCount < 1)
                isSucceed = false;

            return new UpdateMemberCommandResult() { 
               Succeed = isSucceed
            };
        }

        /// <summary>
        /// Gets all members query handler.
        /// </summary>
        /// <returns></returns>
        public async Task<GetAllMembersQueryResult> GetAllMembersQueryHandler()
        {
            IEnumerable<MemberVm> vm = new List<MemberVm>();

            var members = await _memberRepository.Reset().ToListAsync();

            if (members != null && members.Any())
                vm = _mapper.Map<IEnumerable<MemberVm>>(members);

            return new GetAllMembersQueryResult() { 
                Payload = vm
            };
        }

    }
}
