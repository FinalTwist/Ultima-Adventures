using System;
using Server.Items;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a broken machine" )]
	public class MetalGolem : BaseCreature
	{
		private bool m_Stunning;

		public string RealName;
		[CommandProperty( AccessLevel.GameMaster )]
		public string p_RealName { get{ return RealName; } set{ RealName = value; } }

		[Constructable]
		public MetalGolem() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			Name = "a construct";
			Body = Utility.RandomList( 752, 358 );
			
			Item ore = new CopperIngot( Utility.RandomMinMax( 13, 21 ) ); ore.Delete();

			double scalar = 1.0;

			switch ( Utility.Random( 10 ) )
			{
				case 0: ore = new IronIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = 0x430; scalar = 1.0; RealName = "an iron construct"; break; // Iron
				case 1: ore = new DullCopperIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = MaterialInfo.GetMaterialColor( "dull copper", "monster", 0 ); scalar = 1.1; RealName = "a dull copper construct"; break; // Dull Copper
				case 2: ore = new ShadowIronIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = MaterialInfo.GetMaterialColor( "shadow iron", "monster", 0 ); scalar = 1.2; RealName = "a shadow iron construct"; break; // Shadow Iron
				case 3: ore = new CopperIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = MaterialInfo.GetMaterialColor( "copper", "monster", 0 ); scalar = 1.3; RealName = "a copper construct"; break; // Copper
				case 4: ore = new BronzeIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = MaterialInfo.GetMaterialColor( "bronze", "monster", 0 ); scalar = 1.4; RealName = "a bronze construct"; break; // Bronze
				case 5: ore = new GoldIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = MaterialInfo.GetMaterialColor( "gold", "monster", 0 ); scalar = 1.5; RealName = "a golden construct"; break; // Gold
				case 6: ore = new AgapiteIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = MaterialInfo.GetMaterialColor( "agapite", "monster", 0 ); scalar = 1.6; RealName = "an agapite construct"; break; // Agapite
				case 7: ore = new VeriteIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = MaterialInfo.GetMaterialColor( "verite", "monster", 0 ); scalar = 1.7; RealName = "a verite construct"; break; // Verite
				case 8: ore = new ValoriteIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = MaterialInfo.GetMaterialColor( "valorite", "monster", 0 ); scalar = 1.8; RealName = "a valorite construct"; break; // Valorite
				case 9:
					if ( Worlds.IsExploringSeaAreas( this ) == true ){ ore = new NepturiteIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = MaterialInfo.GetMaterialColor( "nepturite", "monster", 0 ); scalar = 1.9; RealName = "a nepturite construct"; }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Serpent Island" ){ ore = new ObsidianIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = MaterialInfo.GetMaterialColor( "obsidian", "monster", 0 ); scalar = 1.9; RealName = "an obsidian construct"; }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" && this.Map == Map.TerMur ){ ore = new XormiteIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = MaterialInfo.GetMaterialColor( "xormite", "monster", 0 ); scalar = 2.0; RealName = "a xormite construct"; }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" ){ ore = new MithrilIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = MaterialInfo.GetMaterialColor( "mithril", "monster", 0 ); scalar = 2.1; RealName = "a mithril construct"; }
					else { ore = new IronIngot( Utility.RandomMinMax( 13, 21 ) ); Hue = 0x430; scalar = 1.0; RealName = "an iron construct"; }
					break; // Special
			}

			PackItem( ore );


			SetStr( (int)(251*scalar), (int)(350*scalar) );
			SetDex( (int)(76*scalar), (int)(100*scalar) );
			SetInt( (int)(101*scalar), (int)(150*scalar) );

			SetHits( (int)(151*scalar), (int)(210*scalar) );

			SetDamage( (int)(13*scalar), (int)(24*scalar) );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, (int)(35*scalar), (int)(55*scalar) );

			SetResistance( ResistanceType.Fire, (int)(100*scalar) );

			SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, (int)(30*scalar), (int)(40*scalar) );

			SetSkill( SkillName.MagicResist, (150.1*scalar), (190.0*scalar) );
			SetSkill( SkillName.Tactics, (60.1*scalar), (100.0*scalar) );
			SetSkill( SkillName.Wrestling, (60.1*scalar), (100.0*scalar) );

			Fame = (int)(3500*scalar);
			Karma = (int)(-3500*scalar);

			if ( 0.1 > Utility.RandomDouble() )
				PackItem( new PowerCrystal() );

			if ( 0.15 > Utility.RandomDouble() )
				PackItem( new ClockworkAssembly() );

			if ( 0.2 > Utility.RandomDouble() )
				PackItem( new ArcaneGem() );

			if ( 0.25 > Utility.RandomDouble() )
				PackItem( new Gears( Utility.RandomMinMax( 1, 5 ) ) );

			if ( 0.25 > Utility.RandomDouble() )
				PackItem( new BottleOil( Utility.RandomMinMax( 1, 5 ) ) );
		}

		public override int GetAngerSound()
		{
			return 541;
		}

		public override int GetIdleSound()
		{
			if ( !Controlled )
				return 542;

			return base.GetIdleSound();
		}

		public override int GetDeathSound()
		{
			if ( !Controlled )
				return 545;

			return base.GetDeathSound();
		}

		public override int GetAttackSound()
		{
			return 562;
		}

		public override int GetHurtSound()
		{
			if ( Controlled )
				return 320;

			return base.GetHurtSound();
		}

		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( !m_Stunning && 0.3 > Utility.RandomDouble() )
			{
				m_Stunning = true;

				defender.Animate( 21, 6, 1, true, false, 0 );
				this.PlaySound( 0xEE );
				defender.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You have been stunned by a colossal blow!" );

				BaseWeapon weapon = this.Weapon as BaseWeapon;
				if ( weapon != null )
					weapon.OnHit( this, defender );

				if ( defender.Alive )
				{
					defender.Frozen = true;
					Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerStateCallback( Recover_Callback ), defender );
				}
			}
		}

		private void Recover_Callback( object state )
		{
			Mobile defender = state as Mobile;

			if ( defender != null )
			{
				defender.Frozen = false;
				defender.Combatant = null;
				defender.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You recover your senses." );
			}

			m_Stunning = false;
		}

		public MetalGolem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( RealName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			RealName = reader.ReadString();
		}
	}
}