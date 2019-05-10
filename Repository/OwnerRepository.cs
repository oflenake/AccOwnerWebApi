using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.RelatedModels;
using Entities.Extensions;
using Entities.Models;

namespace Repository
{
    /// <summary>
    /// The <see cref="OwnerRepository"/> class that access the base repository backend through 
    /// the <see cref="RepositoryWrapper"/> class, to manipulate the <see cref="Owner"/> 
    /// entity data and its related data.
    /// </summary>
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        // Get all Owners
        public IEnumerable<Owner> GetAllData()
        {
            return GetAllBaseData()
                .OrderBy(ow => ow.Name);
        }

        // Get Owner by Id
        public Owner GetByIDData(Guid ownerID)
        {
            return GetByIDBaseData(owner => owner.ID.Equals(ownerID))
                    .DefaultIfEmpty(new Owner())
                    .FirstOrDefault();
        }

        // Get all related Accounts for a particular Owner
        public OwnerRelated GetByIDRelatedData(Guid ownerID)
        {
            return new OwnerRelated(GetByIDData(ownerID))
            {
                Accounts = RepositoryContext.Accounts
                    .Where(a => a.OwnerID == ownerID)
            };
        }

        // Create new Owner
        public void PostCreateData(Owner owner)
        {
            owner.ID = Guid.NewGuid();
            PostCreateBaseData(owner);
            SaveBaseData();
        }

        // Update Owner
        public void PutUpdateData(Owner dbOwner, Owner owner)
        {
            dbOwner.Map(owner);
            PutUpdateBaseData(dbOwner);
            SaveBaseData();
        }

        // Delete Owner
        public void DeleteByIDData(Owner owner)
        {
            DeleteByIDBaseData(owner);
            SaveBaseData();
        }
    }
}
