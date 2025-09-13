using System;
using Server;


namespace Server.Items
{
	public class NoxNightlight : MagicLantern
	{
		[Constructable]
		public NoxNightlight()
		{
            Name = "Nox Nightlight";
            Hue = Utility.RandomList( 1267, 1268, 1269, 1270, 1271, 1271, 1372, 1167 );
			Attributes.WeaponDamage = 35;
			Attributes.SpellDamage = 15;
			Resistances.Poison = 15;
			SkillBonuses.SetValues( 0, SkillName.Poisoning, 30 );
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
			
        } 


		public NoxNightlight( Serial serial ) : base( serial )
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