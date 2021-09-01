using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext context):base(context)
        {

        }

        public void CreateOwner(Owner owner)
        {
            Create(owner);
        }

        public void DeleteOwner(Owner owner)
        {
            Delete(owner);
        }

        public async Task<IEnumerable<Owner>> GetAllOwners()
        {
            return await FindAll().OrderBy(ow => ow.Name).ToListAsync();
        }

        public async Task<Owner> GetOwnerById(Guid owid)
        {
            return await FindByCondition(x => x.Id.Equals(owid)).FirstOrDefaultAsync();
        }

        public async  Task<Owner> GetOwnerWithDetails(Guid owid)
        {
            return await FindByCondition(x => x.Id.Equals(owid)).Include(c => c.Accounts).FirstOrDefaultAsync();

        }

        public void UpdateOwner(Owner owner)
        {
            Update(owner);
        }
    }
}
