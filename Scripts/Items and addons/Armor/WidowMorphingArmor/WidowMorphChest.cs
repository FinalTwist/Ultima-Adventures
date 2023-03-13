using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	[FlipableAttribute( 11016, 11017 )]
	public class WidowMorphChest : BaseArmor
	{
		//public override int LabelNumber{ get{ return 1074303; } }
		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }
		public override int AosStrReq{ get{ return 20; } }
		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }

		[Constructable]
		public WidowMorphChest() : base( 11016 )
		{
			Name = "Morphing Armor of the Widow";
			Weight = 10.0;
			Attributes.RegenHits = 1;
			Attributes.AttackChance = 5;
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			
			list.Add( 1072376, "7" ); //number of pieces
			
			if ( this.Parent is Mobile )
			{
				if ( this.Hue == 0x47E )
				{
					list.Add( 1072377 ); //??
					list.Add( "Grants Widow Form" );
				}
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( this.Hue == 0x0 )
			{
				list.Add( 1072378 ); // when all pieces present
				list.Add( "Bonus Intelligence 50" );
				list.Add( "Bonus Dex 15" );
				list.Add( "Bonus Mana 50" );
				list.Add( "Mana Regen 35" );
				list.Add( "Stamina Regen 10" );
				list.Add( "Magery Bonus 50" );
				list.Add( "Widow's Spawn Ability 50%" );
				list.Add( "Defence Chance 20" );
				list.Add( "Enhance Potions 50%" );
				list.Add( "Spell Damage Increase 100%" );	
				list.Add( "Lower Regeat Cost 100%" );
				list.Add( "Lower Mana Cost 50%" );
				list.Add( "Luck 500" );
				list.Add( "Self Repair 3" );
				list.Add( "Strength -30" );
				list.Add( "Fast Cast Recovery 4" );
				list.Add( "Faster Cast Speed 4" );
				list.Add( 1060441 ); //nightsight
			}
		}

		public override bool OnEquip( Mobile from )
		{
			
			Item glove = from.FindItemOnLayer( Layer.Gloves );
			Item pants = from.FindItemOnLayer( Layer.Pants );
			Item neck = from.FindItemOnLayer( Layer.Neck );
			Item helm = from.FindItemOnLayer( Layer.Helm );
			Item arms = from.FindItemOnLayer( Layer.Arms );
			Item cloak = from.FindItemOnLayer( Layer.Cloak );
			Item twohanded = from.FindItemOnLayer( Layer.TwoHanded );			
			Item firstvalid = from.FindItemOnLayer( Layer.FirstValid );

			if ( from.Mounted == true )
			{
				from.SendMessage( "You cannot be mounted while wearing this armor." );
				return false;
			}
			if ( from.BodyMod != 0 )
			{
				from.SendMessage( "You cannot this armor while your body is transformed!" );
				return false;
			}
			if ( firstvalid != null || twohanded != null )
			{
				from.SendMessage( "You cannot this armor while your hands are holding an item." );
				return false;
			}


			if ( cloak != null && cloak.GetType() == typeof( WidowCloak ) && 
				arms != null && arms.GetType() == typeof( WidowMorphArms ) && 
				glove != null && glove.GetType() == typeof( WidowMorphGloves ) && 
				pants != null && pants.GetType() == typeof( WidowMorphLegs ) && 
				helm != null && helm.GetType() == typeof( WidowMorphHelm ) && 
				neck != null && neck.GetType() == typeof( WidowMorphGorget ) )
			{
				Effects.PlaySound( from.Location, from.Map, 503 );
				from.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );

				if (from is PlayerMobile)
				{
					from.SendMessage( "You Morph!" );
					from.BodyMod = 84;
					from.HueMod = 1931;
					from.NameHue = 39;

				}
				
				Hue = 0x47E;
				Attributes.NightSight = 1;
				Attributes.DefendChance = 20;
				ArmorAttributes.SelfRepair = 3;
				SkillBonuses.SetValues( 0, SkillName.Magery, 50.0 );
				Attributes.EnhancePotions = 50;
				PhysicalBonus = 2;
				FireBonus = 5;
				ColdBonus = 5;
				PoisonBonus = 3;
				EnergyBonus = 5;


				WidowMorphGloves gloves = from.FindItemOnLayer( Layer.Gloves ) as WidowMorphGloves;
				WidowMorphLegs legs = from.FindItemOnLayer( Layer.Pants ) as WidowMorphLegs;
				WidowMorphGorget gorget = from.FindItemOnLayer( Layer.Neck ) as WidowMorphGorget;
				WidowMorphHelm helmet = from.FindItemOnLayer( Layer.Helm ) as WidowMorphHelm;
				WidowMorphArms arm = from.FindItemOnLayer( Layer.Arms ) as WidowMorphArms;


				gloves.Hue = 0x47E;
				gloves.Attributes.NightSight = 1;
				gloves.ArmorAttributes.SelfRepair = 3;
				gloves.Attributes.BonusStr = -30;
				gloves.PhysicalBonus = 2;
				gloves.FireBonus = 5;
				gloves.ColdBonus = 5;
				gloves.PoisonBonus = 3;
				gloves.EnergyBonus = 5;

				legs.Hue = 0x47E;
				legs.Attributes.NightSight = 1;
				legs.ArmorAttributes.SelfRepair = 3;
				legs.Attributes.BonusDex = 15;
				legs.Attributes.BonusMana = 50;
				legs.Attributes.BonusInt = 50;
				legs.PhysicalBonus = 2;
				legs.FireBonus = 5;
				legs.ColdBonus = 5;
				legs.PoisonBonus = 3;
				legs.EnergyBonus = 5;

				helmet.Hue = 0x47E;
				helmet.Attributes.NightSight = 1;
				helmet.ArmorAttributes.SelfRepair = 3;
				helmet.Attributes.BonusHits = 40;
				helmet.Attributes.CastRecovery = 4;
				helmet.Attributes.CastSpeed = 4;
				helmet.PhysicalBonus = 2;
				helmet.FireBonus = 5;
				helmet.ColdBonus = 5;
				helmet.PoisonBonus = 3;
				helmet.EnergyBonus = 5;

				gorget.Hue = 0x47E;
				gorget.Attributes.NightSight = 1;
				gorget.ArmorAttributes.SelfRepair = 3;
				gorget.Attributes.RegenMana = 35;
				gorget.PhysicalBonus = 2;
				gorget.FireBonus = 5;
				gorget.ColdBonus = 5;
				gorget.PoisonBonus = 3;
				gorget.EnergyBonus = 5;
				
				arm.Hue = 0x47E;
				arm.ArmorAttributes.SelfRepair = 3;
				arm.Attributes.RegenStam = 10;
				arm.Attributes.NightSight = 1;
				arm.Attributes.SpellDamage = 100;
				arm.Attributes.LowerRegCost = 100;
				arm.Attributes.LowerManaCost = 50;
				arm.PhysicalBonus = 2;
				arm.FireBonus = 5;
				arm.ColdBonus = 5;
				arm.PoisonBonus = 3;
				arm.EnergyBonus = 5;
				arm.Attributes.Luck = 500;
				

						
				from.SendLocalizedMessage( 1072391 );
			}
			this.InvalidateProperties();
			return base.OnEquip( from );							
		}

		public override void OnRemoved(IEntity parent )
		{
			if ( parent is Mobile )
			{
Mobile m = (Mobile)parent;
				if ( m.FindItemOnLayer( Layer.Cloak ) is WidowCloak
				&& m.FindItemOnLayer( Layer.Gloves ) is WidowMorphGloves 
				&& m.FindItemOnLayer( Layer.Pants ) is WidowMorphLegs 
				&& m.FindItemOnLayer( Layer.Arms ) is WidowMorphArms 
				&& m.FindItemOnLayer( Layer.Neck ) is WidowMorphGorget 
				&& m.FindItemOnLayer( Layer.Helm ) is WidowMorphHelm )
				{
					

				WidowMorphLegs legs = m.FindItemOnLayer( Layer.Pants ) as WidowMorphLegs;
				if (m is PlayerMobile)
				{
					m.SendMessage( "You remove the armor." );
					m.BodyMod = 0;
					m.NameHue = -1;
					m.HueMod = -1;

				}
				

				Hue = 0x0;
				Attributes.NightSight = 0;
				ArmorAttributes.SelfRepair = 0;
				Attributes.DefendChance = 0;
				ArmorAttributes.SelfRepair = 0;
				SkillBonuses.SetValues( 0, SkillName.Magery, 0.0 );
				Attributes.EnhancePotions = 0;
				PhysicalBonus = 0;
				FireBonus = 0;
				ColdBonus = 0;
				PoisonBonus = 0;
				EnergyBonus = 0;					
					
					
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

					WidowMorphArms arm = m.FindItemOnLayer( Layer.Arms ) as WidowMorphArms;
					arm.Hue = 0x0;
					arm.Attributes.NightSight = 0;
					arm.ArmorAttributes.SelfRepair = 0;
					arm.Attributes.RegenStam = 0;
					arm.Attributes.SpellDamage = 0;
					arm.Attributes.LowerRegCost = 0;
					arm.Attributes.LowerManaCost = 0;
					arm.PhysicalBonus = 0;
					arm.FireBonus = 0;
					arm.ColdBonus = 0;
					arm.PoisonBonus = 0;
					arm.EnergyBonus = 0;
					arm.Attributes.Luck = 0;
				}
				this.InvalidateProperties();
			}
			base.OnRemoved( parent );
		}

		public WidowMorphChest( Serial serial ) : base( serial )
		{
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

			if ( Weight == 1.0 )
				Weight = 10.0;
		}
	}
}
