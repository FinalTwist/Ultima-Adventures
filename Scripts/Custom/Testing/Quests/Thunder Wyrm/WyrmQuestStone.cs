using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 
using Server.Menus; 
using Server.Menus.Questions; 

namespace Server.Items 
{ 

   [FlipableAttribute( 0xEDC, 0xEDB )]
   public class WyrmQuestStone : Item 
   {
	[Constructable] 
	public WyrmQuestStone() : base( 3796 ) 
	{
		Hue = 1154;  
		Name = "Wyrm Soul Quest Stone";
		Movable = false;
		LootType = LootType.Blessed; 
	}
	public WyrmQuestStone( Serial serial ) : base( serial ) 
	{ 
	}
	public override void OnDoubleClick( Mobile from )

	{  
	if ( from.InRange( this.GetWorldLocation(), 2 ) ) 
	{ 
		from.SendGump( new WyrmQuestStoneGump( from ) ); 
	}
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
