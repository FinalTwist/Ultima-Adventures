/* This file was created with
Ilutzio's Questmaker. Enjoy! */
/* Created by Hammerhand */
using System;
using Server;
namespace Server.Items
{
public class EnergizedSosarianIngots : Item
{
[Constructable]
public EnergizedSosarianIngots() : this( 1 )
{}
[Constructable]
public EnergizedSosarianIngots( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
{}
[Constructable]


public EnergizedSosarianIngots( int amount ) : base( 7155 )
{



Name = "Energized Sosarian Ingots";


Hue = 55;


Stackable = false;


Weight = 0.01;
Amount = amount;

}
    public EnergizedSosarianIngots(Serial serial)
        : base(serial)
{}
public override void Serialize( GenericWriter writer )
{
base.Serialize( writer );
writer.Write( (int) 0 ); // version
}
public override void Deserialize( GenericReader reader )
{
base.Deserialize( reader ); int version = reader.ReadInt(); }}}
