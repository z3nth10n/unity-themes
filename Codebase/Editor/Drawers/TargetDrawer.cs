using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
[CustomPropertyDrawer(typeof(Target))]
public class TargetDrawer : PropertyDrawer{
	public static Dictionary<Target,bool?> toggled = new Dictionary<Target,bool?>();
	public static Dictionary<Target,bool> unfound = new Dictionary<Target,bool>();
    public override void OnGUI(Rect position,SerializedProperty property,GUIContent label){
		string skin = EditorGUIUtility.isProSkin ? "Dark" : "Light";
		GUI.skin = FileManager.GetAsset<GUISkin>("Gentleface-" + skin + ".guiskin");
		GUI.changed = false;
		Target target = property.GetObject<Target>();
        Rect area = new Rect(position.x,position.y,position.width,position.height);
		Rect toggleRect = new Rect(position);
		Rect propertyRect = new Rect(position);
		float labelWidth = label.text.IsEmpty() ? 0 : EditorGUIUtility.labelWidth;
		propertyRect.x += labelWidth + 18;
		propertyRect.width -= labelWidth + 18;
		toggleRect.x += labelWidth;
		toggleRect.width = 18;
		if(!TargetDrawer.toggled.ContainsKey(target)){TargetDrawer.toggled[target] = null;}
		if(!TargetDrawer.unfound.ContainsKey(target)){TargetDrawer.unfound[target] = false;}
		bool toggleActive = TargetDrawer.toggled[target] ?? target.search.IsEmpty();
		TargetDrawer.toggled[target] = toggleActive.Draw(toggleRect,GUI.skin.GetStyle("TargetToggle"));
		bool toggled = (bool)TargetDrawer.toggled[target];
        EditorGUI.BeginProperty(position,label,property);
		if(toggleActive != toggled){
			target.search = "";
			target.DefaultSearch();
			if(toggled){
				target.direct = null;
				TargetDrawer.unfound[target] = true;
			}
		}
		label.Draw(area,null,true);
		if(toggled){
			if(TargetDrawer.unfound[target]){target.direct = null;}
			GameObject direct = target.direct;
			target.direct = target.direct.Draw(propertyRect,true);
			if(target.direct != direct){
				TargetDrawer.unfound[target] = false;
			}
		}
		else{
			target.search = target.search.DrawField(propertyRect);
			property.FindPropertyRelative("search").stringValue = target.search;
			GUIStyle textStyle = new GUIStyle(GUI.skin.label);
			textStyle.alignment = TextAnchor.MiddleRight;
			textStyle.normal.textColor = Colors.Get("Gray");
			string result = target.direct != null ? "Found." : "Not Found.";
			result.Draw(propertyRect,textStyle);
		}
        EditorGUI.EndProperty();
		if(GUI.changed){
			property.serializedObject.ApplyModifiedProperties();
			EditorUtility.SetDirty(property.serializedObject.targetObject);
		}
    }
}