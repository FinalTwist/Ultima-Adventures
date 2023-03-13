using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Spells.Fifth;
using Server.Spells.Seventh;

namespace Server.Spells.Necromancy
{
	public abstract class TransformationSpell : NecromancerSpell, ITransformationSpell
	{
		public abstract int Body{ get; }
		public virtual int Hue{ get{ return 0; } }

		public virtual int PhysResistOffset{ get{ return 0; } }
		public virtual int FireResistOffset{ get{ return 0; } }
		public virtual int ColdResistOffset{ get{ return 0; } }
		public virtual int PoisResistOffset{ get{ return 0; } }
		public virtual int NrgyResistOffset{ get{ return 0; } }

		public TransformationSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )
		{
		}

		public override bool BlockedByHorrificBeast{ get{ return false; } }

		public override bool CheckCast(Mobile caster)
		{
			if( !TransformationSpellHelper.CheckCast( Caster, this ) )
				return false;

			return base.CheckCast( caster );
		}

		public override void OnCast()
		{
			TransformationSpellHelper.OnCast( Caster, this );

			FinishSequence();
		}

		public virtual double TickRate{ get{ return 1.0; } }

		public virtual void OnTick( Mobile m )
		{
		}

		public virtual void DoEffect( Mobile m )
		{
		}

		public virtual void RemoveEffect( Mobile m )
		{
		}
	}
}
