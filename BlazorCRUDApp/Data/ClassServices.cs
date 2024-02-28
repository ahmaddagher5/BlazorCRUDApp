using BlazorCRUDApp.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BlazorCRUDApp.Data
{
    public class ClassServices
    {
        #region Private members
        private AppDbContext dbContext;
        #endregion

        #region Constructor
        public ClassServices(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// This method returns the list of class
        /// </summary>
        /// <returns></returns>
        public async Task<List<Class>> GetClassesAsync(string name="")
        {
            IQueryable<Class> query = dbContext.Set<Class>();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.ClassName.ToLower().Contains(name.ToLower()));
            }
            return await query.ToListAsync();
        }

        /// <summary>
        /// This method add a new class to the DbContext and saves it
        /// </summary>
        /// <param name="class"></param>
        /// <returns></returns>
        public async Task<Class> AddClassAsync(Class _class)
        {
            try
            {
                dbContext.Classes.Add(_class);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return _class;
        }

        /// <summary>
        /// This method update and existing class and saves the changes
        /// </summary>
        /// <param name="class"></param>
        /// <returns></returns>
        public async Task<Class> UpdateClassAsync(Class _class)
        {
            try
            {
                var classExist = dbContext.Classes.FirstOrDefault(x => x.Id == _class.Id);
                if (classExist != null)
                {
                    _class.ModifiedDate = DateTime.Now;
                    dbContext.Update(_class);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _class;
        }

        /// <summary>
        /// This method removes and existing class from the DbContext and saves it
        /// </summary>
        /// <param name="class"></param>
        /// <returns></returns>
        public async Task DeleteClassAsync(Class _class)
        {
            try
            {
                dbContext.Classes.Remove(_class);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
