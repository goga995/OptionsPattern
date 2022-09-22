using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OptionsPattern.Models;

namespace OptionsPattern.Data
{
    public class CommandDbContext : DbContext
    {
        
        public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options)
        {
            
        }
        public DbSet<Command> Commands { get; set; }
    }
}