using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.DAL.Interfaces
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}
