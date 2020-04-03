using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Model
{
    public class List
    {
        [Key]
        public Int32 ID { get; set; }

        [Required]
        [MaxLength(30)]
        public String Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
