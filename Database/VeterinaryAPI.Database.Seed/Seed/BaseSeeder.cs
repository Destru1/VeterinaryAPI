﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Seed.Seed.Interfaces;

namespace VeterinaryAPI.Database.Seed.Seed
{
    public abstract class BaseSeeder : IBaseSeeder
    {
        public abstract Task SeedAsync(IServiceScope serviceProvider);
    }
}
