namespace CalculationOfEmployeeSalaries.Core
{
    public interface IUnitOfWork
    {

        void Save();
        Task<int> SaveAsync();


    }
}