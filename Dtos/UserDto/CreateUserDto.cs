using System;
using System.ComponentModel.DataAnnotations;

namespace DeadlineNote.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string name { get; init; }
        [Required]
        public string username { get; init; }
        public DateTimeOffset createdDate { get; init; }
    }
}