using System;

namespace DeadlineNote.Entities
{
    public record User
    {
        public Guid id { get; init; }
        public string name { get; init; }
        public string username { get; init; }
        public string password { get; init; }
        public DateTimeOffset createdDate { get; init; }
    }
}