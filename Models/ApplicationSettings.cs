﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.Models
{
    public class ApplicationSettings
    {
        public string Jwt_Secret { get; set; }
        public string ClientUrl { get; set; }

    }
}
