using System;
using Microsoft.Xna.Framework;

using NEA_Physics_Engine.Physics.Properties;
using NEA_Physics_Engine.Utilities;

namespace NEA_Physics_Engine.Physics
{
    
	public sealed class PhysicsCircleDefine : PhysicsShape
	{
        //Methods
        //PhysicsCircleDefine: used to define properties of the circle
        //Rotate: used to rotate the object
        //Radius: get/set the radius of the circle
        //CalculateAABB: used to set data about the axis-aligned bounding box
        //CalculateMass: used to calculate the mass of the circle
        //ComputeInertia: used to calculate the inertia of the circle
        //CalculateCentreOfMass: used to calculate the centre of mass of the circle
        //CalculateVolume: used to calculate the volume of the circle via a redirect to the mathsUtility
        
        //Get the properties of the circle
		public PhysicsCircleDefine(PhysicsCircleDefine other)
		{
			Shape = other.Shape;
            Mass = other.Mass;
			Radius = other.Radius;
            Restitution = other.Restitution;
            StaticFriction = other.StaticFriction;
            DynamicFriction = other.DynamicFriction;
            Inertia = other.Inertia;
            Gravity = other.Gravity;
		}

		public PhysicsCircleDefine(float parameter_Radius, bool isStatic = false, int parameter_Material = Properties.Material.SOLID, bool gravityAffects = true)
		{
            //Set properties of the circle that aren't given
			Material = parameter_Material;
			
			Radius = parameter_Radius;
            
            Gravity = gravityAffects;
            
			Shape = 1;
            
            //Restitution and Friction changes based on material, case not used because requires a jump statement
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

            //Calculate the volume and the bounding box
			CalculateVolume();
			CalculateAABB();
            ComputeInertia();

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
        
        //Rotate the shape
        public override void Rotate(float angle, Vector2 originTranslation = new Vector2())
        {
            Angle += angle;
            //Angles in radians given from 0 to 2 * PI, correct angles to within the range
            if (Angle > 2 * (float)Math.PI)
                Angle -= 2 * (float)Math.PI;
            else if (Angle < 0)
                Angle += 2 * (float)Math.PI;
        }

		public float Radius {get; private set;}

        //Call to the SetAABB method
		protected override void CalculateAABB()
		{
			SetAABB(Radius, Radius);
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

        protected override void ComputeInertia()
        {
            //Circle inertia = 1/2 * (mass * radius^2)
            Inertia = 0.5f * Mass * (Radius * Radius);
        }

        protected override void CalculateCenterOfMass()
        {
        }

        //Calculate volume in the mathsUtility file
        protected override void CalculateVolume()
        {
            Volume = mathsUtility.CircleVolume(Radius);
        }
	}
}