using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace NearestAutoRepairService
{
    [DataContract]
    public class AutoRepairFault
    {
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string StackTrace { get; set; }
        [DataMember]
        public string Target { get; set; }
        [DataMember]
        public string Source { get; set; }
    }
}
