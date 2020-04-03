using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Infrastructure
{
    interface ICacheProvider
    {
        object Get(string key); //getting data from cache
        void Set(string key, object data, int cacheTime); //adding data to cache
        bool isSet(string key); //checking if something in cache is
        void Invalidate(string key); //remove data from cache
    }
}
