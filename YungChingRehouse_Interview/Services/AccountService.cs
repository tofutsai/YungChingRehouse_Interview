using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YungChingRehouse_Interview.Models;
using YungChingRehouse_Interview.Models.DAL;

namespace YungChingRehouse_Interview.Services
{
    public class AccountService : IAccountService
    {
        private IRepository<member> _repository;

        //public AccountService() : this(new EFGenericRepository<member>(new YCReHouseInterviewEntities()))
        //{

        //}
        public AccountService(IRepository<member> repository)
        {
            _repository = repository;
        }

        public void CreateToDatabase(member mem)
        {
            //mem.createdDate = DateTime.Now;
            _repository.Create(mem);
            _repository.SaveChanges();

        }
    }
}