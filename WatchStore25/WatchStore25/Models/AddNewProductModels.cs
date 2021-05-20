using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatchStore25.Models
{
    public class AddNewProductModels
    {
        public string img { get; set; }

        public int idProduct { get; set; }
        
        public string name { get; set; }

        public int inventory { get; set; }

        public decimal amount { get; set; }

        
    }
}