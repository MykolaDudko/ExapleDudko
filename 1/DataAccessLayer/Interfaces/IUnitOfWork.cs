using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Player> Players { get; }
        IRepository<Game> Games { get; }
        IRepository<Stadium> Stadiums { get; }
        void Save();
    }
}
