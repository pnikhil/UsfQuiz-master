using UsfQuiz.Data.CommonModels;
using UsfQuiz.Data.DataModels;

namespace UsfQuiz.Data
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity.Validation;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base(ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString, throwIfV1Schema: false)
        {
        }

        public IDbSet<Quiz> Quiz { get; set; }

        public IDbSet<Categories> Categories { get; set; }

        public IDbSet<Questions> Questions { get; set; }

        public IDbSet<Solutions> Answers { get; set; }

        public IDbSet<UserAnswers> UserSolutions { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();

#if DEBUG
            return this.SaveChangesWithTracingDbExceptions();
#else
            return base.SaveChanges();
#endif
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Questions>()
                .HasMany(q => q.Answers)
                .WithRequired(a => a.ForQuestion)
                .WillCascadeOnDelete(true);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            var auditInfoEntries = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditInfo && (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entry in auditInfoEntries)
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (entity.CreatedOn == default(DateTime))
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void DeleteOrphanQuestions()
        {
            this.Questions
                .Local
                .Where(q => q.Quiz == null)
                .ToList()
                .ForEach(q => this.Questions.Remove(q));

            this.Answers
                .Local
                .Where(a => a.ForQuestion == null)
                .ToList()
                .ForEach(a => this.Answers.Remove(a));
        }

        private int SaveChangesWithTracingDbExceptions()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Exception currentException = ex;
                while (currentException != null)
                {
                    Trace.TraceError(currentException.Message);
                    currentException = currentException.InnerException;
                }

                throw;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceError($"Property: {validationError.PropertyName}{Environment.NewLine} Error: {validationError.ErrorMessage}");
                    }
                }

                throw;
            }
        }
    }
}
