using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EffectStructNew
{
	
	public enum Condition // the condition for this effect to take place
	{
		none, // this will take place when the spell collides
		dmgDealt, // when dmg is dealt, only work after this effect is applied before
		enemyHit, // when enemy is hit, only work after this effect is applied to the enemy on the last collision, so dealing dmg should be none
		casting // when attack frame
	};
	[Header("CONDITION")]
	public Condition when;

	public enum ConditionTriggeredBy // describe for this effect to take place, who need to trigger the condition
	{
		anyone, // anyone who triggers the condition
		owner, // owner of the effect
		player // player
	};
	public ConditionTriggeredBy triggeredBy;

	public enum Effect // describe the effect
	{
		stun,
		hurt,
		hurt_DOT,
		break_atk,
		spawnExtraCollisionDetection,
		spawnExtraSpell,
		knockBack,
		spawnHitDetectionAfterDeath
	};
	[Header("EFFECTS")]
	public Effect doThis;

	public enum Target // describe who this effect is applyed to
	{
		player,
		collisionEnemy,
		air
	};
	public Target toWhom;

	public float forHowMuch; // describe the effect numericals
	public float forHowLong; // describe how long this effect will last
}
