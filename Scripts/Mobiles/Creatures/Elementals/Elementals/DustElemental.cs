using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "an elemental corpse" )]
	public class DustElemental : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public DustElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a dust elemental";
			Body = 790;
			BaseSoundID = 655;

			SetStr( 126, 155 );
			SetDex( 166, 185 );
			SetInt( 101, 125 );

			SetHits( 66, 83 );

			SetDamage( 5, 8 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.EvalInt, 60.1, 75.0 );
			SetSkill( SkillName.Magery, 60.1, 75.0 );
			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 20;
			ControlSlots = 2;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager, 2 );
			AddLoot( LootPack.LowScrolls );
		}

		public override bool BleedImmune{ get{ return true; } }

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Item sand = new Sand();
   			sand.Amount = Utility.RandomMinMax( 1, 2 );
   			c.DropItem(sand);
		}

		public override void OnGaveMeleeAttack( Mobile m )
		{
			base.OnGaveMeleeAttack( m );

			if ( 1 == Utility.RandomMinMax( 1, 20 ) )
			{
				Container cont = m.Backpack;
				Item iSucked = Server.Items.HiddenTrap.GetMyItem( m );

				if ( iSucked != null )
				{
					if ( Server.Items.HiddenTrap.IAmShielding( m, 80 ) || Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
					{
					}
					else if ( Server.Items.HiddenTrap.CheckInsuranceOnTrap( iSucked, m ) == true )
					{
						m.LocalOverheadMessage(MessageType.Emote, 1150, true, "One of your protected items was almost carried by the wind!");
					}
					else
					{
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "One of your items was carried into the wind!");
						m.PlaySound( 0x10B );
						PackItem( iSucked );
					}
				}
			}
		}

		public DustElemental( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 263 )
				BaseSoundID = 655;
		}
	}
}
