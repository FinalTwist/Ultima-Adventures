using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using System.Collections;
using Server.Gumps;
using Server.Targeting;
using Server.Misc;
using Server.Accounting;
using System.Xml;
using Server.Mobiles; 

namespace Server.Items
{
	public class HeroDeed : Item
	{
		
		[Constructable]
		public HeroDeed() : base()
		{
			base.Weight = 0;
			base.Name = "a heroic deed";
		}		

		public override void AddNameProperty( ObjectPropertyList list )
		{
			base.AddNameProperty(list);

			list.Add("name a sarcophagus in the hall of heroes");
			list.Add("target the bottom-right corner of the sarcophagus you wish to change");
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.Target = new InternalTargets(from, this);
			}
			else
			{
				from.SendMessage("This needs to be in your backpack, silly.");
			}
		}
		
		public HeroDeed( Serial serial ) : base( serial )
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
	
		public class InternalTargets : Target
		{
			private Mobile m_From;
			private HeroDeed m_Deed;
			
			public InternalTargets( Mobile from, HeroDeed deed ) :  base ( 3, false, TargetFlags.None )
			{
				m_Deed = deed;
				m_From = from;
				from.SendMessage("Which sarcophagus do you want to name?");
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				
				if (m_Deed.IsChildOf( m_From.Backpack ) )
				{					
					if (targeted is StaticTarget)
					{
						StaticTarget targ = (StaticTarget)targeted;
						if (targ.ItemID != 7264 && targ.ItemID != 7335)
						{
							from.SendMessage("Target the bottom-right most tile of the sarcophagus.");
							return;
						}

						IPoint3D p = targeted as IPoint3D;

						Console.WriteLine(p.X + p.Y + p.Z);

						if (from.Map == null )
						{
							from.SendMessage("map is null");
							return;
						}

						if (targ.ItemID == 7264)
						{
							BaseAddon addon = new HeroSarcoWE(); 
							addon.Map = Map.Trammel;
							addon.MoveToWorld(new Point3D( p ), from.Map);
						}
						if (targ.ItemID == 7335)
						{
							BaseAddon addon = new HeroSarcoNS();
							addon.Map = Map.Trammel;
							addon.MoveToWorld(new Point3D( p), from.Map);
						}

						m_Deed.Delete();
					}
				}
				else{
					from.SendMessage("This needs to be in your backpack, silly.");
				}			
		}
	}
}
