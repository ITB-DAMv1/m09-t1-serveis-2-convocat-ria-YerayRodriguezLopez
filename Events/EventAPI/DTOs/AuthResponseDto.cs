﻿namespace EventAPI.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
        public DateTime Expiration { get; set; }
    }
}
