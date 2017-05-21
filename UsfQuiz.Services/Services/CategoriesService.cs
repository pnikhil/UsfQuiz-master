namespace UsfQuiz.Services.Services
{
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using Data.Commons;
    using Data.DataModels;
    using System;
    using Interfaces;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDbRepository<Categories> categories;

        public CategoriesService(IDbRepository<Categories> categories)
        {
            this.categories = categories;
        }

        public Categories GetById(int id)
        {
            return this.categories.GetById(id);
        }

        public void Save()
        {
            this.categories.Save();
        }

        public void Create(Categories category)
        {
            try
            {
                this.categories.Add(category);
                this.Save();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IQueryable<Categories> GetAll()
        {
            return this.categories.All().OrderBy(x => x.Name);
        }
    }
}
