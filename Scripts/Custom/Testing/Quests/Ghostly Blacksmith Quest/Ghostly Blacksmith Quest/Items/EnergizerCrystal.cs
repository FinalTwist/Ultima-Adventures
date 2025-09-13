/* This file was created with
Ilutzio's Questmaker. Enjoy! */
/* Created by Hammerhand */
using System;
using Server;
namespace Server.Items
{
public class EnergizerCrystal : Item
{
[Constructable]
    public EnergizerCrystal() : this(1)
{}
[Constructable]
public EnergizerCrystal( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
{}
[Constructable]

    ///////////The hexagon value ont he line below is the ItemID
public EnergizerCrystal( int amount ) : base( 7961 )
{


///////////Item name
Name = "Energizer Crystal";

///////////Item hue
Hue = 2953;

///////////Stackable
Stackable = false;

///////////Weight of one item
Weight = 0.01;
Amount = amount;

}
public EnergizerCrystal( Serial serial ) : base( serial )
{}
public override void Serialize( GenericWriter writer )
{
base.Serialize( writer );
writer.Write( (int) 0 ); // version
}
public override void Deserialize( GenericReader reader )
{
base.Deserialize( reader ); int version = reader.ReadInt(); }}}
