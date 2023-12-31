using System;
using System.Collections.Generic;
using UnityEngine;

namespace Generators.Noise
{
    public class StarcastInterpolator : Interpolator
    {
        private double checks;
        
        public StarcastInterpolator(NoisePlane input, double scale, double checks) : base(input, scale)
        {
            this.checks = checks;
        }
        
        public static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        public double getStarcast(float x, float z, float r, NoisePlane n)
        {
            double m = 360F / checks;
            double v = 0;

            for(int i = 0; i < 360; i += iround(m)) {
                float sin = (float) Math.Sin(ToRadians(i));
                float cos = (float) Math.Cos(ToRadians(i));
                float cx = x + ((r * cos) - (r * sin));
                float cz = z + ((r * sin) + (r * cos));
                v += n.noise(cx, cz);
            }

            return v / checks;
        }
        
        public override double noise(double x)
        {
            return noise(x,0);
        }

        public override double noise(double x, double y)
        {
            return getStarcast((float)x, (float)y, (float)scale, input);
        }

        public override double noise(double x, double y, double z)
        {
            return noise(x, y);
        }
    }
    
    public class CubicInterpolator : Interpolator
    {
        public CubicInterpolator(NoisePlane input, double scale) : base(input, scale)
        {
        }

        public override double noise(double x)
        {
            int[] box = getScaleBoundsC1D4(x);
            return cubic(
                input.noise(box[X1_D4]),
                input.noise(box[X2_D4]),
                input.noise(box[X3_D4]),
                input.noise(box[X4_D4]),
                normalize(box[X2_D4], box[X3_D4], x));
        }

        public override double noise(double x, double y)
        {
            int[] box = getScaleBoundsC2D4(x, y);
            return bicubic(
                input.noise(box[X1_D4], box[Y1_D4]),
                input.noise(box[X1_D4], box[Y2_D4]),
                input.noise(box[X1_D4], box[Y3_D4]),
                input.noise(box[X1_D4], box[Y4_D4]),
                input.noise(box[X2_D4], box[Y1_D4]),
                input.noise(box[X2_D4], box[Y2_D4]),
                input.noise(box[X2_D4], box[Y3_D4]),
                input.noise(box[X2_D4], box[Y4_D4]),
                input.noise(box[X3_D4], box[Y1_D4]),
                input.noise(box[X3_D4], box[Y2_D4]),
                input.noise(box[X3_D4], box[Y3_D4]),
                input.noise(box[X3_D4], box[Y4_D4]),
                input.noise(box[X4_D4], box[Y1_D4]),
                input.noise(box[X4_D4], box[Y2_D4]),
                input.noise(box[X4_D4], box[Y3_D4]),
                input.noise(box[X4_D4], box[Y4_D4]),
                normalize(box[X2_D4], box[X3_D4], x),
                normalize(box[Y2_D4], box[Y3_D4], y));
        }

        public override double noise(double x, double y, double z)
        {
            int[] box = getScaleBoundsC3D4(x, y, z);
             return tricubic(
                input.noise(box[X1_D4], box[Y1_D4], box[Z1_D4]),
                input.noise(box[X1_D4], box[Y2_D4], box[Z1_D4]),
                input.noise(box[X1_D4], box[Y3_D4], box[Z1_D4]),
                input.noise(box[X1_D4], box[Y4_D4], box[Z1_D4]),
                input.noise(box[X2_D4], box[Y1_D4], box[Z1_D4]),
                input.noise(box[X2_D4], box[Y2_D4], box[Z1_D4]),
                input.noise(box[X2_D4], box[Y3_D4], box[Z1_D4]),
                input.noise(box[X2_D4], box[Y4_D4], box[Z1_D4]),
                input.noise(box[X3_D4], box[Y1_D4], box[Z1_D4]),
                input.noise(box[X3_D4], box[Y2_D4], box[Z1_D4]),
                input.noise(box[X3_D4], box[Y3_D4], box[Z1_D4]),
                input.noise(box[X3_D4], box[Y4_D4], box[Z1_D4]),
                input.noise(box[X4_D4], box[Y1_D4], box[Z1_D4]),
                input.noise(box[X4_D4], box[Y2_D4], box[Z1_D4]),
                input.noise(box[X4_D4], box[Y3_D4], box[Z1_D4]),
                input.noise(box[X4_D4], box[Y4_D4], box[Z1_D4]),
                input.noise(box[X1_D4], box[Y1_D4], box[Z2_D4]),
                input.noise(box[X1_D4], box[Y2_D4], box[Z2_D4]),
                input.noise(box[X1_D4], box[Y3_D4], box[Z2_D4]),
                input.noise(box[X1_D4], box[Y4_D4], box[Z2_D4]),
                input.noise(box[X2_D4], box[Y1_D4], box[Z2_D4]),
                input.noise(box[X2_D4], box[Y2_D4], box[Z2_D4]),
                input.noise(box[X2_D4], box[Y3_D4], box[Z2_D4]),
                input.noise(box[X2_D4], box[Y4_D4], box[Z2_D4]),
                input.noise(box[X3_D4], box[Y1_D4], box[Z2_D4]),
                input.noise(box[X3_D4], box[Y2_D4], box[Z2_D4]),
                input.noise(box[X3_D4], box[Y3_D4], box[Z2_D4]),
                input.noise(box[X3_D4], box[Y4_D4], box[Z2_D4]),
                input.noise(box[X4_D4], box[Y1_D4], box[Z2_D4]),
                input.noise(box[X4_D4], box[Y2_D4], box[Z2_D4]),
                input.noise(box[X4_D4], box[Y3_D4], box[Z2_D4]),
                input.noise(box[X4_D4], box[Y4_D4], box[Z2_D4]),
                input.noise(box[X1_D4], box[Y1_D4], box[Z3_D4]),
                input.noise(box[X1_D4], box[Y2_D4], box[Z3_D4]),
                input.noise(box[X1_D4], box[Y3_D4], box[Z3_D4]),
                input.noise(box[X1_D4], box[Y4_D4], box[Z3_D4]),
                input.noise(box[X2_D4], box[Y1_D4], box[Z3_D4]),
                input.noise(box[X2_D4], box[Y2_D4], box[Z3_D4]),
                input.noise(box[X2_D4], box[Y3_D4], box[Z3_D4]),
                input.noise(box[X2_D4], box[Y4_D4], box[Z3_D4]),
                input.noise(box[X3_D4], box[Y1_D4], box[Z3_D4]),
                input.noise(box[X3_D4], box[Y2_D4], box[Z3_D4]),
                input.noise(box[X3_D4], box[Y3_D4], box[Z3_D4]),
                input.noise(box[X3_D4], box[Y4_D4], box[Z3_D4]),
                input.noise(box[X4_D4], box[Y1_D4], box[Z3_D4]),
                input.noise(box[X4_D4], box[Y2_D4], box[Z3_D4]),
                input.noise(box[X4_D4], box[Y3_D4], box[Z3_D4]),
                input.noise(box[X4_D4], box[Y4_D4], box[Z3_D4]),
                input.noise(box[X1_D4], box[Y1_D4], box[Z4_D4]),
                input.noise(box[X1_D4], box[Y2_D4], box[Z4_D4]),
                input.noise(box[X1_D4], box[Y3_D4], box[Z4_D4]),
                input.noise(box[X1_D4], box[Y4_D4], box[Z4_D4]),
                input.noise(box[X2_D4], box[Y1_D4], box[Z4_D4]),
                input.noise(box[X2_D4], box[Y2_D4], box[Z4_D4]),
                input.noise(box[X2_D4], box[Y3_D4], box[Z4_D4]),
                input.noise(box[X2_D4], box[Y4_D4], box[Z4_D4]),
                input.noise(box[X3_D4], box[Y1_D4], box[Z4_D4]),
                input.noise(box[X3_D4], box[Y2_D4], box[Z4_D4]),
                input.noise(box[X3_D4], box[Y3_D4], box[Z4_D4]),
                input.noise(box[X3_D4], box[Y4_D4], box[Z4_D4]),
                input.noise(box[X4_D4], box[Y1_D4], box[Z4_D4]),
                input.noise(box[X4_D4], box[Y2_D4], box[Z4_D4]),
                input.noise(box[X4_D4], box[Y3_D4], box[Z4_D4]),
                input.noise(box[X4_D4], box[Y4_D4], box[Z4_D4]),
                normalize(box[X2_D4], box[X3_D4], x),
                normalize(box[Y2_D4], box[Y3_D4], y),
                normalize(box[Z2_D4], box[Z3_D4], z));
        }
    }
    
    public class HermiteInterpolator : Interpolator
    {
        private double tension;
        private double bias;
        
        public HermiteInterpolator(NoisePlane input, double scale) : base(input, scale)
        {
            this.tension = 0;
            this.bias = 0;
        }

        public override double noise(double x)
        {
            int[] box = getScaleBoundsC1D4(x);
            return hermite(
                input.noise(box[X1_D4]),
                input.noise(box[X2_D4]),
                input.noise(box[X3_D4]),
                input.noise(box[X4_D4]),
                normalize(box[X2_D4], box[X3_D4], x), tension, bias);
        }

        public override double noise(double x, double y)
        {
            int[] box = getScaleBoundsC2D4(x, y);
            return bihermite(
                input.noise(box[X1_D4], box[Y1_D4]),
                input.noise(box[X1_D4], box[Y2_D4]),
                input.noise(box[X1_D4], box[Y3_D4]),
                input.noise(box[X1_D4], box[Y4_D4]),
                input.noise(box[X2_D4], box[Y1_D4]),
                input.noise(box[X2_D4], box[Y2_D4]),
                input.noise(box[X2_D4], box[Y3_D4]),
                input.noise(box[X2_D4], box[Y4_D4]),
                input.noise(box[X3_D4], box[Y1_D4]),
                input.noise(box[X3_D4], box[Y2_D4]),
                input.noise(box[X3_D4], box[Y3_D4]),
                input.noise(box[X3_D4], box[Y4_D4]),
                input.noise(box[X4_D4], box[Y1_D4]),
                input.noise(box[X4_D4], box[Y2_D4]),
                input.noise(box[X4_D4], box[Y3_D4]),
                input.noise(box[X4_D4], box[Y4_D4]),
                normalize(box[X2_D4], box[X3_D4], x),
                normalize(box[Y2_D4], box[Y3_D4], y), tension, bias);
        }

        public override double noise(double x, double y, double z)
        {
            int[] box = getScaleBoundsC3D4(x, y, z);
             return trihermite(
                input.noise(box[X1_D4], box[Y1_D4], box[Z1_D4]),
                input.noise(box[X1_D4], box[Y2_D4], box[Z1_D4]),
                input.noise(box[X1_D4], box[Y3_D4], box[Z1_D4]),
                input.noise(box[X1_D4], box[Y4_D4], box[Z1_D4]),
                input.noise(box[X2_D4], box[Y1_D4], box[Z1_D4]),
                input.noise(box[X2_D4], box[Y2_D4], box[Z1_D4]),
                input.noise(box[X2_D4], box[Y3_D4], box[Z1_D4]),
                input.noise(box[X2_D4], box[Y4_D4], box[Z1_D4]),
                input.noise(box[X3_D4], box[Y1_D4], box[Z1_D4]),
                input.noise(box[X3_D4], box[Y2_D4], box[Z1_D4]),
                input.noise(box[X3_D4], box[Y3_D4], box[Z1_D4]),
                input.noise(box[X3_D4], box[Y4_D4], box[Z1_D4]),
                input.noise(box[X4_D4], box[Y1_D4], box[Z1_D4]),
                input.noise(box[X4_D4], box[Y2_D4], box[Z1_D4]),
                input.noise(box[X4_D4], box[Y3_D4], box[Z1_D4]),
                input.noise(box[X4_D4], box[Y4_D4], box[Z1_D4]),
                input.noise(box[X1_D4], box[Y1_D4], box[Z2_D4]),
                input.noise(box[X1_D4], box[Y2_D4], box[Z2_D4]),
                input.noise(box[X1_D4], box[Y3_D4], box[Z2_D4]),
                input.noise(box[X1_D4], box[Y4_D4], box[Z2_D4]),
                input.noise(box[X2_D4], box[Y1_D4], box[Z2_D4]),
                input.noise(box[X2_D4], box[Y2_D4], box[Z2_D4]),
                input.noise(box[X2_D4], box[Y3_D4], box[Z2_D4]),
                input.noise(box[X2_D4], box[Y4_D4], box[Z2_D4]),
                input.noise(box[X3_D4], box[Y1_D4], box[Z2_D4]),
                input.noise(box[X3_D4], box[Y2_D4], box[Z2_D4]),
                input.noise(box[X3_D4], box[Y3_D4], box[Z2_D4]),
                input.noise(box[X3_D4], box[Y4_D4], box[Z2_D4]),
                input.noise(box[X4_D4], box[Y1_D4], box[Z2_D4]),
                input.noise(box[X4_D4], box[Y2_D4], box[Z2_D4]),
                input.noise(box[X4_D4], box[Y3_D4], box[Z2_D4]),
                input.noise(box[X4_D4], box[Y4_D4], box[Z2_D4]),
                input.noise(box[X1_D4], box[Y1_D4], box[Z3_D4]),
                input.noise(box[X1_D4], box[Y2_D4], box[Z3_D4]),
                input.noise(box[X1_D4], box[Y3_D4], box[Z3_D4]),
                input.noise(box[X1_D4], box[Y4_D4], box[Z3_D4]),
                input.noise(box[X2_D4], box[Y1_D4], box[Z3_D4]),
                input.noise(box[X2_D4], box[Y2_D4], box[Z3_D4]),
                input.noise(box[X2_D4], box[Y3_D4], box[Z3_D4]),
                input.noise(box[X2_D4], box[Y4_D4], box[Z3_D4]),
                input.noise(box[X3_D4], box[Y1_D4], box[Z3_D4]),
                input.noise(box[X3_D4], box[Y2_D4], box[Z3_D4]),
                input.noise(box[X3_D4], box[Y3_D4], box[Z3_D4]),
                input.noise(box[X3_D4], box[Y4_D4], box[Z3_D4]),
                input.noise(box[X4_D4], box[Y1_D4], box[Z3_D4]),
                input.noise(box[X4_D4], box[Y2_D4], box[Z3_D4]),
                input.noise(box[X4_D4], box[Y3_D4], box[Z3_D4]),
                input.noise(box[X4_D4], box[Y4_D4], box[Z3_D4]),
                input.noise(box[X1_D4], box[Y1_D4], box[Z4_D4]),
                input.noise(box[X1_D4], box[Y2_D4], box[Z4_D4]),
                input.noise(box[X1_D4], box[Y3_D4], box[Z4_D4]),
                input.noise(box[X1_D4], box[Y4_D4], box[Z4_D4]),
                input.noise(box[X2_D4], box[Y1_D4], box[Z4_D4]),
                input.noise(box[X2_D4], box[Y2_D4], box[Z4_D4]),
                input.noise(box[X2_D4], box[Y3_D4], box[Z4_D4]),
                input.noise(box[X2_D4], box[Y4_D4], box[Z4_D4]),
                input.noise(box[X3_D4], box[Y1_D4], box[Z4_D4]),
                input.noise(box[X3_D4], box[Y2_D4], box[Z4_D4]),
                input.noise(box[X3_D4], box[Y3_D4], box[Z4_D4]),
                input.noise(box[X3_D4], box[Y4_D4], box[Z4_D4]),
                input.noise(box[X4_D4], box[Y1_D4], box[Z4_D4]),
                input.noise(box[X4_D4], box[Y2_D4], box[Z4_D4]),
                input.noise(box[X4_D4], box[Y3_D4], box[Z4_D4]),
                input.noise(box[X4_D4], box[Y4_D4], box[Z4_D4]),
                normalize(box[X2_D4], box[X3_D4], x),
                normalize(box[Y2_D4], box[Y3_D4], y),
                normalize(box[Z2_D4], box[Z3_D4], z), tension, bias);
        }
    }
    
