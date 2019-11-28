using System;

using Microsoft.Xna.Framework;

namespace NEA_Physics_Engine.Rendering.Render
{
    //Types of object (could be expanded)
	public enum OBJECT_TYPE
	{
		CIRCLE,
		LINE,
		STRING
	}

	public class RenderObject
	{
        //Methods
        //Type: get/set the type of object
        //Colour: get/set the colour of the object
        
        //Get object type and colour
		public OBJECT_TYPE Type {get; protected set;}
		public Color Colour {get; protected set;}

		protected RenderObject(OBJECT_TYPE parameter_Type, Color parameter_Colour)
		{
			Type = parameter_Type;
			if (parameter_Colour.A != 0)
				Colour = parameter_Colour;
			else
				Colour = Color.White; //If no colour is given, draw in white
		}
	}
}