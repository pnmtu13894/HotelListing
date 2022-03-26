using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HotelListing.DataAccess;
using HotelListing.DTO.Paging;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using X.PagedList;

namespace HotelListing.Repository
{
    public class GenericRepository<T> : IGeniricRepository<T> where T : class
    {
        private readonly DatabaseContext _dbContext;
        private readonly DbSet<T> _db;
        private IGeniricRepository<T> _geniricRepositoryImplementation;

        public GenericRepository(DatabaseContext context)
        {
            _dbContext = context;
            _db = _dbContext.Set<T>();
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }
            
            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _db;
            if(include != null)
            {
                query = include(query);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _db.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<X.PagedList.IPagedList<T>> GetPagedList(RequestParam requestParams, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _db;

            if (include != null)
            {
                query = include(query);
            }

            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }
    }
}