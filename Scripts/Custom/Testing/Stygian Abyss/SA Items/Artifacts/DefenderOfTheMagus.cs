using System;
using Server;

namespace Server.Items
{
	public class DefenderOfTheMagus : MetalShield
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 1; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 0; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public DefenderOfTheMagus() 
		{
			Name = ("Defender Of The Magus");
		
			Hue = 590;
			

			Attributes.SpellChanneling = 1;
			Attributes.DefendChance = 10;				
			Attributes.CastRecovery = 1;
			
			//TODO: Random Resonance, Random Resistance
			
					
			
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public DefenderOfTheMagus( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
			
		}
	}
}
