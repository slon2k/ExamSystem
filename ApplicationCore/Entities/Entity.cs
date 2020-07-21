using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public abstract class Entity<T>
    {
        public T Id { get; set; }
    }
}
