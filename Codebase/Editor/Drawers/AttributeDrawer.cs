#pragma warning disable 0219
using Zios;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using MenuFunction = UnityEditor.GenericMenu.MenuFunction;
namespace Zios{
	[CustomPropertyDrawer(typeof(AttributeBool))]
	[CustomPropertyDrawer(typeof(AttributeString))]
	[CustomPropertyDrawer(typeof(AttributeInt))]
	[CustomPropertyDrawer(typeof(AttributeFloat))]
	[CustomPropertyDrawer(typeof(AttributeVector3))]
	public class AttributeDrawer : PropertyDrawer{
		public float overallHeight;
		public Rect area;
		public Attribute targetMode;
		public override float GetPropertyHeight(SerializedProperty property,GUIContent label){
			float amount = base.GetPropertyHeight(property,label);
			//if(property.isExpanded){amount = (amount*3)+5;}
			return amount;
		}
		public void DrawFloat(SerializedProperty property,GUIContent label,Rect iconRect){
			SerializedProperty data = property.FindPropertyRelative("data");
			SerializedProperty firstData = data.GetArrayElementAtIndex(0);
			AttributeFloat attribute = property.GetObject<AttributeFloat>();
			AttributeFloatData first = attribute.data[0];
			if(attribute.mode == AttributeMode.Normal){
				if(first.usage == AttributeUsage.Direct){
					first.value = EditorGUI.FloatField(this.area,label,first.value);
				}
				if(first.usage == AttributeUsage.Shaped){
					this.DrawShaped(typeof(AttributeFloat),attribute,firstData,label);
				}
			}
			if(attribute.mode == AttributeMode.Linked){
				//GUI.Box(iconRect,"",GUI.skin.GetStyle("IconLinked"));
				this.DrawShaped(typeof(AttributeFloat),attribute,firstData,label,false);
			}
			if(attribute.mode == AttributeMode.Formula){
				//GUI.Box(iconRect,"",GUI.skin.GetStyle("IconFormula"));
			}
			this.DrawContext(attribute,first);
		}
		public void DrawInt(SerializedProperty property,GUIContent label,Rect iconRect){
			SerializedProperty data = property.FindPropertyRelative("data");
			SerializedProperty firstData = data.GetArrayElementAtIndex(0);
			AttributeInt attribute = property.GetObject<AttributeInt>();
			AttributeIntData first = attribute.data[0];
			if(attribute.mode == AttributeMode.Normal){
				if(first.usage == AttributeUsage.Direct){
					first.value = EditorGUI.IntField(this.area,label,first.value);
				}
				if(first.usage == AttributeUsage.Shaped){
					this.DrawShaped(typeof(AttributeInt),attribute,firstData,label);
				}
			}
			if(attribute.mode == AttributeMode.Linked){
				//GUI.Box(iconRect,"",GUI.skin.GetStyle("IconLinked"));
				this.DrawShaped(typeof(AttributeInt),attribute,firstData,label,false);
			}
			if(attribute.mode == AttributeMode.Formula){
				//GUI.Box(iconRect,"",GUI.skin.GetStyle("IconFormula"));
			}
			this.DrawContext(attribute,first);
		}
		public void DrawBool(SerializedProperty property,GUIContent label,Rect iconRect){
			SerializedProperty data = property.FindPropertyRelative("data");
			SerializedProperty firstData = data.GetArrayElementAtIndex(0);
			AttributeBool attribute = property.GetObject<AttributeBool>();
			AttributeBoolData first = attribute.data[0];
			if(attribute.mode == AttributeMode.Normal){
				if(first.usage == AttributeUsage.Direct){
					first.value = EditorGUI.Toggle(this.area,label,first.value);
				}
				if(first.usage == AttributeUsage.Shaped){
					this.DrawShaped(typeof(AttributeBool),attribute,firstData,label);
				}
			}
			if(attribute.mode == AttributeMode.Linked){
				//GUI.Box(iconRect,"",GUI.skin.GetStyle("IconLinked"));
				this.DrawShaped(typeof(AttributeBool),attribute,firstData,label,false);
			}
			if(attribute.mode == AttributeMode.Formula){
				//GUI.Box(iconRect,"",GUI.skin.GetStyle("IconFormula"));
			}
			this.DrawContext(attribute,first);
		}
		public void DrawString(SerializedProperty property,GUIContent label,Rect iconRect){
			SerializedProperty data = property.FindPropertyRelative("data");
			SerializedProperty firstData = data.GetArrayElementAtIndex(0);
			AttributeString attribute = property.GetObject<AttributeString>();
			AttributeStringData first = attribute.data[0];
			if(attribute.mode == AttributeMode.Normal){
				if(first.usage == AttributeUsage.Direct){
					first.value = EditorGUI.TextField(this.area,label,first.value);
				}
				if(first.usage == AttributeUsage.Shaped){
					this.DrawShaped(typeof(AttributeString),attribute,firstData,label);
				}
			}
			if(attribute.mode == AttributeMode.Linked){
				//GUI.Box(iconRect,"",GUI.skin.GetStyle("IconLinked"));
				this.DrawShaped(typeof(AttributeString),attribute,firstData,label,false);
			}
			if(attribute.mode == AttributeMode.Formula){
				//GUI.Box(iconRect,"",GUI.skin.GetStyle("IconFormula"));
			}
			this.DrawContext(attribute,first);
		}
		public void DrawVector3(SerializedProperty property,GUIContent label,Rect iconRect){
			SerializedProperty data = property.FindPropertyRelative("data");
			SerializedProperty firstData = data.GetArrayElementAtIndex(0);
			AttributeVector3 attribute = property.GetObject<AttributeVector3>();
			AttributeVector3Data first = attribute.data[0];
			if(attribute.mode == AttributeMode.Normal){
				if(first.usage == AttributeUsage.Direct){
					first.value = EditorGUI.Vector3Field(this.area,label,first.value);
				}
				if(first.usage == AttributeUsage.Shaped){
					this.DrawShaped(typeof(AttributeVector3),attribute,firstData,label);
				}
			}
			if(attribute.mode == AttributeMode.Linked){
				//GUI.Box(iconRect,"",GUI.skin.GetStyle("IconLinked"));
				this.DrawShaped(typeof(AttributeVector3),attribute,firstData,label,false);
			}
			if(attribute.mode == AttributeMode.Formula){
				//GUI.Box(iconRect,"",GUI.skin.GetStyle("IconFormula"));
			}
			this.DrawContext(attribute,first);
		}
		/*public void FetchData<Type,DataType>(SerializedProperty property,ref int attributeIndex,ref List<string> attributes,ref int specialIndex,ref List<string> specials)
			where Type : AttributeFloat
			where DataType : AttributeFloatData{
			SerializedProperty targetProperty = property.FindPropertyRelative("target");
			Target target = targetProperty.GetObject<Target>();
			DataType data = property.GetObject<DataType>();
			specials = data.special.GetNames().ToList();
			specialIndex = specials.IndexOf(data.special.ToString());
			//var lookup = typeof(Type).GetField("lookup").GetValue(null);
			if(typeof(Type).GetField("lookup").GetValue(null).ContainsKey(target.direct)){
				attributes = Type.lookup[target.direct].Keys.ToList();
			}
			//attributes.Remove(current.path);
			if(data.reference != null){
				attributeIndex = attributes.IndexOf(data.reference.path);
			}
		}*/
		public void DrawShaped(Type type,Attribute current,SerializedProperty data,GUIContent label,bool drawSpecial=true,bool drawOperator=false){
			Rect labelRect = this.area.SetWidth(EditorGUIUtility.labelWidth);
			Rect valueRect = this.area.Add(labelRect.width,0,-labelRect.width,0);
			EditorGUI.LabelField(labelRect,label);
			SerializedProperty targetProperty = data.FindPropertyRelative("target");
			Target target = targetProperty.GetObject<Target>();
			Rect toggleRect = valueRect.SetWidth(16);
			bool toggleActive = this.targetMode == null && target.direct != null;
			toggleActive = EditorGUI.Toggle(toggleRect,toggleActive,GUI.skin.GetStyle("CheckmarkToggle"));
			this.targetMode = toggleActive ? null : current;
			if(this.targetMode == current){
				Rect targetRect = valueRect.Add(18,0,-18,0);
				EditorGUI.PropertyField(targetRect,targetProperty,new GUIContent(""));
				return;
			}
			List<string> attributeNames = new List<string>();
			List<string> specialNames = new List<string>();
			List<string> operatorNames = new List<string>();
			int specialIndex = 0;
			int attributeIndex = 0;
			int operatorIndex = 0;
			if(target.direct != null){
				if(type == typeof(AttributeFloat)){
					//this.FetchData<AttributeFloat,AttributeFloatData>(data,ref attributeIndex,ref attributes,ref specialIndex, ref specials);
					AttributeFloatData floatData = data.GetObject<AttributeFloatData>();
					operatorNames = floatData.sign.GetNames().ToList();
					operatorIndex = operatorNames.IndexOf(floatData.sign);
					specialNames = floatData.special.GetNames().ToList();
					specialIndex = specialNames.IndexOf(floatData.special);
					if(AttributeFloat.lookup.ContainsKey(target.direct)){
						attributeNames = AttributeFloat.lookup[target.direct].Keys.ToList();
					}
					attributeNames.Remove(current.path);
					if(floatData.reference != null){
						attributeIndex = attributeNames.IndexOf(floatData.reference.path);
					}
				}
				if(type == typeof(AttributeInt)){
					AttributeIntData intData = data.GetObject<AttributeIntData>();
					operatorNames = intData.sign.GetNames().ToList();
					operatorIndex = operatorNames.IndexOf(intData.sign);
					specialNames = intData.special.GetNames().ToList();
					specialIndex = specialNames.IndexOf(intData.special);
					if(AttributeInt.lookup.ContainsKey(target.direct)){
						attributeNames = AttributeInt.lookup[target.direct].Keys.ToList();
					}
					attributeNames.Remove(current.path);
					if(intData.reference != null){
						attributeIndex = attributeNames.IndexOf(intData.reference.path);
					}
				}
				if(type == typeof(AttributeBool)){
					AttributeBoolData boolData = data.GetObject<AttributeBoolData>();
					operatorNames = boolData.sign.GetNames().ToList();
					operatorIndex = operatorNames.IndexOf(boolData.sign);
					specialNames = boolData.special.GetNames().ToList();
					specialIndex = specialNames.IndexOf(boolData.special);
					if(AttributeBool.lookup.ContainsKey(target.direct)){
						attributeNames = AttributeBool.lookup[target.direct].Keys.ToList();
					}
					attributeNames.Remove(current.path);
					if(boolData.reference != null){
						attributeIndex = attributeNames.IndexOf(boolData.reference.path);
					}
				}
				if(type == typeof(AttributeString)){
					AttributeStringData stringData = data.GetObject<AttributeStringData>();
					operatorNames = stringData.sign.GetNames().ToList();
					operatorIndex = operatorNames.IndexOf(stringData.sign);
					specialNames = stringData.special.GetNames().ToList();
					specialIndex = specialNames.IndexOf(stringData.special);
					if(AttributeString.lookup.ContainsKey(target.direct)){
						attributeNames = AttributeString.lookup[target.direct].Keys.ToList();
					}
					attributeNames.Remove(current.path);
					if(stringData.reference != null){
						attributeIndex = attributeNames.IndexOf(stringData.reference.path);
					}
				}
				if(type == typeof(AttributeVector3)){
					AttributeVector3Data vector3Data = data.GetObject<AttributeVector3Data>();
					operatorNames = vector3Data.sign.GetNames().ToList();
					operatorIndex = operatorNames.IndexOf(vector3Data.sign);
					specialNames = vector3Data.special.GetNames().ToList();
					specialIndex = specialNames.IndexOf(vector3Data.special);
					if(AttributeVector3.lookup.ContainsKey(target.direct)){
						attributeNames = AttributeVector3.lookup[target.direct].Keys.ToList();
					}
					attributeNames.Remove(current.path);
					if(vector3Data.reference != null){
						attributeIndex = attributeNames.IndexOf(vector3Data.reference.path);
					}
				}
			}
			if(attributeNames.Count > 0){
				Rect specialRect = valueRect.Add(18,0,0,0).SetWidth(50);
				Rect attributeRect = drawSpecial ? valueRect.Add(68,0,-68,0) : valueRect.Add(18,0,-18,0);
				if(attributeIndex == -1){attributeIndex = 0;}
				if(specialIndex == -1){specialIndex = 0;}
				if(drawSpecial){
					specialIndex = EditorGUI.Popup(specialRect,specialIndex,specialNames.ToArray());
				}
				attributeIndex = EditorGUI.Popup(attributeRect,attributeIndex,attributeNames.ToArray());
				string name = attributeNames[attributeIndex];
				if(type == typeof(AttributeFloat)){
					AttributeFloat reference = AttributeFloat.lookup[target.direct][name];
					AttributeFloatData floatData = data.GetObject<AttributeFloatData>();
					floatData.special = (AttributeNumeralSpecial)specialIndex;
					floatData.reference = reference;
				}
				if(type == typeof(AttributeInt)){
					AttributeInt reference = AttributeInt.lookup[target.direct][name];
					AttributeIntData intData = data.GetObject<AttributeIntData>();
					intData.special = (AttributeNumeralSpecial)specialIndex;
					intData.reference = reference;
				}
				if(type == typeof(AttributeBool)){
					AttributeBool reference = AttributeBool.lookup[target.direct][name];
					AttributeBoolData boolData = data.GetObject<AttributeBoolData>();
					boolData.special = (AttributeBoolSpecial)specialIndex;
					boolData.reference = reference;
				}
				if(type == typeof(AttributeString)){
					AttributeString reference = AttributeString.lookup[target.direct][name];
					AttributeStringData stringData = data.GetObject<AttributeStringData>();
					stringData.special = (AttributeStringSpecial)specialIndex;
					stringData.reference = reference;
				}
				if(type == typeof(AttributeVector3)){
					AttributeVector3 reference = AttributeVector3.lookup[target.direct][name];
					AttributeVector3Data vector3Data = data.GetObject<AttributeVector3Data>();
					vector3Data.special = (AttributeVector3Special)specialIndex;
					vector3Data.reference = reference;
				}
			}
			else{
				Rect warningRect = valueRect.Add(18,0,-18,0);
				string targetName = target.direct.ToString().Strip("(UnityEngine.GameObject)").Trim();
				string typeName = type.ToString().ToLower().Strip("zios",".","attribute");
				string message = "<b>" + targetName.Truncate(16) + "</b> has no <b>"+typeName+"</b> attributes.";
				//EditorGUI.HelpBox(warningRect,message,MessageType.None);
				EditorGUI.LabelField(warningRect,message,GUI.skin.GetStyle("WarningLabel"));
			}
		}
		public void DrawContext(Attribute attribute,AttributeData data){
			if(Event.current.button != 1){return;}
			Vector2 mouse = Event.current.mousePosition;
			Rect labelRect = this.area.SetWidth(EditorGUIUtility.labelWidth);
			if(labelRect.Contains(mouse)){
				//GUI.changed = true;
				GenericMenu menu = new GenericMenu();
				AttributeMode mode = attribute.mode;
				AttributeUsage usage = data.usage;
				MenuFunction modeNormal  = ()=>{attribute.mode = AttributeMode.Normal;};
				MenuFunction modeLinked  = ()=>{attribute.mode = AttributeMode.Linked;};
				MenuFunction modeFormula = ()=>{attribute.mode = AttributeMode.Formula;};
				MenuFunction usageDirect = ()=>{data.usage = AttributeUsage.Direct;};
				MenuFunction usageShaped = ()=>{data.usage = AttributeUsage.Shaped;};
				//menu.AddItem(new GUIContent("Normal"),(mode==AttributeMode.Normal),modeNormal);
				bool normal = attribute.mode == AttributeMode.Normal;
				menu.AddItem(new GUIContent("Normal/Direct"),normal&&(usage==AttributeUsage.Direct),modeNormal+usageDirect);
				menu.AddItem(new GUIContent("Normal/Shaped"),normal&&(usage==AttributeUsage.Shaped),modeNormal+usageShaped);
				menu.AddItem(new GUIContent("Linked"),(mode==AttributeMode.Linked),modeLinked);
				//menu.AddItem(new GUIContent("Formula"),(mode==AttributeMode.Formula),modeFormula);
				menu.ShowAsContext();
			}
		
		}
		public override void OnGUI(Rect area,SerializedProperty property,GUIContent label){
			this.area = area;
			string skin = EditorGUIUtility.isProSkin ? "Dark" : "Light";
			GUI.skin = FileManager.GetAsset<GUISkin>("Gentleface-" + skin + ".guiskin");
			float xOffset = GUI.skin.label.CalcSize(label).x;
			Rect iconRect = this.area.AddX(xOffset+4).SetSize(14,14);
			GUI.changed = false;
			EditorGUI.BeginProperty(area,label,property);
			object generic = property.GetObject<object>();
			if(generic is AttributeString){this.DrawString(property,label,iconRect);}
			if(generic is AttributeInt){this.DrawInt(property,label,iconRect);}
			if(generic is AttributeBool){this.DrawBool(property,label,iconRect);}
			if(generic is AttributeVector3){this.DrawVector3(property,label,iconRect);}
			if(generic is AttributeFloat){this.DrawFloat(property,label,iconRect);}
			EditorGUI.EndProperty();
			//property.serializedObject.ApplyModifiedProperties();
			if(GUI.changed){
				EditorUtility.SetDirty(property.serializedObject.targetObject);
			}
		}
	}
}