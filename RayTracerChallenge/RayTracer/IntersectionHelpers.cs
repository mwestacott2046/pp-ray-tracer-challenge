using System.Collections.Generic;
using System.Linq;

namespace RayTracer
{
    public static class IntersectionHelpers
    {
        public static Intersection Hit(this List<Intersection> intersections)
        {
            var cmp = Comparer<Intersection>.Create((intersectionA, intersectionB) =>
            {
                return intersectionA.T > intersectionB.T ? 1 : intersectionA.T < intersectionB.T ? -1:0;
            });
            intersections.Sort(cmp);
            return intersections.FirstOrDefault(x => x.T > 0.0);
        }
    }
}