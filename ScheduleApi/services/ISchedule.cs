using ScheduleApi.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApi.services
{
    public interface ISchedule
    {
        IEnumerable<Schedule> GetAll();
        Schedule GetById(int id);
        bool ScheduleIdExists(int id);
        bool CreateSchedule (Schedule schedule);
        bool UpdateSchedule(Schedule schedule);
        bool DeleteSchedule(Schedule schedule);
        bool save();
    }
}
