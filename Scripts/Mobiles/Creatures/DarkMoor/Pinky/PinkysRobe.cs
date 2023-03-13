using System;
using Server.Items;

namespace Server.Items
{
	public class PinkysRobe : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 2; } }
		public override int BaseColdResistance{ get{ return 2; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 3; } }
		public override int InitMinHits{ get{ return 200; } }
		public override int InitMaxHits{ get{ return 250; } }
	 	public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }
		public override int ArtifactRarity{ get{ return 6; } }
		
		[Constructable]
		public PinkysRobe() : base( 0x2683 )
		{
			Name = "Pinky's PVP Robe";
			Hue = 2487;
			Weight = 4.5;
            //Attributes.CastRecovery = 3;
            //Attributes.CastSpeed = 2;
            //Attributes.LowerManaCost = 10;
			Attributes.BonusHits = 5;
			Attributes.DefendChance = 15;
			Attributes.SpellDamage = 5;
            //Attributes.ReflectPhysical = 10;
            //Attributes.RegenHits = 2;
            //Attributes.Luck = 200;
            //ArmorAttributes.SelfRepair = 25;
			SkillBonuses.SetValues(0, SkillName.Swords, 10.0 );
			SkillBonuses.SetValues(1, SkillName.Archery, 10.0 );
			SkillBonuses.SetValues(2, SkillName.Hiding, 10.0 );
			SkillBonuses.SetValues(3, SkillName.Healing, 10.0 );
			SkillBonuses.SetValues(4, SkillName.Magery, 10.0 );
		}
		public override void OnDoubleClick( Mobile m )
		{
			if( Parent != m )
			{
				m.SendMessage( "You must be wearing the robe to use it!" );
			}
			else
			{
				if ( ItemID == 0x2683 || ItemID == 0x2684 )
				{
					m.SendMessage( "You lower the hood." );
					m.PlaySound( 0x57 );
					ItemID = 0x1F03;
					m.NameMod = null;
					m.RemoveItem(this);
					m.EquipItem(this);
					
					if( m.GuildTitle != null)
					{
						m.DisplayGuildTitle = true;
					}
				}
				else if ( ItemID == 0x1F03 || ItemID == 0x1F04 )
				{
					m.SendMessage( "You pull the hood over your head." );
					m.PlaySound( 0x57 );
					ItemID = 0x2683;
					m.DisplayGuildTitle = true;
					m.RemoveItem(this);
					m.EquipItem(this);
				}
			}
		}

		public override bool OnEquip( Mobile from )
		{
			if ( ItemID == 0x2683 )
			{
				from.DisplayGuildTitle = false;
				from.Criminal = false;
			}
			return base.OnEquip(from);
		}
		
		public override void OnRemoved(IEntity o )
		{
			if( o is Mobile )
			{
				((Mobile)o).NameMod = null;
			}
			if( o is Mobile && ((Mobile)o).Kills >= 5)
			{
				((Mobile)o).Criminal = true;
			}
			if( o is Mobile && ((Mobile)o).GuildTitle != null )
			{
				((Mobile)o).DisplayGuildTitle = true;
			}
			base.OnRemoved( o );
		}
		
		public PinkysRobe( Serial serial ) : base( serial )
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
