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
    public class FriendsController : Controller
    {
        private readonly FriendsDbContext _context;

        public FriendsController(FriendsDbContext context)
        {
            _context = context;
        }

        // GET: Friends
        public async Task<IActionResult> Index()
        {
            return View(await _context.Friend.ToListAsync());
        }

        // GET: Friends/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // GET: Friends/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Phone, SetImage")] Friend friend)
        {
            if (ModelState.IsValid)
            {
                _context.Add(friend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(friend);
        }

        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }
            return View(friend);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Phone")] Friend friend)
        {
            if (id != friend.Id)
            {
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(friend);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriendExists(friend.Id))
                    {
                        return View("Error");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(friend);
        }
        //GET: Friends/Add/Id
        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            var Friend = await _context.Friend
                 .FirstOrDefaultAsync(m => m.Id == id);
            if (Friend == null)
            {
                return View("Error");
            }
            return View(Friend);
        }

        //POST: Friends/Add/Id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int id, IFormFile SetImage)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Friend = await _context.Friend
                 .FirstOrDefaultAsync(m => m.Id == id);
            if (Friend == null)
            {
                return NotFound();
            }
            if (SetImage == null)
            {
                ModelState.AddModelError("SetImage", "Image file is required.");
                return View(Friend);
            }
            Friend.SetImage = SetImage;
            _context.Update(Friend);
            await _context.SaveChangesAsync();
            return View(Friend);
        }
        // GET: Friends/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var friend = await _context.Friend.FindAsync(id);
            if (friend != null)
            {
                _context.Friend.Remove(friend);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendExists(int id)
        {
            return _context.Friend.Any(e => e.Id == id);
        }
    }
}
