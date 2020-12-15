using Domain.DataModels;
using System;

namespace Core.Abstractions.Repositories
{
    /// <summary>
    /// IMember Repository
    /// </summary>
    /// <seealso cref="Core.Abstractions.Repositories.IBaseRepository{System.Guid, Domain.DataModels.Member, Core.Abstractions.Repositories.IMemberRepository}" />
    public interface IMemberRepository : IBaseRepository<Guid, Member, IMemberRepository>
    {
    }
}
