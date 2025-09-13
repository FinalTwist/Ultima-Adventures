using System;
using Server.Network;
using Server.Items;
using Server.Targeting;


namespace Server.Items
{
	public class FortunateBlades : Daisho, ITokunoDyable
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public FortunateBlades()
		{
          Name = "Fortunate Blades";
          Hue = 2213;
		  WeaponAttributes.MageWeapon = 30;
		  Attributes.SpellChanneling = 1;
		  Attributes.CastSpeed = 1;
		  WeaponAttributes.SelfRepair = 5;
		  Attributes.Luck = 200;
		  Attributes.RegenMana = 5;
		  Attributes.SpellDamage = 15;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public FortunateBlades( Serial serial ) : base( serial )
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
