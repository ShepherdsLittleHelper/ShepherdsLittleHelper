using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShepherdsLittleHelper.Models
{
    public class Pet
    {
        [Key]
        public int PetID { get; set; }
        [Display(Name = "Name")]
        public string PetName { get; set; }
        [StringLength(1)]
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        [Display(Name = "Weight (lbs)")]
        public double Weight { get; set; }
        [Display(Name = "Special notes")]
        public string PetNotes { get; set; }
        [Display(Name = "Image URL")]
        public string ImageURL { get; set; }

        [ForeignKey("Location")]
        public int LocationID { get; set; }
        public virtual Location Location { get; set; }

        [ForeignKey("PetType")]
        public int PetTypeID { get; set; }
        public virtual PetType PetType { get; set; }

    }
}