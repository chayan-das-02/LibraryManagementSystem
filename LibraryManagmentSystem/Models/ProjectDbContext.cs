using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace LibraryManagementSystem.Models
{
    public class ProjectDbContext:DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //adding the users table  
            modelBuilder.Entity<UsersModel>().ToTable("Users_Table").HasKey(c => c.user_id); //toTable is used to map the user table

            //adding the role table 
            modelBuilder.Entity<RoleModel>().ToTable("Roles_Table").HasKey(b => b.role_id); //toTable is used to map the role table

            //adding the role table 
            modelBuilder.Entity<BooksModel>().ToTable("Books_Table").HasKey(b => b.book_id); //toTable is used to map the book table

            //adding the role table 
            modelBuilder.Entity<TransactionModel>().ToTable("Transactions_Table").HasKey(b => b.transaction_id); //toTable is used to map the transaction table

            //Hashkey is used to configure the PRIMARY KEY
        }


        //the OnConfiguring methods needs to be override in this class
        //overriding the method
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=connection_string;database=LMS_DB;Integrated Security = true");
        }

        //Dbset for Users_Table
        public DbSet<UsersModel> UsersTable { get; set; }

        //Dbset for Role_Table
        public DbSet<RoleModel> RoleTable { get; set; }

        //Dbset for Books_Table
        public DbSet<BooksModel> BooksTable { get; set; }

        //Dbset for Books_Table
        public DbSet<TransactionModel> TransactionsTable { get; set; }
    }
}
