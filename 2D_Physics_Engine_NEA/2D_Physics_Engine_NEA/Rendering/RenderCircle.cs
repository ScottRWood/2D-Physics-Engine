using Microsoft.Xna.Framework;

namespace NEA_Physics_Engine.Rendering.Render
{
	public class RenderCircle : RenderObject
	{
        //Methods
        //Radius: get/set the radius of the circle
        //Position: get/set the position of the circle
        
        //Get details fore the circle
		public RenderCircle(float parameter_Radius, Vector2 parameter_Position, OBJECT_TYPE parameter_Type, Color parameter_Color) : base(parameter_Type, parameter_Color)
		{
			Radius = parameter_Radius;
			Position = parameter_Position;
		}
		public float Radius {get; private set;}
		public Vector2 Position {get; private set;}
	}
}