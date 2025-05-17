﻿using Microsoft.AspNetCore.Identity;

namespace IdentityChatMailProject.Entities
{
    public class AppUser:IdentityUser
    {
        public String? Name { get; set; }
        public String? Surname { get; set; }
        public String? City { get; set; }
        public String? ProfileImageUrl { get; set; }

    }
}
