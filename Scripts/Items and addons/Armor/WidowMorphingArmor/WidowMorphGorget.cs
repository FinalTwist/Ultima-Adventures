using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class WidowMorphGorget : BaseArmor
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
		public WidowMorphGorget() : base( 11022 )
		{
			Name = "Morphing Armor of the Widow";
			Weight = 2.0;
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
				list.Add( "Spell Damage Increase 300%" );	
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
			
			Item shirt = from.FindItemOnLayer( Layer.InnerTorso );
			Item glove = from.FindItemOnLayer( Layer.Gloves );
			Item pants = from.FindItemOnLayer( Layer.Pants );
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
			shirt != null && shirt.GetType() == typeof( WidowMorphChest ) && 
			glove != null && glove.GetType() == typeof( WidowMorphGloves ) && 
			pants != null && pants.GetType() == typeof( WidowMorphLegs ) && 
			helm != null && helm.GetType() == typeof( WidowMorphHelm ) && 
			arms != null && arms.GetType() == typeof( WidowMorphArms ) )
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
				ArmorAttributes.SelfRepair = 3;
				Attributes.RegenMana = 35;
				PhysicalBonus = 17;
				FireBonus = 7;
				ColdBonus = 17;
				PoisonBonus = 17;
				EnergyBonus = 10;

				WidowMorphChest chest = from.FindItemOnLayer( Layer.InnerTorso ) as WidowMorphChest;
				WidowMorphGloves gloves = from.FindItemOnLayer( Layer.Gloves ) as WidowMorphGloves;
				WidowMorphLegs legs = from.FindItemOnLayer( Layer.Pants ) as WidowMorphLegs;
				WidowMorphHelm helmet = from.FindItemOnLayer( Layer.Helm ) as WidowMorphHelm;
				WidowMorphArms arm = from.FindItemOnLayer( Layer.Arms ) as WidowMorphArms;

				chest.Hue = 0x47E;
				chest.Attributes.NightSight = 1;
				chest.Attributes.DefendChance = 20;
				chest.ArmorAttributes.SelfRepair = 3;
				chest.SkillBonuses.SetValues( 0, SkillName.Magery, 50.0 );
				chest.Attributes.EnhancePotions = 50;
				chest.PhysicalBonus = 17;
				chest.FireBonus = 7;
				chest.ColdBonus = 17;
				chest.PoisonBonus = 17;
				chest.EnergyBonus = 10;
				
				gloves.Hue = 0x47E;
				gloves.Attributes.NightSight = 1;
				gloves.ArmorAttributes.SelfRepair = 3;
				gloves.Attributes.BonusStr = -30;
				gloves.PhysicalBonus = 17;
				gloves.FireBonus = 7;
				gloves.ColdBonus = 17;
				gloves.PoisonBonus = 17;
				gloves.EnergyBonus = 10;

				legs.Hue = 0x47E;
				legs.Attributes.NightSight = 1;
				legs.ArmorAttributes.SelfRepair = 3;
				legs.Attributes.BonusDex = 15;
				legs.Attributes.BonusMana = 50;
				legs.Attributes.BonusInt = 50;
				legs.PhysicalBonus = 17;
				legs.FireBonus = 7;
				legs.ColdBonus = 17;
				legs.PoisonBonus = 17;
				legs.EnergyBonus = 10;

				helmet.Hue = 0x47E;
				helmet.Attributes.NightSight = 1;
				helmet.ArmorAttributes.SelfRepair = 3;
				helmet.Attributes.BonusHits = 40;
				helmet.Attributes.CastRecovery = 4;
				helmet.Attributes.CastSpeed = 4;
				helmet.PhysicalBonus = 17;
				helmet.FireBonus = 7;
				helmet.ColdBonus = 17;
				helmet.PoisonBonus = 17;
				helmet.EnergyBonus = 10;
				
				arm.Hue = 0x47E;
				arm.ArmorAttributes.SelfRepair = 3;
				arm.Attributes.RegenStam = 10;
				arm.Attributes.NightSight = 1;
				arm.Attributes.SpellDamage = 500;
				arm.Attributes.LowerRegCost = 100;
				arm.Attributes.LowerManaCost = 50;
				arm.PhysicalBonus = 17;
				arm.FireBonus = 7;
				arm.ColdBonus = 17;
				arm.PoisonBonus = 17;
				arm.EnergyBonus = 10;
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

				if ( m.FindItemOnLayer( Layer.InnerTorso ) is WidowMorphChest 
				&& m.FindItemOnLayer( Layer.Cloak ) is WidowCloak
				&& m.FindItemOnLayer( Layer.Gloves ) is WidowMorphGloves 
				&& m.FindItemOnLayer( Layer.Arms ) is WidowMorphArms 
				&& m.FindItemOnLayer( Layer.Pants ) is WidowMorphLegs 
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
					Attributes.RegenMana = 0;
					PhysicalBonus = 0;
					FireBonus = 0;
					ColdBonus = 0;
					PoisonBonus = 0;
					EnergyBonus = 0;					
					
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

		public WidowMorphGorget( Serial serial ) : base( serial )
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
		}
	}
}
