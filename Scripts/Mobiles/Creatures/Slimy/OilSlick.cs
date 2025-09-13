using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "an oily mess" )]
	public class OilSlick : BaseCreature
	{
		[Constructable]
		public OilSlick() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an oil slick";
			Body = 51;
			BaseSoundID = 278;
			Hue = 0x497;
			CanSwim = true;
			CantWalk = true;

			SetStr( 126, 155 );
			SetDex( 66, 85 );
			SetInt( 101, 125 );

			SetHits( 76, 93 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 25 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.EvalInt, 60.1, 75.0 );
			SetSkill( SkillName.Magery, 60.1, 75.0 );
			SetSkill( SkillName.MagicResist, 100.1, 115.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );

			Fame = 4500;
			Karma = -4500;
		}

		public void OilBurn()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				DoHarmful( m );
				Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
				Effects.PlaySound( m.Location, m.Map, 0x225 );
				int itHurts = (int)( (Utility.RandomMinMax(10,20) * ( 100 - m.FireResistance ) ) / 100 );
				m.Damage( itHurts, m );
				m.SendMessage( "The oil covers you and magically ignites!" );
			}
		}

		public override void OnGaveMeleeAttack( Mobile m )
		{
			base.OnGaveMeleeAttack( m );

			if ( 1 == Utility.RandomMinMax( 1, 20 ) )
			{
				Container cont = m.Backpack;
				Item iWrapped = Server.Items.HiddenTrap.GetMyItem( m );

				if ( iWrapped != null )
				{
					if ( Server.Items.HiddenTrap.IAmShielding( m, 100 ) || Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
					{
					}
					else if ( Server.Items.HiddenTrap.CheckInsuranceOnTrap( iWrapped, m ) == true )
					{
						m.LocalOverheadMessage(MessageType.Emote, 1150, true, "Oil almost covered one of your protected items!");
					}
					else
					{
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "One of your items is covered in oil!");
						m.PlaySound( 0x364 );
						Container box = new SlimeItem();
						box.DropItem(iWrapped);
						box.ItemID = iWrapped.ItemID;
						box.Hue = this.Hue;
						m.AddToBackpack ( box );
					}
				}
			}

			if ( 0.1 >= Utility.RandomDouble() )
				OilBurn();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
				OilBurn();
		}

		public override bool BleedImmune{ get{ return true; } }

		public OilSlick( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}