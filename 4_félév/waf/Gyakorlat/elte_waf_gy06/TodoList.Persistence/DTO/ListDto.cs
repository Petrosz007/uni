using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Persistence.DTO
{
    public class ListDto
    {
        [Key]
        public Int32 Id { get; set; }

        [Required]
        [MaxLength(30)]
        public String Name { get; set; }

        public static explicit operator List(ListDto dto) => new List
        {
            Id = dto.Id,
            Name = dto.Name
        };

        public static explicit operator ListDto(List l) => new ListDto
        {
            Id = l.Id,
            Name = l.Name
        };
    }
}
