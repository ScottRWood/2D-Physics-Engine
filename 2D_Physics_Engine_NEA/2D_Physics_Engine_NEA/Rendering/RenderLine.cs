using Microsoft.Xna.Framework;

namespace NEA_Physics_Engine.Rendering.Render
{
	public class RenderLine : RenderObject
	{
        //Methods
        //Start: get/set the start point of the line
        //End: get/set the end point of the line
        //Width: get/set the width of the line
        
        //Get details for line
		public RenderLine(Vector2 parameter_Start, Vector2 parameter_End, int parameter_Width, OBJECT_TYPE parameter_Type, Color parameter_Color) : base(parameter_Type, parameter_Color)
		{
			Start = parameter_Start;
			End = parameter_End;
			Width = parameter_Width;
		}

		public Vector2 Start {get; private set;}
		public Vector2 End {get; private set;}
		public int Width {get; private set;}
	}
}