    public class LinearInterpolator : Interpolator
    {
        public LinearInterpolator(NoisePlane input, double scale) : base(input, scale)
        {
        }

        public override double noise(double x)
        {
            int[] box = getScaleBoundsC1D2(x);
            return lerp(
                input.noise(box[X1_D2]),
                input.noise(box[X2_D2]),
                normalize(box[X1_D2], box[X2_D2], x));
        }

        public override double noise(double x, double y)
        {
            int[] box = getScaleBoundsC2D2(x, y);
            return blerp(
                input.noise(box[X1_D2], box[Y1_D2]),
                input.noise(box[X2_D2], box[Y1_D2]),
                input.noise(box[X1_D2], box[Y2_D2]),
                input.noise(box[X2_D2], box[Y2_D2]),
                normalize(box[X1_D2], box[X2_D2], x),
                normalize(box[Y1_D2], box[Y2_D2], y));
        }

        public override double noise(double x, double y, double z)
        {
            int[] box = getScaleBoundsC3D2(x, y, z);
            return trilerp(
                input.noise(box[X1_D2], box[Y1_D2], box[Z1_D2]),
                input.noise(box[X2_D2], box[Y1_D2], box[Z1_D2]),
                input.noise(box[X1_D2], box[Y2_D2], box[Z1_D2]),
                input.noise(box[X2_D2], box[Y2_D2], box[Z1_D2]),
                input.noise(box[X1_D2], box[Y1_D2], box[Z2_D2]),
                input.noise(box[X2_D2], box[Y1_D2], box[Z2_D2]),
                input.noise(box[X1_D2], box[Y2_D2], box[Z2_D2]),
                input.noise(box[X2_D2], box[Y2_D2], box[Z2_D2]),
                normalize(box[X1_D2], box[X2_D2], x),
                normalize( box[Y1_D2], box[Y2_D2], y),
                normalize(box[Z1_D2], box[Z2_D2], z));
        }
    }
    
    public abstract class Interpolator : NoiseProvider
    {
        protected NoisePlane input;
        protected double scale;
        protected double iscale;
        
        public Interpolator(NoisePlane input, double scale)
        {
            this.input = input;
            this.scale = scale;
            this.iscale = 1.0 / scale;
        }
        
        public override long getSeed()
        {
            if(input is NoiseProvider provider)
            {
                return provider.getSeed();
            }

            return -1;
        }
        
        public static int X1_D2 = 0;
        public static  int X2_D2 = 1;
        public static int Y1_D2 = 2;
        public static int Y2_D2 = 3;
        public static  int Z1_D2 = 4;
        public static int Z2_D2 = 5;
        public static int X1_D4 = 0;
        public static int X2_D4 = 1;
        public static int X3_D4 = 2;
        public static int X4_D4 = 3;
        public static  int Y1_D4 = 4;
        public static int Y2_D4 = 5;
        public static int Y3_D4 = 6;
        public static  int Y4_D4 = 7;
        public static int Z1_D4 = 8;
        public static int Z2_D4 = 9;
        public static int Z3_D4 = 10;
        public static  int Z4_D4 = 11;
        
        /**
     * Gets the x1,x2,y1,y2,z1,z2
     * @param x cx coord
     * @param y cy coord
     * @param z cz coord
     * @return [x1,x2,y1,y2,z1,z2]
     */
        public int[] getScaleBoundsC3D2(double x, double y, double z)
        {
            int fx = getRadiusFactor(x);
            int fy = getRadiusFactor(y);
            int fz = getRadiusFactor(z);
            return new int[]{(int) round(fx * scale),
                (int) round((fx + 1) * scale),
                (int) round(fy * scale),
                (int) round((fy + 1) * scale),
                (int) round(fz * scale),
                (int) round((fz + 1) * scale)};
        }
        
        /**
     * Gets the x1,x2,y1,y2
     * @param x cx coord
     * @param y cy coord
     * @return [x1,x2,y1,y2]
     */
    public int[] getScaleBoundsC2D2(double x, double y)
    {
        int fx = getRadiusFactor(x);
        int fy = getRadiusFactor(y);
        return new int[]{(int) round(fx * scale),
                (int) round((fx + 1) * scale),
                (int) round(fy * scale),
                (int) round((fy + 1) * scale)};
    }

    /**
     * Gets the x1,x2
     * @param x cx coord
     * @return [x1,x2]
     */
    public int[] getScaleBoundsC1D2(double x)
    {
        int fx = getRadiusFactor(x);
        return new int[]{(int) round(fx * scale),
                (int) round((fx + 1) * scale)};
    }

    /**
     * Gets the x1,x2,x3,x4,y1,y2,y3,y4,z1,z2,z3,z4
     * @param x cx coord
     * @return [x1,x2,x3,x4,y1,y2,y3,y4,z1,z2,z3,z4]
     */
    public int[] getScaleBoundsC3D4(double x, double y, double z)
    {
        int fx = getRadiusFactor(x);
        int fy = getRadiusFactor(y);
        int fz = getRadiusFactor(z);
        return new int[]{
                (int) round((fx - 1) * scale),
                (int) round(fx * scale),
                (int) round((fx + 1) * scale),
                (int) round((fx + 2) * scale),
                (int) round((fy - 1) * scale),
                (int) round(fy * scale),
                (int) round((fy + 1) * scale),
                (int) round((fy + 2) * scale),
                (int) round((fz - 1) * scale),
                (int) round(fz * scale),
                (int) round((fz + 1) * scale),
                (int) round((fz + 2) * scale)};
    }

    /**
     * Gets the x1,x2,x3,x4,y1,y2,y3,y4
     * @param x cx coord
     * @return [x1,x2,x3,x4,y1,y2,y3,y4]
     */
    public int[] getScaleBoundsC2D4(double x, double y)
    {
        int fx = getRadiusFactor(x);
        int fy = getRadiusFactor(y);
        return new int[]{
                (int) round((fx - 1) * scale),
                (int) round(fx * scale),
                (int) round((fx + 1) * scale),
                (int) round((fx + 2) * scale),
                (int) round((fy - 1) * scale),
                (int) round(fy * scale),
                (int) round((fy + 1) * scale),
                (int) round((fy + 2) * scale)};
    }

    /**
     * Gets the x1,x2,x3,x4
     * @param x cx coord
     * @return [x1,x2,x3,x4]
     */
    public int[] getScaleBoundsC1D4(double x)
    {
        int fx = getRadiusFactor(x);

        return new int[]{
                (int) round((fx - 1) * scale),
                (int) round(fx * scale),
                (int) round((fx + 1) * scale),
                (int) round((fx + 2) * scale)};
    }

    public static double rangeScale(double amin, double amax, double bmin, double bmax, double b) {
        return amin + ((amax - amin) * ((b - bmin) / (bmax - bmin)));
    }

    public double normalize(double bmin, double bmax, double b) {
        return (b - bmin) / (bmax - bmin);
    }

    public int getRadiusFactor(double coord)
    {
        if(scale > 1 && scale < 3) {return (int)coord >> 1;}
        if(scale > 3 && scale < 5) {return (int)coord >> 2;}
        if(scale > 7 && scale < 9) {return (int)coord >> 3;}
        if(scale > 15 && scale < 17) {return (int)coord >> 4;}
        if(scale > 31 && scale < 33) {return (int)coord >> 5;}
        if(scale > 63 && scale < 65) {return (int)coord >> 6;}
        if(scale > 127 && scale < 129) {return (int)coord >> 7;}
        if(scale > 255 && scale < 257) {return (int)coord >> 8;}
        if(scale > 511 && scale < 513) {return (int)coord >> 9;}
        if(scale > 1023 && scale < 1025) {return (int)coord >> 10;}
        return (int) floor(coord * iscale);
    }

    public static double bigauss(double x, double y, double sigma) {
        return  1.0f / (2.0f * Math.PI * sigma * sigma) * Math.Exp(-(x * x + y * y) / (2.0f * sigma * sigma));
    }

    public static double lerp(double a, double b, double f) {
        return a + (f * (b - a));
    }

    public static double blerp(double a, double b, double c, double d, double tx, double ty) {
        return lerp(lerp(a, b, tx), lerp(c, d, tx), ty);
    }

    public static double trilerp(double v1, double v2, double v3, double v4, double v5, double v6, double v7, double v8, double x, double y, double z) {
        return lerp(blerp(v1, v2, v3, v4, x, y), blerp(v5, v6, v7, v8, x, y), z);
    }

    public static double hermite(double p0, double p1, double p2, double p3, double mu, double tension, double bias) {
        double m0, m1, mu2, mu3;
        double a0, a1, a2, a3;

        mu2 = mu * mu;
        mu3 = mu2 * mu;
        m0 = (p1 - p0) * (1 + bias) * (1 - tension) / 2;
        m0 += (p2 - p1) * (1 - bias) * (1 - tension) / 2;
        m1 = (p2 - p1) * (1 + bias) * (1 - tension) / 2;
        m1 += (p3 - p2) * (1 - bias) * (1 - tension) / 2;
        a0 = 2 * mu3 - 3 * mu2 + 1;
        a1 = mu3 - 2 * mu2 + mu;
        a2 = mu3 - mu2;
        a3 = -2 * mu3 + 3 * mu2;

        return (a0 * p1 + a1 * m0 + a2 * m1 + a3 * p2);
    }

    public static double cubic(double p0, double p1, double p2, double p3, double mu) {
        double a0, a1, a2, a3, mu2;

        mu2 = mu * mu;
        a0 = p3 - p2 - p0 + p1;
        a1 = p0 - p1 - a0;
        a2 = p2 - p0;
        a3 = p1;

        return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }

    public static double bicubic(double p00, double p01, double p02, double p03, double p10, double p11, double p12, double p13, double p20, double p21, double p22, double p23, double p30, double p31, double p32, double p33, double mux, double muy) {
        //@builder
        return cubic(
                cubic(p00, p01, p02, p03, muy),
                cubic(p10, p11, p12, p13, muy),
                cubic(p20, p21, p22, p23, muy),
                cubic(p30, p31, p32, p33, muy),
                mux);
        //@done
    }

    public static double bihermite(double p00, double p01, double p02, double p03, double p10, double p11, double p12, double p13, double p20, double p21, double p22, double p23, double p30, double p31, double p32, double p33, double mux, double muy, double tension, double bias) {
        //@builder
        return hermite(
                hermite(p00, p01, p02, p03, muy, tension, bias),
                hermite(p10, p11, p12, p13, muy, tension, bias),
                hermite(p20, p21, p22, p23, muy, tension, bias),
                hermite(p30, p31, p32, p33, muy, tension, bias),
                mux, tension, bias);
        //@done
    }

    public static double trihermite(double p000, double p001, double p002, double p003, double p010, double p011, double p012, double p013, double p020, double p021, double p022, double p023, double p030, double p031, double p032, double p033, double p100, double p101, double p102, double p103, double p110, double p111, double p112, double p113, double p120, double p121, double p122, double p123, double p130, double p131, double p132, double p133, double p200, double p201, double p202, double p203, double p210, double p211, double p212, double p213, double p220, double p221, double p222, double p223, double p230, double p231, double p232, double p233, double p300, double p301, double p302, double p303, double p310, double p311, double p312, double p313, double p320, double p321, double p322, double p323, double p330, double p331, double p332, double p333, double mux, double muy, double muz, double tension, double bias) {
        //@builder
        return hermite(
                bihermite(p000, p001, p002, p003,
                        p010, p011, p012, p013,
                        p020, p021, p022, p023,
                        p030, p031, p032, p033,
                        mux, muy, tension, bias),
                bihermite(p100, p101, p102, p103,
                        p110, p111, p112, p113,
                        p120, p121, p122, p123,
                        p130, p131, p132, p133,
                        mux, muy, tension, bias),
                bihermite(p200, p201, p202, p203,
                        p210, p211, p212, p213,
                        p220, p221, p222, p223,
                        p230, p231, p232, p233,
                        mux, muy, tension, bias),
                bihermite(p300, p301, p302, p303,
                        p310, p311, p312, p313,
                        p320, p321, p322, p323,
                        p330, p331, p332, p333,
                        mux, muy, tension, bias),
                muz, tension, bias);
        //@done
    }

    public static double tricubic(double p000,
                                  double p001,
                                  double p002,
                                  double p003,
                                  double p010,
                                  double p011,
                                  double p012, double p013, double p020, double p021, double p022, double p023, double p030, double p031, double p032, double p033, double p100, double p101, double p102, double p103, double p110, double p111, double p112, double p113, double p120, double p121, double p122, double p123, double p130, double p131, double p132, double p133, double p200, double p201, double p202, double p203, double p210, double p211, double p212, double p213, double p220, double p221, double p222, double p223, double p230, double p231, double p232, double p233, double p300, double p301, double p302, double p303, double p310, double p311, double p312, double p313, double p320, double p321, double p322, double p323, double p330, double p331, double p332, double p333, double mux, double muy, double muz) {
        //@builder
        return cubic(
                bicubic(p000, p001, p002, p003,
                        p010, p011, p012, p013,
                        p020, p021, p022, p023,
                        p030, p031, p032, p033,
                        mux, muy),
                bicubic(p100, p101, p102, p103,
                        p110, p111, p112, p113,
                        p120, p121, p122, p123,
                        p130, p131, p132, p133,
                        mux, muy),
                bicubic(p200, p201, p202, p203,
                        p210, p211, p212, p213,
                        p220, p221, p222, p223,
                        p230, p231, p232, p233,
                        mux, muy),
                bicubic(p300, p301, p302, p303,
                        p310, p311, p312, p313,
                        p320, p321, p322, p323,
                        p330, p331, p332, p333,
                        mux, muy),
                muz);
        //@done
    }
    }

    public class ScaledProvider : NoisePlane
    {
        private NoisePlane generator;
        private double scale;
        
        public ScaledProvider(NoisePlane generator, double scale)
        {
            this.generator = generator;
            this.scale = scale;
        }

        public override double noise(double x) {
            return generator.noise(x * scale);
        }

        public override  double noise(double x, double y) {
            return generator.noise(x * scale, y * scale);
        }

        public override double noise(double x, double y, double z) {
            return generator.noise(x * scale, y * scale, z * scale);
        }

        public override double getMaxOutput() {
            return generator.getMaxOutput();
        }

        public override double getMinOutput() {
            return generator.getMinOutput();
        }
    }

    public class InvertedProvider : NoisePlane
    {
        private NoisePlane generator;
        
        public InvertedProvider(NoisePlane generator)
        {
            this.generator = generator;
        }
        
        private double invert(double value)
        {
            return (generator.getMaxOutput() - value) + generator.getMinOutput();
        }
        
        public override double noise(double x)
        {
            return invert(generator.noise(x));
        }

