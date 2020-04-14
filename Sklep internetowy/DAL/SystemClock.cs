using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.DAL.Interfaces
{
    public class SystemClock : IClock
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}