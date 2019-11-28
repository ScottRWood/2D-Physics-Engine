namespace NEA_Physics_Engine.Physics.Properties
{
    //The possible types of each property of an object

	public static class Shape
	{
        //Used as a look-up for shape type
		public const int UNSPECIFIED = 0;
		public const int CIRCLE = 1;
		public const int POLYGON = 2;
		public const int NR_SHAPES = 3;
	}

	public static class Material
	{
        //Used as a look-up for material
		public const int UNSPECIFIED = 0;
		public const int SOLID = 1;
        public const int ICE = 2;
        public const int WOOD = 3;
        public const int METAL = 4;
        public const int RUBBER = 5;
		public const int NR_MATERIALS = 6;
	}

	public static class Density
	{
        //Used as a look-up for density
		public const int UNSPECIFIED = 0;
		public const int AIR = 2;
		public const int SOLID = 500;
        public const int ICE = 100;
        public const int WOOD = 700;
        public const int METAL = 2000;
        public const int RUBBER = 200;
	}

    public static class Restitution
    {
        //Used as a look-up for restitution
        public const float UNSPECIFIED = 0f;
        public const float AIR = 0f;
        public const float SOLID = 0.7f;
        public const float ICE = 0.3f;
        public const float WOOD = 0.2f;
        public const float METAL = 0.1f;
        public const float RUBBER = 0.8f;
    }

    public static class StaticFriction
    {
        //Used as a look-up for static friction
        public const float UNSPECIFIED = 0f;
        public const float AIR = 0f;
        public const float SOLID = 0.03f;
        public const float ICE = 0.001f;
        public const float WOOD = 0.05f;
        public const float METAL = 0.03f;
        public const float RUBBER = 0.05f;
    }

    public static class DynamicFriction
    {
        //Used as a look-up for dynamic friction
        public const float UNSPECIFIED = 0f;
        public const float AIR = 0f;
        public const float SOLID = 0.01f;
        public const float ICE = 0.0005f;
        public const float WOOD = 0.03f;
        public const float METAL = 0.02f;
        public const float RUBBER = 0.04f;
    }

	public static class Limits
	{
        //Used as a look-up for limits on different properties
		public const float MAXIMUM_FORCE = 1000.0f;
		public const float MINIMUM_FORCE = 0.0f;
		public const float MAXIMUM_LINEAR_VELOCITY = 500.0f;
		public const float MINIMUM_LINEAR_VELOCITY = 0.0f;
        public const float MAXIMUM_ANGULAR_VELOCITY = 5f;
        public const float MINIMUM_ANGULAR_VELOCITY = 1f;
		public const float MAXIMUM_MASS = 100.0f;
		public const float MINIMUM_MASS = 0.001f;
        public const int MAXIMUM_INERTIA = 900000000;
        public const int MINIMUM_INERTIA = 0;
	}

	public static class Units
	{
        //Set up for scale
		public const int PIXELS_PER_METER = 32;
		public const float PIXELS_PER_METER_INVERSE = (float)1 / (float)PIXELS_PER_METER;
	}
}