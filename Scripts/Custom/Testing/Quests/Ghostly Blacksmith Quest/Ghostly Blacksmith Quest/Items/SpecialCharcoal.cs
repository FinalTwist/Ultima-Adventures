/* This file was created with
Ilutzio's Questmaker. Enjoy! */
/* Created by Hammerhand */
using System;
using Server;
namespace Server.Items
{
public class SpecialCharcoal : Item
{
[Constructable]
    public SpecialCharcoal(): this(1)
{}
[Constructable]
public SpecialCharcoal( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
{}
[Constructable]


public SpecialCharcoal( int amount ) : base( 3979 )
{



Name = "Special Charcoal";


Hue = 0;


Stackable = false;


Weight = 0.01;
Amount = amount;

}
public SpecialCharcoal( Serial serial ) : base( serial )
{}
public override void Serialize( GenericWriter writer )
{
base.Serialize( writer );
writer.Write( (int) 0 ); // version
}
public override void Deserialize( GenericReader reader )
{
base.Deserialize( reader ); int version = reader.ReadInt(); }}}
