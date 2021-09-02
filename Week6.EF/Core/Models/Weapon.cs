using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6.EF.Core.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Description { get; set; }

        //Relazione 1 a molti con cavaliere
        public Knight Knight { get; set; }
        public int KnightId { get; set; }

    }
}
