using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SocialCRM.Dtos.Models;
using SocialCRM.Web.Client.Authentication;
using SocialCRM.Web.Client.Filters;
using SocialCRM.Web.Client.Services;

namespace SocialCRM.Web.Client.Controllers
{
    [HandleApiError]
    [Authorize]
    public class ClientController : ApiController
    {
        private readonly ClientService clientService;

        public ClientController(ClientService clientService)
        {
            this.clientService = clientService;
        }

        // GET: Client
        public async Task<ActionResult> Index()
        {
            var clients = await clientService.GetAsync("/api/Client", User.Identity.Name);

            return View(clients);
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
