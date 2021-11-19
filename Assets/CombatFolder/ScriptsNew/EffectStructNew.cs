using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EffectStructNew
{
	public enum Condition // the condition for this effect to take place
	{
		none,
		dmgDealt, // when dmg is dealt
		enemyHit, // when enemy is hit, only work after this effect is applied to the enemy on the last collision, so dealing dmg should be none
		casting // when attack frame
	};
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
		DOT,
		spawnExtraCollisionDetection,
		spawnExtraSpell
	};
	public Effect doThis;

	public enum Target // describe who this effect is applyed to
	{
		player,
		collisionEnemy
	};
	public Target toWhom;

	public float forHowMuch; // describe the effect numericals
	public float forHowLong; // describe how long this effect will last
}
