namespace UsfQuiz.Data.CommonModels
{
    public interface IHavePrimaryKey<TKey>
    {
        TKey Id { get; set; }
    }
}
