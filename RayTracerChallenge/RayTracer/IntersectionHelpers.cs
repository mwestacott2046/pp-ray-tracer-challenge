using System.Collections.Generic;
using System.Linq;

namespace RayTracer
{
    public static class IntersectionHelpers
    {

        public static Intersection Hit(this List<Intersection> intersections)
        {
            var cmp = CreateComparer();
            intersections.Sort(cmp);
            return intersections.FirstOrDefault(x => x.T > 0.0);
        }

        private static Comparer<Intersection> CreateComparer()
        {
            return Comparer<Intersection>.Create((intersectionA, intersectionB) =>
            {
                return intersectionA.T > intersectionB.T ? 1 : intersectionA.T < intersectionB.T ? -1:0;
            });
        }

        public static Intersection Hit(this Intersection[] intersections)
        {
            var cmp = CreateComparer();
            var data = intersections.ToList();
            
            data.Sort(cmp);

            return data.FirstOrDefault(x => x.T > 0.0);
        }
    }
}