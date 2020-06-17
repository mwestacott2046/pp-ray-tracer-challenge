using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class SchlickReflectance
    {
        public static double Schlick(Computation comps)
        {
            var cos = comps.EyeV.Dot(comps.NormalV);

            if (comps.N1 > comps.N2)
            {
                var n = comps.N1 / comps.N2;
                var sin2T = Math.Pow(n, 2) * (1.0 - Math.Pow(cos, 2));
                if (sin2T > 1.0)
                {
                    return 1.0;
                }

                var cosT = Math.Sqrt(1.0 - sin2T);

                cos = cosT;

            }
            var r0 = Math.Pow((comps.N1 - comps.N2) / (comps.N1 + comps.N2), 2);

            return r0 + (1-r0) * Math.Pow(1-cos,5);
        }
    }
}
