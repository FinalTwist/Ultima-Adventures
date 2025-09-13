using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Misc;

namespace Server.Items
{
	public class sotedeathstarrelic : MagicTalisman
	{
		public Mobile ItemOwner;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Item_Owner { get{ return ItemOwner; } set{ ItemOwner = value; } }

		[Constructable]
		public sotedeathstarrelic()
		{
			Name = "SHADOWS OF THE EMPIRE DEATHSTAR RELIC";
			ItemID = 0x5570;
			Resource = CraftResource.None;
			Layer = Layer.Talisman;
			Weight = 1.0;
			Hue = 0x52F;
			SkillBonuses.SetValues(0, SkillName.EvalInt, 50);
			SkillBonuses.SetValues(1, SkillName.Magery, 50);
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        
		
		
		}


		public sotedeathstarrelic( Serial serial ) : base( serial )
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