using Microsoft.Xna.Framework;

namespace NEA_Physics_Engine.Physics
{
	public abstract class PhysicsShape
	{
        //Attributes
        //my_AABB (AABB): contains data about the axis-aligned bounding box of the object
        
        //Methods
        //Shape: get/set the shape of the object
        //Material: get/set the material of the object
        //Density: get/set the density of the object
        //Angle: get/set the angle of the object
        //Volume: get/set the volume of the object
        //Restitution: get/set the restitution coefficient of the object
        //StaticFriction: get/set the static coefficient of friction of the object
        //DynamicFriction: get/set the dynamic coefficient of friction of the object
        //Mass: get/set the mass of the object
        //Inertia: get/set the inertia of the object
        //Gravity: get/set whether the object is affected by gravity
        //SetAABB: initialise data about the axis-aligned bounding box
        //CalculateAABB: an abstract method which shares a name with methods in PhysicsShapeCircle and PhysicsShapePolygon
        //CalculateVolume: an abstract method which shares a name with methods in PhysicsShapeCircle and PhysicsShapePolygon
        //CalculateMass: an abstract method which shares a name with methods in PhysicsShapeCircle and PhysicsShapePolygon
        //CalculateCenterOfMass: an abstract method which shares a name with methods in PhysicsShapeCircle and PhysicsShapePolygon
        //ComputeInertia: an abstract method which shares a name with methods in PhysicsShapeCircle and PhysicsShapePolygon
        
        //Layout the structure of the axis aligned bounding box
		public struct AABB
		{
            //Using the center and half the height and half the width, we can determine all points we need for the bounding box
			public Vector2 Center {get; set;}
			public float HalfWidth {get; set;}
			public float HalfHeight {get; set;}
		}

        //Get shape properties
		private AABB my_AABB;
		public int Shape {get; protected set;}
		public int Material {get; protected set;}
		public int Density {get; set;}
        public float Angle { get; protected set; }
		public float Volume {get; protected set;}
        public float Restitution { get; set; }
        public float StaticFriction { get; set; }
        public float DynamicFriction { get; set;}
		public float Mass {get; set;}
        public float Inertia { get; set; }
        public bool Gravity { get; set; }

        //Set up the axis aligned bounding box
		protected void SetAABB(float parameter_HalfWidth, float parameter_HalfHeight)
		{
			my_AABB.Center = new Vector2();
			my_AABB.HalfWidth = parameter_HalfWidth;
			my_AABB.HalfHeight = parameter_HalfHeight;
		}

        //Calculate needed properties of the shape
		public AABB GetAABB() {return my_AABB;}
        public abstract void Rotate(float angle, Vector2 originTranslation = new Vector2());
		protected abstract void CalculateAABB();
		protected abstract void CalculateVolume();
		protected abstract void CalculateMass();
		protected abstract void CalculateCenterOfMass();
        protected abstract void ComputeInertia();
	}
}