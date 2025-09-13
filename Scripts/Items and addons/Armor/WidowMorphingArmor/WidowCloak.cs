using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{  
	public class WidowCloak : BaseCloak
	{

		//public static DateTime speciallastused;

		[Constructable]
		public WidowCloak() : this( 0 )
		{
		}

		[Constructable]
		public WidowCloak( int hue ) : base( 11012 )
		{
			Weight = 4.0;
			Name = "Morphing cloak of the Widow";
			//speciallastused = DateTime.UtcNow;
			
		}

		public override void OnRemoved(IEntity parent )
		{
			if ( parent is Mobile )
			{
				
				Mobile m = (Mobile)parent;
				if ( m.FindItemOnLayer( Layer.InnerTorso ) is WidowMorphChest 
				&& m.FindItemOnLayer( Layer.Gloves ) is WidowMorphGloves 
				&& m.FindItemOnLayer( Layer.Arms ) is WidowMorphArms 
				&& m.FindItemOnLayer( Layer.Neck ) is WidowMorphGorget 
				&& m.FindItemOnLayer( Layer.Pants ) is WidowMorphLegs 
				&& m.FindItemOnLayer( Layer.Helm ) is WidowMorphHelm )
				{


				if (m is PlayerMobile)
				{
					m.SendMessage( "You remove the armor." );
					m.BodyMod = 0;
					m.NameHue = -1;
					m.HueMod = -1;
				}
				
					WidowMorphChest chest = m.FindItemOnLayer( Layer.InnerTorso ) as WidowMorphChest;
					chest.Hue = 0x0;
					chest.Attributes.NightSight = 0;
					chest.Attributes.DefendChance = 0;
					chest.ArmorAttributes.SelfRepair = 0;
					chest.SkillBonuses.SetValues( 0, SkillName.Magery, 0.0 );
					chest.Attributes.EnhancePotions = 0;
					chest.PhysicalBonus = 0;
					chest.FireBonus = 0;
					chest.ColdBonus = 0;
					chest.PoisonBonus = 0;
					chest.EnergyBonus = 0;

					WidowMorphGloves gloves = m.FindItemOnLayer( Layer.Gloves ) as WidowMorphGloves;
					gloves.Hue = 0x0;
					gloves.Attributes.NightSight = 0;
					gloves.ArmorAttributes.SelfRepair = 0;
					gloves.Attributes.BonusStr = 0;
					gloves.PhysicalBonus = 0;
					gloves.FireBonus = 0;
					gloves.ColdBonus = 0;
					gloves.PoisonBonus = 0;
					gloves.EnergyBonus = 0;				
					
				
					WidowMorphLegs legs = m.FindItemOnLayer( Layer.Pants ) as WidowMorphLegs;
					legs.Hue = 0x0;
					legs.Attributes.NightSight = 0;
					legs.ArmorAttributes.SelfRepair = 0;
					legs.Attributes.BonusDex = 0;
					legs.Attributes.BonusMana = 0;
					legs.Attributes.BonusInt = 0;
					legs.PhysicalBonus = 0;
					legs.FireBonus = 0;
					legs.ColdBonus = 0;
					legs.PoisonBonus = 0;
					legs.EnergyBonus = 0;


					WidowMorphGorget gorget = m.FindItemOnLayer( Layer.Neck ) as WidowMorphGorget;
					gorget.Hue = 0x0;
					gorget.Attributes.NightSight = 0;
					gorget.ArmorAttributes.SelfRepair = 0;
					gorget.Attributes.RegenMana = 0;
					gorget.PhysicalBonus = 0;
					gorget.FireBonus = 0;
					gorget.ColdBonus = 0;
					gorget.PoisonBonus = 0;
					gorget.EnergyBonus = 0;

					WidowMorphHelm helmet = m.FindItemOnLayer( Layer.Helm ) as WidowMorphHelm;
					helmet.Hue = 0x0;
					helmet.Attributes.NightSight = 0;
					helmet.ArmorAttributes.SelfRepair = 0;
					helmet.Attributes.CastRecovery = 0;
					helmet.Attributes.CastSpeed = 0;
					helmet.PhysicalBonus = 0;
					helmet.FireBonus = 0;
					helmet.ColdBonus = 0;
					helmet.PoisonBonus = 0;
					helmet.EnergyBonus = 0;

					WidowMorphArms arm = m.FindItemOnLayer( Layer.Arms ) as WidowMorphArms;
					arm.Hue = 0x0;
					arm.Attributes.NightSight = 0;
					arm.ArmorAttributes.SelfRepair = 0;
					arm.Attributes.RegenStam = 0;
					arm.Attributes.SpellDamage = 0;
					arm.PhysicalBonus = 0;
					arm.FireBonus = 0;
					arm.ColdBonus = 0;
					arm.PoisonBonus = 0;
					arm.EnergyBonus = 0;
					arm.Attributes.Luck = 0;
					
				this.InvalidateProperties();
				}
				base.OnRemoved( parent );
			}
		}


		public WidowCloak( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			//writer.Write( (DateTime)speciallastused );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			//speciallastused = reader.ReadDateTime();
		}
	}
}
