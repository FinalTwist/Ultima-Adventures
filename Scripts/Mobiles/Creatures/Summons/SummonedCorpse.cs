using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an undead corpse" )]
	public class SummonedCorpse : BaseCreature
	{
		public int BCPoison;
		public int BCImmune;
		
		private bool m_MobSummon;
		[CommandProperty( AccessLevel.GameMaster )]
		public bool MobSummon
		{
		    get{ return m_MobSummon; }
		    set{ m_MobSummon = value; }
		}

		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		[Constructable]
		public SummonedCorpse( int maxhits, int maxstam, int maxmana, int str, int dex, int iq, int poison, int immune ): base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.3, 0.6 )
		{
			BCPoison = poison+0;
			BCImmune = immune+0;

			Name = "an undead creature";
			Body = 2;
			BaseSoundID = 471;

			SetStr( str );
			SetDex( dex);
			SetInt( iq );

			SetHits( maxhits );
			SetStam( maxstam );
			SetMana( maxmana );

			Fame = 0;
			Karma = 0;

			ControlSlots = 1;
		}

		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }

		public override Poison PoisonImmune
		{
			get
			{
				if ( BCImmune == 1 ){ return Poison.Lesser; }
				else if ( BCImmune == 2 ){ return Poison.Regular; }
				else if ( BCImmune == 3 ){ return Poison.Greater; }
				else if ( BCImmune == 4 ){ return Poison.Deadly; }
				else if ( BCImmune == 5 ){ return Poison.Lethal; }

				return null;
			}
		}

		public override Poison HitPoison
		{
			get
			{
				if ( BCPoison == 1 ){ return Poison.Lesser; }
				else if ( BCPoison == 2 ){ return Poison.Regular; }
				else if ( BCPoison == 3 ){ return Poison.Greater; }
				else if ( BCPoison == 4 ){ return Poison.Deadly; }
				else if ( BCPoison == 5 ){ return Poison.Lethal; }

				return null;
			}
		}
		
		public override bool IsEnemy( Mobile m )
		{
			//adding special here for animate dead cast by mage ai mobs
			if (m_MobSummon && ( m is PlayerMobile || ( m is BaseCreature && ((BaseCreature)m).Controlled && ((BaseCreature)m).ControlMaster is PlayerMobile ) ) )
				return true;
			else if (m_MobSummon)
				return false;
			
			return base.IsEnemy(m);
		}
		
		public override bool CanBeHarmful( Mobile target, bool message, bool ignoreOurBlessedness )
		{
			if (m_MobSummon)
			{
				if ( target is PlayerMobile || ( target is BaseCreature && ((BaseCreature)target).Controlled && ((BaseCreature)target).ControlMaster is PlayerMobile ) )
					return true;
				else
					return false;
			}
			return base.CanBeHarmful( target, message, ignoreOurBlessedness );
		}

		public SummonedCorpse( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 );
			writer.Write( (bool) m_MobSummon );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			if (version >= 1)
				m_MobSummon = reader.ReadBool();
		}
	}
}
