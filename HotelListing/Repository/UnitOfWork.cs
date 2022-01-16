using System;
using System.Threading.Tasks;
using HotelListing.DataAccess;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace HotelListing.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _dbContext;
        private IGeniricRepository<Country> _countries;
        private IGeniricRepository<Hotel> _hotels;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public UnitOfWork(DatabaseContext context)
        {
            _dbContext = context;
        }

        public IGeniricRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_dbContext);
        public IGeniricRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_dbContext);
        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}