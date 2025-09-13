using System;using Server;namespace Server.Items
{
public class HellKey : Item
{
[Constructable]
public HellKey() : this( 1 )
{}
[Constructable]
public HellKey( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
{}
[Constructable]
public HellKey( int amount ) : base( 0x3B0C )
{
Stackable = false;
Weight = 0.01;
Amount = amount;
Name = "KeyOfHell";
Hue = 0x26;
}
public HellKey( Serial serial ) : base( serial )
{}
public override void Serialize( GenericWriter writer )
{
base.Serialize( writer );
writer.Write( (int) 0 ); // version
}
public override void Deserialize( GenericReader reader )
{
base.Deserialize( reader ); int version = reader.ReadInt(); }}}
