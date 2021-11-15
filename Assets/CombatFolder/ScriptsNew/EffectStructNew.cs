using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EffectStructNew
{
	public enum Condition
	{
		none,
		dmgDealt,
		colliderHit,
		casting
	};
	public Condition when;

	public enum Effect
	{
		stun,
		hurt,
		makeItOT,
		spawnConsecutive,
		spawnExtra
	};
	public Effect doThis;

	public enum Target
	{
		player,
		enemy
	};
	public Target toWhom;

	public float forHowMuch;
	public float forHowLong; // 0: only this time; >0: buff or debuff
}
