using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Models
{
    public class Item
    {
        [Key] public Int32 Id { get; set; }

        [Required]
        [MaxLength(30)]
        public String Name { get; set; }

        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        public byte[] Image { get; set; }

        [Required]
        [DisplayName("List")]
        public Int32 ListId { get; set; }

        public virtual List List { get; set; }
    }
}