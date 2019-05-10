using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        // Get Owner's related Accounts
        IEnumerable<Account> GetByIDData(Guid ownerID);
    }
}
