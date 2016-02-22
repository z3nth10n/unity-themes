#pragma warning disable 0162
#pragma warning disable 0618
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;
namespace Zios{
	#if UNITY_EDITOR
	using Events;
	using UnityEditor;
	public class UtilityListener : AssetPostprocessor{
		public static void OnPostprocessAllAssets(string[] imported,string[] deleted,string[] movedTo, string[] movedFrom){
			bool playing = EditorApplication.isPlaying || EditorApplication.isPlayingOrWillChangePlaymode;
			if(!playing){Event.Call("On Asset Changed");}
		}
	}
	public class UtilityModificationListener : AssetModificationProcessor{
		public static string[] OnWillSaveAssets(string[] paths){
			foreach(string path in paths){Debug.Log("Saving Changes : " + path);}
			if(paths.Exists(x=>x.Contains(".unity"))){Event.Call("On Scene Saving");}
			Event.Call("On Asset Saving");
			return paths;
		}
		public static string OnWillCreateAssets(string path){
			Debug.Log("Creating : " + path);
			Event.Call("On Asset Creating");
			return path;
		}
		public static string[] OnWillDeleteAssets(string[] paths,RemoveAssetOptions option){
			foreach(string path in paths){Debug.Log("Deleting : " + path);}
			Event.Call("On Asset Deleting");
			return paths;
		}
		public static string OnWillMoveAssets(string path,string destination){
			Debug.Log("Moving : " + path + " to " + destination);
			Event.Call("On Asset Moving");
			return path;
		}
	}
	public static partial class Utility{
		private static EditorWindow[] inspectors;
		private static List<UnityObject> delayedDirty = new List<UnityObject>();
		private static Dictionary<UnityObject,SerializedObject> serializedObjects = new Dictionary<UnityObject,SerializedObject>();
		public static SerializedObject GetSerializedObject(UnityObject target){
			if(!Utility.serializedObjects.ContainsKey(target)){
				Utility.serializedObjects[target] = new SerializedObject(target);
			}
			return Utility.serializedObjects[target];
		}
		public static SerializedObject GetSerialized(UnityObject target){
			Type type = typeof(SerializedObject);
			return type.CallMethod<SerializedObject>("LoadFromCache",target.GetInstanceID());
		}
		public static void UpdateSerialized(UnityObject target){
			var serialized = Utility.GetSerializedObject(target);
			serialized.Update();
			serialized.ApplyModifiedProperties();
			//Utility.UpdatePrefab(target);
		}
		public static EditorWindow[] GetInspectors(){
			if(Utility.inspectors == null){
				Type inspectorType = Utility.GetInternalType("InspectorWindow");
				Utility.inspectors = inspectorType.CallMethod<EditorWindow[]>("GetAllInspectorWindows");
			}
			return Utility.inspectors;
		}
		public static Vector2 GetInspectorScroll(){
			Type inspectorWindow = Utility.GetInternalType("InspectorWindow");
			var window = EditorWindow.GetWindow(inspectorWindow);
			return window.GetVariable<Vector2>("m_ScrollPosition");
		}
		public static Vector2 GetInspectorScroll(this Rect current){
			Type inspectorWindow = Utility.GetInternalType("InspectorWindow");
			var window = EditorWindow.GetWindowWithRect(inspectorWindow,current);
			return window.GetVariable<Vector2>("m_ScrollPosition");
		}
		[MenuItem("Zios/Process/Prefs/Clear Player")]
		public static void DeletePlayerPrefs(){
			if(EditorUtility.DisplayDialog("Clear Player Prefs","Delete all the player preferences?","Yes","No")){
				PlayerPrefs.DeleteAll();
			}
		}
		[MenuItem("Zios/Process/Prefs/Clear Editor")]
		public static void DeleteEditorPrefs(){
			if(EditorUtility.DisplayDialog("Clear Editor Prefs","Delete all the editor preferences?","Yes","No")){
				EditorPrefs.DeleteAll();
			}
		}
		[MenuItem("Zios/Process/Format Code")]
		public static void FormatCode(){
			var output = new StringBuilder();
			var current = "";
			foreach(var file in FileManager.FindAll("*.cs")){
				var contents = file.GetText();
				output.Clear();
				foreach(var line in contents.GetLines()){
					var leading = line.Substring(0,line.TakeWhile(char.IsWhiteSpace).Count()).Replace("    ","\t");
					current = leading+line.Trim();
					if(line.Trim().IsEmpty()){continue;}
					output.AppendLine(current);
				}
				file.WriteText(output.ToString().TrimEnd(null));
			}
		}
	}
	#endif
}