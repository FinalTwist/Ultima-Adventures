using System;
using Server;


namespace Server.Items
{
	public class ArcaneShield : WoodenKiteShield
	{
		public override int LabelNumber{ get{ return 1061101; } } // Arcane Shield 
		

        public override int BaseColdResistance{ get{ return 7; } }
        public override int BaseFireResistance{ get{ return 7; } }
        public override int BaseEnergyResistance{ get{ return 7; } }
        public override int BasePoisonResistance{ get{ return 7; } }

		[Constructable]
		public ArcaneShield()
		{
			ItemID = 0x1B78;
			Hue = 0x556;
			Attributes.NightSight = 1;
			Attributes.SpellChanneling = 1;
			Attributes.DefendChance = 5;
			Attributes.CastSpeed = 1;
			Attributes.CastRecovery = 2;
			Attributes.BonusInt = 10;
			Attributes.RegenMana = 10;
			Attributes.LowerManaCost = 8;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public ArcaneShield( Serial serial ) : base( serial )
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


			if ( Attributes.NightSight == 0 )
				Attributes.NightSight = 1;
		}
	}
}