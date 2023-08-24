using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a scaker corpse" )]
	public class Scaker : Elephant
	{
		[Constructable]
		public Scaker()
		{
			if (Utility.RandomDouble() > 0.85)
				{
			Name = "a greater scaker";
			Body = 0x17;
			BaseSoundID = 229;
			Hue = 1009;

			SetStr( 326, 455 );
			SetDex( 81, 205 );
			SetInt( 16, 40 );

			SetHits( 276, 393 );

			SetDamage( 14, 29 );

			Fame = 3500;

			VirtualArmor = 35;

			ControlSlots = 2;
			MinTameSkill = 89.1;

		}
		else
		{
			Name = "a scaker";
			Body = 0x17;
			BaseSoundID = 229;
			Hue = 1009;

			SetStr( 326, 355 );
			SetDex( 81, 105 );
			SetInt( 16, 40 );

			SetHits( 276, 293 );

			SetDamage( 14, 20 );

			Fame = 3000;

			VirtualArmor = 35;

			ControlSlots = 2;
			MinTameSkill = 82.1;

		}
		}

		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay | FoodType.FruitsAndVegies; } }
		public override bool CanAngerOnTame { get {
			if (Utility.RandomDouble() > 0.90 )
				return true;
			return false; } }

		public override void OnAfterSpawn()
		{

		}
		
		        public override void OnDeath( Container c )
        {
            if (AdventuresFunctions.IsPuritain((object)this))
            {
                //set these for each mob
                double odds = 1; // 0 - 1 max (e.g. 0.50 is 50%)
                int amount = Utility.RandomMinMax(10, 10); // the min/max for the amount of the reg

                if (odds >= Utility.RandomDouble() && this.Backpack != null)
                {
                    //change the reg type here available are 
                    //RandomNecromancyReagent()
                    //RandomReagent()
                    //RandomMixerReagent()
                    //RandomMixerReagent()
                    //RandomHerbReagent()
                    Item reg = Loot.RandomReagent(); 
                    reg.Amount = amount;

                    if (reg != null)
                        this.Backpack.DropItem(reg);

                    Item reg2 = Loot.RandomMixerReagent(); 
                    reg2.Amount = amount;

                    if (reg2 != null)
                        this.Backpack.DropItem(reg2);
                }
            }


            base.OnDeath(c);
        }

		public Scaker( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}