namespace InternetBanking.Core.Application.Interfaces.Service
{
    public interface IBaseService<ViewModel, SaveViewModel, Entity> where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {
        Task<SaveViewModel> SaveAsync(SaveViewModel vm);
        Task<SaveViewModel> GetByIdAsync(int id);
        Task<SaveViewModel> UpdateAsync(SaveViewModel vm, int id);
        Task<List<ViewModel>> GetAllAsync();
        Task RemoveAsync(int id);
        Task<int> CountAsync();
    }
}
