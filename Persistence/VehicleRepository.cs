using Microsoft.EntityFrameworkCore;
using vega.Models;
using System.Threading.Tasks;
using vega.Core;
using System.Collections.Generic;
using vega.Core.Models;
using System.Linq;
using System;
using System.Linq.Expressions;
using vega.Extensions;

namespace vega.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;
        
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<T> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<T>> GetVehicles(VehicleQuery queryObj)
        {
            var query = context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .AsQueryable();
            
            if (queryObj.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId);
            
            var columnsMap = new Dictionary<string, Expression<Func<T, object>>>() {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName
            }; 

            query = query.ApplyOrdering(queryObj, columnsMap);

            query = query.ApplyPaging(queryObj);

            return await query.ToListAsync();
        }


        public void Add(T vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(T vehicle)
        {
            context.Remove(vehicle);
        }
    }
}