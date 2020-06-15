using System;

namespace RayTracer
{
    public class Material
    {
        public Material()
        {
            Colour = new Colour(1,1,1);
            Ambient = 0.1;
            Diffuse = 0.9;
            Specular = 0.9;
            Shininess = 200.0;
        }

        public Colour Colour { get; set; }
        public double Ambient { get; set; }
        public double Diffuse { get; set; }
        public double Specular { get; set; }
        public double Shininess { get; set; }
        public Pattern Pattern { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj != null)
            {
                if (obj is Material objMaterial)
                {
                    return this.Colour.Equals(objMaterial.Colour) &&
                           DoubleUtils.DoubleEquals(this.Ambient, objMaterial.Ambient) &&
                           DoubleUtils.DoubleEquals(this.Diffuse, objMaterial.Diffuse) &&
                           DoubleUtils.DoubleEquals(this.Specular, objMaterial.Specular) &&
                           DoubleUtils.DoubleEquals(this.Shininess, objMaterial.Shininess);

                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Ambient, this.Diffuse, this.Specular, this.Shininess) * this.Colour.GetHashCode();

        }
    }
}
