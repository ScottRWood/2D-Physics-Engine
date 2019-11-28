using System;

namespace NEA_Physics_Engine
{
	#if WINDOWS
		static class Program
		{
			///Program start
			static void Main(string[] args)
			{
				using (ProgramManager engine = NEA_Physics_Engine.ProgramManager.Instance)
				{
					engine.Run();
				}
			}
		}
	#endif 
}