        public override double noise(double x, double y)
        {
            return invert(generator.noise(x,y ));
        }

        public override double noise(double x, double y, double z)
        {
            return invert(generator.noise(x ,y,z));
        }
        
        public override double getMaxOutput() {
            return generator.getMaxOutput();
        }

        public override double getMinOutput() {
            return generator.getMinOutput();
        }
    }

    public class AddingProvider : NoisePlane
    {
        private NoisePlane generator;
        private NoisePlane other;
        
        public AddingProvider(NoisePlane generator, NoisePlane other)
        {
            this.generator = generator;
            this.other = other;
        }
        
        public override double noise(double x)
        {
            return generator.noise(x) + other.noise(x);
        }

        public override double noise(double x, double y)
        {
            return generator.noise(x, y) + other.noise(x, y);
        }

        public override double noise(double x, double y, double z)
        {
            return generator.noise(x, y, z) + other.noise(x, y, z);
        }
    }
    
    public class ClippingProvider : NoisePlane
    {
        private NoisePlane generator;
        private double min;
        private double max;
        
        public ClippingProvider(NoisePlane generator, double min, double max)
        {
            this.generator = generator;
            this.min = min;
            this.max = max;
        }
        
        private double applyClip(double value) {
            return Math.Min(max, Math.Max(value, min));
        }
        
        public override double noise(double x)
        {
            return  applyClip(generator.noise(x));
        }

        public override double noise(double x, double y)
        {
            return  applyClip(generator.noise(x, y));
        }

        public override double noise(double x, double y, double z)
        {
            return  applyClip(generator.noise(x, y, z));
        }
        
        public override double getMaxOutput() {
            return applyClip(generator.getMaxOutput());
        }

        public override double getMinOutput() {
            return applyClip(generator.getMinOutput());
        }
    }
    
    public class ContrastingProvider : NoisePlane
    {
        private NoisePlane generator;
        private  double amount;
        private  double range;
        private  double midpoint;
        private  double divRange;
        
        public ContrastingProvider(NoisePlane generator, double amount)
        {
            this.generator = generator;
            this.amount = amount;
            this.range = (getMaxOutput() - getMinOutput());
            this.midpoint = (getMaxOutput() + getMinOutput()) / 2D;
            this.divRange = 1D / range;
        }
        
        private double applyContrast(double value) {
            double min = getMinOutput();
            double max = getMaxOutput();
            double valuePercent = (value - min) * divRange;
            double valueMidpoint = (valuePercent - 0.5D) * amount;

            return Math.Min(max, Math.Max(valueMidpoint + 0.5D, min));
        }
        
        public override double noise(double x)
        {
            return  applyContrast(generator.noise(x));
        }

        public override double noise(double x, double y)
        {
            return  applyContrast(generator.noise(x, y));
        }

        public override double noise(double x, double y, double z)
        {
            return  applyContrast(generator.noise(x, y, z));
        }
        
        public override double getMaxOutput() {
            return applyContrast(generator.getMaxOutput());
        }

        public override double getMinOutput() {
            return applyContrast(generator.getMinOutput());
        }
    }
    
    public class EdgeProvider : NoisePlane
    {
        private NoisePlane generator;
        private double threshold;
        private double diff;
        private bool fast;

        public EdgeProvider(NoisePlane generator, double threshold, bool fast)
        {
            this.generator = generator;
            this.fast = fast;
            this.threshold = threshold;
            this.diff = 1D / (getMaxOutput() - getMinOutput());
        }

        public override double noise(double x)
        {
            double a = generator.noise(x);
            double b = generator.noise(x+1);
            double c = generator.noise(x-1);
            if(Math.Abs(a - b) * diff > threshold || Math.Abs(a - c) * diff > threshold) {
                return getMaxOutput();
            }

            return getMinOutput();
        }

        public override double noise(double x, double y)
        {
            double c = generator.noise(x, y);

            if(Math.Abs(c - generator.noise(x+1, y)) * diff > threshold) {
                return getMaxOutput();
            }
            if(Math.Abs(c - generator.noise(x, y+1)) * diff > threshold) {
                return getMaxOutput();
            }
            if(!fast) {
                if(Math.Abs(c - generator.noise(x-1, y)) * diff > threshold) {
                    return getMaxOutput();
                }
                if(Math.Abs(c - generator.noise(x, y-1)) * diff > threshold) {
                    return getMaxOutput();
                }
                if(Math.Abs(c - generator.noise(x+1, y+1)) * diff > threshold) {
                    return getMaxOutput();
                }
                if(Math.Abs(c - generator.noise(x-1, y+1)) * diff > threshold) {
                    return getMaxOutput();
                }
                if(Math.Abs(c - generator.noise(x+1, y-1)) * diff > threshold) {
                    return getMaxOutput();
                }
                if(Math.Abs(c - generator.noise(x-1, y-1)) * diff > threshold) {
                    return getMaxOutput();
                }
            }


            return getMinOutput();
        }

        public override double noise(double x, double y, double z)
        {
            double c = generator.noise(x, y);

            if(Math.Abs(c - generator.noise(x+1, y)) * diff > threshold) {
                return getMaxOutput();
            }
            if(Math.Abs(c - generator.noise(x, y+1)) * diff > threshold) {
                return getMaxOutput();
            }
            if(!fast) {
                if(Math.Abs(c - generator.noise(x-1, y)) * diff > threshold) {
                    return getMaxOutput();
                }
                if(Math.Abs(c - generator.noise(x, y-1)) * diff > threshold) {
                    return getMaxOutput();
                }
                if(Math.Abs(c - generator.noise(x+1, y+1)) * diff > threshold) {
                    return getMaxOutput();
                }
                if(Math.Abs(c - generator.noise(x-1, y+1)) * diff > threshold) {
                    return getMaxOutput();
                }
                if(Math.Abs(c - generator.noise(x+1, y-1)) * diff > threshold) {
                    return getMaxOutput();
                }
                if(Math.Abs(c - generator.noise(x-1, y-1)) * diff > threshold) {
                    return getMaxOutput();
                }
            }


            return getMinOutput();
        }
        
        public override bool supports3D() {
            return false;
        }
        
        public override double getMaxOutput() {
            return generator.getMaxOutput();
        }

        public override double getMinOutput() {
            return generator.getMinOutput();
        }
    }
    
    public class PosturizedProvider : NoisePlane
    {
        private NoisePlane generator;
        private  int values;
        private  double range;
        private  double chunk;
        private  double divChunk;

        public PosturizedProvider(NoisePlane generator, int values)
        {
            this.generator = generator;
            this.values = values;
            this.range = (getMaxOutput() - getMinOutput());
            this.chunk = range / values;
            this.divChunk = 1D / chunk;
        }
        
        private double applyPosturize(double v) {
            double a = Interpolator.rangeScale(0, values, generator.getMinOutput(), generator.getMaxOutput(), v);
            return Math.Round(a);
        }

        public override double noise(double x)
        {
            return applyPosturize(generator.noise(x));
        }

        public override double noise(double x, double y)
        {
            return applyPosturize(generator.noise(x,y ));
        }

        public override double noise(double x, double y, double z)
        {
            return applyPosturize(generator.noise(x ,y,z));
        }
        
        public override double getMaxOutput() {
            return values;
        }

        public override double getMinOutput() {
            return 0;
        }
    }

    public class WarpedProvider : NoisePlane
    {
        private NoisePlane generator;
        private NoisePlane warp;
        private double scale;
        private double multiplier;
        
        public WarpedProvider(NoisePlane generator, NoisePlane warp, double scale, double multiplier)
        {
            this.generator = generator;
            this.warp = warp;
            this.scale = scale;
            this.multiplier = multiplier;
        } 
        
        public override double noise(double x)
        {
            return generator.noise((warp.noise(x * scale) * multiplier) + x);
        }

        public override double noise(double x, double y)
        {
            return generator.noise(
                (warp.noise(x * scale, y * scale) * multiplier) + x,
                (warp.noise(y * scale, -x * scale) * multiplier) + y);        }

        public override double noise(double x, double y, double z)
        {
            return generator.noise(
                (warp.noise(-x * scale, y * scale, z * scale) * multiplier) + x,
                (warp.noise(y * scale, -z * scale, x * scale) * multiplier) + y,
                (warp.noise(z * scale, x * scale, -y * scale) * multiplier) + z);        }
        
        public override double getMaxOutput() {
            return generator.getMaxOutput();
        }

        public override double getMinOutput() {
            return generator.getMinOutput();
        }
    }

    public class FittedProvider : NoisePlane
    {
        private NoisePlane generator;
        private double min;
        private double max;
        
        public FittedProvider(NoisePlane generator, double min, double max)
        {
            this.generator = generator;
            this.min = min;
            this.max = max;
        }

        public override double noise(double x)
        {
            if(generator.getMinOutput() == min && generator.getMaxOutput() == max) {
                return generator.noise(x);
            }
            if(generator.getMinOutput() == -1 && generator.getMaxOutput() == 1 && min == 0) {
                return ((generator.noise(x)+1) * 0.5)*max;
            }

            return Interpolator.rangeScale(min, max, generator.getMinOutput(), generator.getMaxOutput(), generator.noise(x));
        }

        public override double noise(double x, double y)
        {
            if(generator.getMinOutput() == min && generator.getMaxOutput() == max) {
                return generator.noise(x, y);
            }
            if(generator.getMinOutput() == -1 && generator.getMaxOutput() == 1 && min == 0) {
                return ((generator.noise(x,y)+1) * 0.5)*max;
            }

            return Interpolator.rangeScale(min, max, generator.getMinOutput(), generator.getMaxOutput(), generator.noise(x, y));
        }

        public override double noise(double x, double y, double z)
        {
            if(generator.getMinOutput() == min && generator.getMaxOutput() == max) {
                return generator.noise(x, y, z);
            }
            if(generator.getMinOutput() == -1 && generator.getMaxOutput() == 1 && min == 0) {
                return ((generator.noise(x,y,z)+1) * 0.5)*max;
            }
            return Interpolator.rangeScale(min, max, generator.getMinOutput(), generator.getMaxOutput(), generator.noise(x, y, z));
        }
        
        public override double getMaxOutput() {
            return max;
        }

        public override double getMinOutput() {
            return min;
        }
    }

    public class CellularHeightProvider : SeededProvider
    {
        public CellularHeightProvider(long seed) : base(seed)
        {
        }

        public override double noise(double x)
        {
            return noise(x, 0);
        }

        public override double noise(double x, double y)
        {
            long xr = round(x);
            long yr = round(y);
            double distance = 999999;
            double distance2 = 999999;
            for(long xi = xr - 1; xi <= xr + 1; xi++) {
                for(long yi = yr - 1; yi <= yr + 1; yi++) {
                    Double2 vec = CELL_2D[(int) hash2D(seed, xi, yi) & 255];
                    double vecX = xi - x + vec.x;
                    double vecY = yi - y + vec.y;
                    double newDistance = (Math.Abs(vecX) + Math.Abs(vecY)) + (vecX * vecX + vecY * vecY);
                    distance2 = Math.Max(Math.Min(distance2, newDistance), distance);
                    distance = Math.Min(distance, newDistance);
                }
            }

            return distance2 - distance - 1;
        }

        public override double noise(double x, double y, double z)
        {
            long xr = round(x);
            long yr = round(y);
            long zr = round(z);
            double distance = 999999;
            double distance2 = 999999;
            for(long xi = xr - 1; xi <= xr + 1; xi++) {
                for(long yi = yr - 1; yi <= yr + 1; yi++) {
                    for(long zi = zr - 1; zi <= zr + 1; zi++) {
                        Double3 vec = CELL_3D[(int) hash3D(seed, xi, yi, zi) & 255];
                        double vecX = xi - x + vec.x;
                        double vecY = yi - y + vec.y;
                        double vecZ = zi - z + vec.z;
                        double newDistance = (Math.Abs(vecX) + Math.Abs(vecY) + Math.Abs(vecZ)) + (vecX * vecX + vecY * vecY + vecZ * vecZ);
                        distance2 = Math.Max(Math.Min(distance2, newDistance), distance);
                        distance = Math.Min(distance, newDistance);
                    }
                }
            }

            return distance2 - distance - 1;
        }
    }

    public class Cellularizer : SeededProvider
    {
        private NoisePlane generator;
        
        public Cellularizer(NoisePlane generator, long seed) : base(seed)
        {
            this.generator = generator;
        }

        public override double noise(double x)
        {
            return noise(x, 0);
        }

        public override double noise(double x, double y)
        {
            long xr = round(x);
            long yr = round(y);

            double distance = 999999;
            long xc = 0, yc = 0, zc = 0;
            for(long xi = xr - 1; xi <= xr + 1; xi++) {
                for(long yi = yr - 1; yi <= yr + 1; yi++) {
                    Double2 vec = CELL_2D[(int) hash2D(seed, xi, yi) & 255];
                    double vecX = xi - x + vec.x;
                    double vecY = yi - y + vec.y;
                    double newDistance = (Math.Abs(vecX) + Math.Abs(vecY)) + (vecX * vecX + vecY * vecY);

                    if(newDistance < distance) {
                        distance = newDistance;
                        xc = xi;
                        yc = yi;
                    }
                }
            }

            return generator.noise(xc, yc);
        }

        public override double noise(double x, double y, double z)
        {
            long xr = round(x);
            long yr = round(y);
            long zr = round(z);
            double distance = 999999;
            long xc = 0, yc = 0, zc = 0;
            for(long xi = xr - 1; xi <= xr + 1; xi++) {
                for(long yi = yr - 1; yi <= yr + 1; yi++) {
                    for(long zi = zr - 1; zi <= zr + 1; zi++) {
                        Double3 vec = CELL_3D[(int) hash3D(seed, xi, yi, zi) & 255];
                        double vecX = xi - x + vec.x;
                        double vecY = yi - y + vec.y;
                        double vecZ = zi - z + vec.z;
                        double newDistance = (Math.Abs(vecX) + Math.Abs(vecY) + Math.Abs(vecZ)) + (vecX * vecX + vecY * vecY + vecZ * vecZ);

                        if(newDistance < distance) {
                            distance = newDistance;
                            xc = xi;
                            yc = yi;
                            zc = zi;
                        }
                    }
                }
            }

            return generator.noise(xc, yc, zc);
        }
    }

    public class CellularProvider : SeededProvider
    {
        public CellularProvider(long seed) : base(seed)
        {
        }

        public override double noise(double x)
        {
            return noise(x, 0);
        }

        public override double noise(double x, double y)
        {
            long xr = round(x);
            long yr = round(y);

            double distance = 999999;
            long xc = 0, yc = 0, zc = 0;
            for(long xi = xr - 1; xi <= xr + 1; xi++) {
                for(long yi = yr - 1; yi <= yr + 1; yi++) {
                    Double2 vec = CELL_2D[(int) hash2D(seed, xi, yi) & 255];
                    double vecX = xi - x + vec.x;
                    double vecY = yi - y + vec.y;
                    double newDistance = (Math.Abs(vecX) + Math.Abs(vecY)) + (vecX * vecX + vecY * vecY);

                    if(newDistance < distance) {
                        distance = newDistance;
                        xc = xi;
                        yc = yi;
                    }
                }
            }

            return valCoord2D(0, xc, yc);
        }

