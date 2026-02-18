using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ShopContext _ShopContext;
        public RatingRepository(ShopContext apiDbContext)
        {
            _ShopContext = apiDbContext;
        }
        public async Task<Rating> AddRating(Rating newRating)
        {
            await _ShopContext.Ratings.AddAsync(newRating);
            await _ShopContext.SaveChangesAsync();
            return newRating;
        }
    }
}
