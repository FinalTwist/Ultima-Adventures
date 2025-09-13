using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Gumps;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a pile of viscera" )]
	public class Viscera : BaseCreature
	{
		[Constructable]
		public Viscera () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a pile of viscera";
			Body = 775;
			Hue = 0x4AA;
			BaseSoundID = 456;

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

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 30;

			int[] list = new int[]
				{
					0x1CF0, 0x1CEF, 0x1CEE, 0x1CED, 0x1CE9, 0x1DA0, 0x1DAE, // pieces
					0x1CEC, 0x1CE5, 0x1CE2, 0x1CDD, 0x1AE4, 0x1DA1, 0x1DA2, 0x1DA4, 0x1DAF, 0x1DB0, 0x1DB1, 0x1DB2, // limbs
					0x1CE8, 0x1CE0, 0x1D9F, 0x1DAD // torsos
				};

			PackItem( new BodyPart( Utility.RandomList( list ) ) );
			PackItem( new BodyPart( Utility.RandomList( list ) ) );
			PackItem( new BodyPart( Utility.RandomList( list ) ) );
			PackItem( new BodyPart( Utility.RandomList( list ) ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager, 2 );
		}

		public override void OnGaveMeleeAttack( Mobile m )
		{
			base.OnGaveMeleeAttack( m );

			if ( 1 == Utility.RandomMinMax( 1, 20 ) )
			{
				Container cont = m.Backpack;
				Item iWrapped = Server.Items.HiddenTrap.GetMyItem( m );

				if ( iWrapped != null )
				{
					if ( Server.Items.HiddenTrap.IAmShielding( m, 120 ) || Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
					{
					}
					else if ( Server.Items.HiddenTrap.CheckInsuranceOnTrap( iWrapped, m ) == true )
					{
						m.LocalOverheadMessage(MessageType.Emote, 1150, true, "Viscera almost covered one of your protected items!");
					}
					else
					{
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "One of your items is covered in viscera!");
						m.PlaySound( 0x364 );
						Container box = new SlimeItem();
						box.DropItem(iWrapped);
						box.ItemID = iWrapped.ItemID;
						box.Hue = 0xB85;
						m.AddToBackpack ( box );
					}
				}
			}
		}

		public Viscera( Serial serial ) : base( serial )
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