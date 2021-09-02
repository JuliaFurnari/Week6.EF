using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6.EF.Core.Models
{
    public class Battle
    {
        public int BattleId { get; set; }
        public string Name { get; set; }

        //Relazione molti a molti con i cavalieri
        public List<Knight> Knights { get; set; } = new List<Knight>();

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
