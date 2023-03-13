/* This file was created with
Ilutzio's Questmaker. Enjoy! */
/* Created by Hammerhand */
using System;
using Server;
namespace Server.Items
{
public class StarMetalFragments : Item
{
[Constructable]
    public StarMetalFragments(): this(1)
{}
[Constructable]
public StarMetalFragments( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
{}
[Constructable]

public StarMetalFragments( int amount ) : base( 3977 )
{



Name = "Star Metal Fragments";


Hue = 2413;


Stackable = false;


Weight = 0.01;
Amount = amount;

}
public StarMetalFragments( Serial serial ) : base( serial )
{}
public override void Serialize( GenericWriter writer )
{
base.Serialize( writer );
writer.Write( (int) 0 ); // version
}
public override void Deserialize( GenericReader reader )
{
base.Deserialize( reader ); int version = reader.ReadInt(); }}}
