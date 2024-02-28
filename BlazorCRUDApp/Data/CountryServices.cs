using BlazorCRUDApp.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace BlazorCRUDApp.Data
{
    public class CountryServices
    {
        #region Private members
        private AppDbContext dbContext;
        #endregion

        #region Constructor
        public CountryServices(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// This method returns the list of country
        /// </summary>
        /// <returns></returns>
        public async Task<List<Country>> GetCountriesAsync(string name="")
        {
            IQueryable<Country> query = dbContext.Set<Country>();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            return await query.ToListAsync();
        }

        /// <summary>
        /// This method add a new country to the DbContext and saves it
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public async Task<Country> AddCountryAsync(Country _country)
        {
            try
            {
                dbContext.Countries.Add(_country);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return _country;
        }

        /// <summary>
        /// This method update and existing country and saves the changes
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public async Task<Country> UpdateCountryAsync(Country _country)
        {
            try
            {
                var countryExist = dbContext.Countries.FirstOrDefault(x => x.Id == _country.Id);
                if (countryExist != null)
                {
                    _country.ModifiedDate = DateTime.Now;
                    dbContext.Update(_country);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _country;
        }

        /// <summary>
        /// This method removes and existing country from the DbContext and saves it
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public async Task DeleteCountryAsync(Country _country)
        {
            try
            {
                dbContext.Countries.Remove(_country);
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
