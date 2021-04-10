using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApi.models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }
       
        [Required]
        [StringLength(40, ErrorMessage = "Maximum charaacters exceeded ")]
        public string FirstName { get; set; }
      
        [Required]
        [StringLength(40, ErrorMessage = "Maximum charaacters exceeded ")]
        public string LastName { get; set; }
       
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public string Comment { get; set; }

        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string ImageSrc { get; set; }
    }
}
