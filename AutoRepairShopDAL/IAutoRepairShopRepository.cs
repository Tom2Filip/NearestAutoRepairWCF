using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Spatial;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShopDTO;

namespace AutoRepairShopDAL
{
    public interface IAutoRepairShopRepository : IDisposable
    {
        List<AutoRepairDTO> GetAllAutoRepairs();
        List<AutoRepairDTO> GetFiveNearestAutoRepairs(string userLocation);
        AutoRepairDTO GetNearestAutoRepair(string userLocation);
    }
}
