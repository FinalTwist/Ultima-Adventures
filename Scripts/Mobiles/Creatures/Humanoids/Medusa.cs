using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a medusan corpse" )]
	public class Medusa : BaseCreature
	{
		[Constructable]
		public Medusa () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a medusan";
			Body = 306;
			BaseSoundID = 219;

			SetStr( 388, 520 );
			SetDex( 121, 170 );
			SetInt( 398, 557 );

			SetHits( 212, 253 );

			SetDamage( 10, 20 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 50, 60 );

			SetSkill( SkillName.Tactics, 80.1, 90.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 14000;
			Karma = -14000;

			VirtualArmor = 40;
		}

		public override int GetIdleSound() { return 1557; } 
		public override int GetAngerSound() { return 1554; } 
		public override int GetHurtSound() { return 1556; } 
		public override int GetDeathSound()	{ return 1555; }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }

		public void TurnStone()
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
				DoHarmful( m );

				m.PlaySound(0x204);
				m.FixedEffect(0x376A, 6, 1);

				int duration = Utility.RandomMinMax(4, 8);
				m.Paralyze(TimeSpan.FromSeconds(duration));

				m.SendMessage( "You are paralyzed!" );
			}
		}

		public override void OnGaveMeleeAttack( Mobile m )
		{
			base.OnGaveMeleeAttack( m );

			if ( 1 == Utility.RandomMinMax( 1, 20 ) )
			{
				Container cont = m.Backpack;
				Item iStone = Server.Items.HiddenTrap.GetMyItem( m );

				if ( iStone != null )
				{
					if ( m.CheckSkill( SkillName.MagicResist, 0, 125 ) || Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
					{
					}
					else if ( Server.Items.HiddenTrap.CheckInsuranceOnTrap( iStone, m ) == true )
					{
						m.LocalOverheadMessage(MessageType.Emote, 1150, true, "The medusa almost turned one of your protected items to stone!");
					}
					else
					{
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "One of your items has been turned to stone!");
						m.PlaySound( 0x1FB );
						Item rock = new BrokenGear();
						rock.ItemID = iStone.ItemID;
						rock.Hue = 2101;
						rock.Weight = iStone.Weight * 3;
						rock.Name = "useless stone";
						iStone.Delete();
						m.AddToBackpack ( rock );
					}
				}
			}

			if ( 0.1 >= Utility.RandomDouble() )
				TurnStone();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
				TurnStone();
		}

		public Medusa( Serial serial ) : base( serial )
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