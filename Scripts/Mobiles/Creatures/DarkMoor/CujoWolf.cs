using System;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;
using Server.Items;
using Server.ContextMenus;

namespace Server.Mobiles
{
	[CorpseName( "Cujo's Corpse" )]
	public class CujoWolf : BaseCreature
	{
		[Constructable]
		public CujoWolf() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{		
			Name = "Cujo";
			Body = 98;
			Hue = 0x502;
			BaseSoundID = 229;

			SetStr( 555, 865 );
			SetDex( 345, 655 );
			SetInt( 331, 643 );

			SetHits( 79000, 99000 );

			SetDamage( 245, 255 );

			SetDamageType( ResistanceType.Physical, 115 );

			SetResistance( ResistanceType.Physical, 150, 165 );
			SetResistance( ResistanceType.Fire, 145, 150);
			SetResistance( ResistanceType.Cold, 145, 150 );
			SetResistance( ResistanceType.Poison, 145, 150 );

			SetSkill( SkillName.EvalInt, 170.1, 185.0 );
			SetSkill( SkillName.Magery, 170.1, 185.0 );
			SetSkill( SkillName.MagicResist, 170.1, 185.0 );
			SetSkill( SkillName.Tactics, 150.1, 170.0 );
			SetSkill( SkillName.Wrestling, 140.1, 180.0 );


			Fame = 13500;
			Karma = 13500;

			VirtualArmor = 100;

			AddItem( new Gold( 7500, 10000) );

			//switch ( Utility.Random( 3 ))
			//{
			//	case 0: PackItem( new LargeGrandfatherClock() ); break;
			//	case 1: PackItem( new SmallGrandfatherClock() ); break;
			//	case 2: PackItem( new WhiteGrandfatherClock() ); break;
			//}

			if ( 0.025 > Utility.RandomDouble() )
				PackItem( new GargoylesPickaxe() );
		}

		public override void OnDeath( Container c )
        {
		    //switch ( Utility.Random( 5 ) ) //Rarity 10
            //{ 
			//    case 0: c.DropItem( new WizardsWalkingStick( ) );
            //    break; 
		   // }
            //base.OnDeath( c );
        }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.MedScrolls );
			AddLoot( LootPack.Gems, Utility.RandomMinMax( 1, 4 ) );
		}

		public override int TreasureMapLevel{ get{ return 1; } }
		public override int Meat{ get{ return 1; } }

		public CujoWolf( Serial serial ) : base( serial )
		{
		}
		
		public override void OnGaveMeleeAttack(Mobile defender)
		{
			Mobile from = (Mobile)this;
			int sound = 0; 	// 0x10E; 0x11D; 	0x0FC; 	0x205; 	0x1F;
			int hue = 0;	// 50; 	1160; 	2100; 	1166; 	120;
			int phys = 0;
			int fire = 0;
			int cold = 0;
			int pois = 0;
			int nrgy = 0;
			
			switch ( Utility.Random(6))
			{
				case 0:
					sound = 0x10E; hue = 50; phys = 100;
					break;
				case 1:
					sound = 0x11D; hue = 1160; fire = 100;
					break;
				case 2:
					sound = 0x0FC; hue = 2100; cold = 100;
					break;
				case 3:
					sound = 0x205; hue = 1166; pois = 100;
					break;
				case 4:
					sound = 0x1F; hue = 120; nrgy = 100;
					break;
				case 5:
					sound = 0x10E; hue = 50; phys = 20; fire = 20; cold = 20; pois = 20; nrgy = 20;
					break;
			}
			
			Map map = from.Map;

			if ( map == null )
				return;

			List<Mobile> list = new List<Mobile>();

			foreach ( Mobile m in from.GetMobilesInRange( 10 ) )
			{
				if ( from != m && defender != m && SpellHelper.ValidIndirectTarget( from, m ) && from.CanBeHarmful( m, false ) && from.InLOS( m ) )
					list.Add( m );
			}

			if ( list.Count == 0 )
				return;

			Effects.PlaySound( from.Location, map, sound );

			// TODO: What is the damage calculation?

			for ( int i = 0; i < list.Count; ++i )
			{
				Mobile m = list[i];

				double scalar = (11 - from.GetDistanceToSqrt( m )) / 10;

				if ( scalar > 1.0 )
					scalar = 1.0;
				else if ( scalar < 0.0 )
					continue;

				from.DoHarmful( m, true );
				m.FixedEffect( 0x3779, 1, 15, hue, 0 );
				AOS.Damage( m, from, (int)(50 * scalar), phys, fire, cold, pois, nrgy );
			}
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