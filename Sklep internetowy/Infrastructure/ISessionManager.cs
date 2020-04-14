using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Infrastructure
{
    public interface ISessionManager
    {
        T Get<T>(string key); //get session
        void Set<T>(string key, T value); //set session
        void Abandon(); //remove session
        T TryGet<T>(string key); //get session with try/catch
    }
}
