using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TodoList.Model
{
    public class Item
    {
        [Key]
        public Int32 ID { get; set; }

        [Required]
        [MaxLength(30)]
        public String Name { get; set; }

        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        [DisplayName("List")]
        public Int32 ListId { get; set; }

        public virtual List List { get; set; }
    }
}
