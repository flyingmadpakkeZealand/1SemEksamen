﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1SemEksamen.Exceptions
{
    public class WrongOrUnsupportedTypeException : Exception
    {
        public WrongOrUnsupportedTypeException(string message):base(message)
        {
            
        }
    }
}