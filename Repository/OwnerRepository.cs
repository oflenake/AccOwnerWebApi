using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.RelatedModels;
using Entities.Extensions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    /// <summary>
    /// The <see cref="OwnerRepository"/> class that access the base repository backend through 
    /// the <see cref="RepositoryWrapper"/> class, to manipulate the <see cref="Owner"/> 
    /// entity data and its related data.
    /// </summary>
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(ILoggerManager logger, RepositoryContext repositoryContext)
            : base(logger, repositoryContext)
        {
        }

        // Get all Owners
        public async Task<IEnumerable<Owner>> GetAllAsyncData()
        {
            return await GetAllBaseData()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        // Get Owner by Id
        public async Task<Owner> GetByIDAsyncData(Guid ownerID)
        {
            return await GetByIDBaseData(o => o.ID.Equals(ownerID))
                    .DefaultIfEmpty(new Owner())
                    .SingleAsync();
        }

        // Get all related Accounts for a particular Owner
        public async Task<OwnerRelated> GetByIDRelatedAsyncData(Guid ownerID)
        {
            return await GetByIDBaseData(o => o.ID.Equals(ownerID))
                .Select(owner => new OwnerRelated(owner)
                {
                    Accounts = RepositoryContext.Accounts
                    .Where(a => a.OwnerID.Equals(owner.ID))
                    .ToList()
                })
                .SingleOrDefaultAsync();
        }

        // Create new Owner
        public async Task PostCreateAsyncData(Owner owner)
        {
            owner.ID = Guid.NewGuid();
            PostCreateBaseData(owner);
            await SaveAsyncBaseData();
        }

        // Update Owner
        public async Task PutUpdateAsyncData(Owner dbOwner, Owner owner)
        {
            dbOwner.Map(owner);
            PutUpdateBaseData(dbOwner);
            await SaveAsyncBaseData();
        }

        // Delete Owner
        public async Task DeleteByIDAsyncData(Owner owner)
        {
            DeleteByIDBaseData(owner);
            await SaveAsyncBaseData();
        }
    }
}
