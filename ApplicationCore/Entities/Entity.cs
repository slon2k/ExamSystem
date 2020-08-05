using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public abstract class Entity
    {
        public virtual Guid Id { get; set; }
    }
}
