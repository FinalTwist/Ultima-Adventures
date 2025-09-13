using System;
using Server;


namespace Server.Items
{
	public class AcidProofRobe : MagicRobe
	{
		public DateTime TimeUsed;


		[CommandProperty(AccessLevel.Owner)]
		public DateTime Time_Used { get { return TimeUsed; } set { TimeUsed = value; InvalidateProperties(); } }


		[Constructable]
		public AcidProofRobe()
		{
			Name = "Acidic Robe";
			Hue = 1167;
			//Resistances.Fire = 20;
			Resistances.Poison = 70;
			ItemID = 0x1F03;
			SkillBonuses.SetValues( 0, SkillName.Poisoning, 30 );
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
            list.Add( 1049644, "Use To Fill Bottles With Acid");
        }


		public override void OnDoubleClick( Mobile from )
		{
			DateTime TimeNow = DateTime.UtcNow;
			long ticksThen = TimeUsed.Ticks;
			long ticksNow = TimeNow.Ticks;
			int minsThen = (int)TimeSpan.FromTicks(ticksThen).TotalMinutes;
			int minsNow = (int)TimeSpan.FromTicks(ticksNow).TotalMinutes;
			int CanFillBottle = 180 - ( minsNow - minsThen );


			if ( Parent != from )
			{
				from.SendMessage( "You must be wearing the robe to use it." );
			}
			else if ( CanFillBottle > 0 )
			{
				TimeSpan t = TimeSpan.FromMinutes( CanFillBottle );
				string wait = string.Format("{0:D1} hours and {1:D2} minutes", 
								t.Hours, 
								t.Minutes);
				from.SendMessage( "You can squeeze out acid in " + wait + "." );
			}
			else
			{
				if (!from.Backpack.ConsumeTotal(typeof(Bottle), 1))
				{
					from.SendMessage("You need an empty bottle to squeeze the acid into.");
				}
				else
				{
					from.PlaySound( 0x240 );
					from.AddToBackpack( new BottleOfAcid() );
					from.SendMessage( "You squeeze some acid from the cloth of the robe." );
					TimeUsed = DateTime.UtcNow;
				}
			}
		}


		public AcidProofRobe( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 );
            writer.Write( TimeUsed );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			TimeUsed = reader.ReadDateTime();
		}
	}
}
