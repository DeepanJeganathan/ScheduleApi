using Microsoft.EntityFrameworkCore;
using ScheduleApi.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApi.data
{
    public class ScheduleApiDbContext:DbContext
    {
        public ScheduleApiDbContext(DbContextOptions<ScheduleApiDbContext> options):base(options)
        {

        }
        public DbSet<Schedule> Schedules { get; set; }
    }
}
