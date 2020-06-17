using System;
using RayTracer.Patterns;

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
            Reflective = 0.0;
            Transparency = 0.0;
            RefractiveIndex = 1.0;
        }

        public Material(Colour colour, double ambient=0.1, double diffuse=0.9, 
            double specular = 0.9, double shininess = 200.0, double reflective = 0.0,
            double transparency = 0.0, double refractiveIndex = 1.0)
        {
            Colour = colour;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
            Reflective = reflective;
            Transparency = transparency;
            RefractiveIndex = refractiveIndex;
        }

        public Colour Colour { get; set; }
        public double Ambient { get; set; }
        public double Diffuse { get; set; }
        public double Specular { get; set; }
        public double Shininess { get; set; }
        public IPattern Pattern { get; set; }
        public double Reflective { get; set; }
        public double Transparency { get; set; }
        public double RefractiveIndex { get; set; }

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
                           DoubleUtils.DoubleEquals(this.Shininess, objMaterial.Shininess) &&
                           DoubleUtils.DoubleEquals(this.Reflective, objMaterial.Reflective) &&
                           DoubleUtils.DoubleEquals(this.Transparency, objMaterial.Transparency) &&
                           DoubleUtils.DoubleEquals(this.RefractiveIndex, objMaterial.RefractiveIndex);
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
