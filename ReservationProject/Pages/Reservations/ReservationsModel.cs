﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Infra;
using ReservationProject.Pages;

namespace ReservationProject.Soft.Pages.Reservations
{
    public class ReservationsModel:BasePageModel
    {
        private readonly ApplicationDbContext db;

        public ReservationsModel(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult OnGetCreate()
        {
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Reservation.Id = Guid.NewGuid().ToString();
            //TODO kontroll kas olemas?
            db.Reservations.Add(Reservation);
            await db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            Reservation = await db.Reservations.FirstOrDefaultAsync(m => m.WorkerId == id);

            if (Reservation == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            Reservation = await db.Reservations.FindAsync(id);

            if (Reservation != null)
            {
                db.Reservations.Remove(Reservation);
                await db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnGetDetailsAsync(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            Reservation = await db.Reservations.FirstOrDefaultAsync(m => m.WorkerId == id);

            if (Reservation == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnGetEditAsync(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            Reservation = await db.Reservations.FirstOrDefaultAsync(m => m.WorkerId == id);

            if (Reservation == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //db.Attach(Reservation).State = EntityState.Modified;

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ReservationExists(Reservation.WorkerId))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return RedirectToPage("./Index");
            if (id == "")
                return NotFound();

            var reservationToUpdate = await db.Reservations.FindAsync(id);

            if (reservationToUpdate == null)
                return NotFound();

            if (await TryUpdateModelAsync(reservationToUpdate, "reservation",
                c => c.ReservationDate, c => c.RoomId, c => c.WorkerId))
            {
                await db.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return RedirectToPage("./Index");
        }

        private bool ReservationExists(string id)
        {
            return db.Reservations.Any(e => e.WorkerId == id);
        }
        public IList<Reservation> ReservationsList { get; set; }

        public async Task OnGetAsync()
        {
            ReservationsList = await db.Reservations.ToListAsync();
        }
    }
}