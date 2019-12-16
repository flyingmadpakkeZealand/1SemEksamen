using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1SemEksamen.Tristan.Model;

namespace _1SemEksamen.Tristan.ViewModel
{
    class BesøgendeStatestikViewModel
    {
        public List<YakseKasser> YakseDag { get; set; }

        public List<YakseKasser> YakseUge { get; set; }
        public BesøgendeStatestikViewModel()
        {
            YakseDag = new List<YakseKasser>();
            YakseDag.Add(new YakseKasser(1, 10));
            YakseDag.Add(new YakseKasser(2, 9));
            YakseDag.Add(new YakseKasser(3, 8));
            YakseDag.Add(new YakseKasser(4, 7));
            YakseDag.Add(new YakseKasser(5, 6));
            YakseDag.Add(new YakseKasser(6, 5));
            YakseDag.Add(new YakseKasser(7, 4));
            YakseDag.Add(new YakseKasser(8, 3));
            YakseDag.Add(new YakseKasser(9, 2));
            YakseDag.Add(new YakseKasser(10, 1));
            YakseDag.Add(new YakseKasser(10, 0));

            YakseUge = new List<YakseKasser>();
            YakseUge.Add(new YakseKasser(1, 1));
            YakseUge.Add(new YakseKasser(2, 2));
            YakseUge.Add(new YakseKasser(3, 3));
            YakseUge.Add(new YakseKasser(4, 4));
            YakseUge.Add(new YakseKasser(5, 5));
            YakseUge.Add(new YakseKasser(6, 6));
            YakseUge.Add(new YakseKasser(7, 7));
            YakseUge.Add(new YakseKasser(8, 8));
            YakseUge.Add(new YakseKasser(9, 9));
            YakseUge.Add(new YakseKasser(10, 10));

        }
    }
}
