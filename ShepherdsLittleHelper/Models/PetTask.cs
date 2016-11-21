using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShepherdsLittleHelper.Models
{
    public class PetTask
    {
        [Key]
        public int TaskID { get; set; }
        [Display(Name = "Description")]
        public string TaskDescription { get; set; }
        public string Frequency { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsArchived { get; set; }

        [ForeignKey("Location")]
        public int? LocationID { get; set; }
        public virtual Location Location { get; set; }

        [ForeignKey("Pet")]
        public int PetID { get; set; }
        public virtual Pet Pet { get; set; }

        [ForeignKey("TaskType")]
        public int TaskTypeID { get; set; }
        public virtual TaskType TaskType { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserID { get; set; }
        public virtual User ApplicationUser { get; set; }

        public PetTask()
        {
            this.CreationDate = DateTime.Today;
            this.IsArchived = false;
        }

        public PetTask(PetTask currentTask)
        {
            CreationDate = DateTime.Today;
            IsArchived = false;
            TaskDescription = currentTask.TaskDescription;
            Frequency = currentTask.Frequency;
            Deadline = currentTask.Deadline;
            Pet = currentTask.Pet;
            Location = currentTask.Location;
            TaskType = currentTask.TaskType;
            IsDone = false;
        }

    }
}