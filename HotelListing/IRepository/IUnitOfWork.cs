using System;
using System.Threading.Tasks;
using HotelListing.DataAccess;

namespace HotelListing.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGeniricRepository<Country> Countries { get; }
        
        IGeniricRepository<Hotel> Hotels { get; }

        Task SaveChanges();
    }
}