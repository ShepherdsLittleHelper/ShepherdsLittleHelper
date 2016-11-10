using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdsLittleHelper.Models
{
    public class TaskType
    {
        [Key]
        public int TaskID { get; set; }
        [Display(Name = "Task")]
        public string TaskTypeName { get; set; }
        [Display(Name = "Note")]
        public string TaskTypeNotes { get; set; }

    }
}