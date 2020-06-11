using Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.ViewModels
{
    public class EditProductNewViewModel
    {
        public Product Product { get; set; }       
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Brand> Brand { get; set; }
        public IEnumerable<Gender> Gender { get; set; }
        public bool ? Confirm { get; set; }
    }
}