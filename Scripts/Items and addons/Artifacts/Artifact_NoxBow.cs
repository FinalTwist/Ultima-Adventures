using System;
using Server;
namespace Server.Items
{
    public class NoxBow : HeavyCrossbow, ITokunoDyable
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


        [Constructable]
        public NoxBow()
        {
            Name = "Nox Bow";
            Attributes.WeaponDamage = 45;
            Hue = 267;
            WeaponAttributes.HitLeechHits = 20;
            WeaponAttributes.HitLeechMana = 20;
            WeaponAttributes.HitLeechStam = 20;
            WeaponAttributes.HitLightning = 5;
            WeaponAttributes.HitLowerAttack = 5;
            WeaponAttributes.HitPhysicalArea = 5;
            WeaponAttributes.LowerStatReq = 5;
            WeaponAttributes.SelfRepair = 2;
            Attributes.ReflectPhysical = 5;
            Attributes.SpellChanneling = 1;
            Attributes.SpellDamage = 10;
            Attributes.WeaponSpeed = 40;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


        public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
        {
            phys = 100;
            cold = 0;
            fire = 0;
            nrgy = 0;
            pois = 0;
            chaos = 0;
            direct = 0;
        }
        public NoxBow( Serial serial )
            : base( serial )
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
