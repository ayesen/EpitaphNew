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
	public bool colliderHit;

	[Header("Spawn Manager")]
	public Transform spellSpawnLoc;
	public GameObject spell_proj_prefab;
	public float extraSpawn_angleRange;

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

    public void CastingEvent() // when player press attack button
	{
		int spawnCount = 1; // number of spells to spawn, default is 1
		int hitCount = 1; // number of detection, default is 1

		foreach (var mat in currentMats)
		{
			MatScriptNew ms = mat.GetComponent<MatScriptNew>();
			foreach (var effect in ms.myEffects)
			{
				if (effect.when == EffectStructNew.Condition.casting) // pick out effects that come into force when casting
				{
					if (effect.doThis == EffectStructNew.Effect.spawnExtra) // if effect spawns extra spells, add it to spawn count
					{
						spawnCount += (int)effect.forHowMuch;
					}
					if (effect.doThis == EffectStructNew.Effect.spawnConsecutive) // if effect increases hit count, add it to default hit count
					{
						hitCount += (int)effect.forHowMuch;
					}
				}
			}
		}
		SpawnSpell(spawnCount, hitCount);
	}

	public void ProcessEffects()
	{
		// when casting
		if (casting)
		{
			CastingEvent();
			casting = false;
		}
		// when dmg dealt
		if (dmgDealt)
		{
			dmgDealt = false;
		}
		// when collider hit
		if (colliderHit)
		{
			colliderHit = false;
		}
	}

	#region spawn related
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
