using System;
using Server;
using Server.Items;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a cute fluffy bunny corpse" )]
	public class CuteFluffyBunny : BaseCreature
	{
		public override bool IsScaryToPets{ get{ return true; } }
		
		[Constructable]
		public CuteFluffyBunny() : base( AIType.AI_Animal, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a cute fluffy bunny";
			Body = 205;
			Hue = 2264;

			SetStr( 740, 1825 );
			SetDex( 186, 305 );
			SetInt( 436, 675 );

			SetHits( 660, 1295 );

			SetDamage( 16, 52 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 165 );
			SetResistance( ResistanceType.Fire, 40, 160 );
			SetResistance( ResistanceType.Cold, 30, 140 );
			SetResistance( ResistanceType.Poison, 85, 205 );
			SetResistance( ResistanceType.Energy, 35, 145 );

			SetSkill( SkillName.EvalInt, 130.1, 140.0 );
			SetSkill( SkillName.Magery, 140.1, 170.0 );
			SetSkill( SkillName.MagicResist, 130.1, 150.0 );
			SetSkill( SkillName.Tactics, 140.0, 150.0 );
			SetSkill( SkillName.Wrestling, 140.1, 255.5 );

			Fame = 10000;
			Karma = 10000;

			VirtualArmor = 69;

			Tamable = false;
			ControlSlots = 7;
			MinTameSkill = 218.9;
		}

		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich );
			AddLoot( LootPack.MedScrolls, 2 );
			AddLoot( LootPack.Gems, 5 ); 	
			if (Utility.Random(5) == 2) //Select random number between 0-4 and if it's 2 continue
				{
					switch ( Utility.Random( 6 )) //select random number between 0-46
					{
						case 0: AddToBackpack( new CuteFluffyBunnyShroud() ); break; 
						case 1: AddToBackpack( new CuteFluffyBunnyFeet() ); break;
						//case 2: AddToBackpack( new RingOfTheCuteFluffyBunny() ); break;
						case 3: AddToBackpack( new CuteFluffyBunnyDagger() ); break;
						case 4: AddToBackpack( new CuteFluffyBunnyEarrings() ); break;
						case 5: AddToBackpack( new Carrot() ); break;
//						case 6: AddToBackpack( new ITEMHERE() ); break;
//						case 7: AddToBackpack( new ITEMHERE() ); break;
//						case 8: AddToBackpack( new ITEMHERE() ); break;
						
					}
					SendMessage( "You got a goodie...."); 
				}
		}
		
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override bool Uncalmable{ get{ return true; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 5; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Greater; } }
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override bool AutoDispel{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies; } }  //it likes noobs better

		public CuteFluffyBunny(Serial serial) : base(serial)
		{
		}

		public override int GetAttackSound() 
		{ 
			return 0xC9; 
		} 

		public override int GetHurtSound() 
		{ 
			return 0xCA; 
		} 

		public override int GetDeathSound() 
		{ 
			return 0xCB; 
		} 

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}