using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Falquan.KeyRinger.Models;

namespace Falquan.KeyRinger.Data
{
    public class UserRepository : IRepository<User>, IDisposable
    {
        private KeyRingerContext _context = new KeyRingerContext();
        
        public User Create(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public IQueryable<User> Retrieve()
        {
            return _context.Users.AsQueryable();
        }

        public User Retrieve(Guid id)
        {
            return _context.Users.Find(id);
        }

        public User Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }

        public User Delete(Guid id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();

            return user;
        }

        #region IDisposable Members

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion

    }
}
