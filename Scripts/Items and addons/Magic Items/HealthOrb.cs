/*
 *
 Scripted by Vinz Clortho (vinz.auberon@stranky.org)
 For Auberon shard (http://auberon.stranky.org/)
 (C) 2016
 */

using System;
using Server.Mobiles;
using System.Collections;
using Server.OneTime; //onetime edit

namespace Server.Items
{
    public class HealthOrb : SelfDeletingItem, IOneTime //onetime edit
    {
        [Constructable]
        public HealthOrb() : base(14265, "Life Essense", 50)  //Duration 50 sec
        {
            Movable = false;
            Hue = 1172;
            Name = "Life Essense"; //We must specify Name again, because in SelfDeletingItem is little bug

            OneTimeType = 3; //onetime edit
            MorphCount = 0; //onetime edit

        }

        public int OneTimeType { get; set; } //onetime edit

        private int MorphCount { get; set; } //onetime edit

        public void OneTimeTick() //onetime edit
        {
            if (MorphCount == 30)
            {
                ItemID = 14270;
                Effects.PlaySound(Location, Map, 553);
            }
            else
            {
                MorphCount++;
            }
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile from, Point3D oldLocation)
        {
			if (from != null && from is PlayerMobile)
			{
				if (from.AccessLevel == AccessLevel.Player && !from.Hidden && from.Alive && !from.Blessed && from.GetDistanceToSqrt(this) <= 1)
					Heal(from);
			}            
			base.OnMovement(from, oldLocation);
        }

        public override void OnDoubleClick(Mobile from)
        {
			if (from != null && from is PlayerMobile)
			{			
				if (!from.Player || from.AccessLevel > AccessLevel.Player || from.Hidden || !from.Alive || from.Blessed)
					return;

				if (!from.InRange(this.GetWorldLocation(), 1))
					from.SendLocalizedMessage(502138);
				else
					Heal(from);
			}
        }

        public virtual void Heal(Mobile from)
        {
            if (this.Deleted || from == null) //from = player who triggered heal (not used yet)
                return;

            else if ( ( from is BaseCreature && (((BaseCreature)from).Controlled || ((BaseCreature)from).Summoned) && !from.IsDeadBondedPet && from.Alive && !from.Blessed && from.InLOS(this)) || (from.Player && !from.Blessed && from.AccessLevel >= AccessLevel.Player && from.Alive && from.InLOS(this)) )
			{
				from.Heal(AOS.Scale(from.HitsMax, BasePotion.Scale(from,20))); //Heal for 20+EnhancePotions% HitsMax
				from.Mana += AOS.Scale(from.ManaMax, 20);
				from.Stam += AOS.Scale(from.StamMax, 20);
				from.FixedParticles(0x376A, 9, 32, 5005, EffectLayer.Waist);
				from.PlaySound(0x1F2);

				this.Delete();
			}
        }

        public override void OnDelete()
        {
			if (this.Deleted) // trying to catch null exception error
				return;
			
            Effects.SendLocationParticles(EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x374A, 9, 32, 5024);
            Effects.PlaySound(Location, Map, 0x5C9);
            base.OnDelete();
        }

        public HealthOrb(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            this.Delete();
        }

        public static void Drop(Mobile harmed)
        {
            if (harmed == null || harmed.Map == null ) 
                return;


            Point3D loc = harmed.Location;
				
			if (harmed.Map.CanFit(harmed.X, harmed.Y, harmed.Z, 6, false, false) )
			{
				HealthOrb orb = new HealthOrb();
				if (orb != null)
				{
					orb.MoveToWorld(loc, harmed.Map);
				}
			}
        }
    }
}
