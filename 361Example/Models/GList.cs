﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _361Example.Models
{
    public class GList
    {
        [Key]
        [Column("GroceryListId")]
        public int Id { get; set; }
        [Column("Name")]
        public String ListName { get; set; }
        public IEnumerable<Item> items { get; set; }

    }
}
