using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a slitheran corpse" )]
	public class Slitheran : BaseCreature
	{
		[Constructable]
		public Slitheran() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a slitheran";
			Body = 286;
			BaseSoundID = 898;
			CanSwim = true;
			CantWalk = true;

			SetStr( 188, 220 );
			SetDex( 21, 70 );
			SetInt( 38, 57 );

			SetHits( 112, 153 );

			SetDamage( 8, 15 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Cold, 25 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 15, 30 );
			SetResistance( ResistanceType.Energy, 5, 15 );

			SetSkill( SkillName.Tactics, 50.1, 65.0 );
			SetSkill( SkillName.Wrestling, 50.1, 65.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 20;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override int Meat{ get{ return 2; } }
		public override MeatType MeatType{ get{ return MeatType.Fish; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int Hides{ get{ return 2; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override int Scales{ get{ return 1; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Blue; } }

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
						m.LocalOverheadMessage(MessageType.Emote, 1150, true, "Slime almost covered one of your protected items!");
					}
					else
					{
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "One of your items is covered in slime!");
						m.PlaySound( 0x364 );
						Container box = new SlimeItem();
						box.DropItem(iWrapped);
						box.ItemID = iWrapped.ItemID;
						box.Hue = 0xA06;
						m.AddToBackpack ( box );
					}
				}
			}
		}

		public Slitheran( Serial serial ) : base( serial )
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