using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Domain;

public abstract class StoredModel
{
    public Guid Id { get; set; }
    public DateTime DeletedAt { get; set; }
}
