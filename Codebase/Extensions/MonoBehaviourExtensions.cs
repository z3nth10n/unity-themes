﻿using UnityEngine;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Zios{
    public static class MonoBehaviourExtension{
	    public static string GetGUID(this MonoBehaviour current){
		    #if UNITY_EDITOR
		    if(Application.isEditor){
			    MonoScript scriptFile = MonoScript.FromMonoBehaviour(current);
			    string path = AssetDatabase.GetAssetPath(scriptFile);
			    return AssetDatabase.AssetPathToGUID(path);
		    }
		    #endif
		    return "";
	    }
    }
}