using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    /// <summary>
    /// The static <see cref="OwnerExtensions"/>, extension class helps to map <see cref="Map"/> 
    /// the same two <see cref="Owner"/> object entities to each other, for further processing.
    /// </summary>
    public static class OwnerExtensions
    {
        public static void Map(this Owner dbOwner, Owner owner)
        {
            dbOwner.Name = owner.Name;
            dbOwner.Address = owner.Address;
            dbOwner.DateOfBirth = owner.DateOfBirth;
        }
    }
}
