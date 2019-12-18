using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Rise.WebApp.Mvc.Controllers
{
    public class SchedulesController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.appointments = GetScheduleData();
            return View();
        }

        public List<AppointmentData> GetScheduleData()
        {
            var t = DateTime.Now;
            var appData = new List<AppointmentData>
            {
                new AppointmentData
                {
                    Id = 1,
                    Subject = "Explosion of Betelgeuse Star",
                    StartTime = new DateTime(t.Year, t.Month, t.Day + 1, 9, 30, 0),
                    EndTime = new DateTime(t.Year, t.Month, t.Day + 1, 11, 0, 0)
                },
                new AppointmentData
                {
                    Id = 2,
                    Subject = "Thule Air Crash Report",
                    StartTime = new DateTime(t.Year, t.Month, t.Day + 2, 12, 0, 0),
                    EndTime = new DateTime(t.Year, t.Month, t.Day + 2, 14, 0, 0)
                },
                new AppointmentData
                {
                    Id = 3,
                    Subject = "Blue Moon Eclipse",
                    StartTime = new DateTime(t.Year, t.Month, t.Day - 1, 9, 30, 0),
                    EndTime = new DateTime(t.Year, t.Month, t.Day - 1, 11, 0, 0)
                },
                new AppointmentData
                {
                    Id = 4,
                    Subject = "Meteor Showers in 2018",
                    StartTime = new DateTime(t.Year, t.Month, t.Day - 4, 13, 0, 0),
                    EndTime = new DateTime(t.Year, t.Month, t.Day - 4, 14, 30, 0)
                },
                new AppointmentData
                {
                    Id = 5,
                    Subject = "Milky Way as Melting pot",
                    StartTime = new DateTime(t.Year, t.Month, t.Day + 7, 12, 0, 0),
                    EndTime = new DateTime(t.Year, t.Month, t.Day + 7, 14, 0, 0)
                }
            };
            return appData;
        }

        public class AppointmentData
        {
            public int Id { get; set; }
            public string Subject { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
        }
    }
}