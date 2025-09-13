using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	
	public class MagicTunic : BaseMiddleTorso
	{

		[Constructable]
		public MagicTunic() : base( 0x1fa1 )
		{
                        Name = "Magical Tunic";
			Weight = 1.0;


                       int val = Utility.RandomList(1,2,3,4,5,6,7,8,9,10);
                       switch ( Utility.Random( 53 ) )   
			{
		                   
		                case 0: SkillBonuses.SetValues( 0, SkillName.Alchemy,  val  ); break;
				case 1: SkillBonuses.SetValues( 0, SkillName.Anatomy, val ); break;
				case 2: SkillBonuses.SetValues( 0, SkillName.AnimalLore, val ); break;
                                case 3: SkillBonuses.SetValues( 0, SkillName.ItemID, val ); break;
				case 4: SkillBonuses.SetValues( 0, SkillName.ArmsLore, val ); break;
                                case 5: SkillBonuses.SetValues( 0, SkillName.Parry, val ); break;
                                case 6: SkillBonuses.SetValues( 0, SkillName.Begging, val ); break;
				case 7: SkillBonuses.SetValues( 0, SkillName.Blacksmith, val ); break;
				case 8: SkillBonuses.SetValues( 0, SkillName.Fletching, val ); break;
                                case 9: SkillBonuses.SetValues( 0, SkillName.Peacemaking , val ); break;
				case 10: SkillBonuses.SetValues( 0,SkillName.Camping, val ); break;
				case 11: SkillBonuses.SetValues( 0,SkillName.Carpentry, val ); break;
                                case 12: SkillBonuses.SetValues( 0,SkillName.Cartography, val ); break;
				case 13: SkillBonuses.SetValues( 0,SkillName.Cooking, val ); break;
				case 14: SkillBonuses.SetValues( 0,SkillName.DetectHidden, val ); break;
                                case 15: SkillBonuses.SetValues( 0,SkillName.Discordance, val ); break;
				//case 16: SkillBonuses.SetValues( 0,SkillName.EvalInt, val ); break;
				case 16: SkillBonuses.SetValues( 0,SkillName.Healing, val ); break;
                                case 17: SkillBonuses.SetValues( 0,SkillName.Fishing, val ); break;
				case 18: SkillBonuses.SetValues( 0,SkillName.Forensics, val ); break;
				case 19: SkillBonuses.SetValues( 0,SkillName.Herding, val ); break;
                                case 20: SkillBonuses.SetValues( 0,SkillName.Hiding, val ); break;
				case 21: SkillBonuses.SetValues( 0,SkillName.Provocation, val ); break;
				case 22: SkillBonuses.SetValues( 0,SkillName.Inscribe, val ); break;
                                case 23: SkillBonuses.SetValues( 0,SkillName.Lockpicking, val ); break;
				case 24: SkillBonuses.SetValues( 0,SkillName.Magery, val ); break;
				case 25: SkillBonuses.SetValues( 0,SkillName.MagicResist, val ); break;
                                case 26: SkillBonuses.SetValues( 0,SkillName.Tactics, val ); break;
				case 27: SkillBonuses.SetValues( 0,SkillName.Snooping, val ); break;
				case 28: SkillBonuses.SetValues( 0,SkillName.Musicianship, val ); break;
                                case 29: SkillBonuses.SetValues( 0,SkillName.Poisoning, val ); break;
				case 30: SkillBonuses.SetValues( 0,SkillName.Archery, val ); break;
				case 31: SkillBonuses.SetValues( 0,SkillName.SpiritSpeak, val ); break;
                                case 32: SkillBonuses.SetValues( 0,SkillName.Stealing, val ); break;
				case 33: SkillBonuses.SetValues( 0,SkillName.Tailoring, val ); break;
				case 34: SkillBonuses.SetValues( 0,SkillName.AnimalTaming, val ); break;
                                case 35: SkillBonuses.SetValues( 0,SkillName.TasteID, val ); break;
				case 36: SkillBonuses.SetValues( 0,SkillName.Tinkering, val ); break;
				case 37: SkillBonuses.SetValues( 0,SkillName.Tracking , val ); break;
                                case 38: SkillBonuses.SetValues( 0,SkillName.Veterinary, val ); break;
				case 39: SkillBonuses.SetValues( 0,SkillName.Swords, val ); break;
				case 40: SkillBonuses.SetValues( 0,SkillName.Macing, val ); break;
                                case 41: SkillBonuses.SetValues( 0,SkillName.Fencing, val ); break;
				case 42: SkillBonuses.SetValues( 0,SkillName.Wrestling, val ); break;
				case 43: SkillBonuses.SetValues( 0,SkillName.Lumberjacking, val ); break;
                                case 44: SkillBonuses.SetValues( 0,SkillName.Mining, val ); break;
				case 45: SkillBonuses.SetValues( 0,SkillName.Meditation, val ); break;
				case 46: SkillBonuses.SetValues( 0,SkillName.Stealth, val ); break;
                                case 47: SkillBonuses.SetValues( 0,SkillName.RemoveTrap, val ); break;
				case 48: SkillBonuses.SetValues( 0,SkillName.Necromancy , val ); break;
				case 49: SkillBonuses.SetValues( 0,SkillName.Focus, val ); break;
                                case 50: SkillBonuses.SetValues( 0,SkillName.Chivalry, val ); break;
				case 51: SkillBonuses.SetValues( 0,SkillName.Bushido, val ); break;
				case 52: SkillBonuses.SetValues( 0,SkillName.Ninjitsu, val ); break;
                          
                         }

                }

		public MagicTunic( Serial serial ) : base( serial )
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