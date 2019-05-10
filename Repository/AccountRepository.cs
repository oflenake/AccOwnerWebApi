using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;

namespace Repository
{
    /// <summary>
    /// The <see cref="AccountRepository"/> class that access the base repository backend through 
    /// the <see cref="RepositoryWrapper"/> class, to manipulate the <see cref="Account"/> 
    /// entity data and its related data.
    /// </summary>
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Account> GetByIDData(Guid ownerID)
        {
            return GetByIDBaseData(a => a.OwnerID.Equals(ownerID));
        }
    }
}
