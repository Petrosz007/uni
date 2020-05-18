using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Persistence.DTO
{
    public class ItemDto
    {
        [Key]
        public Int32 Id { get; set; }

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

        public static explicit operator Item(ItemDto dto) => new Item
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Deadline = dto.Deadline,
            Image = dto.Image,
            ListId = dto.ListId
        };

        public static explicit operator ItemDto(Item i) => new ItemDto
        {
            Id = i.Id,
            Name = i.Name,
            Description = i.Description,
            Deadline = i.Deadline,
            Image = i.Image,
            ListId = i.ListId
        };
    }
}
