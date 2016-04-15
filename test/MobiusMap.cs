using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ProjectCSharp
{
    class MobiusMap
    {
        public MobiusParam ControlParams { get; set; }
        public MobiusMap(List<Complex> sourcePts, List<Complex> targetPts)
        {
            var z0 = sourcePts[0];
            var z1 = sourcePts[1];
            var z2 = sourcePts[2];
            var w0 = targetPts[0];
            var w1 = targetPts[1];
            var w2 = targetPts[2];
            var z10 = z1 - z0;
            var z12 = z1 - z2;
            var w10 = w1 - w0;
            var w12 = w1 - w2;
            var a = z12 * w10 * w2 - z10 * w12 * w0;
            var b = z2 * z10 * w12 * w0 - z0 * z12 * w10 * w2;
            var c = z12 * w10 - z10 * w12;
            var d = z2 * z10 * w12 - z0 * z12 * w10;
            ControlParams = new MobiusParam(a, b, c, d);
        }
        
        /// <summary>
        /// 通过Mobius变换计算映射点
        /// </summary>
        /// <param name="srcPoint">原平面点</param>
        /// <param name="ctrlParams">控制参数</param>
        /// <returns></returns>
        public Complex GetMapPoint(Complex sourcePoint)
        {
            var z = new Complex(sourcePoint.Real, sourcePoint.Imaginary);
            var y = (ControlParams.a * z + ControlParams.b) / (ControlParams.c * z + ControlParams.d);
            return y;
        }
    }

    class MobiusParam
    {
        public Complex a { get; set; }
        public Complex b { get; set; }
        public Complex c { get; set; }
        public Complex d { get; set; }
        public MobiusParam() { }
        public MobiusParam(Complex a, Complex b, Complex c, Complex d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        /*****************************
        重载运算符
        *****************************/
        /// <summary>
        /// Mobius参数加法，对应参数相加
        /// </summary>
        /// <param name="lhs">左值</param>
        /// <param name="rhs">右值</param>
        /// <returns></returns>
        public static MobiusParam operator +(MobiusParam lhs, MobiusParam rhs)
        {
            var result = new MobiusParam();
            result.a = lhs.a + rhs.a;
            result.b = lhs.b + rhs.b;
            result.c = lhs.c + rhs.c;
            result.d = lhs.d + rhs.d;
            return result;
        }
        /// <summary>
        /// Mobius参数数乘，数分别乘上各个参数（double重载版本）
        /// </summary>
        /// <param name="lhs">左值</param>
        /// <param name="rhs">右值</param>
        /// <returns></returns>
        public static MobiusParam operator *(double lhs, MobiusParam rhs)
        {
            var result = new MobiusParam();
            result.a = lhs * rhs.a;
            result.b = lhs * rhs.b;
            result.c = lhs * rhs.c;
            result.d = lhs * rhs.d;
            return result;
        }
        /// <summary>
        /// Mobius参数数乘，数分别乘上各个参数（int重载版本）
        /// </summary>
        /// <param name="lhs">左值</param>
        /// <param name="rhs">右值</param>
        /// <returns></returns>
        public static MobiusParam operator *(int lhs, MobiusParam rhs)
        {
            var result = new MobiusParam();
            result.a = lhs * rhs.a;
            result.b = lhs * rhs.b;
            result.c = lhs * rhs.c;
            result.d = lhs * rhs.d;
            return result;
        }
    }
}
