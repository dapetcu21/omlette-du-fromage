
using UnityEngine;



namespace Database
{
	public class Manager
	{
		static private bool _isInitialized = false;


		static public
		void Initialize()
		{
			if ( _isInitialized )
			{
				return;
			}

			_isInitialized = true;


		}
	}
}
