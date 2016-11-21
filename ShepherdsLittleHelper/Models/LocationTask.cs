using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShepherdsLittleHelper.Models
{
    public class LocationTask
    {
        [Key]
        public int L_TaskID { get; set; }
        [Display(Name = "Description")]
        public string TaskDescription { get; set; }
        public string Frequency { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsArchived { get; set; }

        [ForeignKey("Location")]
        public int LocationID { get; set; }
        public virtual Location Location { get; set; }

        [ForeignKey("TaskType")]
        public int TaskTypeID { get; set; }
        public virtual TaskType TaskType { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserID { get; set; }
        public virtual User ApplicationUser { get; set; }

        public LocationTask()
        {
            this.CreationDate = DateTime.Today;
            this.IsArchived = false;
        }

        public LocationTask(LocationTask currentTask)
        {
            this.CreationDate = DateTime.Today;
            this.IsArchived = false;
            this.TaskDescription = currentTask.TaskDescription;
            this.Frequency = currentTask.Frequency;
            this.Deadline = currentTask.Deadline;
            this.Location = currentTask.Location;
            this.TaskType = currentTask.TaskType;
        }

    }
}