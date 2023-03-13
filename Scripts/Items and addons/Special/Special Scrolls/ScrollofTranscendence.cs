using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Engines.Quests;

namespace Server.Items
{
	public class ScrollofTranscendence : SpecialScroll
	{
		public override int LabelNumber { get { return 1094934; } } // Scroll of Transcendence
		
		public override int Message { get { return 1094933; } } /*Using a Scroll of Transcendence for a given skill will permanently increase your current 
																*level in that skill by the amount of points displayed on the scroll.
																*As you may not gain skills beyond your maximum skill cap, any excess points will be lost.*/
																
		public override string DefaultTitle { get { return String.Format( "<basefont Color=#FBFBFB>Scroll of Transcendence ({0} Skill):</basefont>", Value ); } }

		public static ScrollofTranscendence CreateRandom( int min, int max )
		{
			SkillName skill = (SkillName)Utility.Random( SkillInfo.Table.Length );

			return new ScrollofTranscendence(skill, Utility.RandomMinMax(min, max) * 0.1);
		}

		public ScrollofTranscendence() : this( SkillName.Alchemy, 0.0 )
		{
		}
		
		[Constructable]
		public ScrollofTranscendence( SkillName skill, double value ) : base( skill, value )
		{
			ItemID = 0x14EF;
			Hue = 0x490;
		}

		public ScrollofTranscendence(Serial serial) : base(serial)
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties(list);

			if ( Value == 1 )
				list.Add(1076759, "{0}\t{1}.0 Skill Points", GetName(), Value);
			else
				list.Add(1076759, "{0}\t{1} Skill Points", GetName(), Value);
		}
		
		public override bool CanUse( Mobile from )
		{
			if ( !base.CanUse( from ) )
				return false;
			
			PlayerMobile pm = from as PlayerMobile;
			
			if ( pm == null )
				return false;
			
			#region Mondain's Legacy
			/* to add when skillgain quests will be implemented
			
			for (int i = pm.Quests.Count - 1; i >= 0; i--)
			{
				BaseQuest quest = pm.Quests[i];

				for (int j = quest.Objectives.Count - 1; j >= 0; j--)
				{
					BaseObjective objective = quest.Objectives[j];

					if (objective is ApprenticeObjective)
					{
						from.SendMessage("You are already under the effect of an enhanced skillgain quest.");
						return false;
					}
				}
			}
			
			*/
			#endregion
			
			#region Scroll of Alacrity
			if (pm.AcceleratedStart > DateTime.UtcNow)
			{
				from.SendLocalizedMessage(1077951); // You are already under the effect of an accelerated skillgain scroll.
				return false;
			}
			#endregion
			
			return true;
		}

		public override void Use( Mobile from )
		{
			if ( !CanUse( from ) )
				return;
			
			double tskill = from.Skills[Skill].Base; // value of skill without item bonuses etc
			double tcap = from.Skills[Skill].Cap; // maximum value permitted
			bool canGain = false;
			
			double newValue = Value;

			if ( ( tskill + newValue ) > tcap )
				newValue = tcap - tskill;

			if ( tskill < tcap && from.Skills[Skill].Lock == SkillLock.Up )
			{
				if ( ( from.SkillsTotal + newValue * 10 ) > from.SkillsCap )
				{
					int ns = from.Skills.Length; // number of items in from.Skills[]

					for ( int i = 0; i < ns; i++ )
					{
						// skill must point down and its value must be enough
						if ( from.Skills[i].Lock == SkillLock.Down && from.Skills[i].Base >= newValue )
						{
							from.Skills[i].Base -= newValue;
							canGain = true;
							break;
						}
					}
				}
				else
					canGain = true;
			}
			
			if ( !canGain )
			{
				from.SendLocalizedMessage( 1094935 );	/*You cannot increase this skill at this time. The skill may be locked or set to lower in your skill menu.
														*If you are at your total skill cap, you must use a Powerscroll to increase your current skill cap.*/
				return;
			}

			from.SendLocalizedMessage( 1049513, GetNameLocalized() ); // You feel a surge of magic as the scroll enhances your ~1_type~!
					
			from.Skills[Skill].Base += newValue;

			Effects.PlaySound( from.Location, from.Map, 0x1F7 );
			Effects.SendTargetParticles( from, 0x373A, 35, 45, 0x00, 0x00, 9502, (EffectLayer)255, 0x100 );
			Effects.SendTargetParticles( from, 0x376A, 35, 45, 0x00, 0x00, 9502, (EffectLayer)255, 0x100 );

			Delete();
		}
		
		public static Item RandomTranscendence()
		{
			
			Item scroll = CreateRandom(1, 3);

			return scroll;
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize(reader);

			int version = ( InheritsItem ? 0 : reader.ReadInt() ); //Required for SpecialScroll insertion

			LootType = LootType.Cursed;
			

			if (Hue == 0x7E)
				Hue = 0x490;
		}
	}
}
