using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using NEA_Physics_Engine.Interfaces;

namespace NEA_Physics_Engine.Input
{
	class InputManager : Interfaces.Updateable
	{
        //Attributes
        //my_PreviousKeyboardState (KeyboardState): stores previous state of the keyboard
        //my_PreviousMouseState (MouseState): stores previous state of the mouse
        
        //Methods
        //Update: runs on update and updates the keyboard and mouse states
        //GetMousePosition: returns the mouse's current position
        //LeftMouseWasPressed: returns whether left mouse button was pressed between last update and this update
        //LeftMouseIsPressed: returns whether left mouse button is currently pressed
        //LeftMouseIsReleased: returns whether left mouse button is currently released
        //KeyWasPressed: returns whether a particular key was pressed between last update and this update
        //KeyIsPressed: returns whether a particular key is currently pressed
        //KeysPressed: returns keys that are currently pressed 
        
        //Variables to get the state of the keyboard and mouse when last run
		KeyboardState my_PreviousKeyboardState;
		MouseState my_PreviousMouseState;

		public virtual void Update()
		{
            //On update set the state in the previous update to previous state
			my_PreviousKeyboardState = Keyboard.GetState();
			my_PreviousMouseState = Mouse.GetState();
		}

        //Get a position of the cursor
		public Vector2 GetMousePosition()
		{
			return new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
		}

        //Determine whether the left mouse button was pressed since the last update
		public bool LeftMouseWasPressed()
		{
			return Mouse.GetState().LeftButton == ButtonState.Pressed && my_PreviousMouseState.LeftButton != ButtonState.Pressed;
		}

        //Determine if the left mouse button is currently pressed
		public bool LeftMouseIsPressed()
		{
			return Mouse.GetState().LeftButton == ButtonState.Pressed;
		}
        
        //Determine if left mouse is released
        public bool LeftMouseIsReleased()
        {
            return Mouse.GetState().LeftButton != ButtonState.Pressed;
        }

        //Determine whether a key was pressed since the last update
        public bool KeyWasPressed(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key) && my_PreviousKeyboardState.IsKeyUp(key);
        }

        //Determine if a key is currently pressed
        public bool KeyIsPressed(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        //Determine which keys are currently pressed
        public Keys[] KeysPressed()
        {
            return Keyboard.GetState().GetPressedKeys();
        }

		private static volatile InputManager my_Instance;
		private static object my_SyncRoot = new Object();
		
        //Create an instance of the input manager
		public static InputManager Instance
		{
			get 
			{
				if (my_Instance == null)
					lock (my_SyncRoot)

				if (my_Instance == null)
				{
					my_Instance = new InputManager();
				}

				return my_Instance;
			}
			
		}
	}
}