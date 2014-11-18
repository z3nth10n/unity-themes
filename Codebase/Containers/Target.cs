using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zios;
using Action = Zios.Action;
using UnityObject = UnityEngine.Object;
[Serializable]
public class Target{
	public string search = "";
	public GameObject direct;
	private Component parent;
	private bool hasSearched;
	private bool hasWarned;
	private string lastSearch = "";
	private string fallbackSearch = "";
	private Dictionary<string,GameObject> special = new Dictionary<string,GameObject>();
	public static implicit operator Transform(Target value){
		value.Prepare();
		return value.direct.transform;
	}
	public static implicit operator GameObject(Target value){
		value.Prepare();
		return value.direct;
	}
	public static implicit operator UnityObject(Target value){
		value.Prepare();
		return value.direct;
	}
	public void Setup(string path,Component parent,string defaultSearch="[Self]"){
		this.parent = parent;
		this.AddSpecial("[This]",this.parent.gameObject);
		this.AddSpecial("[Self]",this.parent.gameObject);
		if(parent is ActionPart || parent is Action){
			ActionPart part = parent is ActionPart ? (ActionPart)parent : null;
			Action action = parent is Action ? (Action)parent : part.action;
			GameObject actionObject = action != null ? action.gameObject : part.gameObject;
			GameObject ownerObject = action != null ? action.owner : part.gameObject;
			this.AddSpecial("[Owner]",ownerObject);
			this.AddSpecial("[Action]",actionObject);
		}
		this.DefaultSearch(defaultSearch);
	}
	public GameObject Get(){
		this.Prepare();
		return this.direct;
	}
	public void AddSpecial(string name,GameObject target){
		this.special[name.ToLower()] = target;
	}
	public void SkipWarning(){this.hasWarned = true;}
	public void DefaultSearch(){this.DefaultSearch(this.fallbackSearch);}
	public void DefaultSearch(string target){
		this.fallbackSearch = target;
		if(this.search != this.lastSearch || this.direct == null){
			if(this.search.IsEmpty()){
				this.search = target;
			}
			this.Prepare();
		}
	}
	public void DefaultTarget(GameObject target){
		if(this.direct.IsNull()){
			this.direct = target;
		}
	}
	public GameObject FindTarget(string search){
		foreach(var item in this.special){
			string special = item.Key;
			if(search.ToLower().Contains(special)){
				string specialPath = this.special[special].GetPath();
				search = search.Replace(special,specialPath,true);
			}
		}
		if(search.Contains("/")){
			string[] parts = search.Split("/");
			string total = "";
			for(int index=0;index<parts.Length;++index){
				string part = parts[index];
				if(part == ".." || part == "." || part.IsEmpty()){
					if(part.IsEmpty()){continue;}
					if(total.IsEmpty()){
						GameObject current = this.special.ContainsKey("[this]") ? this.special["[this]"] : null;
						if(!current.IsNull()){
							if(part == ".."){
								total = current.GetParent().IsNull() ? "" : current.GetParent().GetPath();
							}
							else{total = current.GetPath();}
						}
						continue;
					}
					GameObject path = GameObject.Find(total);
					if(!path.IsNull()){
						if(part == ".."){
							total = path.GetParent().IsNull() ? "" : path.GetParent().GetPath();
						}
						continue;
					}
				}
				total += part + "/";
			}
			search = total;
		}
		return Locate.Find(search);
	}
	public void Prepare(){
		bool editorMode = !Application.isPlaying;
		this.search = this.search.Replace("\\","/");
		if((editorMode || !this.hasSearched) && !this.search.IsEmpty()){
			this.direct = this.FindTarget(this.search);
			this.lastSearch = this.search;
			this.hasSearched = true;
		}
		if(!editorMode && this.direct.IsNull() && !this.hasWarned){
			Debug.LogWarning("Target : No gameObject was found for " + this.parent.name,this.parent);
			if(!search.IsEmpty() && !search.Contains("Not Found")){
				this.search = "<" + this.search + " Not Found>";
			}
			this.hasWarned = true;
		}
	}
}