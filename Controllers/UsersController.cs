using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Student_CRUD.Data;
using Student_CRUD.Models;

namespace Student_CRUD.Controllers
{
    public class UsersController : Controller
    {
        private readonly Student_CRUDContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UsersController(Student_CRUDContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }


        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.User != null ? 
                          View(await _context.User.ToListAsync()) :
                          Problem("Entity set 'Student_CRUDContext.User'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Rollnumber == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rollnumber,Group,Comment,isPresent,LastName,FirstName")] User user, IFormFile Image)
        {
            if (true)
            {
            var allowedExtenstions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            var filePaths = new List<string>();
                // Check if the file has a valid extensions
                var fileExtension = Path.GetExtension(Image.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(fileExtension) || !allowedExtenstions.Contains(fileExtension))
                {
                    return BadRequest("Invalid file extension. Allowed extensions are: " + string.Join(", ", allowedExtenstions));
                };

                if (Image.Length > 0)
                {
                    // Change the folder path
                    var uploadFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadFolderPath);

                    var fileName = Path.GetRandomFileName() + fileExtension;
                    var filePath = Path.Combine(uploadFolderPath, fileName);
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    user.Image = "/uploads/" + fileName;
                }
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Rollnumber,Group,Image,Comment,isPresent,LastName,FirstName")] User user)
        {
            if (id != user.Rollnumber)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Rollnumber))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Rollnumber == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'Student_CRUDContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
          return (_context.User?.Any(e => e.Rollnumber == id)).GetValueOrDefault();
        }
    }
}
