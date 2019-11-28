using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using NEA_Physics_Engine.Physics.Properties;
using NEA_Physics_Engine.Rendering;
using NEA_Physics_Engine.Interfaces;
using NEA_Physics_Engine.Input;
using NEA_Physics_Engine.Utilities;

namespace NEA_Physics_Engine.Physics
{
    //Keep store of types of force
	public enum FORCE_TYPE
	{
		IMPULSE,
		FORCE
	};

	public class PhysicsBody : Interfaces.Updateable
	{
        //Attributes
        //my_ObjectCount (integer): the number of objects
        //ShapeDefine (PhysicsShape): the instance of PhysicsShape linked to this physics body
        //ShapeCircleDefine (PhysicsCircleDefine): the instance of PhysicsCircleDefine linked to this physics body, if appropriate
        //ShapePolygonDefine (PhysicsPolygonDefine): the instance of PhysicsPolygonDefine linked to this physics body, if appropriate
        //my_ID (Integer): an ID number unique to this object
        //my_Force (Vector2): a vector containing the x and y components of the force applied
        //my_Acceleration (Vector2): a vector containing the x and y components of the acceleration of the object
        //my_LinearVelocity (Vector2): a vector containing the x and y components of the linear velocity
        //my_AngularVelocity (Float): a float containg the angular velocity of the object
        //my_Position (Vector2): a vector containing the x and y co-ordinates of the object's position
        //my_LastPosition (Vector2): a vector containing the x and y co-ordinates of the object's position at the previous update
        //my_InverseMass (Float): the value of 1/Mass
        //my_InverseInertia (Float): the value of 1/Inertia
        
        //Methods
        //PhysicsBody: defines properties about the object
        //Update: run on update to update position and other properties
        //GetID: returns the objects ID
        //isStatic: returns whether object is static
        //AddForce: adds a given force or impulse to the object
        //AddTorque: adds a given amount of torque to the object
        //Position: get/set my_Position
        //LastPosition: get/set my_LastPosition
        //Force: get/set my_Force
        //LinearVelocity: get/set my_LinearVelocity
        //AngularVelocity: get/set my_AngularVelocity
        //Acceleration: get/set my_Accekeration
        //Shape: get my_Shape
        //Material: get my_Material
        //Restitution: get/set restitution coefficient
        //StaticFriction: get/set static coefficient of friction
        //DynamicFriction: get/set dynamic coefficient of friction
        //Mass: get/set mass of object
        //Volume: get/set volume of object
        //InverseMass: get my_InverseMass
        //Inertia: get/set inertia of object
        //InverseInertia: get my_InverseInertia
        
        //Used to keep count of the number of objects
		private static int my_ObjectCount = 0;

        //Defined so they can be used to define new shapes
		public PhysicsShape ShapeDefine = null;
		public PhysicsCircleDefine CircleDefine = null;
		public PhysicsPolygonDefine PolygonDefine = null;
        
        //An ID for the object
		private int my_ID;

        //Set some empty vectors that can be used to store the force, acceleration, velocity, current and previous position 
		private Vector2 my_Force = new Vector2();
		private Vector2 my_Acceleration = new Vector2();
		private Vector2 my_LinearVelocity = new Vector2();
        private float my_AngularVelocity = 0;
		private Vector2 my_Position = new Vector2();
		private Vector2 my_LastPosition = new Vector2();

        //Inverse mass and inertia to be used in calculations
		private float my_InverseMass = -1;
        private float my_InverseInertia = -1;



		public PhysicsBody(Vector2 parameter_Position, PhysicsShape parameter_ShapeDefine)
		{
            //Get position and definition of the current shape
			ShapeDefine = parameter_ShapeDefine;
			Position = parameter_Position;  
            float Restitution = parameter_ShapeDefine.Restitution;
            float StaticFriction = parameter_ShapeDefine.StaticFriction;
            float DynamicFriction = parameter_ShapeDefine.DynamicFriction;

            //Get the shape's mass
			Mass = parameter_ShapeDefine.Mass;

            //Set the shape's ID
			my_ID = my_ObjectCount++;

            //Define the shape dependent on the shape property of the shape
            if (parameter_ShapeDefine.Shape == 1)
            {
                CircleDefine = (PhysicsCircleDefine)parameter_ShapeDefine;
            }
            else if (parameter_ShapeDefine.Shape == 2)
            {
                PolygonDefine = (PhysicsPolygonDefine)parameter_ShapeDefine;
            }

            //Add the object to the list
			PhysicsManager.Instance.AddBody(this);
		}

