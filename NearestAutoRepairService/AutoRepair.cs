using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace NearestAutoRepairService
{
    [DataContract]
    public partial class AutoRepairWCF
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
