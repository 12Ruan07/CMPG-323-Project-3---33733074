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
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

       

        // GET: Categories 
        // Shows all of the categories from the database
        public async Task<IActionResult> Index()
        {
           return View(_categoryRepository.GetAll());
        }


        // GET: Categories/Details/5
        // Shows details of a singular category with the specified id
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category =  _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        // Returns the view of categories.
        public IActionResult Create()
        {
            return View();
        }


        // POST: Categories/Create
        // Addds a new category to the database, user needs to enter category name and description
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            category.CategoryId = Guid.NewGuid();
            
            _categoryRepository.Add(category);

            _categoryRepository.Save();
            

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit/5
        // Returns the view of categories 
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        

        // POST: Categories/Edit/5
        // Updates an existing catgeory,user is able to change category name and/or description
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }
            try
            {
                _categoryRepository.Update(category);
                _categoryRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
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
        


        // GET: Categories/Delete/5
        // Returns the view of categories
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id);
 
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        // Category of the specified id will be deleted
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = _categoryRepository.GetById(id);
            _categoryRepository.Remove(category);
            _categoryRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // Category Exist
        // Checks if a category with specified id exists
        private bool CategoryExists(Guid id)
        {
            return _categoryRepository.GetById(id) != null;
        }


    }
}
