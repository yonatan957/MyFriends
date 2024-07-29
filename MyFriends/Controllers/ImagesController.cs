using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFriends.DAL;
using MyFriends.Models;

namespace MyFriends.Controllers
{
    public class ImagesController : Controller
    {
        private readonly FriendsDbContext _context;

        public ImagesController(FriendsDbContext context)
        {
            _context = context;
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
            var friendsDbContext = _context.Image.Include(i => i.Friend);
            return View(await friendsDbContext.ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Image
                .Include(i => i.Friend)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            ViewData["FriendId"] = new SelectList(_context.Friend, "Id", "Id");
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FriendId,MyImage")] Image image)
        {
            if (ModelState.IsValid)
            {
                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FriendId"] = new SelectList(_context.Friend, "Id", "Id", image.FriendId);
            return View(image);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Image.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            ViewData["FriendId"] = new SelectList(_context.Friend, "Id", "Id", image.FriendId);
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FriendId,MyImage")] Image image)
        {
            if (id != image.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.Id))
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
            ViewData["FriendId"] = new SelectList(_context.Friend, "Id", "Id", image.FriendId);
            return View(image);
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Image
                .Include(i => i.Friend)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Image.FindAsync(id);
            if (image != null)
            {
                _context.Image.Remove(image);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Images/Images/5
        public async Task<IActionResult> Images(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .Include(f => f.Images)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (Images == null)
            {
                return NotFound();
            }
            return View(friend.Images);
        }

        private bool ImageExists(int id)
        {
            return _context.Image.Any(e => e.Id == id);
        }
    }
}
