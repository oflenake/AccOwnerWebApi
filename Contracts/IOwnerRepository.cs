using Entities.RelatedModels;
using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IOwnerRepository : IRepositoryBase<Owner>
    {
        IEnumerable<Owner> GetAllData();
        Owner GetByIDData(Guid ownerID);
        OwnerRelated GetByIDRelatedData(Guid ownerID);
        void PostCreateData(Owner owner);
        void PutUpdateData(Owner dbOwner, Owner owner);
        void DeleteByIDData(Owner owner);
    }
}
