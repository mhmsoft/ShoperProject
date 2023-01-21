﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoper.Entities
{
    public class AppUser : IdentityUser
    {
        public string fullName { get; set; }

        public Customer Customer { get; set; }
    }
}
