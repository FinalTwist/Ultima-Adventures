//Please leave credit
//Author XSlayerX
using System;
using Server;
using Server.Items;
using Server.Engines.BulkOrders;
using Server.Mobiles;
using System.Collections;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using System.Collections.Generic;
using Server.Multis;


namespace Server.Items
{
	public class BodRewardChest : Item
	{
	[Constructable]
	public BodRewardChest() : base( 0x9AB )
	
	{
		Name ="A Bod Reward Chest";
		Movable = false;
		Hue = 0x11;
		LootType = LootType.Blessed;
	}
	
				public BodRewardChest( Serial serial ) : base( serial )
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
				private static int[] m_Sounds = new int[]
			{
            0x1E0, 0x31E, 0x320, 0x324, 0x32F, 0x158, 0x42D, 0x542, 
            0x551, 0x554, 0x543, 0x556, 0x562, 0x545, 0x53C, 0x54E,
			0x54F, 0x550, 0x551, 0x558, 0x565, 0x58B, 0x5A7, 0x5A5
			};

				public override bool OnDragDrop( Mobile from, Item dropped )
	{
				Mobile m = from;
				PlayerMobile mobile = m as PlayerMobile;
				if ( mobile != null )
	{
	Item reward;
	int gold, fame;
	if (dropped is SmallSmithBOD && !((SmallSmithBOD)dropped).Complete)
	{

		return false;
	}
	else if ( dropped is SmallSmithBOD )
		{
		((SmallSmithBOD)dropped).GetRewards( out reward, out gold, out fame );

		from.AddToBackpack( new BankCheck( Utility.RandomMinMax( 100, 100 ) ) );
		Titles.AwardFame( from, fame, true );
		dropped.Delete();
		return true;
		}
	if ( dropped is LargeSmithBOD && !((LargeSmithBOD)dropped).Complete)
		{


			return false;
		}
	else if ( dropped is LargeSmithBOD )
		{
		((LargeSmithBOD)dropped).GetRewards( out reward, out gold, out fame );

		from.AddToBackpack( new BankCheck( Utility.RandomMinMax( 500, 500 ) ) );
		Titles.AwardFame( from, fame, true );
		dropped.Delete();
		return true;
		}

	    else if ( dropped is SmallTailorBOD)
		{
		((SmallTailorBOD)dropped).GetRewards( out reward, out gold, out fame );
		
	


		from.AddToBackpack( new BankCheck( Utility.RandomMinMax( 100, 100 ) ) );
		Titles.AwardFame( from, fame, true );
		dropped.Delete();
		return true;
		}
	if ( dropped is LargeTailorBOD && !((LargeTailorBOD)dropped).Complete)
		{
		
                   
                    

                    
		return false;
		}
	else if ( dropped is LargeTailorBOD )
		{
		((LargeTailorBOD)dropped).GetRewards( out reward, out gold, out fame );
		
		
					                   
		from.AddToBackpack( new BankCheck( Utility.RandomMinMax( 3000, 6000 ) ) );
		Titles.AwardFame( from, fame, true );
		dropped.Delete();
		return true;
		}	
	}
	return base.OnDragDrop( from, dropped );
		}
	}
}