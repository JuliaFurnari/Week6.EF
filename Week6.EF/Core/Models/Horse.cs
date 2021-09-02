using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6.EF.Core.Models
{
    public class Horse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Relazione 1 a 1 con cavaliere
        public int KnightId { get; set; } //cavallo deve avere un cavaliere perchè
                                          //int non è nullable di default
        //public int? KnightId { get; set; } //se rendo nullable la FK non è più così
    }
}
