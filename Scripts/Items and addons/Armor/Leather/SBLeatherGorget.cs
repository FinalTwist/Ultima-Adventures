using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class SBLeatherGorget : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 13; } }
		public override int BaseFireResistance{ get{ return 14; } }
		public override int BaseColdResistance{ get{ return 13; } }
		public override int BasePoisonResistance{ get{ return 13; } }
		public override int BaseEnergyResistance{ get{ return 13; } }

		public override int InitMinHits{ get{ return 10; } }
		public override int InitMaxHits{ get{ return 20; } }

		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 20; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public SBLeatherGorget() : base( 0x13C7 )
		{
			Weight = 1.0;
		}

		public SBLeatherGorget( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( "Soulbound Armor" );

		}

		public override bool OnEquip( Mobile from )
		{

			if ( from is PlayerMobile && !((PlayerMobile)from).SoulBound )
			{
				from.SendMessage( "Only SoulBound can wear this armor." );
				return false;
			}
			return base.OnEquip( from );	
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}