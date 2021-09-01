using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class AccountRepository:RepositoryBase<Account>,IAccountRepository
    {
        public AccountRepository(RepositoryContext context):base(context)
        {

        }

        public IEnumerable<Account> AccountsByOwner(Guid id)
        {
            return FindByCondition(a => a.OwnerId.Equals(id)).ToList();
        }
    }
}
