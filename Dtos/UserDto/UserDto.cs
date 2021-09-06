using System;

namespace DeadlineNote.Dtos
{
    public record UserDto
    {
        public Guid id { get; init; }
        public string name { get; init; }
        public string username { get; init; }
        public DateTimeOffset createdDate { get; init; }
    }
}