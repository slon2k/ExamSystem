using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Entities
{
    public abstract class Question : Entity
    {
        public string Title { get; set; }
        public string Descripton { get; set; }
        public Guid ExamId { get; set; }
        public Exam Exam { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public int PointsToPass { get; set; }
    }

    public class ChoiseQuestion : Question
    {
        public bool IsMultipleChoise { get; set; } = false;
        public ICollection<AnswerOption> Answers { get; set; } = new List<AnswerOption>();
    }

    public class TextQuestion : Question
    {
        public String Answer { get; set; }
        public int EarnedPoints { get; set; }
    }


}
