using System;
using Microsoft.Xna.Framework;

using NEA_Physics_Engine.Physics.Properties;
using NEA_Physics_Engine.Utilities;
using NEA_Physics_Engine.Rendering;
using System.Diagnostics;

namespace NEA_Physics_Engine.Physics
{
	public sealed class PhysicsPolygonDefine : PhysicsShape
	{
        //Attributes
        //my_Vertices: an array of vertices
        
        //Methods
        //PhysicsPolygonDefine: used to define properties of the polygon
        //Rotate: used to rotate the object
        //VerticesCount: get/set the vertex count of the polygon
        //Vertices: get an array of vertices
        //GetVertex: returns a given vertex
        //CalculateAABB: used to set data about the axis-aligned bounding box
        //CalculateMass: used to calculate the mass of the polygon
        //ComputeInertia: used to calculate the inertia of the polygon
        //CalculateCentreOfMass: used to calculate the centre of mass of the polygon
        //CalculateVolume: used to calculate the volume of the polygon via a redirect to the mathsUtility
        
        //Get the properties of the polygon
		public PhysicsPolygonDefine(PhysicsPolygonDefine other)
		{
			Shape = other.Shape;
			Mass = other.Mass;
			VerticesCount = other.VerticesCount;
			my_Vertices = other.my_Vertices;
            Restitution = other.Restitution;
            StaticFriction = other.StaticFriction;
            DynamicFriction = other.DynamicFriction;
            Inertia = other.Inertia;
            Gravity = other.Gravity;
		}

		public PhysicsPolygonDefine(Vector2[] parameter_Vertices, bool isStatic = false, int parameter_Material = Properties.Material.SOLID, bool gravityAffects = true)
		{   
            if (parameter_Vertices.Length < 3)
            {
                RenderManager.Instance.DrawString("Polygon must have at least 3 vertices");
            }

            //Set properties of the polygon that aren't given
			Shape = 2;
			Material = parameter_Material;
			
			VerticesCount = parameter_Vertices.GetLength(0);
			my_Vertices = parameter_Vertices;
            Gravity = gravityAffects;
            
            //Restitution and Friction affected by material, case not used as require jump statements
            if (Material == 1)
            {
                Restitution = Properties.Restitution.SOLID;
                StaticFriction = Properties.StaticFriction.SOLID;
                DynamicFriction = Properties.DynamicFriction.SOLID;
                Density = Properties.Density.SOLID;
            }
            if (Material == 2)
            {
                Restitution = Properties.Restitution.ICE;
                StaticFriction = Properties.StaticFriction.ICE;
                DynamicFriction = Properties.DynamicFriction.ICE;
                Density = Properties.Density.ICE;
            }
            if (Material == 3)
            {
                Restitution = Properties.Restitution.WOOD;
                StaticFriction = Properties.StaticFriction.WOOD;
                DynamicFriction = Properties.DynamicFriction.WOOD;
                Density = Properties.Density.WOOD;
            }
            if (Material == 4)
            {
                Restitution = Properties.Restitution.METAL;
                StaticFriction = Properties.StaticFriction.METAL;
                DynamicFriction = Properties.DynamicFriction.METAL;
                Density = Properties.Density.METAL;
            }
            if (Material == 5)
            {
                Restitution = Properties.Restitution.RUBBER;
                StaticFriction = Properties.StaticFriction.RUBBER;
                DynamicFriction = Properties.DynamicFriction.RUBBER;
                Density = Properties.Density.RUBBER;
            }


            //Calculate the volume, the bounding box and the center of mass
			CalculateVolume();
			CalculateAABB();
			CalculateCenterOfMass();

            //Calculate mass using density and volume, unless object is static (in which case there is no mass, so no effect by gravity)
			if (isStatic == false)
			{
				CalculateMass();
			}
			else
			{
				Mass = 0;
			}
		}

		private Vector2[] my_Vertices;
		public int VerticesCount {get; private set;}
		public Vector2[] Vertices {get {return my_Vertices;}}
		public Vector2 GetVertex(int parameter_Index) {return my_Vertices[parameter_Index];}
        
        //Rotate the shape
        public override void Rotate(float angle, Vector2 origin = new Vector2())
        {
            //Angles in radians given from 0 to 2 * PI, correct angles to within the range
            Angle += angle;
            if (Angle > 2 * (float)Math.PI)
                Angle -= 2 * (float)Math.PI;
            else if (Angle < 0)
                Angle += 2 * (float)Math.PI;

            float tempX;
            for (int i = 0; i < VerticesCount; i++)
            {
                tempX = my_Vertices[i].X;
                my_Vertices[i].X = (float)(Math.Cos(-angle) * tempX - Math.Sin(-angle) * my_Vertices[i].Y);
                my_Vertices[i].Y = (float)(Math.Sin(-angle) * tempX + Math.Cos(-angle) * my_Vertices[i].Y);
            }
        }

		protected override void CalculateAABB()
		{
            //Initialise the minimum and maximum points to 0
			float minimumX = 0;
			float minimumY = 0;
			float maximumX = 0;
			float maximumY = 0;

            //For every vertex of the polygon, check if the points X or Y co-ordinate is a new minimum or maximum then set new min/max
			for (int i = 0; i < VerticesCount; i++)
			{
				if (my_Vertices[i].X < minimumX)
					minimumX = my_Vertices[i].X;

				if (my_Vertices[i].Y < minimumY)
					minimumY = my_Vertices[i].Y;

				if (my_Vertices[i].X > maximumX)
					maximumX = my_Vertices[i].X;

				if (my_Vertices[i].Y > maximumY)
					maximumY = my_Vertices[i].Y;
			}

            //Set the bounding box
			SetAABB(minimumX + maximumX, minimumY + maximumY);
		}

		protected override void CalculateMass()
		{
            //Calculate the mass using density * volume (Since volume is area, we multiply by a set depth)
            Mass = Volume * Density * 0.0000002f;

            //If mass exceeds the limits then set them to the limit they exceed
			if (Mass > Limits.MAXIMUM_MASS)
				Mass = Limits.MAXIMUM_MASS;
			else if (Mass < Limits.MINIMUM_MASS)
				Mass = Limits.MINIMUM_MASS;
		}

		protected override void CalculateCenterOfMass()
		{
            //Use the polygon center method in the mathsUtility to find the center
			Vector2 center = mathsUtility.PolygonCenter(Vertices, Volume);
            
            //Subtract center from the vertices
			for (int i = 0; i < VerticesCount; i++)
				my_Vertices[i] -= center;
		}

        protected override void ComputeInertia()
        {
            Inertia = mathsUtility.PolygonInertia(Vertices, Mass);
            if (Inertia > Limits.MAXIMUM_INERTIA)
                Inertia = Limits.MAXIMUM_INERTIA;
            else if (Inertia < Limits.MINIMUM_INERTIA)
                Inertia = Limits.MINIMUM_INERTIA;
        }

        //Calculate volume in the mathsUtility file
		protected override void CalculateVolume()
		{
			Volume = mathsUtility.PolygonVolume(Vertices);
		}
	}
}