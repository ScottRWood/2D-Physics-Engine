using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NEA_Physics_Engine.Interfaces;
using NEA_Physics_Engine.Physics;
using NEA_Physics_Engine.Rendering;
using NEA_Physics_Engine.Physics.Properties;

namespace NEA_Physics_Engine
{
	public sealed class GameObject : Interfaces.Updateable
	{   
        //Attributes
        //my_Body (PhysicsBody): the corresponding physics body to this game object 
        //my_Texture (Texture2D): the texture of the object
        //my_Colour (Color): the colour of the object
        
        //Methods
        //Position: get and set position
        //Body: get my_Body
        //Update: run on every update to redraw the object
        
        //Create variables for the object being dealt with, the texture of the object and the colour of the object.
		private PhysicsBody my_Body;
		private Texture2D my_Texture;
		private Color my_Colour;
        
        //Get/Set the position of the object
		public Vector2 Position
		{
			get 
			{
				return my_Body.Position;
			}
			private set
			{
				my_Body.Position = value;
			}
		}
        
        //Get my_Body
		public PhysicsBody Body {get {return my_Body;}}
        
        //Initialisation
		public GameObject(Vector2 parameter_Position, Texture2D parameter_Texture, PhysicsShape parameter_ShapeDefine)
		{
            //get texture and object
			my_Texture = parameter_Texture;
			my_Body = new PhysicsBody(parameter_Position, parameter_ShapeDefine);
            
            //Switch colour based on material (case not used because they require jump statements)
            if (parameter_ShapeDefine.Material == 1)
                my_Colour = Color.White;
            if (parameter_ShapeDefine.Material == 2)
                my_Colour = Color.Cyan;
            if (parameter_ShapeDefine.Material == 3)
                my_Colour = Color.Brown;
            if (parameter_ShapeDefine.Material == 4)
                my_Colour = Color.Gray;
            if (parameter_ShapeDefine.Material == 5)
                my_Colour = Color.Orange;
		}

        
		public void Update()
		{
            //Have the render manager draw the object, with the given properties.
			if (Body.Shape == Shape.CIRCLE)
				RenderManager.Instance.DrawCircle(Position, Body.CircleDefine.Radius, my_Colour);
			else
			{
                //Polygons drawn as a set of lines
				for (int i = 0; i < Body.PolygonDefine.VerticesCount; i++)
					RenderManager.Instance.DrawLine(Position + Body.PolygonDefine.GetVertex(i), Position + Body.PolygonDefine.GetVertex((i + 1 == Body.PolygonDefine.VerticesCount ? 0 : i + 1)), 1, my_Colour);
			}
		}
	}
}