        public override double noise(double x, double y, double z)
        {
            long xr = round(x);
            long yr = round(y);
            long zr = round(z);
            double distance = 999999;
            long xc = 0, yc = 0, zc = 0;
            for(long xi = xr - 1; xi <= xr + 1; xi++) {
                for(long yi = yr - 1; yi <= yr + 1; yi++) {
                    for(long zi = zr - 1; zi <= zr + 1; zi++) {
                        Double3 vec = CELL_3D[(int) hash3D(seed, xi, yi, zi) & 255];
                        double vecX = xi - x + vec.x;
                        double vecY = yi - y + vec.y;
                        double vecZ = zi - z + vec.z;
                        double newDistance = (Math.Abs(vecX) + Math.Abs(vecY) + Math.Abs(vecZ)) + (vecX * vecX + vecY * vecY + vecZ * vecZ);

                        if(newDistance < distance) {
                            distance = newDistance;
                            xc = xi;
                            yc = yi;
                            zc = zi;
                        }
                    }
                }
            }

            return valCoord3D(0, xc, yc, zc);
        }
    }

    public class FlatProvider : SeededProvider
    {
        public FlatProvider(long seed) : base(seed)
        {
        }
        
        public override bool isFlat()
        {
            return true;
        }
        
        public override bool isScalable()
        {
            return false;
        }

        public override double noise(double x)
        {
            return 0;
        }

        public override double noise(double x, double y)
        {
            return 0;
        }

        public override double noise(double x, double y, double z)
        {
            return 0;
        }
    }
    
    
    public class ValueLinearProvider : SeededProvider
    {
        public ValueLinearProvider(long seed) : base(seed)
        {
        }

        public override double noise(double x)
        {
            long x0 = floor(x);
            long x1 = x0 + 1;
            double xs = 0, ys = 0, zs = 0;
            xs = x - x0;
            double xf00 = Interpolator.lerp(valCoord1D(seed, x0), valCoord1D(seed, x1), xs);
            double xf10 = Interpolator.lerp(valCoord1D(seed, x0), valCoord1D(seed, x1), xs);
            double xf01 = Interpolator.lerp(valCoord1D(seed, x0), valCoord1D(seed, x1), xs);
            double xf11 = Interpolator.lerp(valCoord1D(seed, x0), valCoord1D(seed, x1), xs);
            double yf0 = Interpolator.lerp(xf00, xf10, ys);
            double yf1 = Interpolator.lerp(xf01, xf11, ys);
            return Interpolator.lerp(yf0, yf1, zs);
        }

        public override double noise(double x, double y)
        {
            long x0 = floor(x);
            long y0 = floor(y);
            long x1 = x0 + 1;
            long y1 = y0 + 1;
            double xs = 0, ys = 0, zs = 0;
            xs = x - x0;
            ys = y - y0;
            double xf00 = Interpolator.lerp(valCoord2D(seed, x0, y0), valCoord2D(seed, x1, y0), xs);
            double xf10 = Interpolator.lerp(valCoord2D(seed, x0, y1), valCoord2D(seed, x1, y1), xs);
            double xf01 = Interpolator.lerp(valCoord2D(seed, x0, y0), valCoord2D(seed, x1, y0), xs);
            double xf11 = Interpolator.lerp(valCoord2D(seed, x0, y1), valCoord2D(seed, x1, y1), xs);
            double yf0 = Interpolator.lerp(xf00, xf10, ys);
            double yf1 = Interpolator.lerp(xf01, xf11, ys);
            return Interpolator.lerp(yf0, yf1, zs);
        }

        public override double noise(double x, double y, double z)
        {
            long x0 = floor(x);
            long y0 = floor(y);
            long z0 = floor(z);
            long x1 = x0 + 1;
            long y1 = y0 + 1;
            long z1 = z0 + 1;
            double xs = 0, ys = 0, zs = 0;
            xs = x - x0;
            ys = y - y0;
            zs = z - z0;
            double xf00 = Interpolator.lerp(valCoord3D(seed, x0, y0, z0), valCoord3D(seed, x1, y0, z0), xs);
            double xf10 = Interpolator.lerp(valCoord3D(seed, x0, y1, z0), valCoord3D(seed, x1, y1, z0), xs);
            double xf01 = Interpolator.lerp(valCoord3D(seed, x0, y0, z1), valCoord3D(seed, x1, y0, z1), xs);
            double xf11 = Interpolator.lerp(valCoord3D(seed, x0, y1, z1), valCoord3D(seed, x1, y1, z1), xs);
            double yf0 = Interpolator.lerp(xf00, xf10, ys);
            double yf1 = Interpolator.lerp(xf01, xf11, ys);
            return Interpolator.lerp(yf0, yf1, zs);
        }
    }
    
    public class ValueHermiteProvider : SeededProvider
    {
        public ValueHermiteProvider(long seed) : base(seed)
        {
        }

        public override double noise(double x)
        {
            long x0 = floor(x);
            long x1 = x0 + 1;
            double xs = 0, ys = 0, zs = 0;
            xs = longerpHermiteFunc(x - x0);
            double xf00 = Interpolator.lerp(valCoord1D(seed, x0), valCoord1D(seed, x1), xs);
            double xf10 = Interpolator.lerp(valCoord1D(seed, x0), valCoord1D(seed, x1), xs);
            double xf01 = Interpolator.lerp(valCoord1D(seed, x0), valCoord1D(seed, x1), xs);
            double xf11 = Interpolator.lerp(valCoord1D(seed, x0), valCoord1D(seed, x1), xs);
            double yf0 = Interpolator.lerp(xf00, xf10, ys);
            double yf1 = Interpolator.lerp(xf01, xf11, ys);
            return Interpolator.lerp(yf0, yf1, zs);
        }

        public override double noise(double x, double y)
        {
            long x0 = floor(x);
            long y0 = floor(y);
            long x1 = x0 + 1;
            long y1 = y0 + 1;
            double xs = 0, ys = 0, zs = 0;
            xs = longerpHermiteFunc(x - x0);
            ys = longerpHermiteFunc(y - y0);
            double xf00 = Interpolator.lerp(valCoord2D(seed, x0, y0), valCoord2D(seed, x1, y0), xs);
            double xf10 = Interpolator.lerp(valCoord2D(seed, x0, y1), valCoord2D(seed, x1, y1), xs);
            double xf01 = Interpolator.lerp(valCoord2D(seed, x0, y0), valCoord2D(seed, x1, y0), xs);
            double xf11 = Interpolator.lerp(valCoord2D(seed, x0, y1), valCoord2D(seed, x1, y1), xs);
            double yf0 = Interpolator.lerp(xf00, xf10, ys);
            double yf1 = Interpolator.lerp(xf01, xf11, ys);
            return Interpolator.lerp(yf0, yf1, zs);
        }

        public override double noise(double x, double y, double z)
        {
            long x0 = floor(x);
            long y0 = floor(y);
            long z0 = floor(z);
            long x1 = x0 + 1;
            long y1 = y0 + 1;
            long z1 = z0 + 1;
            double xs = 0, ys = 0, zs = 0;
            xs = longerpHermiteFunc(x - x0);
            ys = longerpHermiteFunc(y - y0);
            zs = longerpHermiteFunc(z - z0);
            double xf00 = Interpolator.lerp(valCoord3D(seed, x0, y0, z0), valCoord3D(seed, x1, y0, z0), xs);
            double xf10 = Interpolator.lerp(valCoord3D(seed, x0, y1, z0), valCoord3D(seed, x1, y1, z0), xs);
            double xf01 = Interpolator.lerp(valCoord3D(seed, x0, y0, z1), valCoord3D(seed, x1, y0, z1), xs);
            double xf11 = Interpolator.lerp(valCoord3D(seed, x0, y1, z1), valCoord3D(seed, x1, y1, z1), xs);
            double yf0 = Interpolator.lerp(xf00, xf10, ys);
            double yf1 = Interpolator.lerp(xf01, xf11, ys);
            return Interpolator.lerp(yf0, yf1, zs);
        }
    }
    
    public class FractalBillowProvider : SeededProvider
    {
        private NoisePlane[] planes;
        private int octaves;
        private double lacunarity;
        private double gain;
        private double bounding;
        
        public FractalBillowProvider(Func<long, NoisePlane> generatorFactory, long baseSeed, int octaves, double gain, double lacunarity) : base(baseSeed)
        {
            this.planes = new NoisePlane[octaves];
            this.octaves = octaves;
            this.gain = gain;
            this.lacunarity = lacunarity;
            double amp = gain;
            double ampFractal = 1;
            long seed = baseSeed;
            planes[0] = generatorFactory(seed);
            for(int i = 1; i < octaves; i++) {
                ampFractal += amp;
                amp *= gain;
                planes[i] = generatorFactory(++seed);
            }
            bounding = 1 / ampFractal;
        }
        
        public FractalBillowProvider(Func<long, NoisePlane> generatorFactory, long baseSeed, int octaves, double gain) : this(generatorFactory, baseSeed, octaves, gain, 2D)
        {
            
        }

        public FractalBillowProvider(Func<long, NoisePlane> generatorFactory, long baseSeed, int octaves) : this(generatorFactory, baseSeed, octaves, 0.5)
        {
            
        }

        public FractalBillowProvider(Func<long, NoisePlane> generatorFactory, long baseSeed) : this(generatorFactory, baseSeed, 3)
        {
            
        }

        private NoisePlane shape(NoisePlane plane)
        {
            if(plane.getMinOutput() != -1 || plane.getMaxOutput() != 1)
            {
                return plane.fit(-1, 1);
            }

            return plane;
        }

        public override double noise(double x) {
            double sum = (Math.Abs(planes[0].noise(x)) * 2) - 1;
            double amp = 1;

            for(int i = 1; i < octaves; i++) {
                x *= lacunarity;
                amp *= gain;
                sum += ((Math.Abs(planes[i].noise(x)) * 2) - 1) * amp;
            }

            return sum * bounding;
        }

        public override double noise(double x, double y) {
            double sum = (Math.Abs(planes[0].noise(x,y)) * 2) - 1;
            double amp = 1;

            for(int i = 1; i < octaves; i++) {
                x *= lacunarity;
                y *= lacunarity;
                amp *= gain;
                sum += ((Math.Abs(planes[i].noise(x, y)) * 2) - 1) * amp;
            }

            return sum * bounding;
        }

        public override double noise(double x, double y, double z) {
            double sum = (Math.Abs(planes[0].noise(x,y,z)) * 2) - 1;
            double amp = 1;

            for(int i = 1; i < octaves; i++) {
                x *= lacunarity;
                y *= lacunarity;
                z *= lacunarity;
                amp *= gain;
                sum += ((Math.Abs(planes[i].noise(x, y, z)) * 2) - 1) * amp;
            }

            return sum * bounding;
        }
    }
    
    public class FractalFBMProvider : SeededProvider
    {
        private NoisePlane[] planes;
        private int octaves;
        private double lacunarity;
        private double gain;
        private double bounding;
        
        public FractalFBMProvider(Func<long, NoisePlane> generatorFactory, long baseSeed, int octaves, double gain, double lacunarity) : base(baseSeed)
        {
            this.planes = new NoisePlane[octaves];
            this.octaves = octaves;
            this.gain = gain;
            this.lacunarity = lacunarity;
            double amp = gain;
            double ampFractal = 1;
            long seed = baseSeed;
            planes[0] = generatorFactory(seed);
            for(int i = 1; i < octaves; i++) {
                ampFractal += amp;
                amp *= gain;
                planes[i] = generatorFactory(++seed);
            }
            bounding = 1 / ampFractal;
        }
        
        public FractalFBMProvider(Func<long, NoisePlane> generatorFactory, long baseSeed, int octaves, double gain) : this(generatorFactory, baseSeed, octaves, gain, 2D)
        {
            
        }

        public FractalFBMProvider(Func<long, NoisePlane> generatorFactory, long baseSeed, int octaves) : this(generatorFactory, baseSeed, octaves, 0.5)
        {
            
        }

        public FractalFBMProvider(Func<long, NoisePlane> generatorFactory, long baseSeed) : this(generatorFactory, baseSeed, 3)
        {
            
        }

        private NoisePlane shape(NoisePlane plane)
        {
            if(plane.getMinOutput() != -1 || plane.getMaxOutput() != 1)
            {
                return plane.fit(-1, 1);
            }

            return plane;
        }

        public override double noise(double x) {
            double sum = planes[0].noise(x);
            double amp = 1;

            for(int i = 1; i < octaves; i++) {
                x *= lacunarity;
                amp *= gain;
                sum += planes[i].noise(x) * amp;
            }

            return sum * bounding;
        }

        public override double noise(double x, double y) {
            double sum = planes[0].noise(x,y);
            double amp = 1;

            for(int i = 1; i < octaves; i++) {
                x *= lacunarity;
                y *= lacunarity;
                amp *= gain;
                sum += planes[i].noise(x, y) * amp;
            }

            return sum * bounding;
        }

        public override double noise(double x, double y, double z) {
            double sum = planes[0].noise(x,y,z);
            double amp = 1;

            for(int i = 1; i < octaves; i++) {
                x *= lacunarity;
                y *= lacunarity;
                z *= lacunarity;
                amp *= gain;
                sum += planes[i].noise(x, y, z) * amp;
            }

            return sum * bounding;
        }
    }
    
    public class FractalRigidMultiProvider : SeededProvider
    {
        private NoisePlane[] planes;
        private int octaves;
        private double lacunarity;
        private double gain;
        
        public FractalRigidMultiProvider(Func<long, NoisePlane> generatorFactory, long baseSeed, int octaves, double gain, double lacunarity) : base(baseSeed)
        {
            this.planes = new NoisePlane[octaves];
            this.octaves = octaves;
            this.gain = gain;
            this.lacunarity = lacunarity;
            long seed = baseSeed;
            planes[0] = generatorFactory(seed);
            for(int i = 1; i < octaves; i++) {
                planes[i] = generatorFactory(++seed);
            }
        }
        
        public FractalRigidMultiProvider(Func<long, NoisePlane> generatorFactory, long baseSeed, int octaves, double gain) : this(generatorFactory, baseSeed, octaves, gain, 2D)
        {
            
        }

        public FractalRigidMultiProvider(Func<long, NoisePlane> generatorFactory, long baseSeed, int octaves) : this(generatorFactory, baseSeed, octaves, 0.5)
        {
            
        }

        public FractalRigidMultiProvider(Func<long, NoisePlane> generatorFactory, long baseSeed) : this(generatorFactory, baseSeed, 3)
        {
            
        }

        public override double noise(double x) {
            double sum = 1- Math.Abs(planes[0].noise(x));
            double amp = 1;

            for(int i = 1; i < octaves; i++) {
                x *= lacunarity;
                amp *= gain;
                sum -= (1- Math.Abs(planes[i].noise(x))) * amp;
            }

            return sum;
        }

        public override double noise(double x, double y) {
            double sum = 1- Math.Abs(planes[0].noise(x,y));
            double amp = 1;

            for(int i = 1; i < octaves; i++) {
                x *= lacunarity;
                y *= lacunarity;
                amp *= gain;
                sum -= (1- Math.Abs(planes[i].noise(x, y))) * amp;
            }

            return sum;
        }

