using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RelatedModels
{
    /// <summary>
    /// The <see cref="OwnerRelated"/> class helps to retrieve the <see cref="Owner"/> entity's 
    /// data, and its related <see cref="Account"/> entity's data. The related data is 
    /// retrieved through the <see cref="Accounts"/> navigation property, of the 
    /// <see cref="OwnerRelated"/> class, in which it will be lazy-loaded. Additional 
    /// information for this lazy-loading is in the <see cref="RepositoryContext"/> class.
    /// </summary>
    public class OwnerRelated : IEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public virtual IEnumerable<Account> Accounts { get; set; }

        /// <see cref="OwnerRelated"/> constructor disabled to enable lazy-loading.
        //public OwnerRelated()
        //{
        //}

        public OwnerRelated(Owner owner)
        {
            ID = owner.ID;
            Name = owner.Name;
            DateOfBirth = owner.DateOfBirth;
            Address = owner.Address;
        }
    }
}
