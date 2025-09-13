using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;

namespace Server.Spells.Research
{
	public class ResearchWithstandDeath : ResearchSpell
	{
		public override int spellIndex { get { return 58; } }
		public int CirclePower = 8;
		public static int spellID = 58;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.25 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
                245,
                9062
			);

        public ResearchWithstandDeath( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
        {
        }
 
		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( Caster.Backpack.FindItemByType( typeof ( Sapphire ) ) == null )
			{
				Caster.SendMessage( "You need a sapphire to cast this spell!" );
				return false;
			}

			return true;
		}

        public override void OnCast()
        {
			Caster.SendMessage( "Choose who you are going to summon a jewel for." );
            Caster.Target = new InternalTarget( this );
        }
 
        public void Target( Mobile m )
        {
            if ( !Caster.CanSee( m ) )
            {
                Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
            }
			else if ( Caster.Backpack.FindItemByType( typeof ( Sapphire ) ) == null )
			{
				Caster.SendMessage( "You need a sapphire to cast this spell!" );
			}
            else if ( CheckBSequence( m, true ) )
            {
                m.PlaySound( 0x214 );
                m.FixedEffect( 0x376A, 10, 16, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0 );
				m.SendMessage( "A magical jewel has been summoned to protect you from death." );
				Item jewel = new JewelImmortality();
				m.AddToBackpack( jewel );
				Item sapphire = Caster.Backpack.FindItemByType( typeof ( Sapphire ) );
				if ( sapphire != null ){ sapphire.Consume(); }
				Server.Misc.Research.ConsumeScroll( Caster, true, spellIndex, true );
            }

            FinishSequence();
        }
 
        private class InternalTarget : Target
        {
            private ResearchWithstandDeath m_Owner;
 
			public InternalTarget( ResearchWithstandDeath owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Beneficial )
            {
                m_Owner = owner;
            }
 
            protected override void OnTarget( Mobile from, object o )
            {
                if ( o is Mobile )
                {
                    m_Owner.Target( (Mobile)o );
                }
                else if ( o is Item )
                {
					from.SendMessage( "This spell will not work on that." );
                }
            }
 
            protected override void OnTargetFinish( Mobile from )
            {
                m_Owner.FinishSequence();
            }
        }
    }
}

namespace Server.Items
{
    public class JewelImmortality : Item
	{
        [Constructable]
        public JewelImmortality() : base( 0xF19 )
		{
            Name = "jewel of immortality";
			Hue = 0xB73;
			Weight = 1.0;
			LootType = LootType.Blessed;
			Light = LightType.Circle150;
		}

		public override bool DisplayLootType{ get{ return false; } }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Imbued with Magic");
            list.Add( 1049644, "Magically Avoid Death");
        }

        public JewelImmortality( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
	    }
    }
}

namespace Server.Misc
{
    class SeeIfJewelInBag
    {
		public static bool IHaveAJewel( Mobile m )
		{
			if ( m.Backpack.FindItemByType( typeof ( JewelImmortality ) ) != null && m is PlayerMobile && m != null )
			{
				Item rock = m.Backpack.FindItemByType( typeof ( JewelImmortality ) );
				m.Hits = m.HitsMax;
				m.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
				m.SendMessage( "You are restored with the power of the jewel!" );
				m.CurePoison( m );
				m.PlaySound( 0x202 );
				rock.Delete();

				return true;
			}

			return false;
		}

		public static bool JewelInPocket( Mobile m )
		{
			if ( m.Backpack.FindItemByType( typeof ( JewelImmortality ) ) != null && m is PlayerMobile && m != null )
			{
				return true;
			}

			return false;
		}
	}
}