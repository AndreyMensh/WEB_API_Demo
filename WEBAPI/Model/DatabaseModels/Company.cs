using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WEBAPI.Model.DatabaseModels
{
    public class Company
    {
        public Company()
        {
            CompanySettings = new CompanySettings
            {
                WorkAtNigth = true,
                ActRequired = true
            };
        }

        [Key] public int Id { get; set; }

        public string Name { get; set; }

        public string BillEmail { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        public string Notes { get; set; }


        public bool IsBlocked { get; set; }
        public DateTime CreatedAt { get; set; }

        public int GeneralUserId { get; set; }
        public CompanySettings CompanySettings { get; set; }

        public List<User> Users { get; set; }

    }
}
