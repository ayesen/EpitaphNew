using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManagerNew : MonoBehaviour
{
    public static EffectManagerNew me;
	public List<GameObject> currentMats;

	[Header("Situation Manager")]
	public bool casting;
	public bool dmgDealt;
	public bool enemyHit;

	[Header("Spawn Manager")]
	public Transform spellSpawnLoc;
	public GameObject spell_proj_prefab;
	public float extraSpawn_angleRange;

	[Header("Enemy Stuffs")]
	public List<GameObject> enemiesEffected; // enemies that have effects infliced on them

	[Header("Effect Infliction")]
	public GameObject effectHolder_prefab;

	private void Awake()
	{
		me = this;
	}

	private void Update()
	{
		ProcessEffects();
	}

	public void RefreshCurrentMats()
	{
		currentMats.Clear();
		foreach (var mat in PlayerScriptNew.me.selectedMats)
		{
			currentMats.Add(mat);
		}
	}

	public void ProcessEffects()
	{
		// when casting
		if (casting)
		{
			CastingEvent_Inflict();
			CastingEvent();
			casting = false;
		}
		// when dmg dealt
		if (dmgDealt)
		{

			dmgDealt = false;
		}
		// when collider hit
		if (enemyHit)
		{
			EnemyHitEvent();
			enemyHit = false;
		}
		// no condition
		NoneEvents();
	}

	#region Events
	#region Casting Events
	private void CastingEvent_Inflict() // record effects on targets, now all effects that player inflicts on themselves are recorded when casted
	{
		foreach (var mat in currentMats)
		{
			MatScriptNew ms = mat.GetComponent<MatScriptNew>();
			// inflict effects on target
			foreach (var effect in ms.myEffects)
			{
				if (effect.toWhom == EffectStructNew.Target.player) // pick out effects that works on player, generally one time or continuous buffs
				{
					SpawnEffectHolders(PlayerScriptNew.me.gameObject, effect);
				}
			}
		}
	}

	public void CastingEvent() // process effects on targets
	{
		// get effects inflicted on player
		int spawnCount = 1; // number of spells to spawn, default is 1
		int hitCount = 1; // number of detection, default is 1

		foreach (var effectHolder in PlayerScriptNew.me.gameObject.GetComponent<EffectHoldersHolderScript>().effectHolders)
		{
			EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
			if (ehs.myEffect.doThis == EffectStructNew.Effect.spawnExtra) // if effect spawns extra spells, add it to spawn count
			{
				spawnCount += (int)ehs.myEffect.forHowMuch;
				ehs.destroy = true;
			}
			if (ehs.myEffect.doThis == EffectStructNew.Effect.spawnConsecutive) // if effect increases hit count, add it to default hit count
			{
				hitCount += (int)ehs.myEffect.forHowMuch;
				ehs.destroy = true;
			}
		}

		// get effects inflicted on enemy
		foreach (var enemy in enemiesEffected)
		{
			foreach (var effectHolder in enemy.GetComponent<EffectHoldersHolderScript>().effectHolders)
			{
				EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
				if (ehs.myEffect.when == EffectStructNew.Condition.casting)
				{
					// effects
				}
			}
		}
		SpawnSpell(spawnCount, hitCount);
	}
	#endregion
	#region Enemy Hit Events
	// effects on enemy are inflicted, or in another word, recorded, when the collider hit an enemy collider
	public void EnemyHitEvent() // 
	{
		// check effects on player
		foreach (var effectHolder in PlayerScriptNew.me.gameObject.GetComponent<EffectHoldersHolderScript>().effectHolders)
		{
			EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
			if (ehs.myEffect.when == EffectStructNew.Condition.enemyHit)
			{
				
			}
		}
		
		// check effects on enemie
		foreach (var enemy in enemiesEffected)
		{
			foreach (var effectHolder in enemy.GetComponent<EffectHoldersHolderScript>().effectHolders)
			{
				EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
				if (ehs.myEffect.doThis == EffectStructNew.Effect.hurt)
				{
					
				}
			}
		}
	}
	#endregion
	#region None Events
	public void NoneEvents() // these effects will happen right after they're applied
	{
		// check effects on player
		foreach (var effectHolder in PlayerScriptNew.me.gameObject.GetComponent<EffectHoldersHolderScript>().effectHolders)
		{
			EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
			
		}

		// check effects on enemies
		foreach (var enemy in enemiesEffected) // get every enemies that have effects on them
		{
			foreach (var effectHolder in enemy.GetComponent<EffectHoldersHolderScript>().effectHolders) // get these enemies' effect holders
			{
				EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
				if (ehs.myEffect.doThis == EffectStructNew.Effect.hurt) // for each holder, check its effect
				{
					enemy.GetComponent<Enemy>().LoseHealth((int)ehs.myEffect.forHowMuch);
					ehs.destroy = true;
				}
			}
		}
	}
	#endregion
	#endregion

	public void SpawnEffectHolders(GameObject target, EffectStructNew effect)
	{
		GameObject effectHolder = Instantiate(effectHolder_prefab, target.transform);
		EffectHolderScript ehs = effectHolder.GetComponent<EffectHolderScript>();
		ehs.myOwner = target;
		ehs.myEffect = effect;
		target.GetComponent<EffectHoldersHolderScript>().effectHolders.Add(effectHolder);
	}
	#region spawn spell functions
	private void SpawnSpell(int spawnAmount, int hitAmount)
	{
		int mAmount = spawnAmount;
		while (mAmount > 0)
		{
			RotateSpellSpawnLoc();
			if (mAmount == 1)
			{
				ResetSpellSpawnLoc();
			}
			DefaultSpawn(hitAmount);
			mAmount--;
		}
	}

	private void DefaultSpawn(int hitAmount)
	{
		GameObject spell = Instantiate(spell_proj_prefab, spellSpawnLoc.position, spellSpawnLoc.rotation);
		spell.GetComponent<Rigidbody>().AddForce(spellSpawnLoc.transform.forward * 5, ForceMode.Impulse);
		spell.GetComponent<SpellScript>().hit_amount = hitAmount;
		foreach (var mat in currentMats)
		{
			foreach (var effect in mat.GetComponent<MatScriptNew>().myEffects)
			{
				spell.GetComponent<SpellScript>().myEffects.Add(effect);
			}
		}
	}

	#region for spawning extra spells
	private void RotateSpellSpawnLoc()
	{
		float dir_x = Random.Range(-extraSpawn_angleRange, extraSpawn_angleRange);
		float dir_y = Random.Range(-extraSpawn_angleRange, extraSpawn_angleRange);
		float dir_z = Random.Range(-extraSpawn_angleRange, extraSpawn_angleRange);

		spellSpawnLoc.rotation = Quaternion.Euler(dir_x + spellSpawnLoc.rotation.eulerAngles.x, dir_y + spellSpawnLoc.rotation.eulerAngles.y, dir_z + spellSpawnLoc.rotation.eulerAngles.z);
	}

	private void ResetSpellSpawnLoc()
	{
		spellSpawnLoc.localRotation = Quaternion.Euler(0, 0, 0);
	}
	#endregion

	#endregion
}
