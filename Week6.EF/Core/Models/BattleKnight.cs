using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6.EF.Core.Models
{
    class BattleKnight
    {
        public int BattleId { get; set; }
        public int KnightId {get; set;}

        //Proprietà di Payload
        public DateTime DateJoined { get; set; }
    }
}
