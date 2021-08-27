using UnityEngine;
using System.Collections;

namespace Core
{
	/*Documentations and examples say we need to derive from
 MonoBehaviour to use coroutines*/
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T instance;
//		private static bool isApplicationQuitting = false;
	
		/*Hide the constructor*/
		/*Document says no way to implement generic constructor.
	 Hence the derived classes should do it.*/
		//protected T () { }
	
		public static T Instance
		{
			get
			{
				/*If this is called from a background Task when
			 the application is quitting then return null*/
//				if(isApplicationQuitting)
//				{
//					return instance;
//				}
			
				if(null == instance)
				{
					/*We have to decide on the Object name as the object
				 should not get destroyed till the end of the game.
				 Letting the user choose the object name could let
				 the user use the existing object which might be
				 accidentally destroyed making the component
				 loose Singleton status. */
					GameObject core = GameObject.Find("Core");
					if(null == core)
					{
						core = new GameObject();
						core.name = "Core";//Set a name. This appears in Hierarchy in run mode.
						DontDestroyOnLoad(core);//This will keep this object alive across scenes
					}
					instance = core.AddComponent<T>();
				}
				return instance;
			}
		}

//		public void OnDestroy()
//		{
//			isApplicationQuitting = true;
//		}
	}
}