using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WINTEX.Models;

#nullable disable

namespace WINTEX.DAL
{
    public partial class FagElGamousDbContext : DbContext
    {
        public FagElGamousDbContext()
        {
        }

        public FagElGamousDbContext(DbContextOptions<FagElGamousDbContext> options)
            : base(options)
        {
        }

    }
}
