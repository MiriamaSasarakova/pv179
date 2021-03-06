﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    public class Role : IdentityRole<int, UserRole>, IEntity
    {
        [NotMapped] 
        public string TableName { get; } = "roles";
       
    }
}
