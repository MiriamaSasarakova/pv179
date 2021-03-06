﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace DAL.Entities
{
    public class ItemCategory : IEntity
    {
        public int Id { get; set; }

        [NotMapped] 
        public string TableName { get; } = "items_category";
        
        public Item Item { get; set; }
        
        [Required]
        public int ItemID { get; set; }
        
        public virtual Category Category { get; set; }
        
        [Required]
        public int CategoryID { get; set; }
    }
}
