using System;
using Server;
using Server.Targeting;

namespace Server.Items
{
	public class ElementalWeightingStone : Item
	{
		private int i_Uses;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Uses { get { return i_Uses; } set { i_Uses = value; InvalidateProperties(); } }

		[Constructable] 
		public ElementalWeightingStone() : this( 5 )
		{
		}

		[Constructable] 
		public ElementalWeightingStone( int uses ) : base( 0x1F14 ) 
		{ 
			Weight = 1.0;
			i_Uses = uses;
			Hue = 0x38C;
			Name = "Elemental Weighting Stone";
		} 

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				if ( Uses < 1 )
				{
					Delete();
					from.SendMessage(32, "This have no charges so it's gone!!!");
				}
				from.SendMessage("Which weapon you want to try to weight?");
				from.Target = new ElementalWeightingStoneTarget(this);
			}
			else
				from.SendMessage("This must be in your backpack to use.");
		}
		
        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Can Increase A Mace/Puglist Glove/Stave's Damage");
			list.Add( 1049644, "Even Damage To All Defenses"); // PARENTHESIS
        }

		public void Weighting(Mobile from, object o)
		{
			if ( o is Item )
			{
				if ( !((Item)o).IsChildOf( from.Backpack ) )
				{
					from.SendMessage(32, "This must be in your backpack to weight");
				}
				else if (o is BaseBashing && ((BaseBashing)o).IsChildOf(from.Backpack))
				{
					BaseBashing weap = o as BaseBashing;
					int i_DI = weap.Attributes.WeaponDamage;
					if (weap.Quality == WeaponQuality.Exceptional)
						i_DI += 15;
					if (i_DI >= 70)
					{
						from.SendMessage(32, "This weapon cannot be weighted any further");
						return;
					}
					else if (from.Skills[SkillName.Blacksmith].Value < 100.0)
						from.SendMessage(32, "You need at least 100.0 blacksmith and magery to weight weapons with elemental power");
					else if (from.Skills[SkillName.Magery].Value < 100.0)
						from.SendMessage(32, "You need at least 100.0 blacksmith and magery to weight weapons with elemental power");
					else if ( !Deleted )
					{
						int bonus = Utility.Random((int)(from.Skills[SkillName.Blacksmith].Value/20));
						if (bonus > 0)
						{
							if (70 < i_DI + bonus)
								bonus = 70 - i_DI;
							weap.Attributes.WeaponDamage += bonus;
							weap.AosElementDamages.Fire = 20;
							weap.AosElementDamages.Cold = 20;
							weap.AosElementDamages.Poison = 20;
							weap.AosElementDamages.Energy = 20;
							weap.AosElementDamages.Physical = 20;
							from.SendMessage(88, "You weighted the weapon with {0} elemental damage increase", bonus);
						}
						else
							from.SendMessage(32, "You fail to weight the weapon");
						if (Uses <= 1)
						{
							from.SendMessage(32, "You used up the Weighting stone");
							Delete();
						}
						else
						{
							--Uses;
							from.SendMessage(32, "You have {0} uses left", Uses);
						}
					}
				}
				else if (o is PugilistGlove && ((PugilistGlove)o).IsChildOf(from.Backpack))
				{
                    PugilistGlove weap = o as PugilistGlove;
					int i_DI = weap.Attributes.WeaponDamage;
					if (weap.Quality == WeaponQuality.Exceptional)
						i_DI += 15;
					if (i_DI >= 70)
					{
						from.SendMessage(32, "This weapon cannot be weighted any further");
						return;
					}
					else if (from.Skills[SkillName.Blacksmith].Value < 100.0)
						from.SendMessage(32, "You need at least 100.0 blacksmith to weight weapons with elemntal power");
					else if ( !Deleted )
					{
						int bonus = Utility.Random((int)(from.Skills[SkillName.Blacksmith].Value/20));
						if (bonus > 0)
						{
							if (70 < i_DI + bonus)
								bonus = 70 - i_DI;
							weap.Attributes.WeaponDamage += bonus;
							weap.AosElementDamages.Fire = 20;
							weap.AosElementDamages.Cold = 20;
							weap.AosElementDamages.Poison = 20;
							weap.AosElementDamages.Energy = 20;
							weap.AosElementDamages.Physical = 20;
							from.SendMessage(88, "You weighted the weapon with {0} elemental damange increase", bonus);
						}
						else
							from.SendMessage(32, "You fail to weight the weapon");
						if (Uses <= 1)
						{
							from.SendMessage(32, "You used up the Weighting stone");
							Delete();
						}
						else
						{
							--Uses;
							from.SendMessage(32, "You have {0} uses left", Uses);
						}
					}
				}
				else if (o is BaseStaff && ((BaseStaff)o).IsChildOf(from.Backpack))
				{
					BaseStaff weap = o as BaseStaff;
					int i_DI = weap.Attributes.WeaponDamage;
					if (weap.Quality == WeaponQuality.Exceptional)
						i_DI += 15;
					if (i_DI >= 50)
					{
						from.SendMessage(32, "This weapon cannot be weighted any further");
						return;
					}
					else if (from.Skills[SkillName.Blacksmith].Value < 100.0)
						from.SendMessage(32, "You need at least 100.0 blacksmith to weight weapons with elemental power");
					else if ( !Deleted )
					{
						int bonus = Utility.Random((int)(from.Skills[SkillName.Blacksmith].Value/20));
						if (bonus > 0)
						{
							if (70 < i_DI + bonus)
								bonus = 70 - i_DI;
							weap.Attributes.WeaponDamage += bonus;
							weap.AosElementDamages.Fire = 20;
							weap.AosElementDamages.Cold = 20;
							weap.AosElementDamages.Poison = 20;
							weap.AosElementDamages.Energy = 20;
							weap.AosElementDamages.Physical = 20;
							from.SendMessage(88, "You weighted the weapon with {0} elemental damange increase", bonus);
						}
						else
							from.SendMessage(32, "You fail to weight the weapon");
						if (Uses <= 1)
						{
							from.SendMessage(32, "You used up the Weighting stone");
							Delete();
						}
						else
						{
							--Uses;
							from.SendMessage(32, "You have {0} uses left", Uses);
						}
					}
				}
				else
				{
					from.SendMessage(32, "You can only enhance edged weapons");
				}
			}
			else
			{
				from.SendMessage(32, "You can only enhance edged weapons");
			}
		}

		public ElementalWeightingStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) i_Uses );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			i_Uses = reader.ReadInt();
			if ( version == 0 ) { Serial sr_Owner = reader.ReadInt(); }
		}
	}

	public class ElementalWeightingStoneTarget : Target
	{
		private ElementalWeightingStone sb_Blade;

		public ElementalWeightingStoneTarget(ElementalWeightingStone blade) : base( 18, false, TargetFlags.None )
		{
			sb_Blade = blade;
		}

		protected override void OnTarget(Mobile from, object targeted)
		{
			if (sb_Blade.Deleted)
				return;

			sb_Blade.Weighting(from, targeted);
		}
	}
}