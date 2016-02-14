using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using AutoRepairShopDTO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace NearestAutoRepairService
{
    [MessageContract(IsWrapped = true, WrapperName = "AutoRepairShopRequestObject", WrapperNamespace = "http://MyCompany.com/AutoRepair")]
    public class AutoRepairShopRequest
    {
         [MessageBodyMember(Order = 1)]        
         public string Geolocation { get; set; }         
         
         [MessageHeader]
         public String LicenseKey { get; set; }  
    }

    // Returning One Object
     [MessageContract(IsWrapped = true, WrapperName = "AutoRepairShopInfoObject", WrapperNamespace = "http://MyCompany.com/AutoRepair")]      
     public class AutoRepairShopInfo
     {
         /*
         // Constructor parameterless - for saving if needed.
         public AutoRepairShopInfo()
         {
         }
         */
          
         // Constructor - initialize property values from autoRepairWCF         
         public AutoRepairShopInfo(AutoRepairDTO autoRepairDTO)
         {
             this.ID = autoRepairDTO.ID;
             this.Name = autoRepairDTO.Name;
             this.Address = autoRepairDTO.Address;
             this.City = autoRepairDTO.City;
             this.Phone = autoRepairDTO.Phone;
             this.Url = autoRepairDTO.Url;
             this.Geolocation = autoRepairDTO.Geolocation;
         }
         //   */

         [MessageBodyMember(Name="ID", Namespace="http://MyCompany.com/AutoRepair", Order = 1)]
         public int ID { get; set; }
         [MessageBodyMember(Name = "Name", Namespace = "http://MyCompany.com/AutoRepair", Order = 2)]
         public string Name { get; set; }
         [MessageBodyMember(Name = "Address", Namespace = "http://MyCompany.com/AutoRepair", Order = 3)]
         public string Address { get; set; }
         [MessageBodyMember(Name = "City", Namespace = "http://MyCompany.com/AutoRepair",Order = 4)]
         public string City { get; set; }
         [MessageBodyMember(Name = "Phone", Namespace = "http://MyCompany.com/AutoRepair", Order = 5)]
         public string Phone { get; set; }
         [MessageBodyMember(Name = "Url", Namespace = "http://MyCompany.com/AutoRepair", Order = 6)]
         public string Url { get; set; }
         [MessageBodyMember(Name = "Geolocation", Namespace = "http://MyCompany.com/AutoRepair", Order = 7)]
         public string Geolocation { get; set; }
         
     }        
 

    // Returning List Of Objects
     [MessageContract(IsWrapped = true, WrapperName = "AutoRepairShopResponse", WrapperNamespace = "http://MyCompany.com/AutoRepair")]
     public class AutoRepairShopInfoList
     {
         [MessageBodyMember(Name = "AutoRepairShopInfosList")]
         public List<AutoRepairShopInfoS> AutoRepairShopInfos = new List<AutoRepairShopInfoS>();

        // [MessageBodyMember]
         // public Collection<AutoRepairShopInfoS> autoRepairShopInfos = new Collection<AutoRepairShopInfoS>();
     }

     // If MessageContract and MessageBodyMember is used then properties are not listed in order
     [DataContract(Name = "AutoRepairShopInfos")]
     //[MessageContract(IsWrapped = true, WrapperName = "AutoRepairShopInfos2", WrapperNamespace = "http://MyCompany.com/AutoRepair")]
      public class AutoRepairShopInfoS
      {        
          [DataMember(Order = 1)]
          public int ID { get; set; }
          [DataMember(Order = 2)]
          public string Name { get; set; }
          [DataMember(Order = 3)]
          public string Address { get; set; }
          [DataMember(Order = 4)]
          public string City { get; set; }
          [DataMember(Order = 5)]
          public string Phone { get; set; }
          [DataMember(Order = 6)]
          public string Url { get; set; }
          [DataMember(Order = 7)]
          public string Geolocation { get; set; }
          
      }
    
 }
