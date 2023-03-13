using System;
using Server;
using Server.Items;
 
namespace Server.Items
{
	public class SantasTimepiece : Clock
{
	private LightSource light;
	
	[Constructable]
	
	public SantasTimepiece() : base( 0x1086 )
	{
		Name = "Santa's Timepiece";
		Hue = 1151;
        Layer = Layer.Bracelet;
        LootType = LootType.Blessed;
        
	}
 
 
	public SantasTimepiece( Serial serial ) : base( serial )
	{
	}
	
	public override void OnAdded(IEntity parent )
		{
			light = new LightSource();
			light.Light = LightType.Circle300;
			if( parent is Mobile )
			{
				Mobile from = (Mobile)parent;
				from.AddItem( light );
			}
			base.OnAdded( parent );
		}

		public override void OnRemoved(IEntity parent )
		{
			if( light != null && parent is Mobile )
			{
				light.Delete();
			}
			base.OnRemoved( parent );
		
		}

 
	public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( light );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			light = reader.ReadItem() as LightSource;
		}
	}
}
 
