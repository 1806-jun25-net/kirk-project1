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

        public IEnumerable<User> GetUsers()
        {
            //List<Users> users = _db.Users.AsNoTracking().ToList();
            List<Users> users = _db.Users.AsNoTracking().Include(m => m.Orders).ToList();
            return Mapper.Map(users);
        }

        public User GetUserByUsername(string username)
        {
            Users u = _db.Users.AsNoTracking().Include(m => m.Orders).First(t => t.Username.Equals(username));
            return Mapper.Map(u);
        }

        public bool UsersContainsUsername(string username)
        {
            return _db.Users.AsNoTracking().Include(m => m.Orders).Any(t => t.Username.Equals(username));
        }

        public void AddUser(User u)
        {
            _db.Add(Mapper.Map(u));
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
