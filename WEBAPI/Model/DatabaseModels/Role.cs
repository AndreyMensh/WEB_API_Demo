﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WEBAPI.Model.DatabaseModels
{
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalName { get; set; }

        public List<User> Users { get; set; }
    }
}
