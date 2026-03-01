using System.Security.Principal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchedFrex.Core.Abstractions;

namespace SchedFrex.DataAccess.Repositories;

public class Repository<TModel, TEntity> : IRepository<TModel> where TEntity : class
{
    protected readonly CalendarDbContext DbContext;
    protected readonly IMapper Mapper;

    private readonly DbSet<TEntity> _dbSet;

    public Repository(CalendarDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        Mapper = mapper;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<TModel> CreateAsync(TModel model)
    {
        var entity = Mapper.Map<TEntity>(model);
        _dbSet.Add(entity);
        await DbContext.SaveChangesAsync();

        return Mapper.Map<TModel>(entity);
    }

    public async Task<TModel> UpdateAsync(TModel model)
    {
        var entity = Mapper.Map<TEntity>(model);
        _dbSet.Update(entity);
        await DbContext.SaveChangesAsync();

        return Mapper.Map<TModel>(entity);
    }

    public async Task DeleteAsync(TModel model)
    {
        var entity = Mapper.Map<TEntity>(model);
        _dbSet.Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task<TModel> GetAsync(Guid id)
    { 
        var resultEntity = await _dbSet.FindAsync(id);
        return Mapper.Map<TModel>(resultEntity);
    }
}