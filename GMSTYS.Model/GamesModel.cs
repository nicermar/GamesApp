using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMSTYS.Repositories;


namespace GMSTYS.Model
{
    public class GamesModel : BaseEntity
    {
        protected DateTime timestamp = DateTime.Now;
        public string Name { get; set; }
        public string Description { get; set; }
        public int AgeRestriction { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }
        public DateTime TimeStamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }
    }
}
