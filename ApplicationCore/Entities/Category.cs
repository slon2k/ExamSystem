using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Category : Entity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
