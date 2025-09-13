using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 

    namespace Server.Items 
    { 

    public class CrystalToken : Item 
    {
        //public override int LabelNumber { get { return 1076790; } }
    [Constructable] 
    public CrystalToken() : this( null ) 
    { 
    } 

    [Constructable]
        public CrystalToken(String name): base(13946)
    {
        Name = "Crystal Token";
        Stackable = false;
        Weight = 1.0;
        LootType = LootType.Blessed;
        
    }

        public CrystalToken(Serial serial)
            : base(serial) 
    { 
    } 

    public override void OnDoubleClick( Mobile from ) 
    { 
    if ( !IsChildOf( from.Backpack ) ) 
    { 
    from.SendLocalizedMessage( 1042001 ); 
    } 
    else 
    {
        from.SendGump(new CrystalTokenGump(from, this)); 
    } 
    } 

    public override void Serialize ( GenericWriter writer) 
    { 
    base.Serialize ( writer ); 

    writer.Write ( (int) 0); 
    } 

    public override void Deserialize( GenericReader reader ) 
    { 
    base.Deserialize ( reader ); 

    int version = reader.ReadInt(); 
    } 
    } 
    }
