﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class RegisterDto: IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string ContactNumber { get; set; }
        public string Password { get; set; }

        public int GenderId { get; set; }
        public int MaritalStatusId { get; set; }
    }
}
