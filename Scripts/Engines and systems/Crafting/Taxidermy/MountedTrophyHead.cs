using System;
using Server.Network;
using Server.Items;
using Server;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
    public class MountedTrophyHead : Item
	{
		public string AnimalKiller;
		public string AnimalWhere;

		[CommandProperty(AccessLevel.Owner)]
		public string Animal_Killer { get { return AnimalKiller; } set { AnimalKiller = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Animal_Where { get { return AnimalWhere; } set { AnimalWhere = value; InvalidateProperties(); } }

        [Constructable]
        public MountedTrophyHead() : base( 0x1E60 )
		{
            Name = "mounted head";
			Weight = 1.0;
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( AnimalWhere != "" && AnimalWhere != null ){ list.Add( 1070722, AnimalWhere ); }
			if ( AnimalKiller != "" && AnimalKiller != null ){ list.Add( 1049644, AnimalKiller ); }
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to flip." );
				return;
			}
			else
			{
				if ( this.ItemID == 0x3158 ){ this.ItemID = 0x3159; }
				else if ( this.ItemID == 0x3159 ){ this.ItemID = 0x3158; }
				else if ( this.ItemID == 0x3912 ){ this.ItemID = 0x3913; }
				else if ( this.ItemID == 0x3913 ){ this.ItemID = 0x3912; }
				else if ( this.ItemID == 0x3944 ){ this.ItemID = 0x3945; }
				else if ( this.ItemID == 0x3945 ){ this.ItemID = 0x3944; }
				else if ( this.ItemID == 0x2A79 ){ this.ItemID = 0x2A7A; }
				else if ( this.ItemID == 0x2A7A ){ this.ItemID = 0x2A79; }
				else if ( this.ItemID == 0x2A75 ){ this.ItemID = 0x2A76; }
				else if ( this.ItemID == 0x2A76 ){ this.ItemID = 0x2A75; }
				else if ( this.ItemID == 0x2A71 ){ this.ItemID = 0x2A72; }
				else if ( this.ItemID == 0x2A72 ){ this.ItemID = 0x2A71; }
				else if ( this.ItemID == 0x2A77 ){ this.ItemID = 0x2A78; }
				else if ( this.ItemID == 0x2A78 ){ this.ItemID = 0x2A77; }
				else if ( this.ItemID == 0x2A73 ){ this.ItemID = 0x2A74; }
				else if ( this.ItemID == 0x2A74 ){ this.ItemID = 0x2A73; }
				else if ( this.ItemID == 0x1E6A ){ this.ItemID = 0x1E63; }
				else if ( this.ItemID == 0x1E63 ){ this.ItemID = 0x1E6A; }
				else if ( this.ItemID == 0x1E68 ){ this.ItemID = 0x1E61; }
				else if ( this.ItemID == 0x1E61 ){ this.ItemID = 0x1E68; }
				else if ( this.ItemID == 0x1E6B ){ this.ItemID = 0x1E64; }
				else if ( this.ItemID == 0x1E64 ){ this.ItemID = 0x1E6B; }
				else if ( this.ItemID == 0x1E6D ){ this.ItemID = 0x1E66; }
				else if ( this.ItemID == 0x1E66 ){ this.ItemID = 0x1E6D; }
				else if ( this.ItemID == 0x1E6C ){ this.ItemID = 0x1E65; }
				else if ( this.ItemID == 0x1E65 ){ this.ItemID = 0x1E6C; }
				else if ( this.ItemID == 0x1E67 ){ this.ItemID = 0x1E60; }
				else if ( this.ItemID == 0x1E60 ){ this.ItemID = 0x1E67; }
				else if ( this.ItemID == 0x392D ){ this.ItemID = 0x392C; } // black daemon
				else if ( this.ItemID == 0x392C ){ this.ItemID = 0x392D; } // black daemon
				else if ( this.ItemID == 0x3933 ){ this.ItemID = 0x3932; }
				else if ( this.ItemID == 0x3932 ){ this.ItemID = 0x3933; }
				else if ( this.ItemID == 0x393D ){ this.ItemID = 0x393C; }
				else if ( this.ItemID == 0x393C ){ this.ItemID = 0x393D; }
				else if ( this.ItemID == 0x3931 ){ this.ItemID = 0x3930; }
				else if ( this.ItemID == 0x3930 ){ this.ItemID = 0x3931; }
				else if ( this.ItemID == 0x3935 ){ this.ItemID = 0x3934; }
				else if ( this.ItemID == 0x3934 ){ this.ItemID = 0x3935; }
				else if ( this.ItemID == 0x392B ){ this.ItemID = 0x392A; }
				else if ( this.ItemID == 0x392A ){ this.ItemID = 0x392B; }
				else if ( this.ItemID == 0x3937 ){ this.ItemID = 0x3936; }
				else if ( this.ItemID == 0x3936 ){ this.ItemID = 0x3937; }
				else if ( this.ItemID == 0x393F ){ this.ItemID = 0x393E; }
				else if ( this.ItemID == 0x393E ){ this.ItemID = 0x393F; }
				else if ( this.ItemID == 0x392F ){ this.ItemID = 0x392E; }
				else if ( this.ItemID == 0x392E ){ this.ItemID = 0x392F; }
				else if ( this.ItemID == 0x393B ){ this.ItemID = 0x393A; }
				else if ( this.ItemID == 0x393A ){ this.ItemID = 0x393B; }
				else if ( this.ItemID == 0x2235 ){ this.ItemID = 0x2234; }
				else if ( this.ItemID == 0x2234 ){ this.ItemID = 0x2235; }
				else if ( this.ItemID == 0x21FB ){ this.ItemID = 0x21FA; }
				else if ( this.ItemID == 0x21FA ){ this.ItemID = 0x21FB; }
				else if ( this.ItemID == 0x270D ){ this.ItemID = 0x270E; }
				else if ( this.ItemID == 0x270E ){ this.ItemID = 0x270D; }
				else if ( this.ItemID == 0x21F9 ){ this.ItemID = 0x21F8; }
				else if ( this.ItemID == 0x21F8 ){ this.ItemID = 0x21F9; }
				else if ( this.ItemID == 0x21F7 ){ this.ItemID = 0x21F6; }
				else if ( this.ItemID == 0x21F6 ){ this.ItemID = 0x21F7; }
				else if ( this.ItemID == 0x44E8 ){ this.ItemID = 0x44E7; }
				else if ( this.ItemID == 0x44E7 ){ this.ItemID = 0x44E8; }
				else if ( this.ItemID == 0x1E69 ){ this.ItemID = 0x1E62; }
				else if ( this.ItemID == 0x1E62 ){ this.ItemID = 0x1E69; }
				else if ( this.ItemID == 0x3352 ){ this.ItemID = 0x3353; } //mounted abyss giant
				else if ( this.ItemID == 0x3353 ){ this.ItemID = 0x3352; } //mounted abyss giant
				else if ( this.ItemID == 0x3354 ){ this.ItemID = 0x3355; } //mounted abyss ogre
				else if ( this.ItemID == 0x3355 ){ this.ItemID = 0x3354; } //mounted abyss ogre
				else if ( this.ItemID == 0x3356 ){ this.ItemID = 0x3357; } //mounted ancient cyclops
				else if ( this.ItemID == 0x3357 ){ this.ItemID = 0x3356; } //mounted ancient cyclops
				else if ( this.ItemID == 0x3358 ){ this.ItemID = 0x3359; } //mounted ancient drake
				else if ( this.ItemID == 0x3359 ){ this.ItemID = 0x3358; } //mounted ancient drake
				else if ( this.ItemID == 0x335A ){ this.ItemID = 0x335B; } //mounted cerberus
				else if ( this.ItemID == 0x335B ){ this.ItemID = 0x335A; } //mounted cerberus
				else if ( this.ItemID == 0x335C ){ this.ItemID = 0x335D; } //mounted storm giant
				else if ( this.ItemID == 0x335D ){ this.ItemID = 0x335C; } //mounted storm giant
				else if ( this.ItemID == 0x335E ){ this.ItemID = 0x335F; } //mounted dark unicorn
				else if ( this.ItemID == 0x335F ){ this.ItemID = 0x335E; } //mounted dark unicorn
				else if ( this.ItemID == 0x3360 ){ this.ItemID = 0x3361; } //mounted deep sea giant
				else if ( this.ItemID == 0x3361 ){ this.ItemID = 0x3360; } //mounted deep sea giant
				else if ( this.ItemID == 0x3362 ){ this.ItemID = 0x3363; } //mounted dinosaur
				else if ( this.ItemID == 0x3363 ){ this.ItemID = 0x3362; } //mounted dinosaur
				else if ( this.ItemID == 0x3364 ){ this.ItemID = 0x3365; } //mounted dracolich
				else if ( this.ItemID == 0x3365 ){ this.ItemID = 0x3364; } //mounted dracolich
				else if ( this.ItemID == 0x3366 ){ this.ItemID = 0x3367; } //mounted dragon turtle
				else if ( this.ItemID == 0x3367 ){ this.ItemID = 0x3366; } //mounted dragon turtle
				else if ( this.ItemID == 0x33FD ){ this.ItemID = 0x33FE; } //mounted wyrm
				else if ( this.ItemID == 0x33FE ){ this.ItemID = 0x33FD; } //mounted wyrm
				else if ( this.ItemID == 0x3368 ){ this.ItemID = 0x3369; } //mounted drake
				else if ( this.ItemID == 0x3369 ){ this.ItemID = 0x3368; } //mounted drake
				else if ( this.ItemID == 0x336A ){ this.ItemID = 0x336B; } //mounted flesh golem
				else if ( this.ItemID == 0x336B ){ this.ItemID = 0x336A; } //mounted flesh golem
				else if ( this.ItemID == 0x336C ){ this.ItemID = 0x336D; } //mounted forest giant
				else if ( this.ItemID == 0x336D ){ this.ItemID = 0x336C; } //mounted forest giant
				else if ( this.ItemID == 0x336E ){ this.ItemID = 0x336F; } //mounted frost giant
				else if ( this.ItemID == 0x336F ){ this.ItemID = 0x336E; } //mounted frost giant
				else if ( this.ItemID == 0x3370 ){ this.ItemID = 0x3371; } //mounted griffon
				else if ( this.ItemID == 0x3371 ){ this.ItemID = 0x3370; } //mounted griffon
				else if ( this.ItemID == 0x3372 ){ this.ItemID = 0x3373; } //mounted hydra
				else if ( this.ItemID == 0x3373 ){ this.ItemID = 0x3372; } //mounted hydra
				else if ( this.ItemID == 0x3374 ){ this.ItemID = 0x3375; } //mounted jungle giant
				else if ( this.ItemID == 0x3375 ){ this.ItemID = 0x3374; } //mounted jungle giant
				else if ( this.ItemID == 0x3376 ){ this.ItemID = 0x3377; } //mounted lion
				else if ( this.ItemID == 0x3377 ){ this.ItemID = 0x3376; } //mounted lion
				else if ( this.ItemID == 0x3378 ){ this.ItemID = 0x3379; } //mounted ogre lord
				else if ( this.ItemID == 0x3379 ){ this.ItemID = 0x3378; } //mounted ogre lord
				else if ( this.ItemID == 0x337B ){ this.ItemID = 0x337C; } //mounted owlbear
				else if ( this.ItemID == 0x337C ){ this.ItemID = 0x337B; } //mounted owlbear
				else if ( this.ItemID == 0x337D ){ this.ItemID = 0x337E; } //mounted sea daemon
				else if ( this.ItemID == 0x337E ){ this.ItemID = 0x337D; } //mounted sea daemon
				else if ( this.ItemID == 0x337F ){ this.ItemID = 0x3380; } //mounted sea dragon
				else if ( this.ItemID == 0x3380 ){ this.ItemID = 0x337F; } //mounted sea dragon
				else if ( this.ItemID == 0x3381 ){ this.ItemID = 0x3382; } //mounted sea drake
				else if ( this.ItemID == 0x3382 ){ this.ItemID = 0x3381; } //mounted sea drake
				else if ( this.ItemID == 0x3383 ){ this.ItemID = 0x3384; } //mounted sea giant
				else if ( this.ItemID == 0x3384 ){ this.ItemID = 0x3383; } //mounted sea giant
				else if ( this.ItemID == 0x3385 ){ this.ItemID = 0x3386; } //mounted swamp drake
				else if ( this.ItemID == 0x3386 ){ this.ItemID = 0x3385; } //mounted swamp drake
				else if ( this.ItemID == 0x3387 ){ this.ItemID = 0x3388; } //mounted swamp thing
				else if ( this.ItemID == 0x3388 ){ this.ItemID = 0x3387; } //mounted swamp thing
				else if ( this.ItemID == 0x3389 ){ this.ItemID = 0x338A; } //mounted tiger
				else if ( this.ItemID == 0x338A ){ this.ItemID = 0x3389; } //mounted tiger
				else if ( this.ItemID == 0x338B ){ this.ItemID = 0x338C; } //mounted earth titan
				else if ( this.ItemID == 0x338C ){ this.ItemID = 0x338B; } //mounted earth titan
				else if ( this.ItemID == 0x338D ){ this.ItemID = 0x338E; } //mounted fire titan
				else if ( this.ItemID == 0x338E ){ this.ItemID = 0x338D; } //mounted fire titan
				else if ( this.ItemID == 0x338F ){ this.ItemID = 0x3390; } //mounted water titan
				else if ( this.ItemID == 0x3390 ){ this.ItemID = 0x338F; } //mounted water titan
				else if ( this.ItemID == 0x3391 ){ this.ItemID = 0x3392; } //mounted tyranasaur
				else if ( this.ItemID == 0x3392 ){ this.ItemID = 0x3391; } //mounted tyranasaur
				else if ( this.ItemID == 0x3393 ){ this.ItemID = 0x3394; } //mounted stegosaurus
				else if ( this.ItemID == 0x3394 ){ this.ItemID = 0x3393; } //mounted stegosaurus
				else if ( this.ItemID == 0x3395 ){ this.ItemID = 0x3396; } //mounted alien
				else if ( this.ItemID == 0x3396 ){ this.ItemID = 0x3395; } //mounted alien
				else if ( this.ItemID == 0x3397 ){ this.ItemID = 0x3398; } //mounted wyvern
				else if ( this.ItemID == 0x3398 ){ this.ItemID = 0x3397; } //mounted wyvern
				else if ( this.ItemID == 0x3399 ){ this.ItemID = 0x339A; } //mounted black bear
				else if ( this.ItemID == 0x339A ){ this.ItemID = 0x3399; } //mounted black bear
				else if ( this.ItemID == 0x339B ){ this.ItemID = 0x339C; } //mounted brown bear
				else if ( this.ItemID == 0x339C ){ this.ItemID = 0x339B; } //mounted brown bear
				else if ( this.ItemID == 0x339D ){ this.ItemID = 0x339E; } //mounted cave bear
				else if ( this.ItemID == 0x339E ){ this.ItemID = 0x339D; } //mounted cave bear
				else if ( this.ItemID == 0x339F ){ this.ItemID = 0x33A0; } //mounted polar bear
				else if ( this.ItemID == 0x33A0 ){ this.ItemID = 0x339F; } //mounted polar bear
				else if ( this.ItemID == 0x33A1 ){ this.ItemID = 0x33A2; } //mounted daemon
				else if ( this.ItemID == 0x33A2 ){ this.ItemID = 0x33A1; } //mounted daemon
				else if ( this.ItemID == 0x567F ){ this.ItemID = 0x5680; } //mounted daemon
				else if ( this.ItemID == 0x5680 ){ this.ItemID = 0x567F; } //mounted daemon
				else if ( this.ItemID == 0x5681 ){ this.ItemID = 0x5682; } //mounted balron
				else if ( this.ItemID == 0x5682 ){ this.ItemID = 0x5681; } //mounted balron
				else if ( this.ItemID == 0x33A3 ){ this.ItemID = 0x33A4; } //mounted dragon
				else if ( this.ItemID == 0x33A4 ){ this.ItemID = 0x33A3; } //mounted dragon
				else if ( this.ItemID == 0x33A5 ){ this.ItemID = 0x33A6; } //mounted dragon
				else if ( this.ItemID == 0x33A6 ){ this.ItemID = 0x33A5; } //mounted dragon
				else if ( this.ItemID == 0x33A7 ){ this.ItemID = 0x33A8; } //mounted ettin mage
				else if ( this.ItemID == 0x33A8 ){ this.ItemID = 0x33A7; } //mounted ettin mage
				else if ( this.ItemID == 0x33A9 ){ this.ItemID = 0x33AA; } //mounted hill giant
				else if ( this.ItemID == 0x33AA ){ this.ItemID = 0x33A9; } //mounted hill giant
				else if ( this.ItemID == 0x33AB ){ this.ItemID = 0x33AC; } //mounted lizardman
				else if ( this.ItemID == 0x33AC ){ this.ItemID = 0x33AB; } //mounted lizardman
				else if ( this.ItemID == 0x33AD ){ this.ItemID = 0x33AE; } //mounted nightmare
				else if ( this.ItemID == 0x33AE ){ this.ItemID = 0x33AD; } //mounted nightmare
				else if ( this.ItemID == 0x33AF ){ this.ItemID = 0x33B0; } //mounted satan
				else if ( this.ItemID == 0x33B0 ){ this.ItemID = 0x33AF; } //mounted satan
				else if ( this.ItemID == 0x33B1 ){ this.ItemID = 0x33B2; } //mounted unicorn
				else if ( this.ItemID == 0x33B2 ){ this.ItemID = 0x33B1; } //mounted unicorn
				else if ( this.ItemID == 0x33B3 ){ this.ItemID = 0x33B4; } //mounted skeletal dragon
				else if ( this.ItemID == 0x33B4 ){ this.ItemID = 0x33B3; } //mounted skeletal dragon
				else if ( this.ItemID == 0x33B5 ){ this.ItemID = 0x33B6; } //mounted wyvern
				else if ( this.ItemID == 0x33B6 ){ this.ItemID = 0x33B5; } //mounted wyvern
				else if ( this.ItemID == 0x33B9 ){ this.ItemID = 0x33BA; } //mounted alien 
				else if ( this.ItemID == 0x33BA ){ this.ItemID = 0x33B9; } //mounted alien 
				else if ( this.ItemID == 0x33CB ){ this.ItemID = 0x33CC; } //mounted dragon abysmal
				else if ( this.ItemID == 0x33CC ){ this.ItemID = 0x33CB; } //mounted dragon abysmal
				else if ( this.ItemID == 0x33D1 ){ this.ItemID = 0x33D2; } //mounted dragon amber
				else if ( this.ItemID == 0x33D2 ){ this.ItemID = 0x33D1; } //mounted dragon amber
				else if ( this.ItemID == 0x33CF ){ this.ItemID = 0x33D0; } //mounted dragon cinder
				else if ( this.ItemID == 0x33D0 ){ this.ItemID = 0x33CF; } //mounted dragon cinder
				else if ( this.ItemID == 0x33D7 ){ this.ItemID = 0x33D8; } //mounted dragon fire, primeval
				else if ( this.ItemID == 0x33D8 ){ this.ItemID = 0x33D7; } //mounted dragon fire, primeval
				else if ( this.ItemID == 0x33C1 ){ this.ItemID = 0x33C2; } //mounted dragon green, primeval
				else if ( this.ItemID == 0x33C2 ){ this.ItemID = 0x33C1; } //mounted dragon green, primeval
				else if ( this.ItemID == 0x33D3 ){ this.ItemID = 0x33D4; } //mounted dragon night
				else if ( this.ItemID == 0x33D4 ){ this.ItemID = 0x33D3; } //mounted dragon night
				else if ( this.ItemID == 0x33D9 ){ this.ItemID = 0x33DA; } //mounted dragon primeval
				else if ( this.ItemID == 0x33DA ){ this.ItemID = 0x33D9; } //mounted dragon primeval
				else if ( this.ItemID == 0x33D5 ){ this.ItemID = 0x33D6; } //mounted dragon reanimated
				else if ( this.ItemID == 0x33D6 ){ this.ItemID = 0x33D5; } //mounted dragon reanimated
				else if ( this.ItemID == 0x33C5 ){ this.ItemID = 0x33C6; } //mounted dragon red, primeval
				else if ( this.ItemID == 0x33C6 ){ this.ItemID = 0x33C5; } //mounted dragon red, primeval
				else if ( this.ItemID == 0x33C3 ){ this.ItemID = 0x33C4; } //mounted dragon royal
				else if ( this.ItemID == 0x33C4 ){ this.ItemID = 0x33C3; } //mounted dragon royal
				else if ( this.ItemID == 0x33BF ){ this.ItemID = 0x33C0; } //mounted dragon rune
				else if ( this.ItemID == 0x33C0 ){ this.ItemID = 0x33BF; } //mounted dragon rune
				else if ( this.ItemID == 0x33C7 ){ this.ItemID = 0x33C8; } //mounted dragon sea, primeval
				else if ( this.ItemID == 0x33C8 ){ this.ItemID = 0x33C7; } //mounted dragon sea, primeval
				else if ( this.ItemID == 0x33C9 ){ this.ItemID = 0x33CA; } //mounted dragon stygian
				else if ( this.ItemID == 0x33CA ){ this.ItemID = 0x33C9; } //mounted dragon stygian
				else if ( this.ItemID == 0x33CD ){ this.ItemID = 0x33CE; } //mounted dragon vampiric
				else if ( this.ItemID == 0x33CE ){ this.ItemID = 0x33CD; } //mounted dragon vampiric
				else if ( this.ItemID == 0x33DB ){ this.ItemID = 0x33DC; } //mounted hell beast 
				else if ( this.ItemID == 0x33DC ){ this.ItemID = 0x33DB; } //mounted hell beast 
				else if ( this.ItemID == 0x33DD ){ this.ItemID = 0x33DE; } //mounted hippogriff 
				else if ( this.ItemID == 0x33DE ){ this.ItemID = 0x33DD; } //mounted hippogriff 
				else if ( this.ItemID == 0x33BD ){ this.ItemID = 0x33BE; } //mounted lion 
				else if ( this.ItemID == 0x33BE ){ this.ItemID = 0x33BD; } //mounted lion 
				else if ( this.ItemID == 0x33B7 ){ this.ItemID = 0x33B8; } //mounted styguana 
				else if ( this.ItemID == 0x33B8 ){ this.ItemID = 0x33B7; } //mounted styguana 
				else if ( this.ItemID == 0x33BB ){ this.ItemID = 0x33BC; } //mounted watcher 
				else if ( this.ItemID == 0x33BC ){ this.ItemID = 0x33BB; } //mounted watcher 
				else if ( this.ItemID == 0x33DF ){ this.ItemID = 0x33E0; } //mounted walrus 
				else if ( this.ItemID == 0x33E0 ){ this.ItemID = 0x33DF; } //mounted walrus 
				else if ( this.ItemID == 0x33E1 ){ this.ItemID = 0x33E2; } //mounted ogre 
				else if ( this.ItemID == 0x33E2 ){ this.ItemID = 0x33E1; } //mounted ogre 
				else if ( this.ItemID == 0x33E3 ){ this.ItemID = 0x33E4; } //mounted trollbear 
				else if ( this.ItemID == 0x33E4 ){ this.ItemID = 0x33E3; } //mounted trollbear 
			}
		}

        public MountedTrophyHead( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( AnimalKiller );
            writer.Write( AnimalWhere );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            AnimalKiller = reader.ReadString();
            AnimalWhere = reader.ReadString();
	    }
    }
}