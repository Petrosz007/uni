using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELTE.Windows.TodoList.Model
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [ForeignKey("List")]
        public int ListId { get; set; }
        
        public virtual List List { get; set; }
    }
}