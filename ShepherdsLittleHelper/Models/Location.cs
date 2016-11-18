using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShepherdsLittleHelper.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }
        [Display(Name ="Room")]
        public string LocationName { get; set; }
        [Display(Name = "Max occupancy")]
        public int MaxOccupancy { get; set; }
        [Display(Name = "Key notes")]
        public string LocationNotes { get; set; }

        [ForeignKey("Group")]
        public int GroupID { get; set; }
        public virtual Group Group { get; set; }
        
    }
}