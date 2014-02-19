using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Falquan.KeyRinger.Data;
using Falquan.KeyRinger.Models;
using Falquan.KeyRinger.Services;

namespace Falquan.KeyRinger.Web.Controllers
{
    public class ServiceController : Controller
    {
        private IRepository<User> _userRepo = new UserRepository();
        private IRepository<Service> _repo = new ServiceRepository();

        //
        // GET: /Service/

        public ActionResult Index()
        {
            return View(_repo.Retrieve());
        }

        //
        // GET: /Service/Details/5

        public ActionResult Details(Guid id)
        {
            var service = _repo.Retrieve(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            service.Password = CryptographicProvider.Decrypt(service.User.IV, service.EncryptedPassword);
            return View(service);
        }

        //
        // GET: /Service/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Service/Create

        [HttpPost]
        public ActionResult Create(Service service)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepo.Retrieve().First();
                service.EncryptedPassword = CryptographicProvider.Encrypt(user.IV, service.Password);
                _repo.Create(service);

                return RedirectToAction("Index");
            }

            return View(service);
        }

        //
        // GET: /Service/Edit/5

        public ActionResult Edit(Guid id)
        {
            var service = _repo.Retrieve(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            service.Password = CryptographicProvider.Decrypt(service.User.IV, service.EncryptedPassword);
            return View(service);
        }

        //
        // POST: /Service/Edit/5

        [HttpPost]
        public ActionResult Edit(Service service)
        {
            if (ModelState.IsValid)
            {
                service.EncryptedPassword = CryptographicProvider.Encrypt(service.User.IV, service.Password);
                _repo.Update(service);

                return RedirectToAction("Index");
            }
            return View(service);
        }

        //
        // GET: /Service/Delete/5

        public ActionResult Delete(Guid id)
        {
            var service = _repo.Retrieve(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            service.Password = CryptographicProvider.Decrypt(service.User.IV, service.EncryptedPassword);
            return View(service);
        }

        //
        // POST: /Service/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}