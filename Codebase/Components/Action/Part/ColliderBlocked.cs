using Zios;
using System;
using UnityEngine;
public enum BlockType{Blocked,Unblocked}
public enum Direction{Up,Down,Left,Right,Forward,Back}
[AddComponentMenu("Zios/Component/Action/Part/Blocked State")]
public class BlockedState : ActionPart{
	//public AttributeEnum type = new AttributeEnum(BlockType);
	public BlockType type;
	public Direction direction;
	public AttributeFloat duration;
	public Target target = new Target();
	public override void Start(){
		base.Start();
		this.DefaultPriority(5);
		this.target.Setup("Target",this);
		this.duration.Setup("Duration",this);
	}
	public override void Use(){
		float duration = 0;
		//string direction = this.direction.ToString().ToLower();
		//float duration = this.target.GetDictionary<string,float>("GetUnblocked")[direction];
		bool state = this.type == BlockType.Blocked ? duration < this.duration : duration > this.duration;
		this.Toggle(state);
	}
}
