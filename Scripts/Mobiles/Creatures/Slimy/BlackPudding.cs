using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a pile of black goo" )]
	public class BlackPudding : BaseCreature
	{
		[Constructable]
		public BlackPudding () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a black pudding";
			Body = 100;
			Hue = 0x497;

			SetStr( 62, 84 );
			SetDex( 56, 71 );
			SetInt( 56, 70 );

			SetHits( 70, 100 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 80 );
			SetResistance( ResistanceType.Cold, 0 );
			SetResistance( ResistanceType.Fire, 80 );
			SetResistance( ResistanceType.Poison, 80 );
			SetResistance( ResistanceType.Energy, 80 );

			SetSkill( SkillName.Poisoning, 36.0, 49.1 );
			SetSkill(SkillName.Anatomy, 0);
			SetSkill( SkillName.MagicResist, 15.9, 18.9 );
			SetSkill( SkillName.Tactics, 24.6, 26.1 );
			SetSkill( SkillName.Wrestling, 24.9, 26.1 );

			Fame = 900;
			Karma = -900;

			VirtualArmor = 12;

			int[] list = new int[]
				{
					0x1B11, 0x1B12, 0x1B13, 0x1B14, 0x1B15, 0x1B16, 0x1B19, 0x1B1A, // bone parts
					0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3, 0x1AE4, // skulls
					0x1B17, 0x1B18, 0x1B1B, 0x1B1C, // ribs and spines
					0x1B09, 0x1B0A, 0x1B0B, 0x1B0C, 0x1B0D, 0x1B0E, 0x1B0F, 0x1B10, // bone piles
					0xECA, 0xECB, 0xECC, 0xECD, 0xECE, 0xECF, 0xED0, 0xED1, 0xED2 // bones
				};

			PackItem( new BodyPart( Utility.RandomList( list ) ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems );
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
					if ( Server.Items.HiddenTrap.IAmShielding( m, 70 ) || Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
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
						box.Hue = this.Hue;
						m.AddToBackpack ( box );
					}
				}
			}
		}

        public override int GetAngerSound(){ return 0x581; }
        public override int GetIdleSound(){ return 0x582; }
        public override int GetAttackSound(){ return 0x580; }
        public override int GetHurtSound(){ return 0x583; }
        public override int GetDeathSound(){ return 0x584; }

		public override Poison PoisonImmune { get { return Poison.Regular; } }
		public override Poison HitPoison { get { return Poison.Regular; } }
		public override bool BleedImmune{ get{ return true; } }

		public BlackPudding( Serial serial ) : base( serial )
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