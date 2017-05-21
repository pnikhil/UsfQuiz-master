namespace UsfQuiz.Data.DataModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using CommonModels;
    using Commons;

    public class Quiz : BaseModel<int>
    {
        private ICollection<Questions> questions;
        private ICollection<UserAnswers> solutions;

        public Quiz()
        {
            this.questions = new HashSet<Questions>();
            this.solutions = new HashSet<UserAnswers>();
        }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(GlobalConstants.TitleMinLength)]
        [MaxLength(GlobalConstants.TitleMaxLength)]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual Categories Category { get; set; }

        [MinLength(GlobalConstants.DescriptionMinLength)]
        [MaxLength(GlobalConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [MinLength(GlobalConstants.TermsConditionsMinLength)]
        [MaxLength(GlobalConstants.TermsConditionsMaxLength)]
        public string TermsConditions { get; set; }

        [Required]
        public string CreatedById { get; set; }

        public virtual User CreatedBy { get; set; }

        public bool IsPrivate { get; set; }

        public bool ShuffleAnswers { get; set; }

        [Range(GlobalConstants.MinQuestionsCount, int.MaxValue)]
        public int NumberOfQuestions { get; set; }

        public virtual ICollection<Questions> Questions
        {
            get { return this.questions; }

            set { this.questions = value; }
        }

        public virtual ICollection<UserAnswers> Solutions
        {
            get { return this.solutions; }
            set { this.solutions = value; }
        }

    }
}
