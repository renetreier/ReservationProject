﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;
using ReservationProject.Infra;

namespace ReservationProject.Pages
{
    public class ReservationsModel:BasePageModel<Reservation, Reservation>
    {
        public ReservationsModel(ApplicationDbContext c) : this(new ReservationsRepo(c), c) { }
        protected internal ReservationsModel(IReservationsRepo r, ApplicationDbContext c = null): base(r, c) { }

        public SelectList Workers { get; set; }
        public SelectList Rooms { get; set; }

        protected internal override void DoBeforeCreate()
        {
            LoadRooms();
            LoadWorkers();
        }



        [BindProperty]
        public Reservation Reservation { get; set; }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid) return Page();

            Reservation.Id = Guid.NewGuid().ToString();
            //TODO kontroll kas olemas?
            db.Reservations.Add(Reservation);
            await db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            //if (!await LoadReservation(id)) return NotFound();

            //return Page();
            Reservation = await repo.Get(id);
            return Reservation is null ? NotFound() : Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (id == "") return NotFound();

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
            //if (!await LoadReservation(id)) return NotFound();

            //return Page();
            Reservation = await repo.Get(id);
            return Reservation is null ? NotFound() : Page();
        }
        public async Task<IActionResult> OnGetEditAsync(string id)
        {

            //if (id == "") return NotFound();

            //LoadRooms(db);
            //LoadWorkers(db);
            //Reservation = await db.Reservations.FirstOrDefaultAsync(m => m.Id == id);

            //if (Reservation == null) return NotFound();

            //return Page();
            Reservation = await repo.Get(id);
            return Reservation is null ? NotFound() : Page();
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            if (id == "") return NotFound();

            var reservationToUpdate = await db.Reservations.FindAsync(id);

            if (reservationToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync(reservationToUpdate, "reservation",
                    c => c.ReservationDate, c => c.RoomId, c => c.WorkerId))
            {
                    await db.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }

        public IList<Reservation> ReservationsList { get; set; }


        public void LoadWorkers(object selectedWorker = null)
        {
            var q = from d in db.Workers orderby d.LastName select d;
            Workers = new SelectList(q.AsNoTracking(),
                "Id", "FullName", selectedWorker);
        }

        public void LoadRooms(object selectedRoom = null)
        {
            var q = from d in db.Rooms orderby d.RoomName select d;
            Rooms = new SelectList(q.AsNoTracking(),
                "Id", "RoomName", selectedRoom);
        }

        public async Task<bool> LoadReservation(string id)
        {
            if (id == "") return false;
            Reservation = await db.Reservations
                .AsNoTracking()
                .Include(c => c.ReservedRoom)
                .Include(c=>c.ReservedWorker)
                .FirstOrDefaultAsync(m => m.Id == id);
            return Reservation != null;
        }
        public async Task LoadReservations()
        {
            ReservationsList = await db.Reservations
                .Include(c => c.ReservedRoom)
                .Include(c => c.ReservedWorker)
                .ToListAsync();
        }
        public async Task OnGetAsync()
        {
            //await LoadReservations();
            //ReservationsList = await db.Reservations.ToListAsync();
            ReservationsList = await repo.Get();
        }

    }
}
