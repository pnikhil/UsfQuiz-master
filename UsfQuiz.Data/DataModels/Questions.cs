namespace UsfQuiz.Data.DataModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CommonModels;
    using Commons;

    public class Questions : BaseModel<int>
    {
        private ICollection<Solutions> answers;

        public Questions()
        {
            this.answers = new HashSet<Solutions>();
        }

        [Required]
        [MinLength(GlobalConstants.QuestionMinLength)]
        [MaxLength(GlobalConstants.QuestionMaxLength)]
        public string Title { get; set; }

        public virtual ICollection<Solutions> Answers
        {
            get { return this.answers; }

            set { this.answers = value; }
        }

        public virtual Quiz Quiz { get; set; }

        public int QuizId { get; set; }
    }
}
