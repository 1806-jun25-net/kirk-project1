using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaShop.Library.Repositories
{
    public class UserRepository
    {
        private readonly Project1DBContext _db;

        public UserRepository(Project1DBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<Users> GetUsers()
        {
            //List<Users> users = _db.Users.AsNoTracking().ToList();
            List<Users> users = _db.Users.AsNoTracking().Include(m => m.Orders).ToList();
            return users;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
