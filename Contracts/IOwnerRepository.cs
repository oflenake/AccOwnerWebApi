using Entities.RelatedModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOwnerRepository : IRepositoryBase<Owner>
    {
        Task<IEnumerable<Owner>> GetAllAsyncData();
        Task<Owner> GetByIDAsyncData(Guid ownerID);
        Task<OwnerRelated> GetByIDRelatedAsyncData(Guid ownerID);
        Task PostCreateAsyncData(Owner owner);
        Task PutUpdateAsyncData(Owner dbOwner, Owner owner);
        Task DeleteByIDAsyncData(Owner owner);
    }
}