		public virtual void Update()
		{
            //Store position before update
			LastPosition = Position;

            //Get the change in time
			float deltaTime = ProgramManager.Instance.DeltaTime;

            //Update angle
            ShapeDefine.Rotate(0.2f * angularVelocity * deltaTime);

            //Calculate acceleration using re-arrangement of F=ma, a = F/m. (Use inverse mass in case the mass is 0)
			Acceleration = Force * InverseMass;

            //Calculate velocity using delta v = a * delta t 
			linearVelocity += Acceleration * deltaTime;

            //Damping
            linearVelocity *= 0.99f;
            angularVelocity *= 0.995f;

            //Calculate position using delta s = v * delta t 
			Position += (linearVelocity * deltaTime);
            
			if (isStatic() == false)
				RenderManager.Instance.DrawCircle(Position, 6);


            //Apply the force at the position
			AddForce(-Force, Position);
		}

        //Get the object's ID
        public virtual int GetID() { return my_ID; }

        //If shape is static then set mass to 0
		public bool isStatic() {return ShapeDefine.Mass == 0;}

		public void AddForce(Vector2 parameter_Force, Vector2 parameter_Position, FORCE_TYPE parameter_ForceType = FORCE_TYPE.FORCE)
		{
            //Two types of force, a force or an impulse. In general impulse is used but force is there in case certain scenarios are required
            if (parameter_ForceType == FORCE_TYPE.FORCE)
                Force += parameter_Force;
            else if (parameter_ForceType == FORCE_TYPE.IMPULSE)
                linearVelocity += parameter_Force * InverseMass;


			if (parameter_Position == Position)
				return;

                //Calculate torque 
                //Radius-vector from center of mass to position force being applied
                Vector2 radiusVector = parameter_Position - Position;
                float torque = mathsUtility.CrossProduct(radiusVector, parameter_Force);
                //Apply torque generated by the force acting on the body
                AddTorque(torque);
		}
        
        
        //Add the torque of the force
        public void AddTorque(float parameter_Torque)
        {
            angularVelocity += (parameter_Torque * InverseInertia);
        }

        //Get/Set position
		public Vector2 Position
		{
			get {return my_Position;}
			set {my_Position = value;}
		}

        //Get/Set previous position
		public Vector2 LastPosition
		{
			get {return my_LastPosition;}
			private set {my_LastPosition = value;}
		}

        //Get/Set forces applied
		public Vector2 Force
		{
			get {return my_Force;}
			private set {my_Force = value;}
		}

        //Get/Set linear velocity
		public Vector2 linearVelocity
		{
			get {return my_LinearVelocity;}
			private set {my_LinearVelocity = value;}
		}
        
        
        //Get/Set angular velocity
        public float angularVelocity
        {
            get { return my_AngularVelocity; }
            private set
            {
                my_AngularVelocity = value;
                //Limit to avoid insanity
                if (Math.Abs(my_AngularVelocity) > Limits.MAXIMUM_ANGULAR_VELOCITY)
                    if (my_AngularVelocity > 0)
                        my_AngularVelocity = Limits.MAXIMUM_ANGULAR_VELOCITY;
                    else
                        my_AngularVelocity = -Limits.MAXIMUM_ANGULAR_VELOCITY;
                else if (Math.Abs(my_AngularVelocity) < Limits.MINIMUM_ANGULAR_VELOCITY)
                    my_AngularVelocity = 0;
            }
        }

        //Get/Set acceleration
		public Vector2 Acceleration
		{
			get {return my_Acceleration;}
			private set {my_Acceleration = value;}
		}

        //Get/Set type of shape
		public int Shape 
		{
			get {return ShapeDefine.Shape;}
		}

        //Get/Set material 
		public int Material
		{
			get {return ShapeDefine.Material;}
		}

        //Get/Set restitution
        public float Restitution
        {
            get { return ShapeDefine.Restitution; }
            set { ShapeDefine.Restitution = value; }
        }
        
        //Get/Set static friction
        public float StaticFriction
        {
            get { return ShapeDefine.StaticFriction; }
            set { ShapeDefine.StaticFriction = value; }
        }
        
        //Get/Set dynamic friction
        public float DynamicFriction
        {
            get { return ShapeDefine.DynamicFriction; }
            set { ShapeDefine.DynamicFriction = value; }
        }

        //Get/Set mass and calculate the inverse mass to be used in calcs.
		public float Mass 
		{
			get {return ShapeDefine.Mass;}
			private set
			{
				ShapeDefine.Mass = value;
				if (ShapeDefine.Mass > 0)
					my_InverseMass = 1 / ShapeDefine.Mass;
				else
					my_InverseMass = 0;
			}
		}

        public float Volume
        {
            get { return ShapeDefine.Volume; }
            set { }
        }
        
        //Get inverse mass
		public float InverseMass
		{
			get {return my_InverseMass;}
		}

        
        //Get/Set Inertia
        public float Inertia
        {
            get { return ShapeDefine.Inertia; }
            private set
            {
                ShapeDefine.Inertia = value;
                if (ShapeDefine.Inertia > 0)
                    my_InverseInertia = 1 / ShapeDefine.Inertia;
                else
                    my_InverseInertia = 0;
            }
        }
        
        //Get inverse inertia
        public float InverseInertia
        {
            get { return my_InverseInertia; }
        }
	}
}