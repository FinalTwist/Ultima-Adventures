/* This file was created with
Ilutzio's Questmaker. Enjoy! */
/* Created by Hammerhand */
using System;
using Server;
namespace Server.Items
{
public class SosarianOre : Item
{
[Constructable]
public SosarianOre() : this( 1 )
{}
[Constructable]
public SosarianOre( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
{}
[Constructable]


public SosarianOre( int amount ) : base( 6584 )
{



Name = "SosarianOre";


Hue = 55;


Stackable = false;


Weight = 0.01;
Amount = amount;

}
public SosarianOre( Serial serial ) : base( serial )
{}
public override void Serialize( GenericWriter writer )
{
base.Serialize( writer );
writer.Write( (int) 0 ); // version
}
public override void Deserialize( GenericReader reader )
{
base.Deserialize( reader ); int version = reader.ReadInt(); }}}
