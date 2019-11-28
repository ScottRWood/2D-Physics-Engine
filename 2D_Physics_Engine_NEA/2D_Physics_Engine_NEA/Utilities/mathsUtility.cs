using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace NEA_Physics_Engine.Utilities
{
    public class mathsUtility
    {
        //Methods
        //Angle: find the angle between 2 vectors
        //leftPerpendicular: find the perpendicular vector 90 degrees to left of given vector
        //rightPerpendicular: find the perpendicular 90 degrees to the right of given vector
        //LineLineIntersection: find whether two vectors intersect
        //CrossProduct: calculate the vector cross product of two vectors
        //PolygonCenter: calculate the central point of a polygon
        //PolygonInertia: calculate the inertia of a polygon
        //PolygonVolume: approximate the volume of a polygon
        //CircleVolume: calculate the volume of a circle
        
        //Find angle between vectors
        public static double Angle(Vector2 matrix1, Vector2 matrix2)
        {
            matrix1.Normalize();
            matrix2.Normalize();
            double angle = System.Math.Atan2(matrix2.Y - matrix1.Y, matrix2.X - matrix1.X);
            /// if angle is tiny then don't calculate
            if (angle < 0.0001)
                return 0;
            else
                return angle;
        }

        //Find the perpendicular vector
        public static Vector2 leftPerpendicular(Vector2 matrix)
        {
            return new Vector2(-matrix.Y, matrix.X);
        }

        public static Vector2 rightPerpendicular(Vector2 matrix)
        {
            return new Vector2(matrix.Y, -matrix.X);
        }
        
        //Find whether vectors intersect
        public static bool LineLineIntersection(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2, out Vector2 intersectionPoint)
        {
            //Set intersection point to none
            intersectionPoint = Vector2.Zero;
            float denominator = ((end1.X - start1.X) * (end2.Y - start2.Y)) - ((end1.Y - start1.Y) * (end2.X - start2.X));

            //  AB & CD are parallel 
            if (denominator == 0)
                return false;
            
            
            float numerator = ((start1.Y - start2.Y) * (end2.X - start2.X)) - ((start1.X - start2.X) * (end2.Y - start2.Y));

            float r = numerator / denominator;

            float numerator2 = ((start1.Y - start2.Y) * (end1.X - start1.X)) - ((start1.X - start2.X) * (end1.Y - start1.Y));

            float s = numerator2 / denominator;

            if ((r < 0 || r > 1) || (s < 0 || s > 1))
                return false;

            // Find intersection point
            intersectionPoint = new Vector2();
            intersectionPoint.X = start1.X + (r * (end1.X - start1.X));
            intersectionPoint.Y = start1.Y + (r * (end1.Y - start1.Y));

            return true;
        }
        
        //Calculate vector cross product of two vectors
        public static float CrossProduct(Vector2 matrix1, Vector2 matrix2)
        {
            return (matrix1.X * matrix2.Y) - (matrix1.Y * matrix2.X);
        }

        //Find the center of the polygon
        public static Vector2 PolygonCenter(Vector2[] parameter_Vertices, float parameter_Volume)
        {
            Vector2 center = new Vector2(); //Center vector

            //Find center using the centroid formula
            for (int i = 0; i < parameter_Vertices.Length; i++)
            {
                Vector2 vertex1 = parameter_Vertices[i];
                Vector2 vertex2 = parameter_Vertices[i + 1 < parameter_Vertices.Length ? i + 1 : 0];
                center.X += (vertex1.X + vertex2.X) * (vertex1.X * vertex2.Y - vertex2.X * vertex1.Y);
                center.Y += (vertex1.Y + vertex2.Y) * (vertex1.X * vertex2.Y - vertex2.X * vertex1.Y);
            }
            
            center.X *= 1 / (6 * parameter_Volume);
            center.Y *= 1 / (6 * parameter_Volume);
            return center;
        }

        public static float PolygonInertia(Vector2[] parameter_Vertices, float parameter_Mass)
        {
            //Polygon inertia, found by taking moments around each vertex
            float sum1 = 0.0f;
            float sum2 = 0.0f;
            for (int i = 0; i < parameter_Vertices.Length; i++)
            {
                Vector2 v1 = parameter_Vertices[i];
                Vector2 v2 = parameter_Vertices[(i + 1) % parameter_Vertices.Length];
                v2.Normalize();

                float a = Vector2.Dot(v2, v1);
                float b = Vector2.Dot(v1, v1) + Vector2.Dot(v1, v2) + Vector2.Dot(v2, v2);

                sum1 += a * b;
                sum2 += a;
            }
            return Math.Abs((parameter_Mass * sum1) / (6.0f * sum2));
        }

        public static float PolygonVolume(Vector2[] parameter_Vertices)
        {
            float area = 0;
            int j = parameter_Vertices.Length - 1;

            for (int i = 0; i < parameter_Vertices.Length; i++)
            {
                //Approximatee area by finding the area created between 2 points and adding the area (+ive * -ive gives a -ive area)
                area += (parameter_Vertices[j].X + parameter_Vertices[i].X) * (parameter_Vertices[j].Y - parameter_Vertices[i].Y);
                j = i;
            }

            //Return the modulus of the area of the area
            return Math.Abs(area * 0.5f);
        }

        public static float CircleVolume(float parameter_Radius)
        {
            float area;

            //3.142*Radius^2
            area = (float)Math.PI * (parameter_Radius * parameter_Radius);

            //Return the modulus of the area 
            return Math.Abs(area * 0.5f);
        }
    }
}