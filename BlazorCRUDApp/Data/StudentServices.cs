using BlazorCRUDApp.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlazorCRUDApp.Data
{
    public class StudentServices
    {
        #region Private members
        private AppDbContext dbContext;
        #endregion

        #region Constructor
        public StudentServices(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// This method returns the list of student
        /// </summary>
        /// <returns></returns>
        public async Task<List<Student>> GetStudentsAsync(string name="", int _class=0, int country = 0)
        {
            IQueryable<Student> query = dbContext.Set<Student>();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            if (_class>0)
            {
                query = query.Where(x => x.ClassId == _class);
            }
            if (country > 0)
            {
                query = query.Where(x => x.CountryId == country);
            }
            return await query.ToListAsync();
        }

        /// <summary>
        /// This method add a new student to the DbContext and saves it
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task<Student> AddStudentAsync(Student _student)
        {
            try
            {
                dbContext.Students.Add(_student);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return _student;
        }

        /// <summary>
        /// This method update and existing student and saves the changes
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task<Student> UpdateStudentAsync(Student _student)
        {
            try
            {
                var studentExist = dbContext.Students.FirstOrDefault(x => x.Id == _student.Id);
                if (studentExist != null)
                {
                    _student.ModifiedDate = DateTime.Now;
                    dbContext.Update(_student);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _student;
        }

        /// <summary>
        /// This method removes and existing student from the DbContext and saves it
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task DeleteStudentAsync(Student _student)
        {
            try
            {
                dbContext.Students.Remove(_student);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> GetStudentsAverageAgeAsync()
        {
            int average = 0;
            var today = DateTime.Now;
            var list = await dbContext.Students.Where(x=>x.DateOfBirth.HasValue).ToListAsync();
            average = (int)list.Average(x => (today - x.DateOfBirth.Value).TotalDays)/365;
            return average;
        }
        public async Task<List<ChartObj>> GetStudentsPerClass()
        {
            var list = new List<ChartObj>();
            var classList = await dbContext.Students
                                .Join(dbContext.Classes, s => s.ClassId, c => c.Id, (s, c) => new { s, c })
                                .GroupBy(x => x.c.Id)
                                .Select(x => new { x.Key, x.First().c.ClassName, Count = x.Count() })
                                .ToListAsync();
            if (classList.Count > 0)
            {
                list = classList.Select(x => new ChartObj(x.ClassName, x.Count)).ToList();
            }
            return list;
        }
        public async Task<List<ChartObj>> GetStudentsCountry()
        {
            var list = new List<ChartObj>();
            var classList = await dbContext.Students
                                .Join(dbContext.Countries, s => s.CountryId, c => c.Id, (s, c) => new { s, c })
                                .GroupBy(x => x.c.Id)
                                .Select(x => new { x.Key, x.First().c.Name, Count = x.Count() })
                                .ToListAsync();
            if (classList.Count > 0)
            {
                list = classList.Select(x => new ChartObj(x.Name, x.Count)).ToList();
            }
            return list;
        }
        
        #endregion
    }
}
