﻿using System;
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
        [Display(Name ="Group/Location?")]
        public string GroupName { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}