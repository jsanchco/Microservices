using System;

namespace Users.Service.Queries.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string DNI { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
