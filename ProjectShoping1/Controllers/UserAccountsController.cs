using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectShoping1.Models;
using System.Web.Security;

namespace ProjectShoping1.Controllers
{
    public class UserAccountsController : Controller
    {
        private ProjectShopingEntities db = new ProjectShopingEntities();

        // GET: UserAccounts
        public ActionResult Index()
        {
            return View(db.UserAccounts.ToList());
        }

        // GET: UserAccounts/Details/5
       

        // GET: UserAccounts/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserId,Name,Email,Password,ConfirmPassword")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                db.UserAccounts.Add(userAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userAccount);
        }


        //Login code



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
           
            {
                var usr = db.UserAccounts.Single(u => u.Name == user.Name && u.Password == user.Password);
                if (usr != null)
                {
                    Session["UserID"] = usr.UserId.ToString();
                    Session["Name"] = usr.Name.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "User Name or Password is Wrong!");
                }
            }
            return View();
        }
        public ActionResult LoggedIn()
        {
            if (Session != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

       




        // GET: UserAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            return View(userAccount);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Name,Email,Password,ConfirmPassword")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userAccount);
        }

       

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
