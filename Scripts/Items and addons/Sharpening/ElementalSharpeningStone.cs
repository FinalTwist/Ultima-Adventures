using System;
using Server;
using Server.Targeting;

namespace Server.Items
{
	public class ElementalSharpeningStone : Item
	{
		private int i_Uses;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Uses { get { return i_Uses; } set { i_Uses = value; InvalidateProperties(); } }

		[Constructable] 
		public ElementalSharpeningStone() : this( 5 )
		{
		}

		[Constructable] 
		public ElementalSharpeningStone( int uses ) : base( 0x1F14 ) 
		{ 
			Weight = 1.0;
			i_Uses = uses;
			Hue = 0x38C;
			Name = "Elemental Sharpening Stone";
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
				from.SendMessage("Which weapon you want to try to sharpen?");
				from.Target = new ElementalSharpeningStoneTarget(this);
			}
			else
				from.SendMessage("This must be in your backpack to use.");
		}
		
        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Can Increase A Bladed Weapon's Damage");
			list.Add( 1049644, "Even Damage To All Defenses"); // PARENTHESIS
        }

		public void Sharpening(Mobile from, object o)
		{
			if ( o is Item )
			{
				if ( !((Item)o).IsChildOf( from.Backpack ) )
				{
					from.SendMessage(32, "This must be in your backpack to sharpen");
				}
				else if (o is BaseSword && ((BaseSword)o).IsChildOf(from.Backpack))
				{
					BaseSword weap = o as BaseSword;
					int i_DI = weap.Attributes.WeaponDamage;
					if (weap.Quality == WeaponQuality.Exceptional)
						i_DI += 15;
					if (i_DI >= 70)
					{
						from.SendMessage(32, "This weapon cannot be sharpened any further");
						return;
					}
					else if (from.Skills[SkillName.Blacksmith].Value < 100.0)
						from.SendMessage(32, "You need at least 100.0 blacksmith and magery to sharpen weapons with elemental power");
					else if (from.Skills[SkillName.Magery].Value < 100.0)
						from.SendMessage(32, "You need at least 100.0 blacksmith and magery to sharpen weapons with elemental power");
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
							from.SendMessage(88, "You sharpened the weapon with {0} elemental damage increase", bonus);
						}
						else
							from.SendMessage(32, "You fail to sharpen the weapon");
						if (Uses <= 1)
						{
							from.SendMessage(32, "You used up the sharpening stone");
							Delete();
						}
						else
						{
							--Uses;
							from.SendMessage(32, "You have {0} uses left", Uses);
						}
					}
				}
				else if (o is BaseKnife && ((BaseKnife)o).IsChildOf(from.Backpack))
				{
					BaseKnife weap = o as BaseKnife;
					int i_DI = weap.Attributes.WeaponDamage;
					if (weap.Quality == WeaponQuality.Exceptional)
						i_DI += 15;
					if (i_DI >= 70)
					{
						from.SendMessage(32, "This weapon cannot be sharpened any further");
						return;
					}
					else if (from.Skills[SkillName.Blacksmith].Value < 100.0)
						from.SendMessage(32, "You need at least 100.0 blacksmith to sharpen weapons with elemntal power");
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
							from.SendMessage(88, "You sharpened the weapon with {0} elemental damange increase", bonus);
						}
						else
							from.SendMessage(32, "You fail to sharpen the weapon");
						if (Uses <= 1)
						{
							from.SendMessage(32, "You used up the sharpening stone");
							Delete();
						}
						else
						{
							--Uses;
							from.SendMessage(32, "You have {0} uses left", Uses);
						}
					}
				}
				else if (o is BaseAxe && ((BaseAxe)o).IsChildOf(from.Backpack))
				{
					BaseAxe weap = o as BaseAxe;
					int i_DI = weap.Attributes.WeaponDamage;
					if (weap.Quality == WeaponQuality.Exceptional)
						i_DI += 15;
					if (i_DI >= 50)
					{
						from.SendMessage(32, "This weapon cannot be sharpened any further");
						return;
					}
					else if (from.Skills[SkillName.Blacksmith].Value < 100.0)
						from.SendMessage(32, "You need at least 100.0 blacksmith to sharpen weapons with elemental power");
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
							from.SendMessage(88, "You sharpened the weapon with {0} elemental damange increase", bonus);
						}
						else
							from.SendMessage(32, "You fail to sharpen the weapon");
						if (Uses <= 1)
						{
							from.SendMessage(32, "You used up the sharpening stone");
							Delete();
						}
						else
						{
							--Uses;
							from.SendMessage(32, "You have {0} uses left", Uses);
						}
					}
				}
                else if (o is BaseSpear && ((BaseSpear)o).IsChildOf(from.Backpack))
                {
                    BaseSpear weap = o as BaseSpear;
                    int i_DI = weap.Attributes.WeaponDamage;
                    if (weap.Quality == WeaponQuality.Exceptional)
                        i_DI += 15;
                    if (i_DI >= 50)
                    {
                        from.SendMessage(32, "This weapon cannot be sharpened any further");
                        return;
                    }
                    else if (from.Skills[SkillName.Blacksmith].Value < 100.0)
                        from.SendMessage(32, "You need at least 100.0 blacksmith to sharpen weapons with elemental power");
                    else if (!Deleted)
                    {
                        int bonus = Utility.Random((int)(from.Skills[SkillName.Blacksmith].Value / 20));
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
                            from.SendMessage(88, "You sharpened the weapon with {0} elemental damange increase", bonus);
                        }
                        else
                            from.SendMessage(32, "You fail to sharpen the weapon");
                        if (Uses <= 1)
                        {
                            from.SendMessage(32, "You used up the sharpening stone");
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

		public ElementalSharpeningStone( Serial serial ) : base( serial )
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

	public class ElementalSharpeningStoneTarget : Target
	{
		private ElementalSharpeningStone sb_Blade;

		public ElementalSharpeningStoneTarget(ElementalSharpeningStone blade) : base( 18, false, TargetFlags.None )
		{
			sb_Blade = blade;
		}

		protected override void OnTarget(Mobile from, object targeted)
		{
			if (sb_Blade.Deleted)
				return;

			sb_Blade.Sharpening(from, targeted);
		}
	}
}