        public override double noise(double x, double y, double z) {
            double sum = 1- Math.Abs(planes[0].noise(x,y,z));
            double amp = 1;

            for(int i = 1; i < octaves; i++) {
                x *= lacunarity;
                y *= lacunarity;
                z *= lacunarity;
                amp *= gain;
                sum -= (1- Math.Abs(planes[i].noise(x, y, z))) * amp;
            }

            return sum;
        }
    }
    
    public class WhiteProvider : SeededProvider
    {
        public WhiteProvider(long seed) : base(seed)
        {
            
        }
        
        public override bool isScalable()
        {
            return false;
        }

        public override double noise(double x) {
            long xi = double2Long(x);
            return valCoord1D(seed, xi);
        }

        public override double noise(double x, double y) {
            long xi = double2Long(x);
            long yi = double2Long(y);
            return valCoord2D(seed, xi, yi);
        }

        public override double noise(double x, double y, double z) {
            long xi = double2Long(x);
            long yi = double2Long(y);
            long zi = double2Long(z);
            return valCoord3D(seed, xi, yi, zi);
        }
    }

    public class OctaveProvider : NoisePlane
    {
        private NoisePlane generator;
        private int octaves;
        private  double gain;
        private double multiplier;
        
        public OctaveProvider(NoisePlane generator, int octaves, double gain)
        {
            this.generator = generator;
            this.octaves = octaves;
            this.gain = gain;
            multiplier = 1D / octaves;
        }
        
        public override double noise(double x)
        {
            double n = generator.noise(x);

            for(int i = 1; i < octaves; i++)
            {
                n += generator.noise((x + (i * 100000)) * gain * i);
            }

            return n * multiplier;
        }

        public override double noise(double x, double y)
        {
            double n = generator.noise(x, y);

            for(int i = 1; i < octaves; i++)
            {
                n += generator.noise((x + (i * 100000)) * gain * i, (y + (i * 100000)) * gain * i);
            }

            return n * multiplier;
        }

        public override double noise(double x, double y, double z)
        {
            double n = generator.noise(x);

            for(int i = 1; i < octaves; i++)
            {
                n += generator.noise((x + (i * 100000)) * gain * i, (y + (i * 100000)) * gain * i, (z + (i * 100000)) * gain * i);
            }

            return n * multiplier;
        }
        
        public override double getMaxOutput() {
            return generator.getMaxOutput();
        }

        public override double getMinOutput() {
            return generator.getMinOutput();
        }
    }

    public class PerlinProvider : SeededProvider
    {
        public PerlinProvider(long seed) : base(seed)
        {

        }

        public override double noise(double x)
        {
            long x0 = floor(x);
            long x1 = x0 + 1;
            double xs;
            double ys = 0;
            double zs = 0;
            xs = longerpHermiteFunc(x - x0);
            double xd0 = x - x0;
            double xd1 = xd0 - 1;
            double xf00 = Interpolator.lerp(gradCoord1D(seed, x0, xd0), gradCoord1D(seed, x1, xd1), xs);
            double xf10 = Interpolator.lerp(gradCoord1D(seed, x0, xd0), gradCoord1D(seed, x1, xd1), xs);
            double xf01 = Interpolator.lerp(gradCoord1D(seed, x0, xd0), gradCoord1D(seed, x1, xd1), xs);
            double xf11 = Interpolator.lerp(gradCoord1D(seed, x0, xd0), gradCoord1D(seed, x1, xd1), xs);
            double yf0 = Interpolator.lerp(xf00, xf10, ys);
            double yf1 = Interpolator.lerp(xf01, xf11, ys);
            return Interpolator.lerp(yf0, yf1, zs);
        }

        public override double noise(double x, double y)
        {
            long x0 = floor(x);
            long y0 = floor(y);
            long x1 = x0 + 1;
            long y1 = y0 + 1;
            double xs = 0, ys = 0, zs = 0;
            xs = longerpHermiteFunc(x - x0);
            ys = longerpHermiteFunc(y - y0);
            double xd0 = x - x0;
            double yd0 = y - y0;
            double xd1 = xd0 - 1;
            double yd1 = yd0 - 1;
            double xf00 = Interpolator.lerp(gradCoord2D(seed, x0, y0, xd0, yd0), gradCoord2D(seed, x1, y0, xd1, yd0), xs);
            double xf10 = Interpolator.lerp(gradCoord2D(seed, x0, y1, xd0, yd1), gradCoord2D(seed, x1, y1, xd1, yd1), xs);
            double xf01 = Interpolator.lerp(gradCoord2D(seed, x0, y0, xd0, yd0), gradCoord2D(seed, x1, y0, xd1, yd0), xs);
            double xf11 = Interpolator.lerp(gradCoord2D(seed, x0, y1, xd0, yd1), gradCoord2D(seed, x1, y1, xd1, yd1), xs);
            double yf0 = Interpolator.lerp(xf00, xf10, ys);
            double yf1 = Interpolator.lerp(xf01, xf11, ys);
            return Interpolator.lerp(yf0, yf1, zs);
        }

        public override double noise(double x, double y, double z)
        {
            long x0 = floor(x);
            long y0 = floor(y);
            long z0 = floor(z);
            long x1 = x0 + 1;
            long y1 = y0 + 1;
            long z1 = z0 + 1;
            double xs = 0, ys = 0, zs = 0;
            xs = longerpHermiteFunc(x - x0);
            ys = longerpHermiteFunc(y - y0);
            zs = longerpHermiteFunc(z - z0);
            double xd0 = x - x0;
            double yd0 = y - y0;
            double zd0 = z - z0;
            double xd1 = xd0 - 1;
            double yd1 = yd0 - 1;
            double zd1 = zd0 - 1;
            double xf00 = Interpolator.lerp(gradCoord3D(seed, x0, y0, z0, xd0, yd0, zd0), gradCoord3D(seed, x1, y0, z0, xd1, yd0, zd0), xs);
            double xf10 = Interpolator.lerp(gradCoord3D(seed, x0, y1, z0, xd0, yd1, zd0), gradCoord3D(seed, x1, y1, z0, xd1, yd1, zd0), xs);
            double xf01 = Interpolator.lerp(gradCoord3D(seed, x0, y0, z1, xd0, yd0, zd1), gradCoord3D(seed, x1, y0, z1, xd1, yd0, zd1), xs);
            double xf11 = Interpolator.lerp(gradCoord3D(seed, x0, y1, z1, xd0, yd1, zd1), gradCoord3D(seed, x1, y1, z1, xd1, yd1, zd1), xs);
            double yf0 = Interpolator.lerp(xf00, xf10, ys);
            double yf1 = Interpolator.lerp(xf01, xf11, ys);
            return Interpolator.lerp(yf0, yf1, zs);
        }
    }

    public class SimplexProvider : SeededProvider
    {
        public SimplexProvider(long seed) : base(seed)
        {
            
        }
        
        public override double noise(double x)
        {
            double F1 = 0.5 * (Math.Sqrt(3) - 1);
            double G1 = (3 - Math.Sqrt(3)) / 6;
            double t = x * F1;
            long i = floor(x + t);
            t = i * G1;
            double x0 = x - (i - t);
            long i1 = x0 > 0.5 ? 1 : 0;
            double x1 = x0 - i1 + G1;
            double x2 = x0 - 1 + 2 * G1;

            double n0, n1, n2;

            t = 0.5 - x0 * x0;
            if (t < 0)
            {
                n0 = 0;
            }
            else {
                t *= t;
                n0 = t * t * gradCoord1D(seed, i, x0);
            }

            t = 0.5 - x1 * x1;
            if (t < 0)
            {
                n1 = 0;
            }
            else {
                t *= t;
                n1 = t * t * gradCoord1D(seed, i + i1, x1);
            }

            t = 0.5 - x2 * x2;
            if (t < 0)
            {
                n2 = 0;
            }
            else {
                t *= t;
                n2 = t * t * gradCoord1D(seed, i + 1, x2);
            }

            return 50 * (n0 + n1 + n2);
        }

        public override double noise(double x, double y)
        {
            double F2 = 0.3660254037844386; // (Math.sqrt(3) - 1) / 2
            double G2 = 0.21132486540518713; // (3 - Math.sqrt(3)) / 6

            double t = (x + y) * F2;
            long i = floor(x + t);
            long j = floor(y + t);
            t = (i + j) * G2;
            double x0 = x - (i - t);
            double y0 = y - (j - t);

            long i1, j1;
            if(x0 > y0) {
                i1 = 1;
                j1 = 0;
            } else {
                i1 = 0;
                j1 = 1;
            }

            double x1 = x0 - i1 + G2;
            double y1 = y0 - j1 + G2;
            double x2 = x0 - 1 + 2 * G2;
            double y2 = y0 - 1 + 2 * G2;

            double n0, n1, n2;

            t = 0.5 - x0 * x0 - y0 * y0;
            if(t < 0)
                n0 = 0;
            else {
                t *= t;
                n0 = t * t * gradCoord2D(seed, i, j, x0, y0);
            }

            t = 0.5 - x1 * x1 - y1 * y1;
            if(t < 0)
                n1 = 0;
            else {
                t *= t;
                n1 = t * t * gradCoord2D(seed, i + i1, j + j1, x1, y1);
            }

            t = 0.5 - x2 * x2 - y2 * y2;
            if(t < 0)
                n2 = 0;
            else {
                t *= t;
                n2 = t * t * gradCoord2D(seed, i + 1, j + 1, x2, y2);
            }

            return 70 * (n0 + n1 + n2);
        }

        public override double noise(double x, double y, double z)
        {
             double t = (x + y + z) * F3;
            long i = floor(x + t);
            long j = floor(y + t);
            long k = floor(z + t);
            t = (i + j + k) * G3;
            double x0 = x - (i - t);
            double y0 = y - (j - t);
            double z0 = z - (k - t);
            long i1, j1, k1;
            long i2, j2, k2;

            if(x0 >= y0) {
                if(y0 >= z0) {
                    i1 = 1;
                    j1 = 0;
                    k1 = 0;
                    i2 = 1;
                    j2 = 1;
                    k2 = 0;
                } else if(x0 >= z0) {
                    i1 = 1;
                    j1 = 0;
                    k1 = 0;
                    i2 = 1;
                    j2 = 0;
                    k2 = 1;
                } else // x0 < z0
                {
                    i1 = 0;
                    j1 = 0;
                    k1 = 1;
                    i2 = 1;
                    j2 = 0;
                    k2 = 1;
                }
            } else // x0 < y0
            {
                if(y0 < z0) {
                    i1 = 0;
                    j1 = 0;
                    k1 = 1;
                    i2 = 0;
                    j2 = 1;
                    k2 = 1;
                } else if(x0 < z0) {
                    i1 = 0;
                    j1 = 1;
                    k1 = 0;
                    i2 = 0;
                    j2 = 1;
                    k2 = 1;
                } else // x0 >= z0
                {
                    i1 = 0;
                    j1 = 1;
                    k1 = 0;
                    i2 = 1;
                    j2 = 1;
                    k2 = 0;
                }
            }

            double x1 = x0 - i1 + G3;
            double y1 = y0 - j1 + G3;
            double z1 = z0 - k1 + G3;
            double x2 = x0 - i2 + F3;
            double y2 = y0 - j2 + F3;
            double z2 = z0 - k2 + F3;
            double x3 = x0 + G33;
            double y3 = y0 + G33;
            double z3 = z0 + G33;

            double n0, n1, n2, n3;

            t = 0.6 - x0 * x0 - y0 * y0 - z0 * z0;
            if(t < 0)
                n0 = 0;
            else {
                t *= t;
                n0 = t * t * gradCoord3D(seed, i, j, k, x0, y0, z0);
            }

            t = 0.6 - x1 * x1 - y1 * y1 - z1 * z1;
            if(t < 0)
                n1 = 0;
            else {
                t *= t;
                n1 = t * t * gradCoord3D(seed, i + i1, j + j1, k + k1, x1, y1, z1);
            }

            t = 0.6 - x2 * x2 - y2 * y2 - z2 * z2;
            if(t < 0)
                n2 = 0;
            else {
                t *= t;
                n2 = t * t * gradCoord3D(seed, i + i2, j + j2, k + k2, x2, y2, z2);
            }

            t = 0.6 - x3 * x3 - y3 * y3 - z3 * z3;
            if(t < 0)
                n3 = 0;
            else {
                t *= t;
                n3 = t * t * gradCoord3D(seed, i + 1, j + 1, k + 1, x3, y3, z3);
            }

            return 32 * (n0 + n1 + n2 + n3);
        }
    }
    
    public class Double2
    {
        public double x, y;
        
