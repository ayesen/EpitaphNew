// log

// 09/21/21
// xmovement
// xaiming
// !projectile is combined with straight since they are typically the same, the straight line one is actually a projectile
	// xdestroy spells
	// xlimit range
// !added aoe that allows the player to choose specific location to cast the spell in a certain range, might come in handy
	// xrestrict range

// 09/22/21
// xdamage
// xcontrol
// xrecovery

// 09/25/21
// xmaterial framework 1.0
	//! materials are prefabs
	//! each mat prefab can have multiple effect structs
	//! effect struct = effect the mat gives to the spell
	//! there can be only one material to be activated
	//! combination of materials functions as one new material

// 09/30/21
// adjustments
	// projectile doesn't use gravity for now
	
// features
	// xcollision detection
	// xprojectile
		// xhave place to insert effect codes
	// xaoe
		// xspawn a transparent cyllinder to indicate collider
		// xscale aoe indicator according to the size of the collider
		// xhave place to insert effect codes
		// xfix the bug where no collision is detected
	// xpie
		// xuse angle to check if hit
		// xchange pie range indicator accordingly
		// xfind place to insert effect codes
	// xtarget
		// xfind place to insert effect codes

// 10/03/21
// adjustments
	// xnew .gitignore
	// xget current mat in hit detection and call effect from effect manager based on the mat's effects
	// xdamage
		// xone time damage
		// xdot
	// xctrl
		// xwalkability
		// xatackability
		// xfoece move
	// xrecovery
		// xone time heal
		// xhot
	// xsupply
		// xdrop mats

// 10/09/21
// xcombination
	// xput combat into level
// foce move
	// ximpulse knock back
	// xknock back enemy
	// xknock back player

// 10/22/21
// xcombat refine
	// xwindup and backswing
	// xdrop gradually
		// xdrop meter
		// xui

// 10/23/21
// combat refine
	// xchange to windup state when moving and pressed atk
	// xcancel backswing if cast new spell
	// drop
		// xdrop debugging
		// xrefining
	// deal damage
		// xflash pie
		// xrefining
		// xfreeze frame
	// damage avoidence
		// gp
		// xknock back
	// effect
		// speed up (for dash juice G)
		// slow down
		// aoe dot and hot need to be effective only when in the aoe range
		// multiple ctrl type
// xobject inspection
	// xtext prompt
// xobjects in level solution
	// xenemy knocks back objects
	// xobjects disappear
// xalt cam
	// xperspective
	// xlook at enemy

// 10/31/21
// xvisual cue to indicate backswing state
// vfx
	// xprojectile
	// aoe
		// xheal
		// xdmg
		// dot
	// pie
	// xhot
	// dot

// 11/03/21
// xtry light on vfx
// xrefine vfx
	// xfragment on enemy and on other stuffs
// xdebug cancelling
// xenemy lock-on

// 11/05/21
// xdialogue
	// xtriggering
		// xauto
		// xpress e
	// xshow cht and eng
	// xshow image
	// xoptions
		// xdifferent following dialogues

// 11/07/21
// wall break
	// particle effect
		// xbricks
		// dust
	// xfunction
// xdialogue with ai

// 11/11/21
// xdynamic line duration adjustment according to clip length
// xwall hiding