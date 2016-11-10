using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdsLittleHelper.Models
{
    public class PetType
    {
        [Key]
        public int PetTypeID { get; set; }
        [Display(Name = "Species")]
        public string PetTypeDescription { get; set; }

    }
}