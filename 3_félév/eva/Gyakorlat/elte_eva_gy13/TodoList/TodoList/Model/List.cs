using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ELTE.Windows.TodoList.Model
{
    public class List
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}