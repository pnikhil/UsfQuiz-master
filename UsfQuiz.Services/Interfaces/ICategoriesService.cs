namespace UsfQuiz.Services.Interfaces
{
    using System.Linq;
    using Data.DataModels;

    public interface ICategoriesService
    {
        IQueryable<Categories> GetAll();

        Categories GetById(int id);

        void Save();

        void Create(Categories category);
    }
}
