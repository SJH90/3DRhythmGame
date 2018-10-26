// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using MoonSharp.Interpreter;

// public class ScriptTest : MonoBehaviour {

// 	// Use this for initialization
// 	void Start () {
//         Debug.Log("moon : " + CallbackTest());
// 	}
	
// 	// Update is called once per frame
// 	void Update () {
		
// 	}

//     private static double CallbackTest()
//     {
//         string scriptCode = @"    
//         -- defines a factorial function
//         function fact (n)
//             if (n == 0) then
//                 return 1
//             else
//                 return n * fact(n - 1);
//             end
//         end";

//         Script script = new Script();

//         script.DoString(scriptCode);

//         DynValue res = script.Call(script.Globals["fact"], 4);

//         return res.Number;
//     }
// }
