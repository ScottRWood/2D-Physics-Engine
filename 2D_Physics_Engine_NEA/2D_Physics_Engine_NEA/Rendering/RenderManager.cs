using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NEA_Physics_Engine.Rendering.Render;

namespace NEA_Physics_Engine.Rendering
{
    //The different layers on the screen (Could potentially be expanded)
	public enum LAYER
	{
		BACK = 0,
		MID,
		FRONT,
		NR_OF_LAYERS
	}

	public sealed class RenderManager
	{
        //Attributes
        //my_SpriteBatch (SpriteBatch):instance of SpriteBatch class used to draw objects
        //my_Strings (Queue of string, Vector2 pairs): qeueue of strings to be drawn
        //my_RenderObjects (Queue of RenderObject): objects to be drawn
        //my_SpriteFont (SpriteFont): the font for the strings
        //my_PixelWhite (Texture2D): texture for lines
        //my_CircleWhite (Texture2D): texture for circles
        
        //Methods
        //DrawString: enqueues new string in my_Strings
        //DrawLine: enqueues new line in my_RenderObjects
        //DrawCircle: enqueues new circle in my_RenderObjects
        //Update: run on update and draws the changes that have occured during the update
        //DrawLayer: draws a layer of the scene
        
		private SpriteBatch my_SpriteBatch;

        //Set up the queues for rendering objects
		private Queue<KeyValuePair<string, Vector2>> my_Strings;
		private Queue<RenderObject>[] my_RenderObjects;

        //Textures
		private SpriteFont my_SpriteFont;
		private Texture2D my_PixelWhite;
		private Texture2D my_CircleWhite;

		public void DrawString(string parameter_String, Vector2 parameter_Position = new Vector2())
		{
            //Draw a the given string at the given position
			my_Strings.Enqueue(new KeyValuePair<string, Vector2>(parameter_String, parameter_Position));
		}

		public void DrawLine(Vector2 parameter_Start, Vector2 parameter_End, int parameter_Width = 1, Color parameter_Colour = new Color(), LAYER parameter_Layer = LAYER.FRONT)
		{
            //Draw the given line at the given position on the front layer, also in given colour
			my_RenderObjects[(int)parameter_Layer].Enqueue(new RenderLine(parameter_Start, parameter_End, parameter_Width, OBJECT_TYPE.LINE, parameter_Colour));
		}

		public void DrawCircle(Vector2 parameter_Position, float parameter_Radius, Color parameter_Colour = new Color(), LAYER parameter_Layer = LAYER.FRONT)
		{
            //Draw a circle of a given radius at a given position on the front layer, also in given colour
			my_RenderObjects[(int)parameter_Layer].Enqueue(new RenderCircle(parameter_Radius, parameter_Position, OBJECT_TYPE.CIRCLE, parameter_Colour));
		}

		public void Update()
		{
			my_SpriteBatch.GraphicsDevice.Clear(Color.Black);
			my_SpriteBatch.Begin();
			{
                //Draw each layer
				for (int i = 0; i < (int)LAYER.NR_OF_LAYERS; i++)
					DrawLayer(my_RenderObjects[i]);

				int j = 0;
                //Draw the strings currently in the string list
				foreach (KeyValuePair<string, Vector2> String in my_Strings)
				{
					j++;
					my_SpriteBatch.DrawString(my_SpriteFont, String.Key, (String.Value.X == 0 && String.Value.Y == 0) ? new Vector2(10, my_SpriteFont.LineSpacing * j) : String.Value, Color.White);
				}
			}
			my_SpriteBatch.End();

            //Once everything is drawn, clear the rendering queues
			for (int i = 0; i < (int)LAYER.NR_OF_LAYERS; i++)
				my_RenderObjects[i].Clear();
			my_Strings.Clear();
		}

		private void DrawLayer(Queue<RenderObject> parameter_RenderObjects)
		{
			foreach (RenderObject renderObject in parameter_RenderObjects)
			{
                //Draw each object
				
					if (renderObject.Type == OBJECT_TYPE.LINE)
					{
						RenderLine line = (RenderLine)renderObject;
						Vector2 lineVector = line.End - line.Start; //Get the direction vector of the line
						int lineLength = (int)lineVector.Length(); //Get the vector length
						lineVector.Normalize();
						float perpendicularDotProduct = lineVector.X * Vector2.UnitX.Y - lineVector.Y * Vector2.UnitX.X;
                        
                        //Draw a thin rectangle (i.e. a line)
						my_SpriteBatch.Draw(my_PixelWhite, new Rectangle((int)line.End.X, (int)line.End.Y, lineLength, line.Width), null, line.Colour, (float)Math.Atan2(perpendicularDotProduct, -Vector2.Dot(lineVector, Vector2.UnitX)), new Vector2(), SpriteEffects.None,0);
					}
					
					if (renderObject.Type == OBJECT_TYPE.CIRCLE)
					{
						RenderCircle circle = (RenderCircle)renderObject;

                        //Draw circle
						my_SpriteBatch.Draw(my_CircleWhite, new Rectangle((int)(circle.Position.X - circle.Radius), (int)(circle.Position.Y - circle.Radius), (int)(circle.Radius * 2), (int)(circle.Radius * 2)), circle.Colour);
					}
					
				
			}
		}

        //Set up the instance
		private static volatile RenderManager my_Instance;
		private static object my_SyncRoot = new Object();
		private RenderManager()
		{
			my_SpriteBatch = new SpriteBatch(NEA_Physics_Engine.ProgramManager.Instance.GraphicsDevice);
			my_Strings = new Queue<KeyValuePair<string, Vector2>>();
			my_RenderObjects = new Queue<RenderObject>[(int)LAYER.NR_OF_LAYERS];
			my_RenderObjects[(int)LAYER.FRONT] = new Queue<RenderObject>();
			my_RenderObjects[(int)LAYER.MID] = new Queue<RenderObject>();
			my_RenderObjects[(int)LAYER.BACK] = new Queue<RenderObject>();

			my_SpriteFont = ProgramManager.Instance.Content.Load<SpriteFont>("SpriteFont");
			my_PixelWhite = ProgramManager.Instance.Content.Load<Texture2D>("pixel_white");
			my_CircleWhite = ProgramManager.Instance.Content.Load<Texture2D>("circle_white");
		}

		public static RenderManager Instance
		{
			get
			{
				if (my_Instance == null)
					lock (my_SyncRoot)
				if (my_Instance == null)
				{
					my_Instance = new RenderManager();
				}

				return my_Instance;
			}
		}
	}
}