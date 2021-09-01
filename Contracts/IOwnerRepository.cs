using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{

    //public interface IOwnerRepository : IRepositoryBase<Owner>
    //{
    //    IEnumerable<Owner> GetAllOwners();
    //}
    //test ngan controller goi truc tiep toi RepositoryBase
    public interface IOwnerRepository
    {
       Task<IEnumerable<Owner>> GetAllOwners();
       Task<Owner> GetOwnerById(Guid owid);
        Task<Owner> GetOwnerWithDetails(Guid owid);
        void CreateOwner(Owner owner);

        void UpdateOwner(Owner owner);
        void DeleteOwner(Owner owner);
    }
}
