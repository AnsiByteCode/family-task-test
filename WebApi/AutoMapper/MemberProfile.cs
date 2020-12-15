using AutoMapper;
using Domain.Commands;
using Domain.DataModels;
using Domain.ViewModel;

namespace WebApi.AutoMapper
{
    /// <summary>
    /// Member Profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class MemberProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberProfile" /> class.
        /// </summary>
        public MemberProfile()
        {
            CreateMap<CreateMemberCommand, Member>();
            CreateMap<UpdateMemberCommand, Member>();
            CreateMap<Member, MemberVm>();
        }
    }
}
