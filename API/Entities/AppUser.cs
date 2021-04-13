using System;
using System.Collections.Generic;
using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        //public int Id { get; set; } //already has in parent class

        //public string UserName { get; set; } //UserName already has in parent class

        //public byte[] PasswordHash { get; set; } //already has in parent class

        //public byte[] PasswordSalt { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime LastActivite { get; set; } = DateTime.Now;

        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }

        public ICollection<UserLike> LikedByUser { get; set; }
        public ICollection<UserLike> LikedUser { get; set; }

        public ICollection<Message> MessagesSend { get; set; }
        public ICollection<Message> MessagesRecieve { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }


    }
}