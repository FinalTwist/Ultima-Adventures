using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class MinotaurMorphHelm : BaseArmor
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

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public MinotaurMorphHelm() : base( 11024 )
		{
			Name = "Morphing Armor of the Minotaur";
			Weight = 5.0;
			Attributes.RegenHits = 1;
			Attributes.AttackChance = 5;
		}

		public MinotaurMorphHelm( Serial serial ) : base( serial )
		{
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
					list.Add( "Grants Minotaur Form" );
				}
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( this.Hue == 0x0 )
			{
				list.Add( 1072378 ); // when all pieces present
				list.Add( "Bonus Strength 75" );
				list.Add( "Bonus Dex 15" );
				list.Add( "Bonus Hits 40" );
				list.Add( "Hits Regen 35" );
				list.Add( "Stamina Regen 10" );
				list.Add( "Wrestling Bonus 50" );
				list.Add( "Reflect Physical 35" );
				list.Add( "Defence Chance 20" );
				list.Add( "Hit Chance 15" );
				list.Add( "Damage Increase 250%" );	
				list.Add( "Luck 500" );
				list.Add( "Self Repair 3" );
				list.Add( "Intelligence -30" );
				list.Add( "Mana -50" );
				list.Add( "Fast Cast Recovery -2" );
				list.Add( "Faster Cast Speed -2" );
				list.Add( 1060441 ); //nightsight
			}
		}

		public override bool OnEquip( Mobile from )
		{
			
			Item shirt = from.FindItemOnLayer( Layer.InnerTorso );
			Item glove = from.FindItemOnLayer( Layer.Gloves );
			Item pants = from.FindItemOnLayer( Layer.Pants );
			Item neck = from.FindItemOnLayer( Layer.Neck );
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
			
			if ( cloak != null && cloak.GetType() == typeof( MinotaurCloak ) && 
			shirt != null && shirt.GetType() == typeof( MinotaurMorphChest ) 
			&& glove != null && glove.GetType() == typeof( MinotaurMorphGloves ) 
			&& pants != null && pants.GetType() == typeof( MinotaurMorphLegs ) 
			&& neck != null && neck.GetType() == typeof( MinotaurMorphGorget ) 
			&& arms != null && arms.GetType() == typeof( MinotaurMorphArms ) )
			{
				Effects.PlaySound( from.Location, from.Map, 503 );
				from.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );

				if (from is PlayerMobile)
				{
					from.SendMessage( "You Morph!" );
					from.BodyMod = 263;
					from.HueMod = 1931;
					from.NameHue = 39;
				}
				
				Hue = 0x47E;
				Attributes.NightSight = 1;
				ArmorAttributes.SelfRepair = 3;
				Attributes.BonusHits = 40;
				Attributes.CastRecovery = -2;
				Attributes.CastSpeed = -2;
				PhysicalBonus = 17;
				FireBonus = 17;
				ColdBonus = 17;
				PoisonBonus = 3;
				EnergyBonus = 10;

				MinotaurMorphChest chest = from.FindItemOnLayer( Layer.InnerTorso ) as MinotaurMorphChest;
				MinotaurMorphGloves gloves = from.FindItemOnLayer( Layer.Gloves ) as MinotaurMorphGloves;
				MinotaurMorphLegs legs = from.FindItemOnLayer( Layer.Pants ) as MinotaurMorphLegs;
				MinotaurMorphGorget gorget = from.FindItemOnLayer( Layer.Neck ) as MinotaurMorphGorget;
				MinotaurMorphArms arm = from.FindItemOnLayer( Layer.Arms ) as MinotaurMorphArms;

				chest.Hue = 0x47E;
				chest.Attributes.NightSight = 1;
				chest.Attributes.ReflectPhysical = 35;
				chest.Attributes.DefendChance = 20;
				chest.ArmorAttributes.SelfRepair = 3;
				chest.SkillBonuses.SetValues( 0, SkillName.Wrestling, 50.0 );
				chest.PhysicalBonus = 17;
				chest.FireBonus = 17;
				chest.ColdBonus = 17;
				chest.PoisonBonus = 3;
				chest.EnergyBonus = 10;

				gloves.Hue = 0x47E;
				gloves.Attributes.NightSight = 1;
				gloves.ArmorAttributes.SelfRepair = 3;
				gloves.Attributes.BonusStr = 75;
				gloves.Attributes.AttackChance = 15;
				gloves.PhysicalBonus = 17;
				gloves.FireBonus = 17;
				gloves.ColdBonus = 17;
				gloves.PoisonBonus = 3;
				gloves.EnergyBonus = 10;

				legs.Hue = 0x47E;
				legs.Attributes.NightSight = 1;
				legs.ArmorAttributes.SelfRepair = 3;
				legs.Attributes.BonusDex = 15;
				legs.Attributes.BonusMana = -50;
				legs.Attributes.BonusInt = -30;
				legs.PhysicalBonus = 17;
				legs.FireBonus = 17;
				legs.ColdBonus = 17;
				legs.PoisonBonus = 3;
				legs.EnergyBonus = 10;

				gorget.Hue = 0x47E;
				gorget.Attributes.NightSight = 1;
				gorget.ArmorAttributes.SelfRepair = 3;
				gorget.Attributes.RegenHits = 35;
				gorget.PhysicalBonus = 17;
				gorget.FireBonus = 17;
				gorget.ColdBonus = 17;
				gorget.PoisonBonus = 3;
				gorget.EnergyBonus = 10;
				
				arm.Hue = 0x47E;
				arm.ArmorAttributes.SelfRepair = 3;
				arm.Attributes.RegenStam = 10;
				arm.Attributes.NightSight = 1;
				arm.Attributes.WeaponDamage = 250;
				arm.PhysicalBonus = 17;
				arm.FireBonus = 17;
				arm.ColdBonus = 17;
				arm.PoisonBonus = 3;
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

				if ( m.FindItemOnLayer( Layer.InnerTorso ) is MinotaurMorphChest 
				&& m.FindItemOnLayer( Layer.Gloves ) is MinotaurMorphGloves 
				&& m.FindItemOnLayer( Layer.Arms ) is MinotaurMorphArms 
				&& m.FindItemOnLayer( Layer.Pants ) is MinotaurMorphLegs 
				&& m.FindItemOnLayer( Layer.Neck ) is MinotaurMorphGorget )
				{
					
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
					Attributes.BonusHits = 0;
					Attributes.CastRecovery = 0;
					Attributes.CastSpeed = 0;
					PhysicalBonus = 0;
					FireBonus = 0;
					ColdBonus = 0;
					PoisonBonus = 0;
					EnergyBonus = 0;					
					
					MinotaurMorphChest chest = m.FindItemOnLayer( Layer.InnerTorso ) as MinotaurMorphChest;
					chest.Hue = 0x0;
					chest.Attributes.NightSight = 0;
					chest.Attributes.DefendChance = 0;
					chest.Attributes.ReflectPhysical = 0;
					chest.ArmorAttributes.SelfRepair = 0;
					chest.SkillBonuses.SetValues( 0, SkillName.Wrestling, 0.0 );
					chest.PhysicalBonus = 0;
					chest.FireBonus = 0;
					chest.ColdBonus = 0;
					chest.PoisonBonus = 0;
					chest.EnergyBonus = 0;

					MinotaurMorphGloves gloves = m.FindItemOnLayer( Layer.Gloves ) as MinotaurMorphGloves;
					gloves.Hue = 0x0;
					gloves.Attributes.NightSight = 0;
					gloves.ArmorAttributes.SelfRepair = 0;
					gloves.Attributes.BonusStr = 0;
					gloves.Attributes.AttackChance = 0;
					gloves.PhysicalBonus = 0;
					gloves.FireBonus = 0;
					gloves.ColdBonus = 0;
					gloves.PoisonBonus = 0;
					gloves.EnergyBonus = 0;
					
					MinotaurMorphArms arm = m.FindItemOnLayer( Layer.Arms ) as MinotaurMorphArms;
					arm.Hue = 0x0;
					arm.Attributes.NightSight = 0;
					arm.ArmorAttributes.SelfRepair = 0;
					arm.Attributes.WeaponDamage = 0;
					arm.PhysicalBonus = 0;
					arm.FireBonus = 0;
					arm.ColdBonus = 0;
					arm.PoisonBonus = 0;
					arm.EnergyBonus = 0;
					arm.Attributes.Luck = 0;
					arm.Attributes.RegenStam = 0;


					MinotaurMorphLegs legs = m.FindItemOnLayer( Layer.Pants ) as MinotaurMorphLegs;
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

					MinotaurMorphGorget gorget = m.FindItemOnLayer( Layer.Neck ) as MinotaurMorphGorget;
					gorget.Hue = 0x0;
					gorget.Attributes.NightSight = 0;
					gorget.ArmorAttributes.SelfRepair = 0;
					gorget.Attributes.RegenHits = 0;
					gorget.PhysicalBonus = 0;
					gorget.FireBonus = 0;
					gorget.ColdBonus = 0;
					gorget.PoisonBonus = 0;
					gorget.EnergyBonus = 0;
				}
				this.InvalidateProperties();
			}
			base.OnRemoved( parent );
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
				Weight = 5.0;
		}
	}
}