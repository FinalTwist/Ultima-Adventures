using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a skeletal corpse" )]
	public class GiantSkeleton : BaseCreature
	{
		[Constructable]
		public GiantSkeleton() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "giant" );
			Title = "the skeletal giant";
			Body = 308;
			BaseSoundID = 0x4FB;

			SetStr( 336, 385 );
			SetDex( 96, 115 );
			SetInt( 31, 55 );

			SetHits( 202, 231 );
			SetMana( 0 );

			SetDamage( 7, 23 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 60.3, 105.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 48;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
						MyChest.Name = "bone carved chest";
						MyChest.ItemID = Utility.RandomList( 0x2DF1, 0x2DF1 );
						MyChest.Hue = 0;
						c.DropItem( MyChest );
					}
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						switch( Utility.RandomMinMax( 0, 5 ) )
						{
							case 0: MagicBoneLegs leg = new MagicBoneLegs();		BaseRunicTool.ApplyAttributesTo( (BaseArmor)leg, false, 1000, 5, 25, 100 );	c.DropItem( leg );		break;
							case 1: MagicBoneGloves glv = new MagicBoneGloves();	BaseRunicTool.ApplyAttributesTo( (BaseArmor)glv, false, 1000, 5, 25, 100 );	c.DropItem( glv );		break;
							case 2: MagicBoneArms arm = new MagicBoneArms();		BaseRunicTool.ApplyAttributesTo( (BaseArmor)arm, false, 1000, 5, 25, 100 );	c.DropItem( arm );		break;
							case 3: MagicBoneChest tun = new MagicBoneChest();		BaseRunicTool.ApplyAttributesTo( (BaseArmor)tun, false, 1000, 5, 25, 100 );	c.DropItem( tun );		break;
							case 4: MagicBoneHelm hlm = new MagicBoneHelm();		BaseRunicTool.ApplyAttributesTo( (BaseArmor)hlm, false, 1000, 5, 25, 100 );	c.DropItem( hlm );		break;
							case 5: MagicBoneSkirt skt = new MagicBoneSkirt();		BaseRunicTool.ApplyAttributesTo( (BaseArmor)skt, false, 1000, 5, 25, 100 );	c.DropItem( skt );		break;
						}
					}
				}
			}
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 3; } }

		public GiantSkeleton( Serial serial ) : base( serial )
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