using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class AnswerOption : Entity<Guid>
    {
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public bool IsCorrect { get; set; } = false;
        public bool IsChecked { get; set; } = false;
    }
}
