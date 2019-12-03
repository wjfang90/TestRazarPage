﻿using Microsoft.EntityFrameworkCore;
using TestRazarPage.Entity;
namespace TestRazarPage
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions option) : base(option)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}