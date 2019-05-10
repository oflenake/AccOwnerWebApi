using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    /// <summary>
    /// The static <see cref="IEntityExtensions"/>, extension class helps to check entities for two conditions: 
    ///     1. Check if the whole entity object is null <see cref="IsObjectNull"/> and 
    ///        assign it the value 'null', if the condition is true.
    ///     2. Check if the entity.ID property is empty <see cref="IsEmptyObject"/> and 
    ///        assign it the value 'Empty', if the condition is true.
    /// </summary>
    public static class IEntityExtensions
    {
        public static bool IsObjectNull(this IEntity entity)
        {
            return entity == null;
        }

        public static bool IsEmptyObject(this IEntity entity)
        {
            return entity.ID.Equals(Guid.Empty);
        }
    }
}
