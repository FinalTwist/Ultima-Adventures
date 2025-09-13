using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a pile of wax" )]
	public class WaxSculpture : BaseCreature
	{
		[Constructable]
		public WaxSculpture() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a wax golem";
			BaseSoundID = 268;
			Hue = 0x47E;
			Body = 14;

			SetStr( 151, 180 );
			SetDex( 71, 90 );
			SetInt( 31, 62 );

			SetHits( 101, 118 );

			SetDamage( 14, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.MagicResist, 50.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 4250;
			Karma = -4250;

			VirtualArmor = 39;
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			Beeswax wax = new Beeswax();
   			wax.Amount = Utility.RandomMinMax( 5, 20 );
   			c.DropItem(wax);
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Rich );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public void DrainStamina()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				if ( !Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
				{
					DoHarmful( m );
					m.PlaySound( 0x026 );
					m.SendMessage( "Wax covers your mouth...so it is hard to breath!" );
					int toDrain = Utility.RandomMinMax( 10, 40 );
					m.Stam -= toDrain;
				}
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 >= Utility.RandomDouble() )
			{
				DrainStamina();
				this.Hue = RandomThings.GetRandomColor(0);
			}
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
			{
				DrainStamina();
				this.Hue = RandomThings.GetRandomColor(0);
			}
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ reflect = true; } // 25% spells are reflected back to the caster
			else { reflect = false; }
		}

		public WaxSculpture( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}