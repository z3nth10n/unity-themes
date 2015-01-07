using Zios;
using UnityEngine;
public enum ToggleState{Enable,Disable,Toggle}
[AddComponentMenu("Zios/Component/Action/Set Active")]
public class SetActive : ActionLink{
	public AttributeGameObject target;
	public ToggleState state;
	public override void Awake(){
		base.Awake();
		this.target.Setup("Target",this);
	}
	public override void Use(){
		GameObject target = this.target.Get();
		if(state == ToggleState.Enable && !target.activeSelf){target.SetActive(true);}
		if(state == ToggleState.Disable && target.activeSelf){target.SetActive(false);}
		if(state == ToggleState.Toggle){target.SetActive(!target.activeSelf);}
		if(this.gameObject.activeSelf){
			base.Use();
		}
	}
}