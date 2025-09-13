using System;using Server;namespace Server.Items
{
public class HellKey2 : Item
{
[Constructable]
public HellKey2() : this( 1 )
{}
[Constructable]
public HellKey2( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
{}
[Constructable]
public HellKey2( int amount ) : base( 0x3B0C )
{
Stackable = false;
Weight = 0.01;
Amount = amount;
Name = "HellKey2";
Hue = 0x1;
}
public HellKey2( Serial serial ) : base( serial )
{}
public override void Serialize( GenericWriter writer )
{
base.Serialize( writer );
writer.Write( (int) 0 ); // version
}
public override void Deserialize( GenericReader reader )
{
base.Deserialize( reader ); int version = reader.ReadInt(); }}}
