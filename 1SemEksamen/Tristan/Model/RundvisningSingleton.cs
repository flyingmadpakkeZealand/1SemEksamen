using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.Tristan.Model
{
    class RundvisningSingleton
    {
        public DateTime RundvisningDateTime { get; set; }

        
        private static RundvisningSingleton _instance = new RundvisningSingleton();

        public static RundvisningSingleton Instance
        {
            get { return _instance; }
        }


        private RundvisningSingleton()
        {
            
        }

    }
}
