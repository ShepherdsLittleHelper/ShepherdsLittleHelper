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
        public string LocationName { get; set; }
        public int MaxOccupancy { get; set; }
        public string LocationNotes { get; set; }

        [ForeignKey("Group")]
        public int GroupID { get; set; }
        public virtual Group Group { get; set; }
        
    }
}