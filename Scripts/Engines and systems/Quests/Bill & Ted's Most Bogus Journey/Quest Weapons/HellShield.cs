
using System;
using Server;

namespace Server.Items
{
	public class HellShield : BronzeShield  // your name of weapon(no spaces) : name of the base weapon you want to use. ie:Katana (make sure you capitalize both names)
	{
		public override int ArtifactRarity{ get{ return 666; } } 

		public override int InitMinHits{ get{ return 25; } } // Set the minium amount of hit points for the weapon.
		public override int InitMaxHits{ get{ return 100; } } // Set the Maxium amount of hit points for the weapon.

		[Constructable]
		public HellShield() // your name of weapon(no spaces)
		{
			Weight = 10.0; // Weight in stones for your weapon
            		Name = "The Excellent Shield";  // Name of your weapon with spaces.
            		Hue = 2117;     // The color of your weapon.

			

			Attributes.AttackChance = 10;
			Attributes.BonusDex = 10;
			//Attributes.BonusHits = 20;
			//Attributes.BonusInt = 10;
			//Attributes.BonusMana = 10;
			Attributes.BonusStam = 15;
			Attributes.BonusStr = 5;
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 20;
			//Attributes.EnhancePotions = nn;
			//Attributes.LowerManaCost = 10;
			//Attributes.LowerRegCost = 15;
			Attributes.Luck = 200;
			//Attributes.Nightsight = 1;
			Attributes.ReflectPhysical = 10;
			//Attributes.RegenHits = nn;
			//Attributes.RegenMana = nn;
			Attributes.RegenStam = 10;
			Attributes.SpellChanneling = 1;
			//Attributes.SpellDamage = 10;
			//Attributes.WeaponDamage = 0;
			Attributes.WeaponSpeed = 20;


			//LootType = LootType.Blessed; //Blessed, Newbied or Cursed
		}

		public HellShield( Serial serial ) : base( serial ) // your name of weapon(no spaces)
		{
		}

		public override void Serialize( GenericWriter writer )
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