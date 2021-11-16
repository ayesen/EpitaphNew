using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EffectStructNew
{
	public enum Condition
	{
		none,
		dmgDealt, // when dmg is dealt
		enemyHit, // when enemy is hit, only work after this effect is applied to the enemy on the last collision, so dealing dmg should be none
		casting // when attack frame
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
		collisionEnemy
	};
	public Target toWhom;

	public float forHowMuch;
	public float forHowLong;
}
