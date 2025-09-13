using System;
using Server.Network;
using Server.Prompts;
using Server.Targeting;
using Server.Items;



namespace Server.Items
{

public class OneHandedDeed : Item
{
[Constructable]public OneHandedDeed() : base( 0x14F0 )
{
	Weight = 1.0;
	//LootType = LootType.Blessed;
	Hue = 1161;
	Name = "One Hander deed";
}
public OneHandedDeed( Serial serial ) : base( serial )
{}
public override void Serialize( GenericWriter writer )
{
	base.Serialize( writer );
	writer.Write( (int) 0 );
}
public override void Deserialize( GenericReader reader )
{
	base.Deserialize( reader );
	int version = reader.ReadInt();
}
public override void OnDoubleClick( Mobile from )
{
	if ( !IsChildOf( from.Backpack ) )
	{
		from.SendLocalizedMessage( 1042001 );
	}
	else
	{
		from.Target = new OneHandedDeedT(this);
	}
}
}

public class OneHandedDeedT : Target
{
	private OneHandedDeed m_GToO;
	
	public OneHandedDeedT ( OneHandedDeed GToO ) : base( 1, false, TargetFlags.None )
	{
		m_GToO = GToO;
	}
	
		protected override void OnTarget( Mobile from, object target )
	{
Item selx = from.Backpack.FindItemByType( typeof( OneHandedDeed ) );
		Item GNS = from.Backpack.FindItemByType( typeof( OneHandedDeed ) );
		if(target is BaseWeapon)
		{
			Item needNS = (Item)target;
			BaseWeapon weapon = target as BaseWeapon;
			if( needNS.RootParent == from )
			{
				weapon.Layer = Layer.OneHanded;
				selx.Delete();
				from.SendMessage( 38,"the weapon is now one handed");
			}				
			else
				from.SendMessage( 38,"It should be in your backpack");
		}
		else
			from.SendMessage( 38,"Can only enhance Weapons");
	}
}

}

