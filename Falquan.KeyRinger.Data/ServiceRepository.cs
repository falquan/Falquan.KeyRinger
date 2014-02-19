using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Falquan.KeyRinger.Models;

namespace Falquan.KeyRinger.Data
{
    public class ServiceRepository : IRepository<Service>, IDisposable
    {
        private KeyRingerContext _context = new KeyRingerContext();

        public Service Create(Service entity)
        {
            _context.Services.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public IQueryable<Service> Retrieve()
        {
            return _context.Services.AsQueryable();
        }

        public Service Retrieve(Guid id)
        {
            return _context.Services.Find(id);
        }

        public Service Update(Service entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }

        public Service Delete(Guid id)
        {
            var service = _context.Services.Find(id);
            _context.Services.Remove(service);
            _context.SaveChanges();

            return service;
        }

        #region IDisposable Members

        public void Dispose()
        {
            _context.Dispose();
        }
        
        #endregion

    }
}
