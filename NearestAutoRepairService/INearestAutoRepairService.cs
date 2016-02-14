using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using AutoRepairShopDTO;
using System.Net.Security;

namespace NearestAutoRepairService
{
    /*
         // Controlling WCF message protection using ProtectionLevel parameter
        // www.youtube.com/watch?v=ybY9dD24gtI&index=48&list=PL6n9fhu94yhVxEyaRMaMN_-qnDdNVGsL1
        [OperationContract(Name = "GetAllAutoRepairs", ProtectionLevel=ProtectionLevel.None)]         
         */

    [ServiceContract(Name = "INearestAutoRepairService")]
    public interface INearestAutoRepairService
    {

        [OperationContract(Name = "GetData", IsOneWay = false, ProtectionLevel = ProtectionLevel.None)]
        [FaultContract(typeof(AutoRepairFault))]
        string GetData();
       

        // All Auto Repair Shops
        [OperationContract(Name = "GetAllAutoRepairs", IsOneWay = false, ProtectionLevel = ProtectionLevel.None)]
        [FaultContract(typeof(AutoRepairFault))]
        List<AutoRepairWCF> GetAllAutoRepairs();
        
        // Nearest 5 Auto Repair Shops
        [OperationContract(Name = "GetFiveNearestAutoRepairs", IsOneWay = false)]
        [FaultContract(typeof(AutoRepairFault))]
        List<AutoRepairWCF> GetFiveNearestAutoRepairs(string userLocation);
                
        // Nearest (only one (1)) Auto Repair Shop
        [OperationContract(Name = "GetNearestAutoRepair", IsOneWay = false)]
        [FaultContract(typeof(AutoRepairFault))]
        AutoRepairWCF GetNearestAutoRepair(string userLocation);


        // New Method Request
        [OperationContract(Name = "GetAllAutoRepairsRequest", IsOneWay = false)]
        [FaultContract(typeof(AutoRepairFault))]
        AutoRepairShopInfoList GetAllAutoRepairRequest(AutoRepairShopRequest autoRepairShopRequest);

        // New Method Request
        [OperationContract(Name = "GetNearestAutoRepairRequest", IsOneWay = false)]
        [FaultContract(typeof(AutoRepairFault))]
        AutoRepairShopInfo GetNearestAutoRepairRequest(AutoRepairShopRequest autoRepairShopRequest);
        
        // New Method Request
        [OperationContract(Name = "GetFiveNearestAutoRepairRequest", IsOneWay = false)]
        [FaultContract(typeof(AutoRepairFault))]
        AutoRepairShopInfoList GetFiveNearestAutoRepairRequest(AutoRepairShopRequest autoRepairShopRequest);
        
    }

}
