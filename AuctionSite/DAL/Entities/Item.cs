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
    public class Item : IEntity
    {
        public int Id { get; set; }

        [NotMapped] 
        public string TableName { get; } = "items";

        [MaxLength(50),Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public virtual List<ItemCategory> HasCategories { get; set; }
        
        public virtual User Owner { get; set; }
        
        [Required]
        public int OwnerID { get; set; }
        
        public virtual Auction Auction { get; set; }
        
        public int? AuctionID { get; set; }
        
    }
}
