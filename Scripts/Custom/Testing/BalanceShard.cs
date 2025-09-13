using System;
using Server;
using Server.Regions;
using Server.Targeting;
using Server.Mobiles;
using Server.Misc;
using Server.Engines.CannedEvil;

namespace Server.Items
{
	public class BalanceShard : Item
	{

		[Constructable]
		public BalanceShard( ) : base( 0x188C )
		{
			Name = "a balance shard";
			Weight = 5.0;
		}

		public override void OnDoubleClick( Mobile from )		
		{
			if ( IsChildOf( from.Backpack ) )
			{
                if (from is PlayerMobile && ((PlayerMobile)from).BalanceStatus != 0)
				{

					Map map = from.Map;
					Region reg = Region.Find( from.Location, from.Map );

					if ( map == null )
						return;

					if (reg.IsPartOf( typeof( VillageRegion )))
					{
						from.SendMessage( 0, "This Cannot be placed within city limits." );
						return;
					}
					else if ( reg.IsPartOf( typeof( SafeRegion )) || reg.IsPartOf( typeof ( PublicRegion )) || reg.IsPartOf( typeof ( PublicRegion )) || reg.IsPartOf( typeof( ChampionSpawnRegion ) ) || reg is ChampionSpawnRegion || reg.IsPartOf( typeof ( ProtectedRegion )) || reg.IsPartOf( typeof ( HouseRegion )) )
					{
						from.SendMessage( 0, "The portal doesn't seem to open here." );
						return;
					}
					else if (reg.IsPartOf( typeof( DungeonRegion )) && MyServerSettings.GetDifficultyLevel( from.Location, from.Map ) <= 1)
					{
						from.SendMessage( 0, "The portal wouldn't help in this type of dungeon." );
						return;
					}
					
						Item portal = null;

						if ( ((PlayerMobile)from).BalanceStatus > 0)
						{
							portal = new PortalGood(from);
							portal.MoveToWorld(from.Location, from.Map);
							this.Delete();

						}
							// set portal to good portal
						else if (((PlayerMobile)from).BalanceStatus < 0 )
						{
							portal = new PortalEvil(from);
							portal.MoveToWorld(from.Location, from.Map);
							this.Delete();
						}

					
            			
				}
                else
                    from.SendMessage("The shard does not respond as you are not pledged to the balance.");

			}
			else
			{
				from.SendMessage("This needs to be in your backpack.");
			}
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
		
			list.Add( "Use this item to place a portal, summoning those to help fight for your cause." ); 
			list.Add( "Cannot be placed in Cities or Safe Areas." ); 
	
		}


		public BalanceShard( Serial serial ) : base( serial )
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

			writer.Write( (int)0 ); // version
		}
	

	}
}