        public Double2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Double3
    {
        public double x, y, z;
        
        public Double3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public abstract class SeededProvider : NoiseProvider
    {
        protected long seed;
        
        public SeededProvider(long seed)
        {
            this.seed = seed;
        }
        
        public override long getSeed()
        {
            return seed;
        }
    }
    
    public abstract class NoiseProvider : NoisePlane
    {
        public double[] GRAD_1D = { -1D, 0D, 1D};
        public Double2[] GRAD_2D = {new Double2(-1, -1), new Double2(1, -1), new Double2(-1, 1), new Double2(1, 1), new Double2(0, -1), new Double2(-1, 0), new Double2(0, 1), new Double2(1, 0),};
        public Double3[] GRAD_3D = {new Double3(1, 1, 0), new Double3(-1, 1, 0), new Double3(1, -1, 0), new Double3(-1, -1, 0), new Double3(1, 0, 1), new Double3(-1, 0, 1), new Double3(1, 0, -1), new Double3(-1, 0, -1), new Double3(0, 1, 1), new Double3(0, -1, 1), new Double3(0, 1, -1), new Double3(0, -1, -1), new Double3(1, 1, 0), new Double3(0, -1, 1), new Double3(-1, 1, 0), new Double3(0, -1, -1),};
        public Double2[] CELL_2D = {new Double2(-0.4313539279f, 0.1281943404f), new Double2(-0.1733316799f, 0.415278375f), new Double2(-0.2821957395f, -0.3505218461f), new Double2(-0.2806473808f, 0.3517627718f), new Double2(0.3125508975f, -0.3237467165f), new Double2(0.3383018443f, -0.2967353402f), new Double2(-0.4393982022f, -0.09710417025f), new Double2(-0.4460443703f, -0.05953502905f), new Double2(-0.302223039f, 0.3334085102f), new Double2(-0.212681052f, -0.3965687458f), new Double2(-0.2991156529f, 0.3361990872f), new Double2(0.2293323691f, 0.3871778202f), new Double2(0.4475439151f, -0.04695150755f), new Double2(0.1777518f, 0.41340573f), new Double2(0.1688522499f, -0.4171197882f), new Double2(-0.0976597166f, 0.4392750616f), new Double2(0.08450188373f, 0.4419948321f), new Double2(-0.4098760448f, -0.1857461384f), new Double2(0.3476585782f, -0.2857157906f), new Double2(-0.3350670039f, -0.30038326f), new Double2(0.2298190031f, -0.3868891648f), new Double2(-0.01069924099f, 0.449872789f), new Double2(-0.4460141246f, -0.05976119672f), new Double2(0.3650293864f, 0.2631606867f), new Double2(-0.349479423f, 0.2834856838f), new Double2(-0.4122720642f, 0.1803655873f), new Double2(-0.267327811f, 0.3619887311f), new Double2(0.322124041f, -0.3142230135f), new Double2(0.2880445931f, -0.3457315612f), new Double2(0.3892170926f, -0.2258540565f), new Double2(0.4492085018f, -0.02667811596f), new Double2(-0.4497724772f, 0.01430799601f), new Double2(0.1278175387f, -0.4314657307f), new Double2(-0.03572100503f, 0.4485799926f), new Double2(-0.4297407068f, -0.1335025276f), new Double2(-0.3217817723f, 0.3145735065f), new Double2(-0.3057158873f, 0.3302087162f), new Double2(-0.414503978f, 0.1751754899f), new Double2(-0.3738139881f, 0.2505256519f), new Double2(0.2236891408f, -0.3904653228f), new Double2(0.002967775577f, -0.4499902136f), new Double2(0.1747128327f, -0.4146991995f), new Double2(-0.4423772489f, -0.08247647938f), new Double2(-0.2763960987f, -0.355112935f), new Double2(-0.4019385906f, -0.2023496216f), new Double2(0.3871414161f, -0.2293938184f), new Double2(-0.430008727f, 0.1326367019f), new Double2(-0.03037574274f, -0.4489736231f), new Double2(-0.3486181573f, 0.2845441624f), new Double2(0.04553517144f, -0.4476902368f), new Double2(-0.0375802926f, 0.4484280562f), new Double2(0.3266408905f, 0.3095250049f), new Double2(0.06540017593f, -0.4452222108f), new Double2(0.03409025829f, 0.448706869f), new Double2(-0.4449193635f, 0.06742966669f), new Double2(-0.4255936157f, -0.1461850686f), new Double2(0.449917292f, 0.008627302568f), new Double2(0.05242606404f, 0.4469356864f), new Double2(-0.4495305179f, -0.02055026661f), new Double2(-0.1204775703f, 0.4335725488f), new Double2(-0.341986385f, -0.2924813028f), new Double2(0.3865320182f, 0.2304191809f), new Double2(0.04506097811f, -0.447738214f), new Double2(-0.06283465979f, 0.4455915232f), new Double2(0.3932600341f, -0.2187385324f), new Double2(0.4472261803f, -0.04988730975f), new Double2(0.3753571011f, -0.2482076684f), new Double2(-0.273662295f, 0.357223947f), new Double2(0.1700461538f, 0.4166344988f), new Double2(0.4102692229f, 0.1848760794f), new Double2(0.323227187f, -0.3130881435f), new Double2(-0.2882310238f, -0.3455761521f), new Double2(0.2050972664f, 0.4005435199f), new Double2(0.4414085979f, -0.08751256895f), new Double2(-0.1684700334f, 0.4172743077f), new Double2(-0.003978032396f, 0.4499824166f), new Double2(-0.2055133639f, 0.4003301853f), new Double2(-0.006095674897f, -0.4499587123f), new Double2(-0.1196228124f, -0.4338091548f), new Double2(0.3901528491f, -0.2242337048f), new Double2(0.01723531752f, 0.4496698165f), new Double2(-0.3015070339f, 0.3340561458f), new Double2(-0.01514262423f, -0.4497451511f), new Double2(-0.4142574071f, -0.1757577897f), new Double2(-0.1916377265f, -0.4071547394f), new Double2(0.3749248747f, 0.2488600778f), new Double2(-0.2237774255f, 0.3904147331f), new Double2(-0.4166343106f, -0.1700466149f), new Double2(0.3619171625f, 0.267424695f), new Double2(0.1891126846f, -0.4083336779f), new Double2(-0.3127425077f, 0.323561623f), new Double2(-0.3281807787f, 0.307891826f), new Double2(-0.2294806661f, 0.3870899429f), new Double2(-0.3445266136f, 0.2894847362f), new Double2(-0.4167095422f, -0.1698621719f), new Double2(-0.257890321f, -0.3687717212f), new Double2(-0.3612037825f, 0.2683874578f), new Double2(0.2267996491f, 0.3886668486f), new Double2(0.207157062f, 0.3994821043f), new Double2(0.08355176718f, -0.4421754202f), new Double2(-0.4312233307f, 0.1286329626f), new Double2(0.3257055497f, 0.3105090899f), new Double2(0.177701095f, -0.4134275279f), new Double2(-0.445182522f, 0.06566979625f), new Double2(0.3955143435f, 0.2146355146f), new Double2(-0.4264613988f, 0.1436338239f), new Double2(-0.3793799665f, -0.2420141339f), new Double2(0.04617599081f, -0.4476245948f), new Double2(-0.371405428f, -0.2540826796f), new Double2(0.2563570295f, -0.3698392535f), new Double2(0.03476646309f, 0.4486549822f), new Double2(-0.3065454405f, 0.3294387544f), new Double2(-0.2256979823f, 0.3893076172f), new Double2(0.4116448463f, -0.1817925206f), new Double2(-0.2907745828f, -0.3434387019f), new Double2(0.2842278468f, -0.348876097f), new Double2(0.3114589359f, -0.3247973695f), new Double2(0.4464155859f, -0.0566844308f), new Double2(-0.3037334033f, -0.3320331606f), new Double2(0.4079607166f, 0.1899159123f), new Double2(-0.3486948919f, -0.2844501228f), new Double2(0.3264821436f, 0.3096924441f), new Double2(0.3211142406f, 0.3152548881f), new Double2(0.01183382662f, 0.4498443737f), new Double2(0.4333844092f, 0.1211526057f), new Double2(0.3118668416f, 0.324405723f), new Double2(-0.272753471f, 0.3579183483f), new Double2(-0.422228622f, -0.1556373694f), new Double2(-0.1009700099f, -0.4385260051f), new Double2(-0.2741171231f, -0.3568750521f), new Double2(-0.1465125133f, 0.4254810025f), new Double2(0.2302279044f, -0.3866459777f), new Double2(-0.3699435608f, 0.2562064828f), new Double2(0.105700352f, -0.4374099171f), new Double2(-0.2646713633f, 0.3639355292f), new Double2(0.3521828122f, 0.2801200935f), new Double2(-0.1864187807f, -0.4095705534f), new Double2(0.1994492955f, -0.4033856449f), new Double2(0.3937065066f, 0.2179339044f), new Double2(-0.3226158377f, 0.3137180602f), new Double2(0.3796235338f, 0.2416318948f), new Double2(0.1482921929f, 0.4248640083f), new Double2(-0.407400394f, 0.1911149365f), new Double2(0.4212853031f, 0.1581729856f), new Double2(-0.2621297173f, 0.3657704353f), new Double2(-0.2536986953f, -0.3716678248f), new Double2(-0.2100236383f, 0.3979825013f), new Double2(0.3624152444f, 0.2667493029f), new Double2(-0.3645038479f, -0.2638881295f), new Double2(0.2318486784f, 0.3856762766f), new Double2(-0.3260457004f, 0.3101519002f), new Double2(-0.2130045332f, -0.3963950918f), new Double2(0.3814998766f, -0.2386584257f), new Double2(-0.342977305f, 0.2913186713f), new Double2(-0.4355865605f, 0.1129794154f), new Double2(-0.2104679605f, 0.3977477059f), new Double2(0.3348364681f, -0.3006402163f), new Double2(0.3430468811f, 0.2912367377f), new Double2(-0.2291836801f, -0.3872658529f), new Double2(0.2547707298f, -0.3709337882f), new Double2(0.4236174945f, -0.151816397f), new Double2(-0.15387742f, 0.4228731957f), new Double2(-0.4407449312f, 0.09079595574f), new Double2(-0.06805276192f, -0.444824484f), new Double2(0.4453517192f, -0.06451237284f), new Double2(0.2562464609f, -0.3699158705f), new Double2(0.3278198355f, -0.3082761026f), new Double2(-0.4122774207f, -0.1803533432f), new Double2(0.3354090914f, -0.3000012356f), new Double2(0.446632869f, -0.05494615882f), new Double2(-0.1608953296f, 0.4202531296f), new Double2(-0.09463954939f, 0.4399356268f), new Double2(-0.02637688324f, -0.4492262904f), new Double2(0.447102804f, -0.05098119915f), new Double2(-0.4365670908f, 0.1091291678f), new Double2(-0.3959858651f, 0.2137643437f), new Double2(-0.4240048207f, -0.1507312575f), new Double2(-0.3882794568f, 0.2274622243f), new Double2(-0.4283652566f, -0.1378521198f), new Double2(0.3303888091f, 0.305521251f), new Double2(0.3321434919f, -0.3036127481f), new Double2(-0.413021046f, -0.1786438231f), new Double2(0.08403060337f, -0.4420846725f), new Double2(-0.3822882919f, 0.2373934748f), new Double2(-0.3712395594f, -0.2543249683f), new Double2(0.4472363971f, -0.04979563372f), new Double2(-0.4466591209f, 0.05473234629f), new Double2(0.0486272539f, -0.4473649407f), new Double2(-0.4203101295f, -0.1607463688f), new Double2(0.2205360833f, 0.39225481f), new Double2(-0.3624900666f, 0.2666476169f), new Double2(-0.4036086833f, -0.1989975647f), new Double2(0.2152727807f, 0.3951678503f), new Double2(-0.4359392962f, -0.1116106179f), new Double2(0.4178354266f, 0.1670735057f), new Double2(0.2007630161f, 0.4027334247f), new Double2(-0.07278067175f, -0.4440754146f), new Double2(0.3644748615f, -0.2639281632f), new Double2(-0.4317451775f, 0.126870413f), new Double2(-0.297436456f, 0.3376855855f), new Double2(-0.2998672222f, 0.3355289094f), new Double2(-0.2673674124f, 0.3619594822f), new Double2(0.2808423357f, 0.3516071423f), new Double2(0.3498946567f, 0.2829730186f), new Double2(-0.2229685561f, 0.390877248f), new Double2(0.3305823267f, 0.3053118493f), new Double2(-0.2436681211f, -0.3783197679f), new Double2(-0.03402776529f, 0.4487116125f), new Double2(-0.319358823f, 0.3170330301f), new Double2(0.4454633477f, -0.06373700535f), new Double2(0.4483504221f, 0.03849544189f), new Double2(-0.4427358436f, -0.08052932871f), new Double2(0.05452298565f, 0.4466847255f), new Double2(-0.2812560807f, 0.3512762688f), new Double2(0.1266696921f, 0.4318041097f), new Double2(-0.3735981243f, 0.2508474468f), new Double2(0.2959708351f, -0.3389708908f), new Double2(-0.3714377181f, 0.254035473f), new Double2(-0.404467102f, -0.1972469604f), new Double2(0.1636165687f, -0.419201167f), new Double2(0.3289185495f, -0.3071035458f), new Double2(-0.2494824991f, -0.3745109914f), new Double2(0.03283133272f, 0.4488007393f), new Double2(-0.166306057f, -0.4181414777f), new Double2(-0.106833179f, 0.4371346153f), new Double2(0.06440260376f, -0.4453676062f), new Double2(-0.4483230967f, 0.03881238203f), new Double2(-0.421377757f, -0.1579265206f), new Double2(0.05097920662f, -0.4471030312f), new Double2(0.2050584153f, -0.4005634111f), new Double2(0.4178098529f, -0.167137449f), new Double2(-0.3565189504f, -0.2745801121f), new Double2(0.4478398129f, 0.04403977727f), new Double2(-0.3399999602f, -0.2947881053f), new Double2(0.3767121994f, 0.2461461331f), new Double2(-0.3138934434f, 0.3224451987f), new Double2(-0.1462001792f, -0.4255884251f), new Double2(0.3970290489f, -0.2118205239f), new Double2(0.4459149305f, -0.06049689889f), new Double2(-0.4104889426f, -0.1843877112f), new Double2(0.1475103971f, -0.4251360756f), new Double2(0.09258030352f, 0.4403735771f), new Double2(-0.1589664637f, -0.4209865359f), new Double2(0.2482445008f, 0.3753327428f), new Double2(0.4383624232f, -0.1016778537f), new Double2(0.06242802956f, 0.4456486745f), new Double2(0.2846591015f, -0.3485243118f), new Double2(-0.344202744f, -0.2898697484f), new Double2(0.1198188883f, -0.4337550392f), new Double2(-0.243590703f, 0.3783696201f), new Double2(0.2958191174f, -0.3391033025f), new Double2(-0.1164007991f, 0.4346847754f), new Double2(0.1274037151f, -0.4315881062f), new Double2(0.368047306f, 0.2589231171f), new Double2(0.2451436949f, 0.3773652989f), new Double2(-0.4314509715f, 0.12786735f),};
        public Double3[] CELL_3D = {new Double3(0.1453787434f, -0.4149781685f, -0.0956981749f), new Double3(-0.01242829687f, -0.1457918398f, -0.4255470325f), new Double3(0.2877979582f, -0.02606483451f, -0.3449535616f), new Double3(-0.07732986802f, 0.2377094325f, 0.3741848704f), new Double3(0.1107205875f, -0.3552302079f, -0.2530858567f), new Double3(0.2755209141f, 0.2640521179f, -0.238463215f), new Double3(0.294168941f, 0.1526064594f, 0.3044271714f), new Double3(0.4000921098f, -0.2034056362f, 0.03244149937f), new Double3(-0.1697304074f, 0.3970864695f, -0.1265461359f), new Double3(-0.1483224484f, -0.3859694688f, 0.1775613147f), new Double3(0.2623596946f, -0.2354852944f, 0.2796677792f), new Double3(-0.2709003183f, 0.3505271138f, -0.07901746678f), new Double3(-0.03516550699f, 0.3885234328f, 0.2243054374f), new Double3(-0.1267712655f, 0.1920044036f, 0.3867342179f), new Double3(0.02952021915f, 0.4409685861f, 0.08470692262f), new Double3(-0.2806854217f, -0.266996757f, 0.2289725438f), new Double3(-0.171159547f, 0.2141185563f, 0.3568720405f), new Double3(0.2113227183f, 0.3902405947f, -0.07453178509f), new Double3(-0.1024352839f, 0.2128044156f, -0.3830421561f), new Double3(-0.3304249877f, -0.1566986703f, 0.2622305365f), new Double3(0.2091111325f, 0.3133278055f, -0.2461670583f), new Double3(0.344678154f, -0.1944240454f, -0.2142341261f), new Double3(0.1984478035f, -0.3214342325f, -0.2445373252f), new Double3(-0.2929008603f, 0.2262915116f, 0.2559320961f), new Double3(-0.1617332831f, 0.006314769776f, -0.4198838754f), new Double3(-0.3582060271f, -0.148303178f, -0.2284613961f), new Double3(-0.1852067326f, -0.3454119342f, -0.2211087107f), new Double3(0.3046301062f, 0.1026310383f, 0.314908508f), new Double3(-0.03816768434f, -0.2551766358f, -0.3686842991f), new Double3(-0.4084952196f, 0.1805950793f, 0.05492788837f), new Double3(-0.02687443361f, -0.2749741471f, 0.3551999201f), new Double3(-0.03801098351f, 0.3277859044f, 0.3059600725f), new Double3(0.2371120802f, 0.2900386767f, -0.2493099024f), new Double3(0.4447660503f, 0.03946930643f, 0.05590469027f), new Double3(0.01985147278f, -0.01503183293f, -0.4493105419f), new Double3(0.4274339143f, 0.03345994256f, -0.1366772882f), new Double3(-0.2072988631f, 0.2871414597f, -0.2776273824f), new Double3(-0.3791240978f, 0.1281177671f, 0.2057929936f), new Double3(-0.2098721267f, -0.1007087278f, -0.3851122467f), new Double3(0.01582798878f, 0.4263894424f, 0.1429738373f), new Double3(-0.1888129464f, -0.3160996813f, -0.2587096108f), new Double3(0.1612988974f, -0.1974805082f, -0.3707885038f), new Double3(-0.08974491322f, 0.229148752f, -0.3767448739f), new Double3(0.07041229526f, 0.4150230285f, -0.1590534329f), new Double3(-0.1082925611f, -0.1586061639f, 0.4069604477f), new Double3(0.2474100658f, -0.3309414609f, 0.1782302128f), new Double3(-0.1068836661f, -0.2701644537f, -0.3436379634f), new Double3(0.2396452163f, 0.06803600538f, -0.3747549496f), new Double3(-0.3063886072f, 0.2597428179f, 0.2028785103f), new Double3(0.1593342891f, -0.3114350249f, -0.2830561951f), new Double3(0.2709690528f, 0.1412648683f, -0.3303331794f), new Double3(-0.1519780427f, 0.3623355133f, 0.2193527988f), new Double3(0.1699773681f, 0.3456012883f, 0.2327390037f), new Double3(-0.1986155616f, 0.3836276443f, -0.1260225743f), new Double3(-0.1887482106f, -0.2050154888f, -0.353330953f), new Double3(0.2659103394f, 0.3015631259f, -0.2021172246f), new Double3(-0.08838976154f, -0.4288819642f, -0.1036702021f), new Double3(-0.04201869311f, 0.3099592485f, 0.3235115047f), new Double3(-0.3230334656f, 0.201549922f, -0.2398478873f), new Double3(0.2612720941f, 0.2759854499f, -0.2409749453f), new Double3(0.385713046f, 0.2193460345f, 0.07491837764f), new Double3(0.07654967953f, 0.3721732183f, 0.241095919f), new Double3(0.4317038818f, -0.02577753072f, 0.1243675091f), new Double3(-0.2890436293f, -0.3418179959f, -0.04598084447f), new Double3(-0.2201947582f, 0.383023377f, -0.08548310451f), new Double3(0.4161322773f, -0.1669634289f, -0.03817251927f), new Double3(0.2204718095f, 0.02654238946f, -0.391391981f), new Double3(-0.1040307469f, 0.3890079625f, -0.2008741118f), new Double3(-0.1432122615f, 0.371614387f, -0.2095065525f), new Double3(0.3978380468f, -0.06206669342f, 0.2009293758f), new Double3(-0.2599274663f, 0.2616724959f, -0.2578084893f), new Double3(0.4032618332f, -0.1124593585f, 0.1650235939f), new Double3(-0.08953470255f, -0.3048244735f, 0.3186935478f), new Double3(0.118937202f, -0.2875221847f, 0.325092195f), new Double3(0.02167047076f, -0.03284630549f, -0.4482761547f), new Double3(-0.3411343612f, 0.2500031105f, 0.1537068389f), new Double3(0.3162964612f, 0.3082064153f, -0.08640228117f), new Double3(0.2355138889f, -0.3439334267f, -0.1695376245f), new Double3(-0.02874541518f, -0.3955933019f, 0.2125550295f), new Double3(-0.2461455173f, 0.02020282325f, -0.3761704803f), new Double3(0.04208029445f, -0.4470439576f, 0.02968078139f), new Double3(0.2727458746f, 0.2288471896f, -0.2752065618f), new Double3(-0.1347522818f, -0.02720848277f, -0.4284874806f), new Double3(0.3829624424f, 0.1231931484f, -0.2016512234f), new Double3(-0.3547613644f, 0.1271702173f, 0.2459107769f), new Double3(0.2305790207f, 0.3063895591f, 0.2354968222f), new Double3(-0.08323845599f, -0.1922245118f, 0.3982726409f), new Double3(0.2993663085f, -0.2619918095f, -0.2103333191f), new Double3(-0.2154865723f, 0.2706747713f, 0.287751117f), new Double3(0.01683355354f, -0.2680655787f, -0.3610505186f), new Double3(0.05240429123f, 0.4335128183f, -0.1087217856f), new Double3(0.00940104872f, -0.4472890582f, 0.04841609928f), new Double3(0.3465688735f, 0.01141914583f, -0.2868093776f), new Double3(-0.3706867948f, -0.2551104378f, 0.003156692623f), new Double3(0.2741169781f, 0.2139972417f, -0.2855959784f), new Double3(0.06413433865f, 0.1708718512f, 0.4113266307f), new Double3(-0.388187972f, -0.03973280434f, -0.2241236325f), new Double3(0.06419469312f, -0.2803682491f, 0.3460819069f), new Double3(-0.1986120739f, -0.3391173584f, 0.2192091725f), new Double3(-0.203203009f, -0.3871641506f, 0.1063600375f), new Double3(-0.1389736354f, -0.2775901578f, -0.3257760473f), new Double3(-0.06555641638f, 0.342253257f, -0.2847192729f), new Double3(-0.2529246486f, -0.2904227915f, 0.2327739768f), new Double3(0.1444476522f, 0.1069184044f, 0.4125570634f), new Double3(-0.3643780054f, -0.2447099973f, -0.09922543227f), new Double3(0.4286142488f, -0.1358496089f, -0.01829506817f), new Double3(0.165872923f, -0.3136808464f, -0.2767498872f), new Double3(0.2219610524f, -0.3658139958f, 0.1393320198f), new Double3(0.04322940318f, -0.3832730794f, 0.2318037215f), new Double3(-0.08481269795f, -0.4404869674f, -0.03574965489f), new Double3(0.1822082075f, -0.3953259299f, 0.1140946023f), new Double3(-0.3269323334f, 0.3036542563f, 0.05838957105f), new Double3(-0.4080485344f, 0.04227858267f, -0.184956522f), new Double3(0.2676025294f, -0.01299671652f, 0.36155217f), new Double3(0.3024892441f, -0.1009990293f, -0.3174892964f), new Double3(0.1448494052f, 0.425921681f, -0.0104580805f), new Double3(0.4198402157f, 0.08062320474f, 0.1404780841f), new Double3(-0.3008872161f, -0.333040905f, -0.03241355801f), new Double3(0.3639310428f, -0.1291284382f, -0.2310412139f), new Double3(0.3295806598f, 0.0184175994f, -0.3058388149f), new Double3(0.2776259487f, -0.2974929052f, -0.1921504723f), new Double3(0.4149000507f, -0.144793182f, -0.09691688386f), new Double3(0.145016715f, -0.0398992945f, 0.4241205002f), new Double3(0.09299023471f, -0.299732164f, -0.3225111565f), new Double3(0.1028907093f, -0.361266869f, 0.247789732f), new Double3(0.2683057049f, -0.07076041213f, -0.3542668666f), new Double3(-0.4227307273f, -0.07933161816f, -0.1323073187f), new Double3(-0.1781224702f, 0.1806857196f, -0.3716517945f), new Double3(0.4390788626f, -0.02841848598f, -0.09435116353f), new Double3(0.2972583585f, 0.2382799621f, -0.2394997452f), new Double3(-0.1707002821f, 0.2215845691f, 0.3525077196f), new Double3(0.3806686614f, 0.1471852559f, -0.1895464869f), new Double3(-0.1751445661f, -0.274887877f, 0.3102596268f), new Double3(-0.2227237566f, -0.2316778837f, 0.3149912482f), new Double3(0.1369633021f, 0.1341343041f, -0.4071228836f), new Double3(-0.3529503428f, -0.2472893463f, -0.129514612f), new Double3(-0.2590744185f, -0.2985577559f, -0.2150435121f), new Double3(-0.3784019401f, 0.2199816631f, -0.1044989934f), new Double3(-0.05635805671f, 0.1485737441f, 0.4210102279f), new Double3(0.3251428613f, 0.09666046873f, -0.2957006485f), new Double3(-0.4190995804f, 0.1406751354f, -0.08405978803f), new Double3(-0.3253150961f, -0.3080335042f, -0.04225456877f), new Double3(0.2857945863f, -0.05796152095f, 0.3427271751f), new Double3(-0.2733604046f, 0.1973770973f, -0.2980207554f), new Double3(0.219003657f, 0.2410037886f, -0.3105713639f), new Double3(0.3182767252f, -0.271342949f, 0.1660509868f), new Double3(-0.03222023115f, -0.3331161506f, -0.300824678f), new Double3(-0.3087780231f, 0.1992794134f, -0.2596995338f), new Double3(-0.06487611647f, -0.4311322747f, 0.1114273361f), new Double3(0.3921171432f, -0.06294284106f, -0.2116183942f), new Double3(-0.1606404506f, -0.358928121f, -0.2187812825f), new Double3(-0.03767771199f, -0.2290351443f, 0.3855169162f), new Double3(0.1394866832f, -0.3602213994f, 0.2308332918f), new Double3(-0.4345093872f, 0.005751117145f, 0.1169124335f), new Double3(-0.1044637494f, 0.4168128432f, -0.1336202785f), new Double3(0.2658727501f, 0.2551943237f, 0.2582393035f), new Double3(0.2051461999f, 0.1975390727f, 0.3484154868f), new Double3(-0.266085566f, 0.23483312f, 0.2766800993f), new Double3(0.07849405464f, -0.3300346342f, -0.2956616708f), new Double3(-0.2160686338f, 0.05376451292f, -0.3910546287f), new Double3(-0.185779186f, 0.2148499206f, 0.3490352499f), new Double3(0.02492421743f, -0.3229954284f, -0.3123343347f), new Double3(-0.120167831f, 0.4017266681f, 0.1633259825f), new Double3(-0.02160084693f, -0.06885389554f, 0.4441762538f), new Double3(0.2597670064f, 0.3096300784f, 0.1978643903f), new Double3(-0.1611553854f, -0.09823036005f, 0.4085091653f), new Double3(-0.3278896792f, 0.1461670309f, 0.2713366126f), new Double3(0.2822734956f, 0.03754421121f, -0.3484423997f), new Double3(0.03169341113f, 0.347405252f, -0.2842624114f), new Double3(0.2202613604f, -0.3460788041f, -0.1849713341f), new Double3(0.2933396046f, 0.3031973659f, 0.1565989581f), new Double3(-0.3194922995f, 0.2453752201f, -0.200538455f), new Double3(-0.3441586045f, -0.1698856132f, -0.2349334659f), new Double3(0.2703645948f, -0.3574277231f, 0.04060059933f), new Double3(0.2298568861f, 0.3744156221f, 0.0973588921f), new Double3(0.09326603877f, -0.3170108894f, 0.3054595587f), new Double3(-0.1116165319f, -0.2985018719f, 0.3177080142f), new Double3(0.2172907365f, -0.3460005203f, -0.1885958001f), new Double3(0.1991339479f, 0.3820341668f, -0.1299829458f), new Double3(-0.0541918155f, -0.2103145071f, 0.39412061f), new Double3(0.08871336998f, 0.2012117383f, 0.3926114802f), new Double3(0.2787673278f, 0.3505404674f, 0.04370535101f), new Double3(-0.322166438f, 0.3067213525f, 0.06804996813f), new Double3(-0.4277366384f, 0.132066775f, 0.04582286686f), new Double3(0.240131882f, -0.1612516055f, 0.344723946f), new Double3(0.1448607981f, -0.2387819045f, 0.3528435224f), new Double3(-0.3837065682f, -0.2206398454f, 0.08116235683f), new Double3(-0.4382627882f, -0.09082753406f, -0.04664855374f), new Double3(-0.37728353f, 0.05445141085f, 0.2391488697f), new Double3(0.1259579313f, 0.348394558f, 0.2554522098f), new Double3(-0.1406285511f, -0.270877371f, -0.3306796947f), new Double3(-0.1580694418f, 0.4162931958f, -0.06491553533f), new Double3(0.2477612106f, -0.2927867412f, -0.2353514536f), new Double3(0.2916132853f, 0.3312535401f, 0.08793624968f), new Double3(0.07365265219f, -0.1666159848f, 0.411478311f), new Double3(-0.26126526f, -0.2422237692f, 0.2748965434f), new Double3(-0.3721862032f, 0.252790166f, 0.008634938242f), new Double3(-0.3691191571f, -0.255281188f, 0.03290232422f), new Double3(0.2278441737f, -0.3358364886f, 0.1944244981f), new Double3(0.363398169f, -0.2310190248f, 0.1306597909f), new Double3(-0.304231482f, -0.2698452035f, 0.1926830856f), new Double3(-0.3199312232f, 0.316332536f, -0.008816977938f), new Double3(0.2874852279f, 0.1642275508f, -0.304764754f), new Double3(-0.1451096801f, 0.3277541114f, -0.2720669462f), new Double3(0.3220090754f, 0.0511344108f, 0.3101538769f), new Double3(-0.1247400865f, -0.04333605335f, -0.4301882115f), new Double3(-0.2829555867f, -0.3056190617f, -0.1703910946f), new Double3(0.1069384374f, 0.3491024667f, -0.2630430352f), new Double3(-0.1420661144f, -0.3055376754f, -0.2982682484f), new Double3(-0.250548338f, 0.3156466809f, -0.2002316239f), new Double3(0.3265787872f, 0.1871229129f, 0.2466400438f), new Double3(0.07646097258f, -0.3026690852f, 0.324106687f), new Double3(0.3451771584f, 0.2757120714f, -0.0856480183f), new Double3(0.298137964f, 0.2852657134f, 0.179547284f), new Double3(0.2812250376f, 0.3466716415f, 0.05684409612f), new Double3(0.4390345476f, -0.09790429955f, -0.01278335452f), new Double3(0.2148373234f, 0.1850172527f, 0.3494474791f), new Double3(0.2595421179f, -0.07946825393f, 0.3589187731f), new Double3(0.3182823114f, -0.307355516f, -0.08203022006f), new Double3(-0.4089859285f, -0.04647718411f, 0.1818526372f), new Double3(-0.2826749061f, 0.07417482322f, 0.3421885344f), new Double3(0.3483864637f, 0.225442246f, -0.1740766085f), new Double3(-0.3226415069f, -0.1420585388f, -0.2796816575f), new Double3(0.4330734858f, -0.118868561f, -0.02859407492f), new Double3(-0.08717822568f, -0.3909896417f, -0.2050050172f), new Double3(-0.2149678299f, 0.3939973956f, -0.03247898316f), new Double3(-0.2687330705f, 0.322686276f, -0.1617284888f), new Double3(0.2105665099f, -0.1961317136f, -0.3459683451f), new Double3(0.4361845915f, -0.1105517485f, 0.004616608544f), new Double3(0.05333333359f, -0.313639498f, -0.3182543336f), new Double3(-0.05986216652f, 0.1361029153f, -0.4247264031f), new Double3(0.3664988455f, 0.2550543014f, -0.05590974511f), new Double3(-0.2341015558f, -0.182405731f, 0.3382670703f), new Double3(-0.04730947785f, -0.4222150243f, -0.1483114513f), new Double3(-0.2391566239f, -0.2577696514f, -0.2808182972f), new Double3(-0.1242081035f, 0.4256953395f, -0.07652336246f), new Double3(0.2614832715f, -0.3650179274f, 0.02980623099f), new Double3(-0.2728794681f, -0.3499628774f, 0.07458404908f), new Double3(0.007892900508f, -0.1672771315f, 0.4176793787f), new Double3(-0.01730330376f, 0.2978486637f, -0.3368779738f), new Double3(0.2054835762f, -0.3252600376f, -0.2334146693f), new Double3(-0.3231994983f, 0.1564282844f, -0.2712420987f), new Double3(-0.2669545963f, 0.2599343665f, -0.2523278991f), new Double3(-0.05554372779f, 0.3170813944f, -0.3144428146f), new Double3(-0.2083935713f, -0.310922837f, -0.2497981362f), new Double3(0.06989323478f, -0.3156141536f, 0.3130537363f), new Double3(0.3847566193f, -0.1605309138f, -0.1693876312f), new Double3(-0.3026215288f, -0.3001537679f, -0.1443188342f), new Double3(0.3450735512f, 0.08611519592f, 0.2756962409f), new Double3(0.1814473292f, -0.2788782453f, -0.3029914042f), new Double3(-0.03855010448f, 0.09795110726f, 0.4375151083f), new Double3(0.3533670318f, 0.2665752752f, 0.08105160988f), new Double3(-0.007945601311f, 0.140359426f, -0.4274764309f), new Double3(0.4063099273f, -0.1491768253f, -0.1231199324f), new Double3(-0.2016773589f, 0.008816271194f, -0.4021797064f), new Double3(-0.07527055435f, -0.425643481f, -0.1251477955f),};
        public double F3 = 1.0 / 3.0;
        public double G3 = 1.0 / 6.0;
        public double K1 = 0.366025404; // (sqrt(3)-1)/2;
        public double K2 = 0.211324865; // (3-sqrt(3))/6;
        public double G33 = (1.0 / 6.0) * 3 - 1;
        public long X_PRIME = 1619;
        public long Y_PRIME = 31337;
        public long Z_PRIME = 6971;
        public long W_PRIME = 1013;

        public abstract long getSeed();
        
        public virtual long floor(double f)
        {
            return (f >= 0 ?  (long)f : (long)f - 1);
        }
        
        public virtual double gradCoord1D(long seed, long x, double xd) {
            long hash = seed;
            hash ^= X_PRIME * x;
            hash = hash * hash * hash * 60493;
            hash = (hash >> 13) ^ hash;
            return xd * GRAD_1D[(int) hash & 2];
        }

        public virtual double gradCoord2D(long seed, long x, long y, double xd, double yd) {
            long hash = seed;
            hash ^= X_PRIME * x;
            hash ^= Y_PRIME * y;

            hash = hash * hash * hash * 60493;
            hash = (hash >> 13) ^ hash;

            Double2 g = GRAD_2D[(int) hash & 7];

            return xd * g.x + yd * g.y;
        }

        public virtual double gradCoord3D(long seed, long x, long y, long z, double xd, double yd, double zd) {
            long hash = seed;
            hash ^= X_PRIME * x;
            hash ^= Y_PRIME * y;
            hash ^= Z_PRIME * z;

            hash = hash * hash * hash * 60493;
            hash = (hash >> 13) ^ hash;

            Double3 g = GRAD_3D[(int) (hash & 15)];

            return xd * g.x + yd * g.y + zd * g.z;
        }
        
        public virtual double valCoord1D(long seed, long x) {
            long n = seed;
            n ^= X_PRIME * x;

            return ((n * n * n * 60493L) / (double) long.MaxValue);
        }

        public virtual double valCoord2D(long seed, long x, long y) {
            long n = seed;
            n ^= X_PRIME * x;
            n ^= Y_PRIME * y;

            return ((n * n * n * 60493L) / (double) long.MaxValue);
        }

        public virtual double valCoord3D(long seed, long x, long y, long z) {
            long n = seed;
            n ^= X_PRIME * x;
            n ^= Y_PRIME * y;
            n ^= Z_PRIME * z;

            return ((n * n * n * 60493L) / (double) long.MaxValue);
        }
        
        public virtual long hash2D(long seed, long x, long y) {
            long hash = seed;
            hash ^= X_PRIME * x;
            hash ^= Y_PRIME * y;

            hash = hash * hash * hash * 60493;
            hash = (hash >> 13) ^ hash;

            return hash;
        }

        public virtual long hash3D(long seed, long x, long y, long z) {
            long hash = seed;
            hash ^= X_PRIME * x;
            hash ^= Y_PRIME * y;
            hash ^= Z_PRIME * z;

            hash = hash * hash * hash * 60493;
            hash = (hash >> 13) ^ hash;

            return hash;
        }

        public virtual long hash4D(long seed, long x, long y, long z, long w) {
            long hash = seed;
            hash ^= X_PRIME * x;
            hash ^= Y_PRIME * y;
            hash ^= Z_PRIME * z;
            hash ^= W_PRIME * w;

            hash = hash * hash * hash * 60493;
            hash = (hash >> 13) ^ hash;

            return hash;
        }
        
        public virtual double longerpHermiteFunc(double t) {
            return t * t * (3 - 2 * t);
        }

        public virtual long round(double f)
        {
            return (f >= 0) ? (long) (f + 0.5) : (long) (f - 0.5);
        }
        
        public virtual int iround(double f)
        {
            return (f >= 0) ? (int) (f + 0.5) : (int) (f - 0.5);
        }

        public virtual bool isFlat()
        {
            return false;
        }
        
        public virtual bool isScalable()
        {
            return true;
        }

        public virtual double step(double a, double b)
        {
            return a > b ? 1 : 0;
        }
        
        public virtual long double2Long(double f)
        {
            long i = BitConverter.DoubleToInt64Bits(f);

            return i ^ (i >> 16);
        }
    }
    
    public class ExponentProvider : NoisePlane{
        private NoisePlane generator;
        private double exponent;
        
        public ExponentProvider(NoisePlane generator, double exponent) {
            this.generator = generator;
            this.exponent = exponent;
        }
        
        public override double noise(double x)
        {
            return Math.Pow(generator.noise(x), exponent);
        }

        public override double noise(double x, double y)
        {
            return Math.Pow(generator.noise(x, y), exponent);
        }

        public override double noise(double x, double y, double z)
        {
            return Math.Pow(generator.noise(x, y, z), exponent);
        }
        
        public override double getMaxOutput() {
            return generator.getMaxOutput();
        }

        public override  double getMinOutput() {
            return generator.getMinOutput();
        }
    }

    public class AbstractNoisePlane3D : NoisePlane
    {
        private Func<double, double, double, double> func;
        
        public AbstractNoisePlane3D(Func<double, double, double, double> func)
        {
            this.func = func;
        }
        
        public override double noise(double x)
        {
            return noise(x, 0, 0);
        }

        public override double noise(double x, double y)
        {
            return noise(x, y, 0);
        }

        public override double noise(double x, double y, double z)
        {
            return func(x, y, z);
        }
    }
    
    public class AbstractNoisePlane2D : NoisePlane
    {
        private Func<double, double, double> func;
        
        public AbstractNoisePlane2D(Func<double, double, double> func)
        {
            this.func = func;
        }
        
        public override double noise(double x)
        {
            return noise(x, 0, 0);
        }

        public override double noise(double x, double y)
        {
            return func(x, y);
        }

        public override double noise(double x, double y, double z)
        {
            return func(x, y);
        }
    }
    
    public abstract class NoisePlane
    {
        public NoisePlane clip(double min, double max)
        {
            return new ClippingProvider(this, min, max);
        }
        
        public NoisePlane contrast(double amount)
        {
            return new ContrastingProvider(this, amount);
        }
        
        public NoisePlane add(NoisePlane other)
        {
            return new AddingProvider(this, other);
        }

        public NoisePlane scale(double scale)
        {
            return new ScaledProvider(this, scale);
        }

        public NoisePlane octave(int octaves, double gain)
        {
            return new OctaveProvider(this, octaves, gain);
        }

        public NoisePlane fit(double min, double max)
        {
            return new FittedProvider(this, min, max);
        }
        
        public NoisePlane exponent(double exponent)
        {
            return new ExponentProvider(this, exponent);
        }

        public NoisePlane edgeDetect(double threshold)
        {
            return new EdgeProvider(this, threshold, false);
        }

        public NoisePlane edgeDetectFast(double threshold)
        {
            return new EdgeProvider(this, threshold, true);
        }

        public NoisePlane invert()
        {
            return new InvertedProvider(this);
        }

        public NoisePlane posturize(int values)
        {
            return new PosturizedProvider(this, values);
        }
        
        public NoisePlane warp(NoisePlane warp, double scale, double multiplier)
        {
            return new WarpedProvider(this, warp, scale, multiplier);
        }

        public NoisePlane cellularize(long seed)
        {
            return new Cellularizer(this, seed);
        }

        public NoisePlane linear(double scale)
        {
            return new LinearInterpolator(this, scale);
        }

        public NoisePlane cubic(double scale)
        {
            return new CubicInterpolator(this, scale);
        }

        public NoisePlane hermite(double scale)
        {
            return new HermiteInterpolator(this, scale);
        }
        
        public bool supports1D() {
            return true;
        }

        public bool supports2D() {
            return true;
        }

        public virtual bool supports3D() {
            return true;
        }
        
        public virtual double getMaxOutput()
        {
            return 1;
        }

        public virtual double getMinOutput()
        {
            return -1;
        }
        
        public int i(double x, double y, double z, int min, int max)
        {
            return (int)Math.Round(fit(min, max).noise(x,y,z));
        }

        public int i(double x, double y, int min, int max)
        {
            return (int)Math.Round(fit(min, max).noise(x,y));
        }

        public int i(double x, int min, int max)
        {
            return (int)Math.Round(fit(min, max).noise(x));
        }

        public double d(double x, double y, double z, double min, double max)
        {
            return fit(min, max).noise(x,y,z);
        }

        public double d(double x, double y, double min, double max)
        {
            return fit(min, max).noise(x,y);
        }

        public double d(double x, double min, double max)
        {
            return fit(min, max).noise(x);
        }
        
        public abstract double noise(double x);
        
        public abstract double noise(double x, double y);
        
        public abstract double noise(double x, double y, double z);

        public T select<T>(T[] array, double x, double y, double z)
        {
            if (array.Length == 1)
            {
                return array[0];
            }

            double n = noise(x, y, z);

            try
            {
                return array[Convert.ToInt32(n * (array.Length - 1))];
            }
            catch (Exception e)
            {
                Debug.LogError("Noise was " + n + " Array length " + array.Length + " Index " + Convert.ToInt32(n * (array.Length - 1)));
                throw;
            }
        }
        
        public T pickWeighted<T>(double x, double y, double z, List<T> list) where T : IWeighted
        {
            double totalWeight = 0;
            double[] weights = new double[list.Count];
            T t;

            for(int i = 0; i < list.Count; i++) {
                t = list[i];
                weights[i] = t.getWeight();
                totalWeight += list[i].getWeight();
            }

            double r = d(x,y,z, 0, totalWeight);

            for(int i = 0; i < list.Count; i++) {
                if(r <= weights[i]) {
                    return list[i];
                }

                r -= weights[i];
            }

            return list[list.Count-1];
        }

        public T pickWeighted<T>(double x, double y, List<T> list) where T : IWeighted
        {
            double totalWeight = 0;
            double[] weights = new double[list.Count];
            T t;

            for(int i = 0; i < list.Count; i++) {
                t = list[i];
                weights[i] = t.getWeight();
                totalWeight += list[i].getWeight();
            }

            double r = d(x,y, 0, totalWeight);

            for(int i = 0; i < list.Count; i++) {
                if(r <= weights[i]) {
                    return list[i];
                }

                r -= weights[i];
            }

            return list[list.Count-1];
        }
        
        public T pickWeighted<T>(double x, List<T> list) where T : IWeighted
        {
            double totalWeight = 0;
            double[] weights = new double[list.Count];
            T t;

            for(int i = 0; i < list.Count; i++) {
                t = list[i];
                weights[i] = t.getWeight();
                totalWeight += list[i].getWeight();
            }

            double r = d(x, 0, totalWeight);

            for(int i = 0; i < list.Count; i++) {
                if(r <= weights[i]) {
                    return list[i];
                }

                r -= weights[i];
            }

            return list[list.Count-1];
        }

        public static NoisePlane of(NoisePreset preset, long s)
        {
            return preset switch
            {
                NoisePreset.Simplex => new SimplexProvider(s),
                NoisePreset.Cellular => new CellularProvider(s),
                NoisePreset.CellularEdge => of(NoisePreset.Cellular, s).edgeDetectFast(0.000000001),
                NoisePreset.CellularEdgeThin => of(NoisePreset.Cellular, s).scale(0.25).edgeDetectFast(0.000000001),
                NoisePreset.CellularEdgeThick => of(NoisePreset.Cellular, s).scale(2).edgeDetectFast(0.000000001),
                NoisePreset.CellularEdgeHairthin => of(NoisePreset.Cellular, s).scale(0.1).edgeDetectFast(0.000000001),
                NoisePreset.CellularHeight => new CellularHeightProvider(s),
                NoisePreset.Flat => new FlatProvider(s),
                NoisePreset.Perlin => new PerlinProvider(s),
                NoisePreset.White => new WhiteProvider(s),
                NoisePreset.Value => new ValueLinearProvider(s),
                NoisePreset.ValueHermite => new ValueHermiteProvider(s),
                NoisePreset.Bendplex => new SimplexProvider(s).warp(new PerlinProvider(s+1), 3.5, 0.25),
                NoisePreset.Droopy => new SimplexProvider(s).warp(new PerlinProvider(s+1), 1.7, 0.75),
                NoisePreset.Wrinkleplex =>  new SimplexProvider(s).warp(new PerlinProvider(s+1), 5.7, 0.15),
                NoisePreset.Natural =>  new SimplexProvider(s).octave(2, 0.5)
                    .warp(new PerlinProvider(s+2), 0.99, 0.55)
                    .warp(new PerlinProvider(s+1), 8.7, 0.07),
                NoisePreset.Wetland => new FractalBillowProvider((l) =>new PerlinProvider(l), s, 1, 0, 2D),
                NoisePreset.Lava => new FractalBillowProvider((l) =>new PerlinProvider(l), s, 3, 0.5, 2D),
                NoisePreset.LavaEdge => of(NoisePreset.Lava, s).scale(0.1).exponent(75)
                    .edgeDetectFast(0.000002),
                NoisePreset.Spatter =>  new FractalFBMProvider((l) =>new SimplexProvider(l), s, 4, 0.6, 2.75D),
                NoisePreset.Therma => new SimplexProvider(s).octave(3, 0.5)
                    .warp(of(NoisePreset.Lava, s + 1), 0.75, 3).scale(4),
                NoisePreset.ThermaEdge => of(NoisePreset.Therma, s).scale(0.1).exponent(10)
                    .edgeDetectFast(0.000002),
                _ => throw new ArgumentOutOfRangeException(nameof(preset), preset, null)
            };
        }
    }

    public enum NoisePreset
    {
        Simplex,
        Cellular,
        CellularEdge,
        CellularEdgeThin,
        CellularEdgeThick,
        CellularEdgeHairthin,
        CellularHeight,
        Flat,
        Perlin,
        White,
        Value,
        ValueHermite,
        Bendplex,
        Droopy,
        Wrinkleplex,
        Natural,
        Wetland,
        Lava,
        LavaEdge,
        Spatter,
        Therma,
        ThermaEdge,
    }

    public interface IWeighted
    {
            double getWeight();
    }
}
