using System.Threading.Tasks;
using FleetMgmt.Data;
using FleetMgmt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace FleetMgmt.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FmDbContext _dbContext;

        private readonly IDbContextTransaction _transaction;
        // readonly IDbContextTransaction _transaction;
        public UnitOfWork(FmDbContext dbContext)
        {
            _dbContext = dbContext;
            IsActive = true;
            _transaction = dbContext.Database.BeginTransaction();
        }

        public int AffectedRows { get; private set; }
        public bool IsActive { get; private set; }

        public void Rollback()
        {
            _transaction.Rollback();
        }
        public int Save()
        {
            AffectedRows = _dbContext.SaveChanges();
            return AffectedRows;
        }

        public async Task<int> SaveAsync()
        {
            AffectedRows = await _dbContext.SaveChangesAsync();
            return AffectedRows;
        }
        public int Commit()
        {
            var result = Save();
            if (IsActive)
                _transaction.Commit();
            return result;
        }

        public async Task<int> CommitAsync()
        {
            var result = await SaveAsync();
            if (IsActive)
                await _transaction.CommitAsync();
            return result;
        }

        public void SetIsActive(bool value)
        {
            IsActive = value;
        }
    }
}
