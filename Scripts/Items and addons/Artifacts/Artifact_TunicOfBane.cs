using System;
using Server;


namespace Server.Items
{
	public class TunicOfBane : ChainChest
	{
		public override int LabelNumber{ get{ return 1061100; } } // Tunic of Bane


		public override int BasePoisonResistance{ get{ return 40; } }


		[Constructable]
		public TunicOfBane()
		{
			Name = "Tunic of Bane";
			Hue = 0x4F5;
			ArmorAttributes.DurabilityBonus = 100;
			this.HitPoints = this.MaxHitPoints = Utility.RandomMinMax(100, 125);//Cause the Durability bonus and such and the min/max hits as well as all other hits being whole #'s...
			Attributes.BonusStam = 15;
			Attributes.AttackChance = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public TunicOfBane( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.Write( (int) 2 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();


			if( version <= 1 )
			{
				if( this.HitPoints > 100 || this.MaxHitPoints > 100 )
					this.HitPoints = this.MaxHitPoints = 100;
			}


			if ( version < 1 )
			{
				if ( Hue == 0x559 )
					Hue = 0x4F5;


				if ( ArmorAttributes.DurabilityBonus == 0 )
					ArmorAttributes.DurabilityBonus = 100;


				PoisonBonus = 0;
			}
		}
	}
}