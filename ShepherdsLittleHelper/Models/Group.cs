using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdsLittleHelper.Models
{
    public class Group
    {
        [Key]
        public int GroupID { get; set; }
        public string GroupName { get; set; }

    }
}