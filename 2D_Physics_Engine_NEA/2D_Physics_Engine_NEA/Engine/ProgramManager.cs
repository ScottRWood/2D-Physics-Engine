using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using NEA_Physics_Engine.Physics;
using NEA_Physics_Engine.Physics.Properties;
using NEA_Physics_Engine.Rendering;
using NEA_Physics_Engine.Utilities;
using NEA_Physics_Engine.Input;

namespace NEA_Physics_Engine
{
	///Main type for the engine
	public class ProgramManager : Microsoft.Xna.Framework.Game
	{
        //Attributes
        //SCREEN_WIDTH (integer): the screen width in pixels
        //SCREEN_HEIGHT (integer): the screen height in pixels
        //my_Shape (Texture2D): the texture of shapes
        //texture_Background (Texture2D): the texture of the background
        //my_DeltaTime (Float): a float used to keep track of time
        //lastUpdate (Double): a double used to track when the last update occured
        //lastDraw (Double): a double used to track when the last object was drawn
        //drawingCircle (Bool): a boolean used to track whether the circle drawing mode is engaged
        //drawingPolygon (Bool): a boolean used to track whether the polygon drawing mode is engaged
        //showControls (Bool): a boolean used to decide whether control help should be displayed or not
        //isStatic (Bool): a boolean used to decide whether the next object drawn should be affected by collisions and gravity
        //gravityAffects (Bool): a boolean used to decide whether the next object drawn is affected by gravity
        //MaterialOfNextObject (Integer): an integer which corresponds to what material the next object drawn will be made of
        //preset (Integer): an integer which corresponds to the preset on display
        //circleRadius (Float): a float which contains the radius of the circle being drawn
        //circlePosition (Vector2): a vector which contains the central position of the circle being drawn
        //polygonVertices (List of Vector2): a list of vectors which give the co-ordinates of each vertex of the polygon being drawn
        
        //Methods
        //Initialize: an override of the basic visual studio initialize, used so managers can be initialised
        //drawPreset1: draws the 1st preset
        //drawPreset2: draws the 2nd preset
        //drawPreset3: draws the 3rd preset
        //drawPreset4: draws the 4th preset
        //drawPreset5: draws the 5th preset
        //drawPreset6: draws the 6th preset
        //drawPreset7: draws the 7th preset
        //drawPreset8: draws the 8th preset
        //drawPreset9: draws the 9th preset
        //drawPreset10: draws the 10th preset
        //LoadContent: draws the sandbox mode and is run at the start
        //UnloadContent: unloads the content
        //Update: runs on update, controls the drawing tool and calls other managers to update
        //DrawResults: Draws results of the update
        //DeltaTime: Get my_DeltaTime
        //ScreenWidth: Get SCREEN_WIDTH
        //ScreenHeight: Get SCREEN_HEIGHT
        
        //Height and Width in pixels
		private const int SCREEN_WIDTH = 800;
		private const int SCREEN_HEIGHT = 600;
    
        
 
        //Textures for shapes and the background
        private Texture2D my_Shape;
		public Texture2D texture_Background;

        //Reference to other scripts in the base engine namespace
		private ObjectsManager my_ObjectsManager;
		private PhysicsManager my_PhysicsManager;
        private InputManager my_InputManager;
        private RenderManager my_RenderManager;

        

        //Initialise time 
		private float my_DeltaTime = 0;

        //Setup the graphics device manager which handles config. and management of the graphics device
		public GraphicsDeviceManager my_Graphics {get; private set;}

		///Allows engine to perform initialisations it requires before it runs.
		///Can get required services and load any related content.
		protected override void Initialize()
		{
			my_ObjectsManager = ObjectsManager.Instance;
            my_PhysicsManager = PhysicsManager.Instance;
            my_RenderManager = RenderManager.Instance;
			my_InputManager = InputManager.Instance;
			base.Initialize();
		}
        
        //-----------------------------------**Preset Drawing Functions**------------------------------------------------------------
        public void drawPreset1()
        {
            my_ObjectsManager.Clear();
            //Scene content, set out as a series of vertices 
            Vector2[] vertices = new Vector2[4];
            vertices[3] = new Vector2(-SCREEN_WIDTH * 0.5f, 50);
            vertices[2] = new Vector2(SCREEN_WIDTH * 0.5f, 50);
            vertices[1] = new Vector2(SCREEN_HEIGHT * 0.5f, -200);
            vertices[0] = new Vector2(-SCREEN_HEIGHT * 0.5f, -200);
            Vector2[] vertices2 = new Vector2[4];
            vertices2[3] = new Vector2(0, SCREEN_HEIGHT);
            vertices2[2] = new Vector2(50, SCREEN_HEIGHT);
            vertices2[1] = new Vector2(50, 0);
            vertices2[0] = new Vector2(0, 0);
            Vector2[] vertices3 = new Vector2[4];
            vertices3[3] = new Vector2(0, 50);
            vertices3[2] = new Vector2(SCREEN_WIDTH, 50);
            vertices3[1] = new Vector2(SCREEN_WIDTH, 0);
            vertices3[0] = new Vector2(0, 0);
            Vector2[] vertices4 = new Vector2[3];
            vertices4[2] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 50);
            vertices4[1] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 100);
            vertices4[0] = new Vector2(0, 100);

