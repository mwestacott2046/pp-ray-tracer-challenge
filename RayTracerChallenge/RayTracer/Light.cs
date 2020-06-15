using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Shapes;

namespace RayTracer
{
    public class Light
    {
        public Light(Point position, Colour intensity)
        {
            Position = position;
            Intensity = intensity;
        }

        public Colour Intensity { get; private set; }

        public Point Position { get; private set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position.GetHashCode(), Intensity.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is Light compareLight)
                {
                    return compareLight.Position.Equals(this.Position) && compareLight.Intensity.Equals(this.Intensity);
                }
            }

            return false;
        }

        public static Colour Lighting(Material material, ISceneObject sceneObject, 
            Light light, Point point, Vector eyeV, Vector normalV, bool inShadow)
        {
            Colour materialColour;
            if (material.Pattern != null)
            {
                materialColour = material.Pattern.PatternAtShape(sceneObject, point);
            }
            else
            {
                materialColour = material.Colour;
            }

            var effectiveColour = materialColour.Multiply(light.Intensity);

            var lightV = (light.Position - point).Normalize().ToVector();

            var ambient = effectiveColour.Multiply(material.Ambient);

            // light_dot_normal represents the cosine of the angle between the​
            // light vector and the normal vector. A negative number means the​
            // light is on the other side of the surface.​

            var lightDotNormal = lightV.Dot(normalV);
            Colour diffuse;
            Colour specular;
            if (lightDotNormal < 0)
            {
                diffuse = Colour.Black;
                specular = Colour.Black;
            }
            else
            {
                //diffuse ← effective_color * material.diffuse * light_dot_normal
                diffuse = effectiveColour.Multiply(material.Diffuse).Multiply(lightDotNormal);

                /*
                    # reflect_dot_eye represents the cosine of the angle between the​
​            	    ​# reflection vector and the eye vector. A negative number means the​
            ​ 	    ​# light reflects away from the eye.​
                 */
                var reflectV = (-lightV).ToVector().Reflect(normalV);
                var reflectDotEye = reflectV.Dot(eyeV);

                if (reflectDotEye <= 0)
                {
                    specular = Colour.Black;
                }
                else
                {
                    var factor = Math.Pow(reflectDotEye, material.Shininess);
                    specular = light.Intensity.Multiply(material.Specular).Multiply(factor);

                }

            }

            return inShadow ? ambient : ambient.Add(diffuse).Add(specular);
        }
    }
}
