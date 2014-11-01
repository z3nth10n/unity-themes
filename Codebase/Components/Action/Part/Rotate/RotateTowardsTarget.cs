using Zios;
using UnityEngine;
[AddComponentMenu("Zios/Component/Action/Part/Rotate Towards (Target)")]
public class RotateTowardsTarget : ActionPart{
	public Target source = new Target();
	public Target target = new Target();
	public AttributeVector3 offset;
	public OffsetType offsetType;
	public ClerpVector3 angles = new ClerpVector3();
	public override void OnValidate(){
		base.OnValidate();
		this.DefaultRate("LateUpdate");
		this.source.Setup("Source",this);
		this.target.Setup("Target",this);
		this.offset.Setup("Offset",this);
		this.angles.Setup("Angles",this);
		this.angles.isAngle = true;
	}
	public Vector3 AdjustVector(Vector3 value){
		Vector3 adjusted = value;
		if(this.offsetType == OffsetType.Relative){
			Transform target = this.target.direct.transform;
			adjusted = target.right * value.x;
			adjusted += target.up * value.y;
			adjusted += target.forward * value.z;
		}
		return adjusted;
	}
	public override void Use(){
		Transform source = this.source.Get().transform;
		Transform target = this.target.Get().transform;
		Vector3 offset = this.AdjustVector(this.offset);
		Vector3 current = source.localEulerAngles;
		source.LookAt(target.position + offset);
		Vector3 goal = source.localEulerAngles;
		source.localEulerAngles = this.angles.Step(current,goal);
		base.Use();
	}
}
