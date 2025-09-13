using System;
using Server;
using System.Collections;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class BaseElixir : BasePotion
	{
		public override int Hue{ get { return ( Server.Items.PotionKeg.GetPotionColor( this ) ); } }

		[Constructable]
		public BaseElixir( PotionEffect p ) : base( 0x1FD9, p )
		{
		}

		public BaseElixir( Serial serial ) : base( serial )
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

		public static int Buff( Mobile m, string category, int level, SkillName skill )
		{
			int value = 10;

			int time = 120; 												// 2 MINUTES MINIMUM
			int bonus = 2 * EnhancePotions( m ); 							// MAX 160
			int skill1 = 2 * (int)(m.Skills[SkillName.Cooking].Value); 		// MAX 250
			int skill2 = 2 * (int)(m.Skills[SkillName.TasteID].Value); 		// MAX 250
			int TotalTime = (int)(( time + bonus + skill1 + skill2 ) / 120);
				if ( level > 0 ){ TotalTime = (int)(( time + bonus + skill1 + skill2 ) / 60); }

			int buff_default = 10;											// +10 DEFAULT
			int buff_bonus = (int)(EnhancePotions( m ) / 8 ); 				// +10 MAX
			int buff_skill1 = (int)(m.Skills[SkillName.Cooking].Value / 5); // +20 MAX
			int buff_skill2 = (int)(m.Skills[SkillName.TasteID].Value / 5); // +20 MAX
			int TotalBuff = ( buff_default + buff_bonus + buff_skill1 + buff_skill2 );

			int MySkill = 125 - (int)m.Skills[skill].Base;
				if ( MySkill < 1 ){ MySkill = 1; }
				if ( MySkill > TotalBuff ){ MySkill = TotalBuff; }

			if ( category == "time" ){ value = TotalTime; }
			else if ( category == "strength" ){ value = MySkill; }

			return value;
		}

		public override void Drink( Mobile m )
		{
		}

		public static bool DrankTooMuch( Mobile m )
		{
			int elix = 0;

			if ( !m.CanBeginAction( typeof( ElixirAlchemy ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirAnatomy ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirAnimalLore ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirAnimalTaming ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirArchery ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirArmsLore ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirBegging ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirBlacksmith ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirCamping ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirCarpentry ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirCartography ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirCooking ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirDetectHidden ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirDiscordance ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirEvalInt ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirFencing ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirFishing ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirFletching ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirFocus ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirForensics ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirHealing ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirHerding ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirHiding ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirInscribe ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirItemID ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirLockpicking ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirLumberjacking ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirMacing ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirMagicResist ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirMeditation ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirMining ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirMusicianship ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirParry ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirPeacemaking ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirPoisoning ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirProvocation ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirRemoveTrap ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirSnooping ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirSpiritSpeak ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirStealing ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirStealth ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirSwords ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirTactics ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirTailoring ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirTasteID ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirTinkering ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirTracking ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirVeterinary ) ) ){ elix++; }
			if ( !m.CanBeginAction( typeof( ElixirWrestling ) ) ){ elix++; }

			if ( elix >= 2 )
				return false;

			return true;
		}
	}
}