using BlazorCRUDApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorCRUDApp.ApplicationDbContext
{
    public class AppDbContext: DbContext
    {
        #region Contructor
        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {

        }
        #endregion

        #region Public properties
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Country> Countries { get; set; }
        #endregion

        #region Overidden methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>().HasData(GetClasses());
            modelBuilder.Entity<Student>().HasData(GetStudents());
            modelBuilder.Entity<Country>().HasData(GetCountries());
            base.OnModelCreating(modelBuilder);
        }
        #endregion


        #region Private methods
        private List<Class> GetClasses()
        {
            return new List<Class>
            {
                new Class { Id = 1001, ClassName = "Class 1"},
                new Class { Id = 1002, ClassName = "Class 2"},
                new Class { Id = 1003, ClassName = "Class 3"},
                new Class { Id = 1004, ClassName = "Class 4"},
                new Class { Id = 1005, ClassName = "Class 5"}
            };
        }
        private List<Student> GetStudents()
        {
            return new List<Student>
            {
                new Student { Id = 1001, Name = "Ahmed", ClassId = 1001, CountryId=1004, DateOfBirth=DateTime.Parse("1990-01-01")},
                new Student { Id = 1002, Name = "Ali", ClassId = 1002, CountryId=1001, DateOfBirth=DateTime.Parse("1990-07-14")},
                new Student { Id = 1003, Name = "Yusuf", ClassId = 1004, CountryId=1001, DateOfBirth=DateTime.Parse("1990-05-12")},
                new Student { Id = 1004, Name = "Mohamed", ClassId = 1003, CountryId=1001, DateOfBirth=DateTime.Parse("1990-04-05")},
                new Student { Id = 1005, Name = "Salem", ClassId = 1005, CountryId=1001, DateOfBirth=DateTime.Parse("1990-08-22")}
            };
        }
        private List<Country> GetCountries()
        {
            return new List<Country>
            {
                new Country { Id = 1001, Name = "Oman"},
                new Country { Id = 1002, Name = "UAE"},
                new Country { Id = 1003, Name = "Qatar"},
                new Country { Id = 1004, Name = "Bahrain"},
                new Country { Id = 1005, Name = "Saudi Arabia"},
                new Country { Id = 1006, Name = "Kuwait"},
            };
        }
        #endregion
    }
}
