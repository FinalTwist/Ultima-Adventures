using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a bone demon corpse" )]
	public class BoneDemon : BaseCreature
	{
		[Constructable]
		public BoneDemon() : base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Name = "a bone demon";
			Body = 339;
			BaseSoundID = 0x48D;
			Hue = 0x80F;

			SetStr( 451, 575 );
			SetDex( 151, 175 );
			SetInt( 171, 220 );

			SetHits( 851, 975 );

			SetDamage( 26, 32 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 75 );
			SetResistance( ResistanceType.Fire, 60 );
			SetResistance( ResistanceType.Cold, 90 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 60 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.EvalInt, 77.6, 87.5 );
			SetSkill( SkillName.Magery, 77.6, 87.5 );
			SetSkill( SkillName.Meditation, 100.0 );
			SetSkill( SkillName.MagicResist, 50.1, 75.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 100.0 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 34;

			PackItem( new BoneContainer() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 5 );
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

		public override bool Unprovokable { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 1; } }

		public BoneDemon( Serial serial ) : base( serial )
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