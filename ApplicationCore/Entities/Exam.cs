using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Exam: Entity
    {
        public string Title { get; set; }
        public string AppUserId { get; set; }
        public AppUser User { get; set; }
        public string Description { get; set; }
        public DateTime? DateTimePublished { get; set; } = null;
        public DateTime? DateTimeStarted { get; set; } = null;
        public DateTime? DateTimeSubmitted { get; set; } = null;
        public int Duration { get; set; }
        public bool IsChecked { get; set; }
        public bool IsSubmitted { get => DateTimeSubmitted != null; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }

}
