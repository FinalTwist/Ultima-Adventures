using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a gorgon corpse" )]
	public class GorgonRiding : BaseMount
	{
		[Constructable]
		public GorgonRiding() : this( "a gorgon" )
		{
		}

		[Constructable]
		public GorgonRiding( string name ) : base( name, 0x11C, 0x3E92, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 362;
			Hue = 0xB63;

			SetStr( 176, 205 );
			SetDex( 46, 65 );
			SetInt( 46, 70 );

			SetHits( 106, 123 );

			SetDamage( 8, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 15 );
			SetResistance( ResistanceType.Energy, 5, 15 );

			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 40;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 73.9;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

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

				m.PlaySound(0x16B);
				m.FixedEffect(0x376A, 6, 1);

				int duration = Utility.RandomMinMax(4, 8);
				m.Paralyze(TimeSpan.FromSeconds(duration));

				m.SendMessage( "You are petrified from the GorgonRiding breath!" );
			}
		}

		public override void OnGaveMeleeAttack( Mobile m )
		{
			base.OnGaveMeleeAttack( m );

			if ( 1 == Utility.RandomMinMax( 1, 20 ) && m is PlayerMobile )
			{
				Container cont = m.Backpack;
				Item iStone = Server.Items.HiddenTrap.GetMyItem( m );

				if ( iStone != null )
				{
					if ( m.CheckSkill( SkillName.MagicResist, 0, 100 ) || Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
					{
					}
					else if ( Server.Items.HiddenTrap.CheckInsuranceOnTrap( iStone, m ) == true )
					{
						m.LocalOverheadMessage(MessageType.Emote, 1150, true, "The GorgonRiding almost turned one of your protected items to stone!");
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

		public GorgonRiding( Serial serial ) : base( serial )
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