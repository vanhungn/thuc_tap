using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Shared.model
{
    [ProtoContract]
    public class DeleteRequest
    {
        [ProtoMember(1)]
        public int Id { get; set; }
    }
}
