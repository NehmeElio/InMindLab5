using Microsoft.EntityFrameworkCore;
using UMS_Lab5.Persistence.UMS_Lab5.Domain.Models;
using System.Linq;

namespace UMS_Lab5.Common.Helpers
{
    public static class IdGenerator
    {
        public static int GenerateNewId<TEntity>(UMSContext context) where TEntity : class
        {
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var primaryKey = entityType.FindPrimaryKey();
            if (primaryKey == null || primaryKey.Properties.Count != 1)
                throw new InvalidOperationException("The entity does not have a single primary key.");
        
            var idName = primaryKey.Properties[0].Name;
            var maxId = context.Set<TEntity>().Any() 
                ? context.Set<TEntity>().Max(e => EF.Property<int>(e, idName)) 
                : 0;
            return maxId + 1;
        }
    }
}