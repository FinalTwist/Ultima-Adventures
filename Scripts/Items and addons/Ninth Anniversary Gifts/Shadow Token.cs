using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 

    namespace Server.Items 
    { 

    public class ShadowToken : Item 
    {
        //public override int LabelNumber { get { return 1076790; } }
    [Constructable] 
    public ShadowToken() : this( null ) 
    { 
    } 

    [Constructable]
        public ShadowToken(String name): base(13945)
    {
        Name = "Shadow Token";
        Stackable = false;
        Weight = 1.0;
        LootType = LootType.Blessed;
        
    }

        public ShadowToken(Serial serial)
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
        from.SendGump(new ShadowTokenGump(from, this)); 
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
