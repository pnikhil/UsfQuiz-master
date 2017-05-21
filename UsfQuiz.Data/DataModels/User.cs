namespace UsfQuiz.Data.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using CommonModels;
    using Commons;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser, IHavePrimaryKey<string>, IAuditInfo, IDeletableEntity
    {
        private ICollection<Quiz> quizzesCreated;
        private ICollection<UserAnswers> solutionsSubmited;

        public User()
        {
            this.quizzesCreated = new HashSet<Quiz>();
            this.solutionsSubmited = new HashSet<UserAnswers>();
            this.CreatedOn = DateTime.Now;
        }

        [MinLength(GlobalConstants.NameMinLength)]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string FirstName { get; set; }

        [MinLength(GlobalConstants.NameMinLength)]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string LastName { get; set; }

        public virtual ICollection<Quiz> QuizzesCreated
        {
            get { return this.quizzesCreated; }
            set { this.quizzesCreated = value; }
        }

        public virtual ICollection<UserAnswers> SolutionsSubmited
        {
            get { return this.solutionsSubmited; }
            set { this.solutionsSubmited = value; }
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
