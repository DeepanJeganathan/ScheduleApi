using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleApi.models;
using ScheduleApi.services;

namespace ScheduleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ISchedule _schedule;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ScheduleController(ISchedule schedule, IWebHostEnvironment webHostEnvironment)
        {
            this._schedule = schedule;
            this._webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        // GET: api/Schedule
        public ActionResult Schedules()
        {
            var model =_schedule.GetAll();
            var newModel = model.Select(x => new Schedule()
            {
                ScheduleId = x.ScheduleId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                WorkStationType=x.WorkStationType,
                Date = x.Date,
                Comment = x.Comment,
                ImageName = x.ImageName,
                ImageSrc =
                  String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
            }).ToList();


            return Ok(newModel);
        }

        [HttpGet("{id}")]
        // GET: api/Schedule
        public ActionResult Schedule(int id)
        {
            var model = _schedule.GetById(id);
            model.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, model.ImageName);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        // POST: api/Schedule
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] Schedule schedule)
        {

            if (schedule == null)
            {
                return BadRequest(ModelState);
            }
            if (schedule.ImageFile !=null)
            {
                schedule.ImageName = await SaveImage(schedule.ImageFile);
            }
        
            if (!_schedule.CreateSchedule(schedule))
            {
                ModelState.AddModelError("", "Error in saving entry");
                return StatusCode(500, ModelState);
            }


            return StatusCode(201);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> Edit(int id, [FromForm] Schedule schedule)
        {
            if (!_schedule.ScheduleIdExists(id))
            {
                return NotFound();
            }

            if (schedule.ImageFile != null)
            {
                DeleteImage(schedule.ImageName);
                schedule.ImageName = await SaveImage(schedule.ImageFile);

            }

            if (!_schedule.UpdateSchedule(schedule)
)
            {
                ModelState.AddModelError("", "error in updating");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }



        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!_schedule.ScheduleIdExists(id))
            {
                return NotFound();
            }
            var schedule = _schedule.GetById(id);
            if (!_schedule.DeleteSchedule(schedule))
            {
                ModelState.AddModelError("", "Erroe in deleting record");
                return StatusCode(500, ModelState);
            }
            if (!string.IsNullOrWhiteSpace(schedule.ImageName))
            {
                DeleteImage(schedule.ImageName);
            }
            

            return NoContent();

        }


        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {

            string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray());
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);

            using (var filestream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(filestream);
            }
            return imageName;

        }


        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }

}
