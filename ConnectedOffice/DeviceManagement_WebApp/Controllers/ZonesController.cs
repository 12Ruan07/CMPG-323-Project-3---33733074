using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;

namespace DeviceManagement_WebApp.Controllers
{
    public class ZonesController : Controller
    {
        private readonly IZoneRepository _zoneRepository;

        public ZonesController(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        // GET: Zones
        // Shows all of the zones from the database
       public async Task<IActionResult> Index()
        {
            return View(_zoneRepository.GetAll());
        }

        // GET: Zones/Details/5
        // Shows details of a singular zone with specified id
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById(id);
               
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }


        // GET: Zones/Create
        // Returns view of zones
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zones/Create
        // Adds a new zone to the database, user has to enter zone name and description
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            zone.ZoneId = Guid.NewGuid();
            _zoneRepository.Add(zone);
            _zoneRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: Zones/Edit/5
        // Returns view of zones
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById(id);
            if (zone == null)
            {
                return NotFound();
            }
            return View(zone);
        }

        // POST: Zones/Edit/5
        // Updates an existing zone, user is able to change zone name and/or description
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            if (id != zone.ZoneId)
            {
                return NotFound();
            }

            try
            {
                _zoneRepository.Update(zone);
                _zoneRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(zone.ZoneId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Zones/Delete/5
        // Returns view of zones
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById(id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }



        // POST: Zones/Delete/5
        // Deletes an existing zone from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var zone = _zoneRepository.GetById(id);
            _zoneRepository.Remove(zone);
            _zoneRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // CheckExists
        // Checks if the zone with specified id exists in the database
        private bool ZoneExists(Guid id)
        {
            return _zoneRepository.GetById(id) != null;
        }

    }
}
