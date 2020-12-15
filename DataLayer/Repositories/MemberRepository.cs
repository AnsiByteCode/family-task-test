using Core.Abstractions.Repositories;
using Domain.DataModels;
using System;

namespace DataLayer
{
    /// <summary>
    /// Member Repository
    /// </summary>
    /// <seealso cref="DataLayer.BaseRepository{System.Guid, Domain.DataModels.Member, DataLayer.MemberRepository}" />
    /// <seealso cref="Core.Abstractions.Repositories.IMemberRepository" />
    public class MemberRepository : BaseRepository<Guid, Member, MemberRepository>, IMemberRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MemberRepository(FamilyTaskContext context) : base(context)
        { }



        /// <summary>
        /// Noes the track.
        /// </summary>
        /// <returns></returns>
        IMemberRepository IBaseRepository<Guid, Member, IMemberRepository>.NoTrack()
        {
            return base.NoTrack();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        /// <returns></returns>
        IMemberRepository IBaseRepository<Guid, Member, IMemberRepository>.Reset()
        {
            return base.Reset();
        }

       
    }
}
