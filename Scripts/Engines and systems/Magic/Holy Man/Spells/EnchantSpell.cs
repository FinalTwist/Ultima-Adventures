using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Spells;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Misc;

namespace Server.Spells.HolyMan
{
	public class EnchantSpell : HolyManSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
				"Enchant", "Fascinare",
				266,
				9040
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
		public override int RequiredTithing{ get{ return 180; } }
		public override double RequiredSkill{ get{ return 90.0; } }
		public override int RequiredMana{ get{ return 45; } }

        public EnchantSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

        public void Target( object o )
        {
			bool success = false;
			var item = o as Item;
			if ( Apply(item, true) )
			{
				var pearl = Caster.Backpack.FindItemByType( typeof ( MysticalPearl ) );
				if (pearl == null || pearl.Deleted)
				{
					Caster.SendMessage( "You need a mystical pearl to cast this spell!" );
				}
				else if (!Caster.CanSee(item))
				{
					Caster.SendLocalizedMessage(500237); // Target can not be seen.
				}
				else if (!Caster.CanBeginAction(typeof(EnchantSpell)))
				{
					Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
				}
				else if ( !item.IsChildOf( Caster.Backpack ) )
				{
					Caster.SendMessage( "The weapon must be in your pack to enchant." );
				}
				else if (CheckSequence())
				{
					success = Apply(item, false);
					if (success)
					{
						pearl.Delete();
						string name = item.Name;
						if ( item.Name != null && item.Name != "" ){ name = item.Name; }
						if ( name == null ){ name = MorphingItem.AddSpacesToSentence( (item.GetType()).Name ); }
						item.Name = name + " [enchanted]";
						item.Hue = 0x9C4;

						Caster.FixedParticles( 0x375A, 9, 20, 5027, EffectLayer.Waist );
						Caster.PlaySound( 0x1F7 );
					}
				}
			}

			if (!success)
			{
				Caster.SendMessage( "You can only enchant instruments, weapons, and spellbooks with this spell." );
			}

            FinishSequence();
        }

		private bool Apply(Item item, bool checkOnly)
		{
			if (item != null && item.CanAugment() && (item is BaseInstrument || item is BaseWeapon || item is Spellbook))
			{
				if (checkOnly) return true;

				if (item is BaseInstrument)
				{
					return item.AugmentInstrument(i =>
						{
							i.Attributes.SpellDamage = 50;
							i.Slayer = SlayerName.Silver;
							i.Slayer2 = SlayerName.Exorcism;
						});
				}
				
				if (item is Spellbook)
				{
					return item.AugmentSpellbook(i =>
						{
							i.Attributes.SpellDamage = 50;
							i.Slayer = SlayerName.Silver;
							i.Slayer2 = SlayerName.Exorcism;
						});
				}

				if (item is BaseWeapon)
				{
					return item.AugmentWeapon(i =>
						{
							i.Attributes.SpellDamage = 50;
							i.Slayer = SlayerName.Silver;
							i.Slayer2 = SlayerName.Exorcism;
						});
				}
				
			}

			return false;
		}

        private class InternalTarget : Target
        {
            private EnchantSpell m_Owner;

            public InternalTarget( EnchantSpell owner ) : base(12, false, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is BaseWeapon)
                {
                    m_Owner.Target( (BaseWeapon)o );
                }
                else
                {
                    from.SendMessage("That cannot be enchanted.");
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}

namespace Server.Items
{
	public class EnchantSpellStone : Item
	{

		private class InternalTimer : Timer
		{
			private Mobile m_m;
			private DateTime m_Expire;

			public InternalTimer( Mobile Caster, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 0.1 ) )
			{
				m_m = Caster;
				m_Expire = DateTime.UtcNow + duration;
			}

			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					EndEffects( m_m );
					Stop();
				}
			}
		}

		private static void EndEffects( Mobile m )
		{
			int serial = 0;
			string name = "";
			int hue = 0;
			int dmg = 0;

			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is EnchantSpellStone )
				{
					EnchantSpellStone orb = (EnchantSpellStone)item;
					if ( ( orb.EnchantOwner == m && m != null ) || m == null )
					{
						serial = orb.EnchantSerial;
						name = orb.EnchantName;
						hue = orb.EnchantHue;
						dmg = orb.EnchantDmg;
						targets.Add( item );
					}
				}
			}

			for ( int i = 0; i < targets.Count; ++i )
			{
				Item item = ( Item )targets[ i ];
				item.Delete();
				}

			foreach ( Item item in World.Items.Values )
			{
				if ( item.Serial == serial && item is BaseWeapon )
				{
					if ( (item.Name).Contains("[enchanted]") )
					{
						BaseWeapon w = (BaseWeapon)item;
						w.Hue = hue;
						w.Name = name;
						w.Attributes.WeaponDamage = dmg;

					}
				}
			}

			if ( m != null ){ m.PlaySound( 0x1F8 ); }
		}

		[Constructable]
		public EnchantSpellStone() : base( 0x3199 )
		{
			Weight = 1.0;
			Movable = false;
			Hue = 0x9C4;
			Name = "Holy Enchantment Orb";
			LootType = LootType.Blessed;
		}

		public override bool DisplayLootType{ get{ return false; } }

		public EnchantSpellStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version

			writer.Write( (Mobile)EnchantOwner );
			writer.Write( EnchantSerial );
			writer.Write( EnchantName );
			writer.Write( EnchantDmg );
			writer.Write( EnchantHue );
			writer.Write( (int)EnchantSlayer1 );
			writer.Write( (int)EnchantSlayer2 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			EnchantOwner = reader.ReadMobile();
			EnchantSerial = reader.ReadInt();
			EnchantName = reader.ReadString();
			EnchantDmg = reader.ReadInt();
			EnchantHue = reader.ReadInt();
			EnchantSlayer1 = (SlayerName)reader.ReadInt();
			EnchantSlayer2 = (SlayerName)reader.ReadInt();

			RunTime thisTimer = new RunTime( this ); 
			thisTimer.Start();
		}

		public class RunTime : Timer 
		{ 
			public RunTime( Item task ) : base( TimeSpan.FromSeconds( 10.0 ) )
			{ 
				Priority = TimerPriority.OneSecond; 
			} 

			protected override void OnTick() 
			{
				EndEffects( null );
			} 
		}

		public Mobile EnchantOwner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Enchant_Owner { get{ return EnchantOwner; } set{ EnchantOwner = value; } }

		public int EnchantSerial;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Enchant_Serial { get{ return EnchantSerial; } set{ EnchantSerial = value; } }

		public string EnchantName;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Enchant_Name { get{ return EnchantName; } set{ EnchantName = value; } }

		public int EnchantDmg;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Enchant_Dmg { get{ return EnchantDmg; } set{ EnchantDmg = value; } }

		public int EnchantHue;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Enchant_Hue { get{ return EnchantHue; } set{ EnchantHue = value; } }

		public SlayerName EnchantSlayer1;
		[CommandProperty( AccessLevel.GameMaster )]
		public SlayerName Enchant_Slayer1{ get{ return EnchantSlayer1; } set{ EnchantSlayer1 = value; InvalidateProperties(); } }

		public SlayerName EnchantSlayer2;
		[CommandProperty( AccessLevel.GameMaster )]
		public SlayerName Enchant_Slayer2{ get{ return EnchantSlayer2; } set{ EnchantSlayer2 = value; InvalidateProperties(); } }
	}
}