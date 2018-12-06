using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Core.Models;
using vega.Models;

namespace vega.Core
{
    public interface IVehicleRepository
    {
         Task<T> GetVehicle(int id, bool includeRelated = true);

         void Add(T vehicle);

         void Remove(T vehicle);
         
         Task<IEnumerable<T>> GetVehicles(VehicleQuery filter);
    }
}