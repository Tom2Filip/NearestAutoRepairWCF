using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using AutoRepairShopDTO;
using AutoRepairShopBLL;
using System.Globalization;
using System.Data.SqlClient;
using System.Security.Permissions;


namespace NearestAutoRepairService
{

    // Set InstanceContextMode to PerCall
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class NearestAutoRepairService: INearestAutoRepairService
    {

        //[PrincipalPermission(SecurityAction.Demand, Role = "member")]
        public string GetData()
        {
            //if (HttpContext.Current.Session["Result"] != null)
            //    return string.Format("You entered: {0}", value);
            return string.Format("Hello there!");
        }
        /*
         // Controlling WCF message protection using ProtectionLevel parameter
        // www.youtube.com/watch?v=ybY9dD24gtI&index=48&list=PL6n9fhu94yhVxEyaRMaMN_-qnDdNVGsL1
        [OperationContract(Name = "GetAllAutoRepairs", ProtectionLevel=ProtectionLevel.None)]         
         */
        // User has to be in Role to call the service operation
       // [PrincipalPermission(SecurityAction.Demand, Role = "member")]
        public List<AutoRepairWCF> GetAllAutoRepairs()
        {
            List<AutoRepairDTO> autoRepairDTOList = new List<AutoRepairDTO>();
           // List<AutoRepairWCF> autoRepairWCFList = new List<AutoRepairWCF>();
            List<AutoRepairWCF> autoRepairWCFList2 = new List<AutoRepairWCF>();                     
            try
            {                
              using(AutoRepairShopRepositoryBLL context = new AutoRepairShopRepositoryBLL())
              { 
                autoRepairDTOList = context.GetAllAutoRepairs();
               // TranslateAutoRepairShopDTOListToAuotoRepairWCFList(autoRepairDTOList, ref autoRepairWCFList);
                autoRepairWCFList2 = TranslateAutoRepairShopDTOListToAuotoRepairWCFList2(autoRepairDTOList);
              }
            }
            catch (Exception ex)
            {                
                if (ex.InnerException != null) ex = ex.InnerException;

                string sqlExceptionMessage = null;
                if (ex is SqlException)
                {
                    sqlExceptionMessage = "The underlying provider failed on Open. Cannot Open the Database.";

                    throw new FaultException<AutoRepairFault>(
                    new AutoRepairFault
                    {
                        ErrorMessage = sqlExceptionMessage,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace,
                        Target = ex.TargetSite.ToString()
                    },
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception: " + ex.Message)));
                }
                else
                {
                    throw new FaultException<AutoRepairFault>(
                    new AutoRepairFault
                    {
                        ErrorMessage = sqlExceptionMessage + "\n" + ex.Message,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace,
                        Target = ex.TargetSite.ToString()
                    },
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception: " + ex.Message)));
                }                
            }

            if (autoRepairDTOList == null)
            {
                string msg = string.Format("No Auto Repair Shop found.");
                throw new FaultException<AutoRepairFault>(new AutoRepairFault
                {
                    ErrorMessage = msg,
                    Source = null,
                    StackTrace = null,
                    Target = null
                },
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception: " + msg)));
            }
            return autoRepairWCFList2;
          //return autoRepairWCFList;
        }

        // Nearest 5 Auto Repair Shops
        public List<AutoRepairWCF> GetFiveNearestAutoRepairs(string userLocation)
        {
            List<AutoRepairDTO> autoRepairDTOList = new List<AutoRepairDTO>();
            List<AutoRepairWCF> autoRepairWCFList = new List<AutoRepairWCF>();
            try
            {
                using (AutoRepairShopRepositoryBLL context = new AutoRepairShopRepositoryBLL())
                {
                    autoRepairDTOList = context.GetFiveNearestAutoRepairs(userLocation);
                    TranslateAutoRepairShopDTOListToAuotoRepairWCFList(autoRepairDTOList, ref autoRepairWCFList);
                }
            }
            catch (Exception ex)
            {
                //Part 18 Throwing fault exceptions from a WCF service
                //throw new FaultException();
                throw new FaultException<AutoRepairFault>(
                    new AutoRepairFault
                    {
                        ErrorMessage = ex.Message,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace,
                        Target = ex.TargetSite.ToString()
                    },
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception: " + ex.Message)));
            }

            if (autoRepairDTOList == null)
            {
                string msg = string.Format("No Auto Repair Shop found.");
                throw new FaultException<AutoRepairFault>(new AutoRepairFault
                {
                    ErrorMessage = msg,
                    Source = null,
                    StackTrace = null,
                    Target = null
                },
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception: " + msg)));
            }
            
            return autoRepairWCFList;
        }
              
        public AutoRepairWCF GetNearestAutoRepair(string userLocation)
        {
            AutoRepairDTO autoRepairDTO = null;
            AutoRepairWCF autoRepairWCF = new AutoRepairWCF();

            try
            {
                using (AutoRepairShopRepositoryBLL context = new AutoRepairShopRepositoryBLL())
                {
                    autoRepairDTO = context.GetNearestAutoRepair(userLocation);
                    TranslateAutoRepairDTOToAutoRepairWCF(autoRepairDTO, autoRepairWCF);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<AutoRepairFault>(
                    new AutoRepairFault
                    {
                        ErrorMessage = ex.Message,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace,
                        Target = ex.TargetSite.ToString()
                    },
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception: " + ex.Message)));

                // throw new FaultException("Service fault exception: " +ex.Message);
            }

            if (autoRepairDTO == null)
            {
                string msg = string.Format("No AutoRepairShop found for user location {0}", userLocation);
                throw new FaultException<AutoRepairFault>(new AutoRepairFault
                {
                    ErrorMessage = msg,
                    Source = null,
                    StackTrace = null,
                    Target = null
                },
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception: " + msg)));
            }           

            return autoRepairWCF;
        }


        private void TranslateAutoRepairDTOToAutoRepairWCF(AutoRepairDTO autoRepairDTO, AutoRepairWCF autoRepairWCF)
        {
             autoRepairWCF.ID = autoRepairDTO.ID;
             autoRepairWCF.Name = autoRepairDTO.Name;
             autoRepairWCF.Address = autoRepairDTO.Address;
             autoRepairWCF.City = autoRepairDTO.City;
             autoRepairWCF.Phone   = autoRepairDTO.Phone;
             autoRepairWCF.Url = autoRepairDTO.Url;
             autoRepairWCF.Geolocation = autoRepairDTO.Geolocation;
        }
        
                                            // if there is no 'ref' keyword then autoRepairWCFList is null and nothing (empty list) gets passed to the client
        private void TranslateAutoRepairShopDTOListToAuotoRepairWCFList(List<AutoRepairDTO> autoRepairShopDTOList, ref List<AutoRepairWCF> autoRepairWCFList)
        {
            autoRepairWCFList = autoRepairShopDTOList.ConvertAll(x => new AutoRepairWCF
            {
                ID = x.ID,
                Name = x.Name,
                Address = x.Address,
                City = x.City,
                Phone = x.Phone,
                Url = x.Url,
                Geolocation = x.Geolocation.ToString()
                //RowVersion = product.RowVersion
            });
        }

        // Another way to convert AutoRepairDTO list to AutoRepairWCF list
        private List<AutoRepairWCF> TranslateAutoRepairShopDTOListToAuotoRepairWCFList2(List<AutoRepairDTO> autoRepairShopDTOList)
        {
            List<AutoRepairWCF> autoRepairWCFList = autoRepairShopDTOList.ConvertAll(x => new AutoRepairWCF
            {
                ID = x.ID,
                Name = x.Name,
                Address = x.Address,
                City = x.City,
                Phone = x.Phone,
                Url = x.Url,
                Geolocation = x.Geolocation.ToString()
                //RowVersion = product.RowVersion
            });
            return autoRepairWCFList;
        }


        public AutoRepairShopInfoList GetAllAutoRepairRequest(AutoRepairShopRequest autoRepairShopRequest)
        {
            List<AutoRepairDTO> autoRepairDTOList = new List<AutoRepairDTO>();
            AutoRepairShopInfoList autoRepairShopInfoList = new AutoRepairShopInfoList();

            try
            {
                using (AutoRepairShopRepositoryBLL context = new AutoRepairShopRepositoryBLL())
                {
                    autoRepairDTOList = context.GetAllAutoRepairs();
                    TranslateAutoRepairShopDTOListToAutoRepairShopInfoSList(autoRepairDTOList, ref autoRepairShopInfoList);
                }
            }
            catch (Exception ex)
            {
                //Part 18 Throwing fault exceptions from a WCF service
                throw new FaultException<AutoRepairFault>(
                    new AutoRepairFault
                    {
                        ErrorMessage = ex.Message,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace,
                        Target = ex.TargetSite.ToString()
                    },
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception: " + ex.Message)));
            }

            if (autoRepairShopInfoList == null)
            {
                string msg = string.Format("No Auto Repair Shop found.");
                throw new FaultException<AutoRepairFault>(new AutoRepairFault
                {
                    ErrorMessage = msg,
                    Source = null,
                    StackTrace = null,
                    Target = null
                },
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception: " + msg)));
            }
            return autoRepairShopInfoList;
        }

        /*
        // Another way to convert AutoRepairDTO list to AutoRepairWCF list
        private List<AutoRepairShopInfo> TranslateAutoRepairShopDTOListToAutoRepairShopInfoList2(List<AutoRepairDTO> autoRepairShopDTOList)
        {
            List<AutoRepairShopInfo> autoRepairShopInfoList = autoRepairShopDTOList.ConvertAll(x => new AutoRepairShopInfo
            {
                ID = x.ID,
                Name = x.Name,
                Address = x.Address,
                City = x.City,
                Phone = x.Phone,
                Url = x.Url,
                Geolocation = x.Geolocation.ToString()
                //RowVersion = product.RowVersion
            });
            return autoRepairShopInfoList;
        }
        */

        /* */ // AutoRepairShopRequestObject - check for License Key or UserName and Password
        public AutoRepairShopInfo GetNearestAutoRepairRequest(AutoRepairShopRequest autoRepairShopRequest)
        {
            //AutoRepairDTO autoRepairDTO = null;
           // AutoRepairShopInfo autoRepairShopInfoObject = new AutoRepairShopInfo();                        
            
            try
            {
                using (AutoRepairShopRepositoryBLL context = new AutoRepairShopRepositoryBLL())
                {
                    AutoRepairDTO autoRepairDTO = null;                    
                                                                // AutoRepairShopRequestObject
                    autoRepairDTO = context.GetNearestAutoRepair(autoRepairShopRequest.Geolocation);

                    AutoRepairShopInfo autoRepairShopInfoObject = new AutoRepairShopInfo(autoRepairDTO);
                    //TranslateAutoRepairDTOToAutoRepairShopInfoObject(autoRepairDTO, autoRepairShopInfoObject);
                    //autoRepairShopInfoObject = context.GetNearestAutoRepair(autoRepairShopRequest.Geolocation);
                    return autoRepairShopInfoObject;                    
                }
            }
            catch (Exception ex)
            {
                 throw new FaultException<AutoRepairFault>(
                    new AutoRepairFault
                    {
                        ErrorMessage = ex.Message,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace,
                        Target = ex.TargetSite.ToString()
                    },
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception: " + ex.Message)));                
            }
            /*
            if (autoRepairShopInfoObject == null)
            {
                string msg = string.Format("No AutoRepairShop found for user location {0}", autoRepairShopRequest.Geolocation);
                throw new FaultException<AutoRepairFault>(new AutoRepairFault
                {
                    ErrorMessage = msg,
                    Source = null,
                    StackTrace = null,
                    Target = null
                },
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception: " + msg)));
            }
            */
          //return autoRepairShopInfoObject;
        }
        
        /**/

        /*
        private void TranslateAutoRepairDTOToAutoRepairShopInfoObject(AutoRepairDTO autoRepairDTO, AutoRepairShopInfo autoRepairShopInfoObject)
        {
            autoRepairShopInfoObject.ID = autoRepairDTO.ID;
            autoRepairShopInfoObject.Name = autoRepairDTO.Name;
            autoRepairShopInfoObject.Address = autoRepairDTO.Address;
            autoRepairShopInfoObject.City = autoRepairDTO.City;
            autoRepairShopInfoObject.Phone = autoRepairDTO.Phone;
            autoRepairShopInfoObject.Url = autoRepairDTO.Url;
            autoRepairShopInfoObject.Geolocation = autoRepairDTO.Geolocation;
        }
        */

        /**/
        /*
        // if there is no 'ref' keyword then autoRepairWCFList is null and nothing (empty list) gets passed to the client       
        private void TranslateAutoRepairShopDTOListToAutoRepairShopInfoList(List<AutoRepairDTO> autoRepairShopDTOList, ref List<AutoRepairShopInfo> autoRepairShopInfoObjectList)
        {
            autoRepairShopInfoObjectList = autoRepairShopDTOList.ConvertAll(x => new AutoRepairShopInfo
            {
                ID = x.ID,
                Name = x.Name,
                Address = x.Address,
                City = x.City,
                Phone = x.Phone,
                Url = x.Url,
                Geolocation = x.Geolocation.ToString()
                //RowVersion = product.RowVersion
            });
        }
       */        
                

        /**/
        // stackoverflow.com/questions/13739729/wcf-messagecontract-wrapping-and-lists
        // forums.asp.net/t/1775946.aspx?WCF+Service+returning+an+array+instead+of+the+response+object+defined+in+the+Message+Contract        
        public AutoRepairShopInfoList GetFiveNearestAutoRepairRequest(AutoRepairShopRequest autoRepairShopRequest)
        {
            List<AutoRepairDTO> autoRepairDTOList = new List<AutoRepairDTO>();
            AutoRepairShopInfoList autoRepairShopInfoList = new AutoRepairShopInfoList();

            try
            {
                using (AutoRepairShopRepositoryBLL context = new AutoRepairShopRepositoryBLL())
                {
                autoRepairDTOList = context.GetFiveNearestAutoRepairs(autoRepairShopRequest.Geolocation);
                TranslateAutoRepairShopDTOListToAutoRepairShopInfoSList(autoRepairDTOList, ref autoRepairShopInfoList);           
               }
            }
            catch (Exception ex)
            {
                //Part 18 Throwing fault exceptions from a WCF service
                throw new FaultException<AutoRepairFault>(
                    new AutoRepairFault
                    {
                        ErrorMessage = ex.Message,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace,
                        Target = ex.TargetSite.ToString()
                    },
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception: " + ex.Message)));
            }

            if (autoRepairShopInfoList == null)
            {
                string msg = string.Format("No Auto Repair Shop found.");
                throw new FaultException<AutoRepairFault>(new AutoRepairFault
                {
                    ErrorMessage = msg,
                    Source = null,
                    StackTrace = null,
                    Target = null
                },
                    new FaultReason(string.Format(CultureInfo.InvariantCulture, "{0}", "Service fault exception: " + msg)));
            }

          return autoRepairShopInfoList;
        }
        
        
        ///////////
        // if there is no 'ref' keyword then autoRepairWCFList is null and nothing (empty list) gets passed to the client       
        private void TranslateAutoRepairShopDTOListToAutoRepairShopInfoSList(List<AutoRepairDTO> autoRepairShopDTOList, ref AutoRepairShopInfoList autoRepairShopInfoList)
        {
            foreach (AutoRepairDTO item in autoRepairShopDTOList)
            {
                autoRepairShopInfoList.AutoRepairShopInfos.Add(new AutoRepairShopInfoS() 
                                                                { ID= item.ID,
                                                                  Name = item.Name,
                                                                  Address = item.Address,
                                                                  City = item.City,                                                                    
                                                                  Geolocation = item.Geolocation,
                                                                  Phone = item.Phone,                                                                      
                                                                  Url = item.Url
                                                                });               
            }                      
        }
       /////////

    }
}
