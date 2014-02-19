using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Falquan.KeyRinger.Models;
using Falquan.KeyRinger.Data;
using Falquan.KeyRinger.Services;

namespace Falquan.KeyRinger.Web.Controllers
{
    public class UserController : Controller
    {
        private IRepository<User> _repo = new UserRepository();
        private CryptographicProvider _crypto = new CryptographicProvider();
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View(_repo.Retrieve());
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(Guid id)
        {
            var user = _repo.Retrieve(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            user.IV = CryptographicProvider.GetIV();
            if (ModelState.IsValid)
            {
                _repo.Create(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(Guid id)
        {
            var user = _repo.Retrieve(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(Guid id)
        {
            var user = _repo.Retrieve(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}