using DeadlineNote.Dtos;
using DeadlineNote.Entities;

namespace DeadlineNote
{
    public static class Extensions
    {
        public static UserDto AsDto(this User user)
        {
            return new UserDto
            {
                id = user.id,
                name = user.name,
                username = user.username,
                createdDate = user.createdDate
            };
        }
        
    }
}