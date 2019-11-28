using System;
using System.Collections.Generic;

using NEA_Physics_Engine.Interfaces;
using NEA_Physics_Engine.Physics;

namespace NEA_Physics_Engine
{
	class ObjectsManager : Interfaces.Updateable
	{   
        //Attributes
        //my_Objects (List of GameObject): a list of the game objects that currently exist
        
        //Methods
        //Add: add a new object to the list
        //Clear: clear the list
        //Update: run on each update to update each object
        
        //Create list that contains all game objects
		private static List<GameObject> my_Objects;
        
        //Will add new object to the game objects list
		public void Add(GameObject new_PhysicsObject)
		{
			my_Objects.Add(new_PhysicsObject);
		}

        //Clears all objects from the screen and removes their details from the program
		public void Clear()
		{
			my_Objects.Clear();
			PhysicsManager.Instance.Clear();
		}

        //Update each object
		public void Update()
		{
			foreach (GameObject shape in my_Objects)
				shape.Update();
		}

		private static volatile ObjectsManager my_Instance;
		private static object my_SyncRoot = new Object();
		private ObjectsManager()
		{
			my_Objects = new List<GameObject>();
		}

        //Run instance of objects manager
		public static ObjectsManager Instance
		{
			get
			{
				if (my_Instance == null)
					lock (my_SyncRoot)

				if (my_Instance == null)
				{
					my_Instance = new ObjectsManager();
				}

			return my_Instance;
			}
		}
	}
}