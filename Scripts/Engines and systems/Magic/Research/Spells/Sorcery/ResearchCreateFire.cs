using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Items;
using System.Collections.Generic;
using System.Collections;
using Server.Mobiles;

namespace Server.Spells.Research
{
	public class ResearchCreateFire : ResearchSpell
	{
		public override int spellIndex { get { return 4; } }
		public int CirclePower = 1;
		public static int spellID = 4;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				212,
				9001
			);

		public ResearchCreateFire( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			int fires = 0;

			foreach ( Item m in Caster.GetItemsInRange( 10 ) )
			{
				if ( m is MagicalFire )
					++fires;
			}

			if ( fires > 1 )
			{
				Caster.SendMessage( "There are too many magical fires in the area!" );
			}
			else if ( CheckSequence() )
			{
				double time = DamagingSkill( Caster );
					if ( time > 200 ){ time = 200.0; }
					if ( time < 15 ){ time = 15.0; }

				int dmg = (int)(DamagingSkill( Caster )/10);
					if ( dmg > 20 ){ dmg = 20; }
					if ( dmg < 2 ){ dmg = 2; }

				Caster.FixedParticles( 0x3709, 10, 30, 5052, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.LeftFoot );
				Caster.PlaySound( 0x208 );
				Caster.SendMessage( "You summon a magical fire at your feet." );
				Item iFire = new MagicalFire(Caster,time,dmg);
				iFire.MoveToWorld( Caster.Location, Caster.Map );
				Server.Misc.Research.ConsumeScroll( Caster, true, spellIndex, false );
			}
			FinishSequence();
		}
	}
}

namespace Server.Items
{
    public class MagicalFire : Item
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		private DateTime m_NextFire;
		public DateTime NextFire{ get{ return m_NextFire; } set{ m_NextFire = value; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			Mobile pet = m;
			if ( m is BaseCreature )
				pet = ((BaseCreature)m).GetMaster();

			if ( this.Map == m.Map && owner != m && owner != pet && m.Blessed == false && owner.CanBeHarmful( m, true ) )
			{
				if ( DateTime.UtcNow >= m_NextFire && Utility.InRange( m.Location, this.Location, 6 ) )
				{
					int dmg_min = (int)(damage/2);
						if ( dmg_min < 2 ){ dmg_min = 2;}
					int dmg_max = damage;
					int dmg = Utility.RandomMinMax( dmg_min, dmg_max );
					m.FixedParticles( 0x3709, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
					m.PlaySound( 0x208 );
					if ( owner != null ){ AOS.Damage( m, owner, dmg, 0, 100, 0, 0, 0 ); owner.DoHarmful( m ); }
					m_NextFire = (DateTime.UtcNow + TimeSpan.FromSeconds( 2.0 ));
				}
			}
		}

		public Mobile owner;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return owner; }
			set{ owner = value; }
		}

		public double lasts;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public double Lasts
		{
			get{ return lasts; }
			set{ lasts = value; }
		}

		public int damage;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Damage
		{
			get{ return damage; }
			set{ damage = value; }
		}

		[Constructable]
		public MagicalFire() : this( null, 0.0, 0 )
		{
		}

		[Constructable]
		public MagicalFire( Mobile from, double time, int damage )
		{
			ItemID = 0xDE3;
			Movable = false;
			Name = "magical fire";
			Light = LightType.Circle300;
			this.owner = from;
			this.lasts = time;
			this.damage = damage;
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this, lasts ); 
			thisTimer.Start(); 
		}

		public MagicalFire( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
			writer.Write( (Mobile)owner );
			writer.Write( lasts );
			writer.Write( damage );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			owner = reader.ReadMobile();
			lasts = reader.ReadDouble();
			damage = reader.ReadInt();
			this.Delete(); // none when the world starts 
		}

		public class ItemRemovalTimer : Timer 
		{ 
			private Item i_item; 
			public ItemRemovalTimer( Item item, Double lasts ) : base( TimeSpan.FromSeconds( lasts ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
				i_item = item; 
			} 

			protected override void OnTick() 
			{ 
				if (( i_item != null ) && ( !i_item.Deleted ))
				{
					i_item.Delete();
				}
			} 
		}
	}	
}



