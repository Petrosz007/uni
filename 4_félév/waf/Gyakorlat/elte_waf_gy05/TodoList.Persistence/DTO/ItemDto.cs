using System;

namespace TodoList.Persistence.DTO
{
    public class ItemDto
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public DateTime Deadline { get; set; }

        public byte[] Image { get; set; }

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
