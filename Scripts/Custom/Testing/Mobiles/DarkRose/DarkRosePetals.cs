/* Created by Hammerhand*/
using System;
using Server.Misc;

namespace Server.Items
{

	public class DarkRosePetals : Cloak 
	{
        public override int BaseFireResistance { get { return 20; } }
		
		[Constructable] 
		public DarkRosePetals()  
		{ 
			Name = "Petals of the Dark Rose";
            Hue = 2949;

            Attributes.RegenStam = 10;
            Attributes.RegenHits = 8;
            Attributes.RegenMana = 9;
		}

		public override void OnSingleClick( Mobile from ) 
		{ 
			this.LabelTo( from, Name ); 
		}

        public DarkRosePetals(Serial serial)
            : base(serial) 
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
