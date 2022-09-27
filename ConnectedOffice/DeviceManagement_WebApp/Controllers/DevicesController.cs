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
    public class DevicesController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;


        public DevicesController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
            return View(_deviceRepository.GetAll());
        }
       

        // GET: Devices/Details/5
        

        // GET: Devices/Create
        
        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        

        // GET: Devices/Edit/5
       

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       

        // GET: Devices/Delete/5
        

        // POST: Devices/Delete/5
       
        
    }
}
