
using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;

namespace InternetBanking.Core.Application.Services
{
    public class BaseService<ViewModel, SaveViewModel, Entity> : IBaseService<ViewModel, SaveViewModel, Entity>
             where SaveViewModel : class
          where ViewModel : class
          where Entity : class
    {
        private readonly IBaseRepository<Entity> _repository;
        private readonly IMapper _mapper;
        public BaseService(IBaseRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public virtual async Task<List<ViewModel>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<ViewModel>>(entities);
        }

        public async Task<SaveViewModel> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<SaveViewModel>(entity);
        }

        public virtual async Task RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            await _repository.RemoveAsync(entity);
        }

        public virtual async Task<SaveViewModel> SaveAsync(SaveViewModel vm)
        {
            Entity entity = await _repository.SaveAsync(_mapper.Map<Entity>(vm));
            return _mapper.Map<SaveViewModel>(entity);
        }

        public virtual async Task<SaveViewModel> UpdateAsync(SaveViewModel vm, int id)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            Entity t = await _repository.UpdateAsync(entity, id);
            return _mapper.Map<SaveViewModel>(t);
        }
    }
}
