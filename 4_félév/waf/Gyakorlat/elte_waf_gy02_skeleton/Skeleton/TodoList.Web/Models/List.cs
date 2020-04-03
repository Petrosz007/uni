using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Models
{
    public class List
    {
        [Key]
        public Int32 Id { get; set; }

        [Required]
        [MaxLength(30)]
        public String Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}