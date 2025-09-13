using System;
using Server;


namespace Server.Items
{
	public class CandleCold : MagicCandle
	{
		[Constructable]
		public CandleCold()
		{
			Hue = 0x48D;
			Name = "Candle of Cold Light";
			Resistances.Cold = 60;
			Attributes.BonusHits = 20;
			Attributes.BonusStam = 20;
			Attributes.BonusMana = 20;
			Attributes.Luck = 200;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        } 


		public CandleCold( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class CandleFire : MagicCandle
	{
		[Constructable]
		public CandleFire()
		{
			Hue = 0x48E;
			Name = "Candle of Fire Light";
			Resistances.Fire = 60;
			Attributes.BonusHits = 20;
			Attributes.BonusStam = 20;
			Attributes.BonusMana = 20;
			Attributes.Luck = 200;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        } 


		public CandleFire( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class CandlePoison : MagicCandle
	{
		[Constructable]
		public CandlePoison()
		{
			Hue = 0x48F;
			Name = "Candle of Poisonous Light";
			Resistances.Poison = 60;
			Attributes.BonusHits = 20;
			Attributes.BonusStam = 20;
			Attributes.BonusMana = 20;
			Attributes.Luck = 200;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        } 


		public CandlePoison( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class CandleEnergy : MagicCandle
	{
		[Constructable]
		public CandleEnergy()
		{
			Hue = 0x490;
			Name = "Candle of Energized Light";
			Resistances.Energy = 60;
			Attributes.BonusHits = 20;
			Attributes.BonusStam = 20;
			Attributes.BonusMana = 20;
			Attributes.Luck = 200;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        } 


		public CandleEnergy( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class CandleWizard : MagicCandle
	{
		[Constructable]
		public CandleWizard()
		{
			Hue = 0xB96;
			Name = "Candle of Wizardly Light";
			SkillBonuses.SetValues( 0, SkillName.Magery, 15 );
			SkillBonuses.SetValues( 1, SkillName.Meditation, 10 );
			SkillBonuses.SetValues( 2, SkillName.EvalInt, 15 );
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 1;
			Attributes.LowerManaCost = 25;
			Attributes.LowerRegCost = 25;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        } 


		public CandleWizard( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class CandleNecromancer : MagicCandle
	{
		[Constructable]
		public CandleNecromancer()
		{
			Hue = 0x47E;
			Name = "Candle of Ghostly Light";
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 2;
			Attributes.LowerManaCost = 35;
			Attributes.LowerRegCost = 35;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        } 


		public CandleNecromancer( Serial serial ) : base( serial )
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