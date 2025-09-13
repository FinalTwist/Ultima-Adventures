// Created by Script Creator

using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( " corpse of a Fire Burden" )]
     public class FireBurden : FireElemental
    {
        [Constructable]
        public FireBurden() : base()
        {
            Name = "Fire Burden";
            Hue = 0;
            //Body = 15; // Uncomment these lines and input values
            //BaseSoundID = 838; // To use your own custom body and sound.
            SetStr( 1500 );
            SetDex( 1300 );
            SetInt( 1250 );
            SetHits( 55000 );
            SetDamage( 100, 125 );
            SetDamageType( ResistanceType.Physical, 25 );
            SetDamageType( ResistanceType.Cold, 25 );
            SetDamageType( ResistanceType.Fire, 25 );
            SetDamageType( ResistanceType.Energy, 25 );
            SetDamageType( ResistanceType.Poison, 25 );

            SetResistance( ResistanceType.Physical, 100 );
            SetResistance( ResistanceType.Cold, 50 );
            SetResistance( ResistanceType.Fire, 50 );
            SetResistance( ResistanceType.Energy, 50 );
            SetResistance( ResistanceType.Poison, 50 );
            Fame = 0;
            Karma = 0;
            VirtualArmor = 99;

            //switch ( Utility.Random( 30 ))
            //{
            //    case 0: PackItem( new EternalFlame() ); break;
           // }
        }
        public override bool HasBreath{ get{ return true ; } }
        public override bool AutoDispel{ get{ return true; } }
        public override bool BardImmune{ get{ return true; } }
        public override bool Unprovokable{ get{ return true; } }
        public override bool AlwaysMurderer{ get{ return true; } }

        public FireBurden( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int) 0 );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    }
}
