using Microsoft.AspNetCore.Hosting;
using ScheduleApi.data;
using ScheduleApi.models;
using ScheduleApi.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApi.repository
{
    public class ScheduleRepository : ISchedule
    {
        private readonly ScheduleApiDbContext _scheduleApiDbContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ScheduleRepository(ScheduleApiDbContext scheduleApiDbContext ,IWebHostEnvironment hostEnvironment)
        {
            this._scheduleApiDbContext = scheduleApiDbContext;
            this._hostEnvironment = hostEnvironment;
        }
        public bool CreateSchedule(Schedule schedule)
        {
            _scheduleApiDbContext.Add(schedule);
            return save();
        }

        public bool DeleteSchedule(Schedule schedule)
        {
            _scheduleApiDbContext.Remove(schedule);
            return save();
        }

        public IEnumerable<Schedule> GetAll()
        {
            return _scheduleApiDbContext.Schedules.ToList();
           
        }

        public Schedule GetById(int id)
        {
            return _scheduleApiDbContext.Schedules.FirstOrDefault(schedule => schedule.ScheduleId == id);
        }

        public bool save()
        {
            return _scheduleApiDbContext.SaveChanges() >= 0 ? true : false;

        }

        public bool ScheduleIdExists(int id)
        {
            return _scheduleApiDbContext.Schedules.Any(schedule => schedule.ScheduleId == id);
        }

        public bool UpdateSchedule(Schedule schedule)
        {
             _scheduleApiDbContext.Schedules.Update(schedule);
            return save();
        }
    }
}
