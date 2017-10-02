﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompression
{
    abstract class Compress<T> where T:Normalise
    {
        public virtual void compressFile(T norm)
        {

        }
    }
}
