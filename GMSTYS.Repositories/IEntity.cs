﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMSTYS.Repositories
{
    interface IEntity
    {
        int Id { get; set; }
        bool IsNew { get; set; }
    }
}