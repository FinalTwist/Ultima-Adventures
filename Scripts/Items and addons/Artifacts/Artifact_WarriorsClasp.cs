using System;
using Server;
namespace Server.Items
{
    public class WarriorsClasp : GoldBracelet, ITokunoDyable
    {
        [Constructable]
        public WarriorsClasp()
        {
            Name = "Warrior's Clasp";
            Hue = 2117;
            Attributes.AttackChance = 10;
            Attributes.DefendChance = 10;
            Attributes.BonusMana = 5;
            Attributes.BonusHits = 7;
            Attributes.BonusStam = 15;
            Attributes.RegenHits = 3;
            Attributes.RegenStam = 3;
            Attributes.RegenMana = 3;
            SkillBonuses.SetValues( 0, SkillName.Tactics, 10.0 );
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


        public WarriorsClasp( Serial serial ) : base( serial )
        {
        }
        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int)0 );
        }
        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    }
}
