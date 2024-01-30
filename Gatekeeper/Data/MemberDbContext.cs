using Gatekeeper.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Gatekeeper.Data
{
    public class MemberDbContext : DbContext
    {
        private readonly IConfiguration _config;
        private readonly ILogger<MemberDbContext> _logger;

        public MemberDbContext(DbContextOptions<MemberDbContext> options, IConfiguration config, ILogger<MemberDbContext> logger) : base(options)
        {
            _config = config;
            _logger = logger;
        }

        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<GuildAffiliation> GuildAffiliations { get; set; } = null!;
        public DbSet<AuthCode> AuthCodes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? db_conn_str = _config["DB_CONN_STR"];
            if (db_conn_str == null)
                _logger.LogCritical("No DB connection string provided.");
            else
                optionsBuilder.UseNpgsql(db_conn_str);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
