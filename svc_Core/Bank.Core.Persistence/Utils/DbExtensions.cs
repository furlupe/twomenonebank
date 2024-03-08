using Bank.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Persistence.Utils;

internal static class DbExtensions
{
    internal static void SoftDelete(this EntityEntry<StoredModel> entry, DateTime now)
    {
        if (entry.State == EntityState.Deleted)
        {
            entry.State = EntityState.Modified;
            entry.Entity.DeletedAt = now;
        }
    }
}
