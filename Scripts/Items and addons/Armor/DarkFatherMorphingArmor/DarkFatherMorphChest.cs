using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	[FlipableAttribute( 11016, 11017 )]
	public class DarkFatherMorphChest : BaseArmor
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
		public DarkFatherMorphChest() : base( 11016 )
		{
			Name = "Morphing Armor of the Dark Father";
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
					list.Add( "Grants DarkFather Form" );
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
				list.Add( "Bonus Mana 150" );
				list.Add( "Mana Regen 35" );
				list.Add( "Stamina Regen 10" );
				list.Add( "Necromancy Bonus 50" );
				list.Add( "Death Curse 33%" );
				list.Add( "Defence Chance 20" );
				list.Add( "Spell Damage Increase 300%" );	
				list.Add( "Lower Regeat Cost 100%" );
				list.Add( "Lower Mana Cost 50%" );
				list.Add( "Luck 4000" );
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


			if ( cloak != null && cloak.GetType() == typeof( DarkFatherCloak ) && 
				arms != null && arms.GetType() == typeof( DarkFatherMorphArms ) && 
				glove != null && glove.GetType() == typeof( DarkFatherMorphGloves ) && 
				pants != null && pants.GetType() == typeof( DarkFatherMorphLegs ) && 
				helm != null && helm.GetType() == typeof( DarkFatherMorphHelm ) && 
				neck != null && neck.GetType() == typeof( DarkFatherMorphGorget ) )
			{
				Effects.PlaySound( from.Location, from.Map, 503 );
				from.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );

				if (from is PlayerMobile)
				{
					from.SendMessage( "You Morph!" );
					from.BodyMod = 318;
					from.HueMod = 1931;
					from.NameHue = 39;
				}
				
				Hue = 0x47E;
				Attributes.NightSight = 1;
				Attributes.DefendChance = 20;
				ArmorAttributes.SelfRepair = 3;
				SkillBonuses.SetValues( 0, SkillName.Necromancy, 50.0 );
				PhysicalBonus = 10;
				FireBonus = 17;
				ColdBonus = 17;
				PoisonBonus = 10;
				EnergyBonus = 17;


				DarkFatherMorphGloves gloves = from.FindItemOnLayer( Layer.Gloves ) as DarkFatherMorphGloves;
				DarkFatherMorphLegs legs = from.FindItemOnLayer( Layer.Pants ) as DarkFatherMorphLegs;
				DarkFatherMorphGorget gorget = from.FindItemOnLayer( Layer.Neck ) as DarkFatherMorphGorget;
				DarkFatherMorphHelm helmet = from.FindItemOnLayer( Layer.Helm ) as DarkFatherMorphHelm;
				DarkFatherMorphArms arm = from.FindItemOnLayer( Layer.Arms ) as DarkFatherMorphArms;


				gloves.Hue = 0x47E;
				gloves.Attributes.NightSight = 1;
				gloves.ArmorAttributes.SelfRepair = 3;
				gloves.Attributes.BonusStr = -30;
				gloves.PhysicalBonus = 10;
				gloves.FireBonus = 17;
				gloves.ColdBonus = 17;
				gloves.PoisonBonus = 10;
				gloves.EnergyBonus = 17;

				legs.Hue = 0x47E;
				legs.Attributes.NightSight = 1;
				legs.ArmorAttributes.SelfRepair = 3;
				legs.Attributes.BonusDex = 15;
				legs.Attributes.BonusMana = 150;
				legs.Attributes.BonusInt = 50;
				legs.PhysicalBonus = 10;
				legs.FireBonus = 17;
				legs.ColdBonus = 17;
				legs.PoisonBonus = 10;
				legs.EnergyBonus = 17;

				helmet.Hue = 0x47E;
				helmet.Attributes.NightSight = 1;
				helmet.ArmorAttributes.SelfRepair = 3;
				helmet.Attributes.BonusHits = 40;
				helmet.Attributes.CastRecovery = 4;
				helmet.Attributes.CastSpeed = 4;
				helmet.PhysicalBonus = 10;
				helmet.FireBonus = 17;
				helmet.ColdBonus = 17;
				helmet.PoisonBonus = 10;
				helmet.EnergyBonus = 17;

				gorget.Hue = 0x47E;
				gorget.Attributes.NightSight = 1;
				gorget.ArmorAttributes.SelfRepair = 3;
				gorget.Attributes.RegenMana = 35;
				gorget.PhysicalBonus = 10;
				gorget.FireBonus = 17;
				gorget.ColdBonus = 17;
				gorget.PoisonBonus = 10;
				gorget.EnergyBonus = 17;
				
				arm.Hue = 0x47E;
				arm.ArmorAttributes.SelfRepair = 3;
				arm.Attributes.RegenStam = 10;
				arm.Attributes.NightSight = 1;
				arm.Attributes.SpellDamage = 300;
				arm.Attributes.LowerRegCost = 100;
				arm.Attributes.LowerManaCost = 50;
				arm.PhysicalBonus = 10;
				arm.FireBonus = 17;
				arm.ColdBonus = 17;
				arm.PoisonBonus = 10;
				arm.EnergyBonus = 17;
				arm.Attributes.Luck = 4000;
				

						
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
				if ( m.FindItemOnLayer( Layer.Cloak ) is DarkFatherCloak
				&& m.FindItemOnLayer( Layer.Gloves ) is DarkFatherMorphGloves 
				&& m.FindItemOnLayer( Layer.Pants ) is DarkFatherMorphLegs 
				&& m.FindItemOnLayer( Layer.Arms ) is DarkFatherMorphArms 
				&& m.FindItemOnLayer( Layer.Neck ) is DarkFatherMorphGorget 
				&& m.FindItemOnLayer( Layer.Helm ) is DarkFatherMorphHelm )
				{
					

				DarkFatherMorphLegs legs = m.FindItemOnLayer( Layer.Pants ) as DarkFatherMorphLegs;
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
				SkillBonuses.SetValues( 0, SkillName.Necromancy, 0.0 );
				PhysicalBonus = 0;
				FireBonus = 0;
				ColdBonus = 0;
				PoisonBonus = 0;
				EnergyBonus = 0;					
					
					
					DarkFatherMorphGloves gloves = m.FindItemOnLayer( Layer.Gloves ) as DarkFatherMorphGloves;
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

					DarkFatherMorphHelm helmet = m.FindItemOnLayer( Layer.Helm ) as DarkFatherMorphHelm;
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
					
					DarkFatherMorphGorget gorget = m.FindItemOnLayer( Layer.Neck ) as DarkFatherMorphGorget;
					gorget.Hue = 0x0;
					gorget.Attributes.NightSight = 0;
					gorget.ArmorAttributes.SelfRepair = 0;
					gorget.Attributes.RegenMana = 0;
					gorget.PhysicalBonus = 0;
					gorget.FireBonus = 0;
					gorget.ColdBonus = 0;
					gorget.PoisonBonus = 0;
					gorget.EnergyBonus = 0;

					DarkFatherMorphArms arm = m.FindItemOnLayer( Layer.Arms ) as DarkFatherMorphArms;
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

		public DarkFatherMorphChest( Serial serial ) : base( serial )
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
