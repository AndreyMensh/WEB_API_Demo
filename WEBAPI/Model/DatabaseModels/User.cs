using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WEBAPI.Enums;

namespace WEBAPI.Model.DatabaseModels
{
    public class User
    {
        public User()
        {
            UserSettings = new UserSettings();
            TableSettings = new TableSettings
            {
                Worker = true,
                Date = true,
                Start = true,
                End = true,
                Duration = true,
                Break = true,
                Comment = true,
                Summ = true
            };
        }

        [Key] public int Id { get; set; }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordEncrypted { get; set; }
        public int PasswordFailCount { get; set; }

        public UserStatusEnum UserStatus { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string ExportCode { get; set; }
        
        public DateTime LastUpdate { get; set; }

        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        public UserSettings UserSettings { get; set; }
        public TableSettings TableSettings { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Code { get; set; }
        public DateTime CodeExpire { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public List<Job> Jobs { get; set; }
        public List<AllowedUser> AllowedUsers { get; set; }
        public List<TrustedIpAddress> TrustedIpAddresses { get; set; }


        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
