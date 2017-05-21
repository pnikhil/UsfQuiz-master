namespace UsfQuiz.Data.DataModels
{
    using System.Collections.Generic;
    using UsfQuiz.Data.CommonModels;

    public class UserAnswers : BaseModel<int>
    {
        private ICollection<Solutions> selectedAnswers;

        public UserAnswers()
        {
            this.selectedAnswers = new HashSet<Solutions>();
        }

        public int ForQuizId { get; set; }

        public virtual Quiz ForQuiz { get; set; }

        public string ByUserId { get; set; }

        public virtual User ByUser { get; set; }

        public virtual ICollection<Solutions> SelectedAnswers
        {
            get { return this.selectedAnswers; }
            set { this.selectedAnswers = value; }
        }
    }
}
