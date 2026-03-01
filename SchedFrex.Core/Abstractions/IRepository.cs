namespace SchedFrex.Core.Abstractions;

public interface IRepository<TModel>
{
    public Task<TModel> CreateAsync(TModel model);
    public Task<TModel> UpdateAsync(TModel model);
    public Task DeleteAsync(TModel model);
    public Task<TModel> GetAsync(Guid id);
}