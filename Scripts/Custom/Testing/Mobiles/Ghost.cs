/// Created by: Espcevan
/// For RunUo 1.0
/// Finished: Sept 29, 2006
using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a ghosts corpse" )]
	public class Ghost : BaseCreature
	{
		[Constructable]
		public Ghost() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a ghost fighter";
			Body = 400;
			Hue = 1;

			SetStr( 150 );
			SetDex( 150 );
			SetInt( 150 );

			SetDamage( 16, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 110.0 );
			SetSkill( SkillName.Swords, 125.0 );
			SetSkill( SkillName.DetectHidden, 75.0 );


			SetResistance( ResistanceType.Physical, 100  );
			SetResistance( ResistanceType.Cold, 100 );
			SetResistance( ResistanceType.Fire, 100 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 100 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 32;

			HoodedShroudOfShadows shroud = new HoodedShroudOfShadows();
			shroud.Hue = 0;
			shroud.Movable = true;
			AddItem( shroud );

			Halberd weapon = new Halberd();
			weapon.Hue = 1;
			weapon.Movable = true;
			AddItem( weapon );

			LeatherChest chest = new LeatherChest();
			chest.Movable = true;
			AddItem( chest );

			LeatherLegs legs = new LeatherLegs();
			legs.Movable = true;
			AddItem( legs );

			LeatherArms arms = new LeatherArms();
			arms.Movable = true;
			AddItem( arms );

			LeatherGloves gloves = new LeatherGloves();
			gloves.Movable = true;
			AddItem( gloves );

			
			LeatherGorget gorget = new LeatherGorget();
			gorget.Movable = true;
			AddItem( gorget );

			ThighBoots boots = new ThighBoots();
			boots.Movable = true;
			AddItem( boots );
                }

		public override void OnDeath( Container c )
		{
		if(25 > Utility.Random( 100 ) )
		c.DropItem( new HoodedShroudOfShadows() );
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 10 );
			AddLoot( LootPack.Gems );
		}

		public Ghost( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}