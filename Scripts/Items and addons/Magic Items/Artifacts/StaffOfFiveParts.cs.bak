using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Misc;

namespace Server.Items
{
	public class StaffFiveParts : QuarterStaff
	{
		public Mobile StaffOwner;
		public string StaffName;
		public int StaffMagic;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Staff_Owner { get{ return StaffOwner; } set{ StaffOwner = value; } }

		[CommandProperty(AccessLevel.Owner)]
		public string Staff_Name { get { return StaffName; } set { StaffName = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Staff_Magic { get { return StaffMagic; } set { StaffMagic = value; InvalidateProperties(); } }

		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }

		public override bool DisplayLootType{ get{ return false; } }

		[Constructable]
		public StaffFiveParts( Mobile from, int magic )
		{
			StaffMagic = magic;
			LootType = LootType.Blessed;

			this.StaffOwner = from;
			string StaffName = StaffOwner.Name + " the Wizard";
			if ( magic > 0 ){ StaffName = StaffOwner.Name + " the Necromancer"; }
			EngravedText = StaffName;

			Name = "Staff of Ultimate Power";
			Hue = 0x491;
				if ( StaffMagic > 0 ){ this.Hue = 0x96C; } 

			AosElementDamages.Physical = 100;
			Attributes.SpellChanneling = 1;
			Attributes.SpellDamage = 50;
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 2;
			Attributes.LowerManaCost = 40;
			Attributes.LowerRegCost = 100;
			LootType = LootType.Blessed;
			WeaponAttributes.LowerStatReq = 50;
			AccuracyLevel = WeaponAccuracyLevel.Supremely;
			DamageLevel = WeaponDamageLevel.Vanq;
			DurabilityLevel = WeaponDurabilityLevel.Indestructible;

			SkillBonuses.SetValues(0, SkillName.Macing, 20);
			SkillBonuses.SetValues(1, SkillName.MagicResist, 25);
			SkillBonuses.SetValues(2, SkillName.Magery, 25);
			if ( magic > 0 ){ SkillBonuses.SetValues(2, SkillName.Necromancy, 25); }
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public override void OnHit( Mobile attacker, Mobile defender, double damageBonus )
		{
			base.OnHit( attacker, defender, damageBonus );

			this.AosElementDamages.Physical = 0;
			this.AosElementDamages.Fire = 0;
			this.AosElementDamages.Cold = 0;
			this.AosElementDamages.Poison = 0;
			this.AosElementDamages.Energy = 0;

			switch ( Utility.RandomMinMax( 0, 4 ) ) 
			{
				case 0: this.Hue = 0x48F; if ( StaffMagic > 0 ){ this.Hue = 0x558; } this.AosElementDamages.Poison = 100; break;
				case 1: this.Hue = 0x48D; if ( StaffMagic > 0 ){ this.Hue = 0x554; } this.AosElementDamages.Cold = 100; break;
				case 2: this.Hue = 0x48E; if ( StaffMagic > 0 ){ this.Hue = 0x54D; } this.AosElementDamages.Fire = 100; break;
				case 3: this.Hue = 0x491; if ( StaffMagic > 0 ){ this.Hue = 0x96C; } this.AosElementDamages.Physical = 100; break;
				case 4: this.Hue = 0x490; if ( StaffMagic > 0 ){ this.Hue = 0x561; } this.AosElementDamages.Energy = 100; break;
			}

			this.InvalidateProperties();
		}

		public override bool OnEquip( Mobile from )
		{
			if ( this.StaffOwner == from )
			{
				base.OnEquip( from );
			}
			else
			{
				from.LocalOverheadMessage( MessageType.Emote, 0xB1F, true, "The magic drains from the staff!" );
				BrokenGear broke = new BrokenGear();
				broke.ItemID = this.ItemID;
				broke.Hue = 0x47E;
				broke.Name = "drained magical staff";
				broke.Weight = this.Weight;
				from.AddToBackpack ( broke );
				this.Delete();
				return false;
			}
			return true;
		}

		public StaffFiveParts( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (Mobile)StaffOwner );
            writer.Write( StaffName );
            writer.Write( StaffMagic );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			StaffOwner = reader.ReadMobile();
			StaffName = reader.ReadString();
			StaffMagic = reader.ReadInt();
			EngravedText = StaffName;
			LootType = LootType.Blessed;
		}
	}

	public class StaffPartVenom : Item
	{
		[Constructable]
		public StaffPartVenom() : base( 0x3A7 )
		{
			Hue = 0x48F;
			Name = "a piece of a staff";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "1st of 5 pieces");
            list.Add( 1049644, "Venom Piece");
        }

		public StaffPartVenom( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	///////////////////////////////////////////////////////////////////////////
	public class StaffPartCaddellite : Item
	{
		[Constructable]
		public StaffPartCaddellite() : base( 0x3A7 )
		{
			Hue = 0x48D;
			Name = "a piece of a staff";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "2nd of 5 pieces");
            list.Add( 1049644, "Caddellite Piece");
        }

		public StaffPartCaddellite( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	///////////////////////////////////////////////////////////////////////////
	public class StaffPartFire : Item
	{
		[Constructable]
		public StaffPartFire() : base( 0x3A7 )
		{
			Hue = 0x48E;
			Name = "a piece of a staff";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "3rd of 5 pieces");
            list.Add( 1049644, "Fire Piece");
        }

		public StaffPartFire( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	///////////////////////////////////////////////////////////////////////////
	public class StaffPartLight : Item
	{
		[Constructable]
		public StaffPartLight() : base( 0x3A7 )
		{
			Hue = 0x491;
			Name = "a piece of a staff";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "4th of 5 pieces");
            list.Add( 1049644, "Light Piece");
        }

		public StaffPartLight( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	///////////////////////////////////////////////////////////////////////////
	public class StaffPartEnergy : Item
	{
		[Constructable]
		public StaffPartEnergy() : base( 0x3A7 )
		{
			Hue = 0x490;
			Name = "a piece of a staff";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "5th of 5 pieces");
            list.Add( 1049644, "Energy Piece");
        }

		public StaffPartEnergy( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}