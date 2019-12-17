using Microsoft.EntityFrameworkCore;
using TestRazarPage.Entity;
namespace TestRazarPage
{

    /// <summary>
    /// 添加 ef core
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions option) : base(option)
        {

        }

        /// <summary>
        /// 客户表对应实体
        /// </summary>
        /// <value></value>
        public DbSet<Customer> Customers { get; set; }
    }
}