            //Adds scenes base polygons i.e. walls and floor
            my_ObjectsManager.Add(new GameObject(new Vector2(0, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT), my_Shape, new PhysicsPolygonDefine(vertices3, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 250, SCREEN_HEIGHT - 45), my_Shape, new PhysicsPolygonDefine(vertices4, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 350, SCREEN_HEIGHT - 100), my_Shape, new PhysicsCircleDefine(20, false)));
        }

        public void drawPreset2()
        {
            my_ObjectsManager.Clear();
            //Scene content, set out as a series of vertices 
            Vector2[] vertices = new Vector2[4];
            vertices[3] = new Vector2(-SCREEN_WIDTH * 0.5f, 50);
            vertices[2] = new Vector2(SCREEN_WIDTH * 0.5f, 50);
            vertices[1] = new Vector2(SCREEN_HEIGHT * 0.5f, -200);
            vertices[0] = new Vector2(-SCREEN_HEIGHT * 0.5f, -200);
            Vector2[] vertices2 = new Vector2[4];
            vertices2[3] = new Vector2(0, SCREEN_HEIGHT);
            vertices2[2] = new Vector2(50, SCREEN_HEIGHT);
            vertices2[1] = new Vector2(50, 0);
            vertices2[0] = new Vector2(0, 0);
            Vector2[] vertices3 = new Vector2[4];
            vertices3[3] = new Vector2(0, 50);
            vertices3[2] = new Vector2(SCREEN_WIDTH, 50);
            vertices3[1] = new Vector2(SCREEN_WIDTH, 0);
            vertices3[0] = new Vector2(0, 0);
            Vector2[] vertices4 = new Vector2[6];
            vertices4[5] = new Vector2(SCREEN_WIDTH * 0.5f, 50);
            vertices4[4] = new Vector2(20, 90);
            vertices4[3] = new Vector2(-20, 90);
            vertices4[2] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 50);
            vertices4[1] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 100);
            vertices4[0] = new Vector2(SCREEN_WIDTH * 0.5f, 100);

            //Adds scenes base polygons i.e. walls and floor
            my_ObjectsManager.Add(new GameObject(new Vector2(0, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT), my_Shape, new PhysicsPolygonDefine(vertices3, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 7, SCREEN_HEIGHT - 45), my_Shape, new PhysicsPolygonDefine(vertices4, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH) - 100, SCREEN_HEIGHT - 100), my_Shape, new PhysicsCircleDefine(20, false)));
        }

        public void drawPreset5()
        {
            my_ObjectsManager.Clear();
            //Scene content, set out as a series of vertices 
            Vector2[] vertices = new Vector2[4];
            vertices[3] = new Vector2(-SCREEN_WIDTH * 0.5f, 50);
            vertices[2] = new Vector2(SCREEN_WIDTH * 0.5f, 50);
            vertices[1] = new Vector2(SCREEN_HEIGHT * 0.5f, -200);
            vertices[0] = new Vector2(-SCREEN_HEIGHT * 0.5f, -200);
            Vector2[] vertices2 = new Vector2[4];
            vertices2[3] = new Vector2(0, SCREEN_HEIGHT);
            vertices2[2] = new Vector2(50, SCREEN_HEIGHT);
            vertices2[1] = new Vector2(50, 0);
            vertices2[0] = new Vector2(0, 0);
            Vector2[] vertices3 = new Vector2[4];
            vertices3[3] = new Vector2(0, 50);
            vertices3[2] = new Vector2(SCREEN_WIDTH, 50);
            vertices3[1] = new Vector2(SCREEN_WIDTH, 0);
            vertices3[0] = new Vector2(0, 0);
            Vector2[] vertices4 = new Vector2[3];
            vertices4[2] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 50);
            vertices4[1] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 100);
            vertices4[0] = new Vector2(0, 100);

            //Adds scenes base polygons i.e. walls and floor
            my_ObjectsManager.Add(new GameObject(new Vector2(0, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT), my_Shape, new PhysicsPolygonDefine(vertices3, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 250, SCREEN_HEIGHT - 45), my_Shape, new PhysicsPolygonDefine(vertices4, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 350, SCREEN_HEIGHT - 100), my_Shape, new PhysicsCircleDefine(20, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 150, SCREEN_HEIGHT - 70), my_Shape, new PhysicsCircleDefine(20, false)));
        }

        public void drawPreset6()
        {
            my_ObjectsManager.Clear();
            //Scene content, set out as a series of vertices 
            Vector2[] vertices = new Vector2[4];
            vertices[3] = new Vector2(-SCREEN_WIDTH * 0.5f, 50);
            vertices[2] = new Vector2(SCREEN_WIDTH * 0.5f, 50);
            vertices[1] = new Vector2(SCREEN_HEIGHT * 0.5f, -200);
            vertices[0] = new Vector2(-SCREEN_HEIGHT * 0.5f, -200);
            Vector2[] vertices2 = new Vector2[4];
            vertices2[3] = new Vector2(0, SCREEN_HEIGHT);
            vertices2[2] = new Vector2(50, SCREEN_HEIGHT);
            vertices2[1] = new Vector2(50, 0);
            vertices2[0] = new Vector2(0, 0);
            Vector2[] vertices3 = new Vector2[4];
            vertices3[3] = new Vector2(0, 50);
            vertices3[2] = new Vector2(SCREEN_WIDTH, 50);
            vertices3[1] = new Vector2(SCREEN_WIDTH, 0);
            vertices3[0] = new Vector2(0, 0);
            Vector2[] vertices4 = new Vector2[3];
            vertices4[2] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 50);
            vertices4[1] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 100);
            vertices4[0] = new Vector2(0, 100);

            //Adds scenes base polygons i.e. walls and floor
            my_ObjectsManager.Add(new GameObject(new Vector2(0, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT), my_Shape, new PhysicsPolygonDefine(vertices3, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT * 0.75f), my_Shape, new PhysicsPolygonDefine(vertices3, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 250, SCREEN_HEIGHT - 45), my_Shape, new PhysicsPolygonDefine(vertices4, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 250, (SCREEN_HEIGHT * 0.75f) - 45), my_Shape, new PhysicsPolygonDefine(vertices4, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 350, SCREEN_HEIGHT - 100), my_Shape, new PhysicsCircleDefine(20, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 150, SCREEN_HEIGHT - 70), my_Shape, new PhysicsCircleDefine(20, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 350, (SCREEN_HEIGHT * 0.75f) - 100), my_Shape, new PhysicsCircleDefine(20, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 150, (SCREEN_HEIGHT * 0.75f) - 70), my_Shape, new PhysicsCircleDefine(40, false)));
        }

        public void drawPreset7()
        {
            my_ObjectsManager.Clear();
            //Scene content, set out as a series of vertices 
            Vector2[] vertices = new Vector2[4];
            vertices[3] = new Vector2(-SCREEN_WIDTH * 0.5f, 50);
            vertices[2] = new Vector2(SCREEN_WIDTH * 0.5f, 50);
            vertices[1] = new Vector2(SCREEN_HEIGHT * 0.5f, -200);
            vertices[0] = new Vector2(-SCREEN_HEIGHT * 0.5f, -200);
            Vector2[] vertices2 = new Vector2[4];
            vertices2[3] = new Vector2(0, SCREEN_HEIGHT);
            vertices2[2] = new Vector2(50, SCREEN_HEIGHT);
            vertices2[1] = new Vector2(50, 0);
            vertices2[0] = new Vector2(0, 0);
            Vector2[] vertices3 = new Vector2[4];
            vertices3[3] = new Vector2(0, 50);
            vertices3[2] = new Vector2(SCREEN_WIDTH, 50);
            vertices3[1] = new Vector2(SCREEN_WIDTH, 0);
            vertices3[0] = new Vector2(0, 0);
            Vector2[] vertices4 = new Vector2[3];
            vertices4[2] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 50);
            vertices4[1] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 100);
            vertices4[0] = new Vector2(0, 100);
            Vector2[] vertices5 = new Vector2[3];
            vertices5[2] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 0);
            vertices5[1] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 100);
            vertices5[0] = new Vector2(0, 100);

            //Adds scenes base polygons i.e. walls and floor
            my_ObjectsManager.Add(new GameObject(new Vector2(0, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT), my_Shape, new PhysicsPolygonDefine(vertices3, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT * 0.75f), my_Shape, new PhysicsPolygonDefine(vertices3, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 250, SCREEN_HEIGHT - 45), my_Shape, new PhysicsPolygonDefine(vertices4, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 250, (SCREEN_HEIGHT * 0.75f) - 63), my_Shape, new PhysicsPolygonDefine(vertices5, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 350, SCREEN_HEIGHT - 100), my_Shape, new PhysicsCircleDefine(20, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 150, SCREEN_HEIGHT - 70), my_Shape, new PhysicsCircleDefine(20, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 350, (SCREEN_HEIGHT * 0.75f) - 150), my_Shape, new PhysicsCircleDefine(20, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 150, (SCREEN_HEIGHT * 0.75f) - 70), my_Shape, new PhysicsCircleDefine(20, false)));
        }

        protected void drawPreset8()
        {
            my_ObjectsManager.Clear();
            
            

            //Scene content, set out as a series of vertices 
            Vector2[] vertices = new Vector2[4];
            vertices[3] = new Vector2(-SCREEN_WIDTH * 0.5f, 50);
            vertices[2] = new Vector2(SCREEN_WIDTH * 0.5f, 50);
            vertices[1] = new Vector2(SCREEN_HEIGHT * 0.5f, -200);
            vertices[0] = new Vector2(-SCREEN_HEIGHT * 0.5f, -200);
            Vector2[] vertices2 = new Vector2[4];
            vertices2[3] = new Vector2(0, SCREEN_HEIGHT);
            vertices2[2] = new Vector2(50, SCREEN_HEIGHT);
            vertices2[1] = new Vector2(50, 0);
            vertices2[0] = new Vector2(0, 0);
            Vector2[] vertices3 = new Vector2[4];
            vertices3[3] = new Vector2(0, 50);
            vertices3[2] = new Vector2(SCREEN_WIDTH, 50);
            vertices3[1] = new Vector2(SCREEN_WIDTH, 0);
            vertices3[0] = new Vector2(0, 0);


            //Adds scenes base polygons i.e. walls and floor
            my_ObjectsManager.Add(new GameObject(new Vector2(0, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT), my_Shape, new PhysicsPolygonDefine(vertices3, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 350, (SCREEN_HEIGHT * 0.75f) - 150), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 150, (SCREEN_HEIGHT * 0.75f) - 70), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 180, (SCREEN_HEIGHT * 0.75f) - 272), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 342, (SCREEN_HEIGHT * 0.75f) - 73), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 325, (SCREEN_HEIGHT * 0.75f) - 124), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 157, (SCREEN_HEIGHT * 0.75f) - 62), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 200, (SCREEN_HEIGHT * 0.75f) - 74), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 220, (SCREEN_HEIGHT * 0.75f) - 85), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 210, (SCREEN_HEIGHT * 0.75f) - 25), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 320, (SCREEN_HEIGHT * 0.75f) - 74), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 132, (SCREEN_HEIGHT * 0.75f) - 70), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 143, (SCREEN_HEIGHT * 0.75f) - 272), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 250, (SCREEN_HEIGHT * 0.75f) - 57), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 320, (SCREEN_HEIGHT * 0.75f) - 145), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 165, (SCREEN_HEIGHT * 0.75f) - 58), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 210, (SCREEN_HEIGHT * 0.75f) - 71), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 223, (SCREEN_HEIGHT * 0.75f) - 88), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 243, (SCREEN_HEIGHT * 0.75f) - 35), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f), (SCREEN_HEIGHT * 0.75f)), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 100, (SCREEN_HEIGHT * 0.75f) - 300), my_Shape, new PhysicsCircleDefine(100, false)));
        }

        public void drawPreset9()
        {
            my_ObjectsManager.Clear();
            //Scene content, set out as a series of vertices 
            Vector2[] vertices = new Vector2[4];
            vertices[3] = new Vector2(-SCREEN_WIDTH * 0.5f, 50);
            vertices[2] = new Vector2(SCREEN_WIDTH * 0.5f, 50);
            vertices[1] = new Vector2(SCREEN_HEIGHT * 0.5f, -200);
            vertices[0] = new Vector2(-SCREEN_HEIGHT * 0.5f, -200);
            Vector2[] vertices2 = new Vector2[4];
            vertices2[3] = new Vector2(0, SCREEN_HEIGHT);
            vertices2[2] = new Vector2(50, SCREEN_HEIGHT);
            vertices2[1] = new Vector2(50, 0);
            vertices2[0] = new Vector2(0, 0);
            Vector2[] vertices3 = new Vector2[4];
            vertices3[3] = new Vector2(0, 50);
            vertices3[2] = new Vector2(SCREEN_WIDTH, 50);
            vertices3[1] = new Vector2(SCREEN_WIDTH, 0);
            vertices3[0] = new Vector2(0, 0);
            Vector2[] vertices4 = new Vector2[3];
            vertices4[2] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 50);
            vertices4[1] = new Vector2(-(SCREEN_WIDTH * 0.5f) + 50, 100);
            vertices4[0] = new Vector2(0, 100);

            //Adds scenes base polygons i.e. walls and floor
            my_ObjectsManager.Add(new GameObject(new Vector2(0, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT), my_Shape, new PhysicsPolygonDefine(vertices3, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT * 0.75f), my_Shape, new PhysicsPolygonDefine(vertices3, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 250, SCREEN_HEIGHT - 45), my_Shape, new PhysicsPolygonDefine(vertices4, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 250, (SCREEN_HEIGHT * 0.75f) - 45), my_Shape, new PhysicsPolygonDefine(vertices4, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 350, SCREEN_HEIGHT - 100), my_Shape, new PhysicsCircleDefine(20, false)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 350, (SCREEN_HEIGHT * 0.75f) - 100), my_Shape, new PhysicsCircleDefine(20, false, MaterialOfNextObject)));
        }

        protected void drawPreset10()
        {
            my_ObjectsManager.Clear();
            


            //Scene content, set out as a series of vertices 
            Vector2[] vertices = new Vector2[4];
            vertices[3] = new Vector2(-SCREEN_WIDTH * 0.5f, 50);
            vertices[2] = new Vector2(SCREEN_WIDTH * 0.5f, 50);
            vertices[1] = new Vector2(SCREEN_HEIGHT * 0.5f, -200);
            vertices[0] = new Vector2(-SCREEN_HEIGHT * 0.5f, -200);
            Vector2[] vertices2 = new Vector2[4];
            vertices2[3] = new Vector2(0, SCREEN_HEIGHT);
            vertices2[2] = new Vector2(50, SCREEN_HEIGHT);
            vertices2[1] = new Vector2(50, 0);
            vertices2[0] = new Vector2(0, 0);
            Vector2[] vertices3 = new Vector2[4];
            vertices3[3] = new Vector2(0, 50);
            vertices3[2] = new Vector2(SCREEN_WIDTH, 50);
            vertices3[1] = new Vector2(SCREEN_WIDTH, 0);
            vertices3[0] = new Vector2(0, 0);


            //Adds scenes base polygons i.e. walls and floor
            my_ObjectsManager.Add(new GameObject(new Vector2(0, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT), my_Shape, new PhysicsPolygonDefine(vertices3, true)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f), (SCREEN_HEIGHT * 0.75f) - 100), my_Shape, new PhysicsCircleDefine(20, false, Material.METAL)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 175, (SCREEN_HEIGHT * 0.75f) - 100), my_Shape, new PhysicsCircleDefine(20, false, Material.ICE)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) - 350, (SCREEN_HEIGHT * 0.75f) - 100), my_Shape, new PhysicsCircleDefine(20, false, Material.SOLID)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 175, (SCREEN_HEIGHT * 0.75f) - 100), my_Shape, new PhysicsCircleDefine(20, false, Material.WOOD)));
            my_ObjectsManager.Add(new GameObject(new Vector2((SCREEN_WIDTH * 0.5f) + 350, (SCREEN_HEIGHT * 0.75f) - 100), my_Shape, new PhysicsCircleDefine(20, false, Material.RUBBER)));
        }
        
        //---------------------------------------**End of Preset Drawing Functions**------------------------------------------------

		//LoadContent is called once and loads all the content
		protected override void LoadContent()
		{
            my_ObjectsManager.Clear();
            texture_Background = Content.Load<Texture2D>("background");
          

            //Scene content, set out as a series of vertices 
			Vector2[] vertices = new Vector2[4];
			vertices[3] = new Vector2(-SCREEN_WIDTH * 0.5f, 50);
			vertices[2] = new Vector2(SCREEN_WIDTH * 0.5f, 50);
			vertices[1] = new Vector2(SCREEN_HEIGHT * 0.5f, -200);
			vertices[0] = new Vector2(-SCREEN_HEIGHT * 0.5f, -200);
			Vector2[] vertices2 = new Vector2[4];
			vertices2[3] = new Vector2(0, SCREEN_HEIGHT);
			vertices2[2] = new Vector2(50, SCREEN_HEIGHT);
			vertices2[1] = new Vector2(50, 0);
			vertices2[0] = new Vector2(0, 0);
			Vector2[] vertices3 = new Vector2[4];
			vertices3[3] = new Vector2(0, 50);
			vertices3[2] = new Vector2(SCREEN_WIDTH, 50);
			vertices3[1] = new Vector2(SCREEN_WIDTH, 0);
			vertices3[0] = new Vector2(0, 0);
        

            //Adds scenes base polygons i.e. walls and floor
			my_ObjectsManager.Add(new GameObject(new Vector2(0, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
			my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT * 0.5f), my_Shape, new PhysicsPolygonDefine(vertices2, true)));
			my_ObjectsManager.Add(new GameObject(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT), my_Shape, new PhysicsPolygonDefine(vertices3, true)));
		}

		///UnloadContents will be called once and unloads all the content
		protected override void UnloadContent()
		{
			Content.Unload();
		}

		///Updates the scene and the physics
		private double lastUpdate = 0;
		private double lastDraw = 0;
		bool drawingCircle = false;
		bool drawingPolygon = false;
        bool showControls = true;
		bool isStatic = false;
        bool gravityAffects = true;
        int MaterialOfNextObject = 1;
        int preset = 0;
		float circleRadius;
		Vector2 circleStartPosition;
		List<Vector2> polygonVertices = new List<Vector2>();

		protected override void Update(GameTime runTime)
		{
            //Updates time
			my_DeltaTime = (float)runTime.ElapsedGameTime.TotalSeconds;

			///Allow engine to exit
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				this.Exit();

			
			double endtime = runTime.TotalGameTime.Milliseconds;
            
            //------------------------------------------**Checking for presets**-----------------------------------------------------
            
            //Haven't used switch statements (Case Statements) because they require jump statements between cases
            if (preset == 1)
            {
                my_RenderManager.DrawString("  This scenario shows a ball rolling down the ramp demonstrating");
                my_RenderManager.DrawString("  gravity and collision resolution.");
                my_RenderManager.DrawString("  If you watch the ball after it comes off the ramp, you'll");
                my_RenderManager.DrawString("  notice it stops just short of the wall.");
                my_RenderManager.DrawString("  This is due to a combination of damping and frictional force.");
            }

            if (preset == 2)
            {
                my_RenderManager.DrawString("  If you watch this scenario you'll see that the ball gains speed");
                my_RenderManager.DrawString("  as it rolls down the first side of the ramp but it does not");
                my_RenderManager.DrawString("  gain enough kinetic energy to roll all the way up the other");
                my_RenderManager.DrawString("  side and thus rolls back down.");
            }

            if (preset == 3)
            {
                my_RenderManager.DrawString("  This is the polygon drawing tutorial. The controls are as below:");
                my_RenderManager.DrawString("  -Press P to initiate polygon drawing mode");
                my_RenderManager.DrawString("  -Click to place a point (if cursor is red it means the point is");
                my_RenderManager.DrawString("   invalid.");
                my_RenderManager.DrawString("  -Press P again to create the polygon (with 3 or more points)");
                my_RenderManager.DrawString(" ");
                my_RenderManager.DrawString("  Try it now with the 3 markers given");
                my_RenderManager.DrawCircle(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT - 300), 10, Color.Yellow);
                my_RenderManager.DrawCircle(new Vector2((SCREEN_WIDTH * 0.5f) + 100, SCREEN_HEIGHT - 350), 10, Color.Yellow);
                my_RenderManager.DrawCircle(new Vector2((SCREEN_WIDTH * 0.5f) + 200, SCREEN_HEIGHT - 300), 10, Color.Yellow);
            }

            if (preset == 4)
            {
                my_RenderManager.DrawString("  This is the circle drawing tutorial. The controls are as below:");
                my_RenderManager.DrawString("  -Press O to initiate polygon drawing mode");
                my_RenderManager.DrawString("  -Click and Drag to set radius and position (Start of drag is centre)");
                my_RenderManager.DrawString("  -Press O again to create the circle");
                my_RenderManager.DrawString(" ");
                my_RenderManager.DrawString("  Try it now with the 2 markers given");
                my_RenderManager.DrawCircle(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT - 300), 10, Color.Yellow);
                my_RenderManager.DrawCircle(new Vector2(SCREEN_WIDTH * 0.5f, SCREEN_HEIGHT - 300), 40, Color.Yellow);
            }
            if (preset == 5)
            {
                my_RenderManager.DrawString("  In this scenario, the ball gathers speed as it rolls down the ramp.");
                my_RenderManager.DrawString("  It then collides with the other ball which is at rest");
                my_RenderManager.DrawString("  The collision causes the 1st ball to slow down and the second ball");
                my_RenderManager.DrawString("  to move. Thereby conserving the overall momentum and kinetic energy");
                my_RenderManager.DrawString("  of the system.");
            }
            if (preset == 6)
            {
                my_RenderManager.DrawString("  This scenario compares how far two different sized balls move when");
                my_RenderManager.DrawString("  an equal impulse is applied. The balls rolling down the ramps");
                my_RenderManager.DrawString("  end with the same amount of kinetic energy. The larger ball moves");
                my_RenderManager.DrawString("  with less speed in order to conserve momentum it also exerts a ");
                my_RenderManager.DrawString("  greater impulse on the ball that hits it.");
            }
            if (preset == 7)
            {
                my_RenderManager.DrawString("  This scenario shows how a ball of equal size but greater speed");
                my_RenderManager.DrawString("  exerts a greater impulse. The ball on top travels down a");
                my_RenderManager.DrawString("  steeper gradient and so gathers more speed. This causes the ball");
                my_RenderManager.DrawString("  it hits to move with greater speed and travel further.");
            }
            if (preset == 8)
            {
                my_RenderManager.DrawString("  This scenario shows off the zero gravity affect, all but one");
                my_RenderManager.DrawString("  of the balls is unaffected by gravity. It also shows off the");
                my_RenderManager.DrawString("  engine's ability to deal with a large amount of collisions.");
            }
            if (preset == 9)
            {
                my_RenderManager.DrawString("  This scenario shows the material you currently have selected");
                my_RenderManager.DrawString("  having it's friction compared to the standard solid material.");
                my_RenderManager.DrawString("  The material selected has a clear affect on the frictional");
                my_RenderManager.DrawString("  properties of the object.");
            }
            if (preset == 10)
            {
                my_RenderManager.DrawString("  This scenario compares the restitution of all 5 materials. It");
                my_RenderManager.DrawString("  can be seen that certain balls are bouncing higher than the ");
                my_RenderManager.DrawString("  others due to the material they are made from.");
            }
            //------------------------------------------**End of preset checks**-----------------------------------------------------
            
            //----------------------------------------**Miscellaneous Input checks**-------------------------------------------------

            //Clear objects when user inputs C
            if (my_InputManager.KeyWasPressed(Keys.C))
            {
                my_ObjectsManager.Clear();
            }

            //Decide whether new object will be static (i.e. it feels no force but has collision physics)
            if (my_InputManager.KeyWasPressed(Keys.Q))
            {
                if (isStatic)
                    isStatic = false;
                else
                    isStatic = true;
            }
            
            //Decide whether new object will be affected by gravity (automatically false when static is true)
            if (my_InputManager.KeyWasPressed(Keys.G))
            {
                if (gravityAffects)
                    gravityAffects = false;
                else
                    gravityAffects = true;
            }
            
            if (isStatic)
                gravityAffects = false;
            
            //------------------------**Load Preset Inputs**-------------------------------
            if (my_InputManager.KeyWasPressed(Keys.D0))
            {
                LoadContent();
                preset = 0;
            }

            if (my_InputManager.KeyWasPressed(Keys.D1))
            {
                showControls = false;
                drawPreset1();
                preset = 1;
            }

            if (my_InputManager.KeyWasPressed(Keys.D2))
            {
                showControls = false;
                drawPreset2();
                preset = 2;
            }

            if (my_InputManager.KeyWasPressed(Keys.D3))
            {
                showControls = false;
                LoadContent();
                preset = 3;
            }

            if (my_InputManager.KeyWasPressed(Keys.D4))
            {
                showControls = false;
                LoadContent();
                preset = 4;
            }

            if (my_InputManager.KeyWasPressed(Keys.D5))
            {
                showControls = false;
                drawPreset5();
                preset = 5;
            }

            if (my_InputManager.KeyWasPressed(Keys.D6))
            {
                showControls = false;
                drawPreset6();
                preset = 6;
            }

            if (my_InputManager.KeyWasPressed(Keys.D7))
            {
                showControls = false;
                drawPreset7();
                preset = 7;
            }

            if (my_InputManager.KeyWasPressed(Keys.D8))
            {
                showControls = false;
                drawPreset8();
                preset = 8;
            }

            if (my_InputManager.KeyWasPressed(Keys.D9))
            {
                showControls = false;
                drawPreset9();
                preset = 9;
            }
            if (my_InputManager.KeyWasPressed(Keys.F1))
            {
                showControls = false;
                drawPreset10();
                preset = 10;
            }
            //------------------------**End of Load Preset Inputs**-------------------------------
            
            //------------------------**Material switching inputs**-------------------------------
            if (my_InputManager.KeyWasPressed(Keys.NumPad1))
            {
                MaterialOfNextObject = 1;
            }

            if (my_InputManager.KeyWasPressed(Keys.NumPad2))
                MaterialOfNextObject = 2;

            if (my_InputManager.KeyWasPressed(Keys.NumPad3))
                MaterialOfNextObject = 3;

            if (my_InputManager.KeyWasPressed(Keys.NumPad4))
                MaterialOfNextObject = 4;

            if (my_InputManager.KeyWasPressed(Keys.NumPad5))
                MaterialOfNextObject = 5;
            //------------------------**End of Material switching inputs**-------------------------------
            //----------------------------------**End of miscallaneous input checks**------------------------------------------------

            if (preset != 0)
            {
                //Lock gravity manipulation during preset scenarios
                my_PhysicsManager.Gravity = new Vector2 (0f, 20.0f);
            }

            if ((preset != 0) && (preset != 3))
            {
                drawingPolygon = false;
            }

            if ((preset != 0) && (preset != 4))
            {
                drawingCircle = false;
            }

			if (drawingPolygon == true || drawingCircle == true)
			{   
                //tells user whether the object will be static, whether gravity is applied (if non-static) and what material it is made of 
                RenderManager.Instance.DrawString("   Gravity: " + gravityAffects.ToString());
				RenderManager.Instance.DrawString("   Static: " + isStatic.ToString());
				RenderManager.Instance.DrawString("   Material: " + MaterialOfNextObject.ToString());
			}

            //Toggle polygon drawing mode when P is entered
			if (my_InputManager.KeyWasPressed(Keys.P))
			{
				if (drawingPolygon == false)
					drawingPolygon = true;
				else if (polygonVertices.Count > 2)
				{
                    //Creates polygon out of points given by the user
					Vector2 startPosition = new Vector2();

                    //Calculate center point
					foreach (Vector2 vertex in polygonVertices)
						startPosition += vertex;
					startPosition.X /= polygonVertices.Count;
					startPosition.Y /= polygonVertices.Count;

                    //Add object to the list of objects
					my_ObjectsManager.Add(new GameObject(startPosition, my_Shape, new PhysicsPolygonDefine(polygonVertices.ToArray(), isStatic, MaterialOfNextObject, gravityAffects)));

                    //Clear the vertices list of points for this polygon
					polygonVertices.Clear();
					drawingPolygon = false;
				}
				else
				{
                    //If there aren't enough points to draw more than a line (i.e. if there are less than 3 points)...
                    //then clear the vertices list of points for this polygon
					drawingPolygon = false;
					polygonVertices.Clear();
				}
			}
            //Toggle circle drawing mode when O is entered
			else if (my_InputManager.KeyWasPressed(Keys.O))
			{
				if (drawingCircle == false)
					drawingCircle = true;
				else if (circleStartPosition != Vector2.Zero)
				{
                    //When circle's position is in a valid position i.e. not vector (0,0), add to the objects list
                    my_ObjectsManager.Add(new GameObject(circleStartPosition, my_Shape, new PhysicsCircleDefine(circleRadius, isStatic, MaterialOfNextObject, gravityAffects)));

                    //Reset the circle properties ready for the next circle
					circleStartPosition = Vector2.Zero;
					circleRadius = 0;
					drawingCircle = false;
				}
				else
					drawingCircle = false;
			}
            //Toggle control help when X is entered
            else if (my_InputManager.KeyWasPressed(Keys.X))
            {
                if (showControls == false)
                    showControls = true;
                else
                    showControls = false;
            }

			if (drawingPolygon)
			{
                //Tell user they are in polygon drawing mode
				RenderManager.Instance.DrawString("   Drawing polygon");
				foreach (Vector2 vertex in polygonVertices)
				{
                    //Create small circles to represent the vertices (will use to show collision detection)
					RenderManager.Instance.DrawCircle(vertex, 6);
				}
				if (polygonVertices.Count > 2)
				{
                    //Find perpendicular vectors for line between first 2 vertices, between current vertex and last vertex, between current vertex and first vertex
					Vector2 normal1 = mathsUtility.leftPerpendicular(polygonVertices[polygonVertices.Count - 1] - polygonVertices[polygonVertices.Count - 2]);
					Vector2 normal2 = mathsUtility.leftPerpendicular(polygonVertices[1] - polygonVertices[0]);
					Vector2 normal3 = mathsUtility.leftPerpendicular(polygonVertices[0] - polygonVertices[polygonVertices.Count - 1]);
					normal1.Normalize();
					normal2.Normalize();
                    //Create edges
					Vector2 newEdge1 = polygonVertices[polygonVertices.Count - 1] - my_InputManager.GetMousePosition();
					Vector2 newEdge2 = polygonVertices[0] - my_InputManager.GetMousePosition();

                    //Check normals and edges aren't going to intersect
					if (Vector2.Dot(newEdge1, normal1) < 0 && Vector2.Dot(newEdge2, normal2) < 0 && Vector2.Dot(newEdge2, normal3) > 0)
					{
                        //If new edge is valid draw a green point on the mouse position to indicate vertex can be placed
						RenderManager.Instance.DrawCircle(my_InputManager.GetMousePosition(), 6, Color.Green);
						if (my_InputManager.LeftMouseWasPressed())
                            //Add vertex to the list for the current polygon
							polygonVertices.Add(my_InputManager.GetMousePosition());
					}
					else
                        //If new edge is invalid draw a red point on the mouse position to indicate vertex can't be placed
						RenderManager.Instance.DrawCircle(my_InputManager.GetMousePosition(), 6, Color.Red);
				}
				else
				{
					if (polygonVertices.Count == 2)
					{
                        //Since only on the second vertex there are only two points and thus one edge with one normal
						Vector2 edge = my_InputManager.GetMousePosition() - polygonVertices[1];
						Vector2 normal = mathsUtility.leftPerpendicular(polygonVertices[1] - polygonVertices[0]);
						normal.Normalize();
						if (Vector2.Dot(edge, normal) > 0)
						{
                            //If vertex in valid position, draw green point to indicate vertex can be placed
							RenderManager.Instance.DrawCircle(my_InputManager.GetMousePosition(), 6, Color.Green);

                            //Add vertex on click
							if (my_InputManager.LeftMouseWasPressed())
								polygonVertices.Add(my_InputManager.GetMousePosition());
						}
						else
                            //If position is invalid, draw red point to indicate vertex can't be placed
							RenderManager.Instance.DrawCircle(my_InputManager.GetMousePosition(), 6, Color.Red);
					}
					else
					{
                        //If first point it can be placed anywhere so point is always green
						RenderManager.Instance.DrawCircle(my_InputManager.GetMousePosition(), 6, Color.Green);

                        //Add vertex on click
						if (my_InputManager.LeftMouseWasPressed())
							polygonVertices.Add(my_InputManager.GetMousePosition());
					}
				}
			}
			else if (drawingCircle)
			{
                //Notify user they are in circle drawing mode
				RenderManager.Instance.DrawString("   Drawing circle");

                //Set start position of circle on click
				if (my_InputManager.LeftMouseWasPressed())
					circleStartPosition = my_InputManager.GetMousePosition();

                //Set circle radius when clicked again
				if (my_InputManager.LeftMouseIsPressed())
					circleRadius = (circleStartPosition - my_InputManager.GetMousePosition()).Length();

				RenderManager.Instance.DrawCircle(circleStartPosition, circleRadius, Color.Green);
			}

            //Control help display
            if (showControls == true)
            {
                RenderManager.Instance.DrawString("   P : Toggle Polygon Drawing Mode||O : Toggle Circle Drawing Mode");
                RenderManager.Instance.DrawString("   X : Toggle Control Help||Q : Toggle Static");
                RenderManager.Instance.DrawString("   G : Toggle Whether Gravity Affects Next Object");
                RenderManager.Instance.DrawString("   M : Toggle Selected Object Information");
                RenderManager.Instance.DrawString("   NUMPAD 1 : Material 1 (Solid)||NUMPAD 2 : Material 2 (Ice)");
                RenderManager.Instance.DrawString("   NUMPAD 3 : Material 3 (Wood)||NUMPAD 4 : Material 4 (Metal)");
                RenderManager.Instance.DrawString("   NUMPAD 5 : Material 5 (Rubber)");
                RenderManager.Instance.DrawString("   W : Change Gravity Vector Up||S : Change Gravity Vector Down");
                RenderManager.Instance.DrawString("   A : Change Gravity Vector Left||D : Change Gravity Vector Right");
                RenderManager.Instance.DrawString("   0 : Sandbox||1 : Scenario 1||2 : Scenario 2||3 : Scenario 3");
                RenderManager.Instance.DrawString("   4 : Scenario 4||5 : Scenario 5||6 : Scenario 6");
                RenderManager.Instance.DrawString("   7 : Scenario 7||8 : Scenario 8||9 : Scenario 9");
                RenderManager.Instance.DrawString("   F1 : Scenario 10");

                if ((drawingPolygon == false) & (drawingCircle == false))
                {
                    RenderManager.Instance.DrawString("   Left Click : Select Object");
                }
                if (drawingPolygon == true)
                {
                    RenderManager.Instance.DrawString("   Left Click : Place Vertex");
                }
                if (drawingCircle == true)
                {
                    RenderManager.Instance.DrawString("   Left Click and Drag : Set Radius");
                }
            }

            //Update other managers
			my_PhysicsManager.Update();
			my_ObjectsManager.Update();
			my_InputManager.Update();
            my_RenderManager.Update();
            
			base.Update(runTime);
		}

		//This is called when the engine should draw the results of the update
		protected void DrawResults(GameTime runTime)
		{
			//Updates the renderer making it draw
			NEA_Physics_Engine.Rendering.RenderManager.Instance.Update();
			
			double endtime = runTime.TotalGameTime.Milliseconds - lastDraw;
			lastDraw = runTime.TotalGameTime.Milliseconds;

			base.Draw(runTime);
		}
        
        //Get my_DeltaTime
		public float DeltaTime {get {return my_DeltaTime;}}

        //Get window properties
		public int ScreenHeight {get {return SCREEN_HEIGHT;}}
		public int ScreenWidth {get {return SCREEN_WIDTH;}}

        //Set up instance of program manager
		private static volatile ProgramManager my_Instance;
		private static object my_SyncRoot = new Object();
		private ProgramManager()
		{
            //Setup window title, buffer and root directory of content 
			my_Graphics = new GraphicsDeviceManager(this);
			Window.Title = "2D Physics Engine";
			my_Graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
			my_Graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
			my_Graphics.ApplyChanges();
			Content.RootDirectory = "Content";

            //Make the mouse visible to aid in drawing
			IsMouseVisible = true;
			my_Graphics.SynchronizeWithVerticalRetrace = true;
			IsFixedTimeStep = false;
		}

        //Run instance of program manager
		public static ProgramManager Instance
		{
			get
			{
				if (my_Instance == null)
					lock (my_SyncRoot)
				if (my_Instance == null)
				{
					my_Instance = new ProgramManager();
				}
				return my_Instance;
			}
		}
	}
}
