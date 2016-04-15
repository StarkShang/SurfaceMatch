using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace ProjectCSharp
{
    static class ExtendMethod
    {
        static public Complex ToComplex(this UVParam value)
        {
            return new Complex(value.U, value.V);
        }
        static public UVParam ToUVParam(this Complex value)
        {
            return new UVParam(value.Real, value.Imaginary);
        }
    }
}
