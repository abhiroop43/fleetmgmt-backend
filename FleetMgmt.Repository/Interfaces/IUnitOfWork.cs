using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FleetMgmt.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        bool IsActive { get; }
        void SetIsActive(bool value);
        void Rollback();
        int Save();
        Task<int> SaveAsync();
        Task<int> CommitAsync();
    }
}
