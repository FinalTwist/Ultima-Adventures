using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Engines.Quests;
// using Server.Engines.Quests.Hag;
using Server.Targeting;

namespace Server.Items
{
	[Flipable( 0x14F5, 0x14F6 )]
	public class SquireStatsSpyglass : Item
	{
		[Constructable]
		public SquireStatsSpyglass() : base( 0x14F5 )
		{
			Name = "Squire Inspection Spyglass";
			Weight = 3.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.Target = new SpyglassSTarget();
			from.SendMessage( "Who's stats do you wish to see?" );
		}
		
		private class SpyglassSTarget : Target
		{
			public SpyglassSTarget() : base( 15, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if ( targ != null && targ is Squire )
				{
					if ( ( ((BaseCreature)targ).Controlled && ((BaseCreature)targ).ControlMaster == from ) || from.AccessLevel >= AccessLevel.GameMaster || ((Squire)targ).m_Inspectable == true )
					{
						from.SendGump( new SquireLoreGump( ((BaseCreature)targ), from, SquireLorePage.Stats ) );
						from.SendMessage( "You check out " + ((BaseCreature)targ).Name + "'s stats." );
					}
					else
					{
						from.SendMessage( ((BaseCreature)targ).Name + " refuses to let you inspect them in this way." );
					}
				}
				else
				{
					from.SendMessage( "That cannot be inspected with this spyglass." );
				}
			}
		}

		public SquireStatsSpyglass( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}