using System;using Server;namespace Server.Items
{
public class HeadOfMondain : Item
{
[Constructable]
public HeadOfMondain() : this( 1 )
{}
[Constructable]
public HeadOfMondain( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
{}
[Constructable]

///////////The hexagon value ont he line below is the ItemID
public HeadOfMondain( int amount ) : base( 0x1DA0 )
{


///////////Item name
Name = "Head Of Mondain";

///////////Item hue
Hue = 0x0;

///////////Stackable
Stackable = false;

///////////Weight of one item
Weight = 0.01;
Amount = amount;

}
public HeadOfMondain( Serial serial ) : base( serial )
{}
public override void Serialize( GenericWriter writer )
{
base.Serialize( writer );
writer.Write( (int) 0 ); // version
}
public override void Deserialize( GenericReader reader )
{
base.Deserialize( reader ); int version = reader.ReadInt(); }}}
