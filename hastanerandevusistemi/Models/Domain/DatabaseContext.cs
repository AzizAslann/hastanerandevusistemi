﻿using hastanerandevusistemi.Models.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hastanerandevusistemi.Models.Domain
{
    public class DatabaseContext:IdentityDbContext<ApplicationUser> 
    {
        public DatabaseContext(DbContextOptions<DatabaseContext>options):base(options)
        {
        }
    }
}
