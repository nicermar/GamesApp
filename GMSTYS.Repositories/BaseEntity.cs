using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMSTYS.Repositories
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public bool IsNew { get; set; }
    }
}
