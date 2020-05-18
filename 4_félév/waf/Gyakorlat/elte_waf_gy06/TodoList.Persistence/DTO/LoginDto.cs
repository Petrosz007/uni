﻿using System.ComponentModel.DataAnnotations;

namespace TodoList.Persistence.DTO
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}