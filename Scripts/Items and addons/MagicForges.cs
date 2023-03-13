using System;
using Server; 
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Items;
using Server.Regions;
using Server.Network;
using Server.Misc;
using Server.Engines.PartySystem;

namespace Server.Items
{
	public class MagicForges : Item
	{
		public override bool HandlesOnSpeech{ get{ return true; } }

		public override void OnSpeech( SpeechEventArgs e )
		{
			if ( !e.Handled )
			{
				Mobile m = e.Mobile;

				string keyword = "";
             bool priestCheck = true;
				if ( m.Region.IsPartOf( "the Altar of Golden Rangers" ) || m.Region.IsPartOf( "the Ranger Outpost" ) ){ keyword = "Aurum"; }
				else if ( m.Region.IsPartOf( "the Moon's Core" ) ){ keyword = "Ultimum Potentiae"; }
				else if ( m.Region.IsPartOf( "the Black Magic Guild" ) ){ keyword = "Kas"; }
				else if ( m.Region.IsPartOf( "the Tomb of Kas the Bloody Handed" ) ){ keyword = "Mortem Mangone"; }
				else if ( m.Region.IsPartOf( "the Tomb of Malak the Syth Lord" ) ){ keyword = "Anakasu Arrii Venaal"; }
				else if ( m.Region.IsPartOf( "the Tomb of Zoda the Jedi Master" ) ){ keyword = "Oh Beh Wahn"; }
				else if ( m.Region.IsPartOf( "Dungeon Rock" ) ){ keyword = "Vas An Ort Ailem"; }
				else if ( m.Region.IsPartOf( "the Lost Graveyard" ) ){ keyword = "Cryst An Immortalis"; }
				else if ( m.Map == Map.Malas && m.Region.IsPartOf( "the Ethereal Void" ) ){ keyword = "balance"; }
				else if ( m.Map == Map.Malas && m.Region.IsPartOf( "the Serpent Sanctum" ) ){ keyword = "balance"; }
				
				else if ( this.Name == "Jedi Grave" )
				{
							 if ( this.X == 3083 ){ keyword = "jacen sollo"; }
					else if ( this.X == 806 ){ keyword = "kiadi mundia"; }
					else if ( this.X == 982 ){ keyword = "kip fisto"; }
					else if ( this.X == 3250 ){ keyword = "marra jade"; }
					else if ( this.X == 1399 ){ keyword = "numi sunrider"; }
					else if ( this.X == 2984 ){ keyword = "plo kune"; }
					else if ( this.X == 4379 ){ keyword = "kyle katran"; }
					else if ( this.X == 2698 ){ keyword = "kyp duron"; }
					else if ( this.X == 4168 ){ keyword = "ganer rhysode"; }
					else if ( this.X == 6643 ){ keyword = "coran horn"; }
				
				}
				
				else if ( this.Name == "Priest Grave" )
				{
					if ( this.X == 3082 ){ keyword = "caelesti lumine"; }
					else if ( this.X == 805 ){ keyword = "famem prohibere"; }
					else if ( this.X == 981 ){ keyword = "sacrum munus"; }
					else if ( this.X == 1756 ){ keyword = "tactus vitae"; }
					else if ( this.X == 3249 ){ keyword = "benedicite"; }
					else if ( this.X == 2163 ){ keyword = "igne iudicii"; }
					else if ( this.X == 1398 ){ keyword = "deiectionem"; }
					else if ( this.X == 1854 ){ keyword = "percutiat"; }
					else if ( this.X == 2983 ){ keyword = "malleo fidei"; }
					else if ( this.X == 4378 ){ keyword = "exilium"; }
					else if ( this.X == 2697 ){ keyword = "spiritus mundi"; }
					else if ( this.X == 926 ){ keyword = "accipe spiritum"; }
					else if ( this.X == 4167 ){ keyword = "reditus vitae"; }
					else { keyword = "fascinare"; }
				}
				else if ( this.Name != "Magic Forge Trigger" ){ keyword = this.Name; }
				else if ( m.Map == Map.Felucca ){ keyword = "Dugero"; }
				else if ( m.Map == Map.TerMur ){ keyword = "Urag"; }
				else if ( m.Map == Map.Malas ){ keyword = "Purslos"; }
				else if ( m.Map == Map.Trammel ){ keyword = "Galzan"; }

				if ( !m.Player )
					return;

				if ( !m.InRange( GetWorldLocation(), 10 ) )
					return;

				bool isMatch = false;

				if ( e.Speech.ToLower().IndexOf( keyword.ToLower() ) >= 0 )
					isMatch = true;

				if ( !isMatch )
					return;

				e.Handled = true;

				
				// JEDI CHECK
				if ( !isMatch && m.Karma >=0 && m.Skills[SkillName.EvalInt].Base >= 0 && Server.Misc.GetPlayerInfo.isJedi(m,false) && this.Name == "Jedi Grave"  );
				{
					 	if ( this.X == 3083 ){ keyword = "jacen sollo"; }
					else if ( this.X == 806 ){ keyword = "kiadi mundia"; }
					else if ( this.X == 982 ){ keyword = "kip fisto"; }
					else if ( this.X == 3250 ){ keyword = "marra jade"; }
					else if ( this.X == 1399 ){ keyword = "numi sunrider"; }
					else if ( this.X == 2984 ){ keyword = "plo kune"; }
					else if ( this.X == 4379 ){ keyword = "kyle katran"; }
					else if ( this.X == 2698 ){ keyword = "kyp duron"; }
					else if ( this.X == 4168 ){ keyword = "ganer rhysode"; }
					else if ( this.X == 6643 ){ keyword = "coran horn"; }

					if ( e.Speech.ToLower().IndexOf( keyword.ToLower() ) >= 0 )
					{
						isMatch = true;
						priestCheck = false;
					}
				}

				if ( !isMatch )
					return;

				e.Handled = true;
				
				
				if ( m.Karma >= 0 && m.Skills[SkillName.EvalInt].Base >= 0 && Server.Misc.GetPlayerInfo.isJedi(m,false) && this.Name == "Jedi Grave" );
					{
						string jedi = "";
						string yoda = "";

					
						if ( this.X == 3083 ){ yoda = "Jacen Sollo"; jedi = "JediDatacron01"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron01 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 806 ){ yoda = "Kiadi Mundia"; jedi = "JediDatacron02"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron02 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 982 ){ yoda = "Kip Fisto"; jedi = "JediDatacron03"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron03 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 3250 ){ yoda = "Marra Jade"; jedi = "JediDatacron04"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron04 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 1399 ){ yoda = "Numi Sunrider"; jedi = "JediDatacron05"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron05 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 2984 ){ yoda = "Plo Kune"; jedi = "JediDatacron06"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron06 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 4379 ){ yoda = "Kyle Katran"; jedi = "JediDatacron07"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron07 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 2698 ){ yoda = "Kyp Duron"; jedi = "JediDatacron08"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron08 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 4168 ){ yoda = "Ganer Rhysode"; jedi = "JediDatacron09"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron09 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 6643 ){ yoda = "Coran Horn"; jedi = "JediDatacron10"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron10 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }

						if ( jedi != "" )
						{
							Item cron = null;
							Type itemType = ScriptCompiler.FindTypeByName( jedi );

							if ( itemType != null )
							{
								cron = (Item)Activator.CreateInstance( itemType );
								m.AddToBackpack ( cron );
								m.SendMessage( "You have the holocron of " + yoda + "!" );
								m.FixedParticles( 0x376A, 9, 32, 5005, EffectLayer.Waist );
								m.PlaySound( 0x1F2 );
							}
						}
					}
				
			

				if ( this.Name == "Priest Grave" ) // PRIEST GRAVES
				{
					if ( m.Karma >= 2500 )
					{
						int isPriest = 0;

						foreach ( Item item in World.Items.Values )
						{
							if ( item is HolySymbol )
							{
								HolySymbol symbol = (HolySymbol)item;
								if ( symbol.owner == m )
								{
									isPriest++;
								}
							}
							else if ( item is HolyManSpellbook )
							{
								HolyManSpellbook book = (HolyManSpellbook)item;
								if ( book.owner == m )
								{
									isPriest++;
								}
							}
						}

						if ( isPriest > 0 )
						{
							string symbol = "HolyManSymbol774";
							string priest = "Drumat the Apostle";

							if ( this.X == 3082 ){ priest = "Drumat the Apostle"; symbol = "HolyManSymbol774"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol774 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
							else if ( this.X == 805 ){ priest = "Vincent the Priest"; symbol = "HolyManSymbol775"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol775 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
							else if ( this.X == 981 ){ priest = "Father Michal"; symbol = "HolyManSymbol778"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol778 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
							else if ( this.X == 1756 ){ priest = "Xephyn the Monk"; symbol = "HolyManSymbol782"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol782 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
							else if ( this.X == 3249 ){ priest = "Sister Tiana"; symbol = "HolyManSymbol779"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol779 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
							else if ( this.X == 2163 ){ priest = "Chancellor Davis"; symbol = "HolyManSymbol783"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol783 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
							else if ( this.X == 1398 ){ priest = "Abigayl the Preacher"; symbol = "HolyManSymbol776"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol776 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
							else if ( this.X == 1854 ){ priest = "Edwin the Pope"; symbol = "HolyManSymbol781"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol781 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
							else if ( this.X == 2983 ){ priest = "Deacon Wilems"; symbol = "HolyManSymbol773"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol773 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
							else if ( this.X == 4378 ){ priest = "Patriarch Morden"; symbol = "HolyManSymbol770"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol770 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
							else if ( this.X == 2697 ){ priest = "Brother Kurklan"; symbol = "HolyManSymbol780"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol780 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
							else if ( this.X == 926 ){ priest = "Archbishop Halyrn"; symbol = "HolyManSymbol771"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol771 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
							else if ( this.X == 4167 ){ priest = "Cardinal Greggs"; symbol = "HolyManSymbol777"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol777 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
							else { priest = "Bishop Leantre"; symbol = "HolyManSymbol772"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is HolyManSymbol772 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }

							Item holy = null;
							Type itemType = ScriptCompiler.FindTypeByName( symbol );

							if ( itemType != null )
							{
								holy = (Item)Activator.CreateInstance( itemType );
								m.AddToBackpack ( holy );
								m.SendMessage( "You have the holy symbol of " + priest + "!" );
								m.FixedParticles( 0x376A, 9, 32, 5005, EffectLayer.Waist );
								m.PlaySound( 0x1F2 );
							}
						}
					}
				}
				/*
				else if ( m.Karma >= 0 && m.Skills[SkillName.EvalInt].Base >= 0 && Server.Misc.GetPlayerInfo.isJedi(m,false) )
					{
						string jedi = "";
						string yoda = "";

						if ( this.X == 3082 ){ yoda = "Jacen Sollo"; jedi = "JediDatacron01"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron01 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 805 ){ yoda = "Kiadi Mundia"; jedi = "JediDatacron02"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron02 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 981 ){ yoda = "Kip Fisto"; jedi = "JediDatacron03"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron03 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 3249 ){ yoda = "Marra Jade"; jedi = "JediDatacron04"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron04 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 1398 ){ yoda = "Numi Sunrider"; jedi = "JediDatacron05"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron05 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 2983 ){ yoda = "Plo Kune"; jedi = "JediDatacron06"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron06 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 4378 ){ yoda = "Kyle Katran"; jedi = "JediDatacron07"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron07 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 2697 ){ yoda = "Kyp Duron"; jedi = "JediDatacron08"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron08 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 4167 ){ yoda = "Ganer Rhysode"; jedi = "JediDatacron09"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron09 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.X == 6642 ){ yoda = "Coran Horn"; jedi = "JediDatacron10"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is JediDatacron10 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }

						if ( jedi != "" )
						{
							Item cron = null;
							Type itemType = ScriptCompiler.FindTypeByName( jedi );

							if ( itemType != null )
							{
								cron = (Item)Activator.CreateInstance( itemType );
								m.AddToBackpack ( cron );
								m.SendMessage( "You have the holocron of " + yoda + "!" );
								m.FixedParticles( 0x376A, 9, 32, 5005, EffectLayer.Waist );
								m.PlaySound( 0x1F2 );
							}
						}
					}
				*/
				
				else if ( this.Name != "Magic Forge Trigger" ) // DEATH KNIGHT SHRINES
				{
					if ( m.Karma < 0 && m.Skills[SkillName.Chivalry].Base > 0 )
					{
						string skull = "";

						if ( this.Name == "Kath" && this.Name == keyword ){ skull = "DeathKnightSkull752"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull752 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Farian" && this.Name == keyword ){ skull = "DeathKnightSkull755"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull755 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Kargoth" && this.Name == keyword ){ skull = "DeathKnightSkull750"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull750 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Maeril" && this.Name == keyword ){ skull = "DeathKnightSkull754"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull754 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Myrhal" && this.Name == keyword ){ skull = "DeathKnightSkull753"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull753 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Monduiz" && this.Name == keyword ){ skull = "DeathKnightSkull751"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull751 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Androma" && this.Name == keyword ){ skull = "DeathKnightSkull756"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull756 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Thyrian" && this.Name == keyword ){ skull = "DeathKnightSkull759"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull759 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Rezinar" && this.Name == keyword ){ skull = "DeathKnightSkull758"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull758 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Minar" && this.Name == keyword ){ skull = "DeathKnightSkull760"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull760 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Luren" && this.Name == keyword ){ skull = "DeathKnightSkull762"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull762 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Urkar" && this.Name == keyword ){ skull = "DeathKnightSkull761"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull761 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Khayven" && this.Name == keyword ){ skull = "DeathKnightSkull763"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull763 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Oslan" && this.Name == keyword ){ skull = "DeathKnightSkull757"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is DeathKnightSkull757 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }

						if ( skull != "" )
						{
							Item head = null;
							Type itemType = ScriptCompiler.FindTypeByName( skull );

							if ( itemType != null )
							{
								head = (Item)Activator.CreateInstance( itemType );
								m.AddToBackpack ( head );
								m.SendMessage( "You have the skull of " + this.Name + "!" );
								Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
								Effects.PlaySound( m.Location, m.Map, 0x1ED );
							}
						}
					}

					if ( m.Karma < 0 && m.Skills[SkillName.EvalInt].Base > 0 && Server.Misc.GetPlayerInfo.isSyth(m,false) )
					{
						string syth = "";

						if ( this.Name == "Dzwol Hyal" && this.Name == keyword ){ syth = "SythDatacron01"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is SythDatacron01 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Zayin Kun" && this.Name == keyword ){ syth = "SythDatacron02"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is SythDatacron02 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Rhak Skuri" && this.Name == keyword ){ syth = "SythDatacron03"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is SythDatacron03 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Chwit Sutta" && this.Name == keyword ){ syth = "SythDatacron04"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is SythDatacron04 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Qyasik Tukata" && this.Name == keyword ){ syth = "SythDatacron05"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is SythDatacron05 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Sutta Wo" && this.Name == keyword ){ syth = "SythDatacron06"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is SythDatacron06 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Taral Wai" && this.Name == keyword ){ syth = "SythDatacron07"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is SythDatacron07 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Wai Kusk" && this.Name == keyword ){ syth = "SythDatacron08"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is SythDatacron08 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Derriphan Tyuk" && this.Name == keyword ){ syth = "SythDatacron09"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is SythDatacron09 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }
						else if ( this.Name == "Itsu Sutta" && this.Name == keyword ){ syth = "SythDatacron10"; ArrayList targets = new ArrayList(); foreach ( Item item in World.Items.Values ) { if ( item is SythDatacron10 ) { targets.Add( item ); } } for ( int i = 0; i < targets.Count; ++i ) { Item item = ( Item )targets[ i ]; item.Delete(); } }

						if ( syth != "" )
						{
							Item cron = null;
							Type itemType = ScriptCompiler.FindTypeByName( syth );

							if ( itemType != null )
							{
								cron = (Item)Activator.CreateInstance( itemType );
								m.AddToBackpack ( cron );
								m.SendMessage( "You have the mysticron of " + this.Name + "!" );
								Point3D sythL = new Point3D( m.X+1, m.Y+1, m.Z+5 );
								Effects.SendLocationParticles( EffectItem.Create( sythL, m.Map, EffectItem.DefaultDuration ), 0x3789, 10, 32, 5032 );
								Effects.PlaySound( m.Location, m.Map, 0x1F8 );
							}
						}
					}
				}
				else if ( m.Region.IsPartOf( "the Black Magic Guild" ) ) // DEATH KNIGHT DEMON FRIEND
				{
					if ( m.Skills[SkillName.Chivalry].Base >= 60 && m.Karma <= -5000 )
					{
						MorphingTime.ColorMyClothes( m, 0x497 );
						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
						Effects.PlaySound( m.Location, m.Map, 0x1ED );

						if ( m.Hue == 0x47E )
						{
							CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
							m.Hue = DB.CharHue;
							m.HairHue = DB.CharHairHue;
							m.FacialHairHue = DB.CharHairHue;
							m.SendMessage("Your body turns back to the colors of life, and your equipment is blackened.");
						}
						else
						{
							m.Hue = 0x47E;
							m.HairHue = 0x47E;
							m.FacialHairHue = 0x47E;
							m.SendMessage("Your body turned a deathly white, and your equipment is blackened.");
						}
					}
				}
				else if ( m.Map == Map.Malas && m.Region.IsPartOf( "the Serpent Sanctum" ) )
				{
					Item rock = m.Backpack.FindItemByType( typeof ( BlackrockSerpentBalance ) );
					if ( rock != null )
					{
						rock.Delete();

						m.AddToBackpack ( new SerpentCapturedBalance() );
						m.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
						m.PlaySound( 0x1F7 );

						LoggingFunctions.LogGenericQuest( m, "has acquired the power from the serpent of balance" );
						m.SendMessage( "You have acquired the power from the Serpent of Balance." );
					}
				}
				else if ( m.Map == Map.Malas && m.Region.IsPartOf( "the Ethereal Void" ) )
				{
					Item rock1 = m.Backpack.FindItemByType( typeof ( SerpentCapturedBalance ) );
					Item rock2 = m.Backpack.FindItemByType( typeof ( SerpentCapturedOrder ) );
					Item rock3 = m.Backpack.FindItemByType( typeof ( SerpentCapturedChaos ) );
					if ( rock1 != null && rock2 != null && rock3 != null )
					{
						Server.Items.QuestSouvenir.GiveReward( m, rock1.Name, rock1.Hue, Utility.RandomList( 0x1A7F, 0x1A80 ) );
						Server.Items.QuestSouvenir.GiveReward( m, rock2.Name, rock2.Hue, Utility.RandomList( 0x1A7F, 0x1A80 ) );
						Server.Items.QuestSouvenir.GiveReward( m, rock3.Name, rock3.Hue, Utility.RandomList( 0x1A7F, 0x1A80 ) );

						rock1.Delete();
						rock2.Delete();
						rock3.Delete();

						foreach ( Mobile who in this.GetMobilesInRange( 20 ) )
						{
							if ( who is EpicCharacter )
							{
								who.Say("You have maintained the balance between order and chaos.");
							}
						}

						if ( m != null )
						{
							if ( m is PlayerMobile )
							{
								Party p = Engines.PartySystem.Party.Get( m );
								if ( p != null )
								{
									foreach ( PartyMemberInfo pmi in p.Members )
									{
										if ( pmi.Mobile is PlayerMobile && pmi.Mobile.InRange(this.Location, 20) )
										{
											ManualOfItems book = new ManualOfItems();
												book.Hue = 0xB96;
												book.Name = "Tome of Magical Serpent Relics";
												book.m_Charges = 1;
												book.m_Skill_1 = 99;
												book.m_Skill_2 = 40;
												book.m_Skill_3 = 0;
												book.m_Skill_4 = 0;
												book.m_Skill_5 = 0;
												book.m_Value_1 = 10.0;
												book.m_Value_2 = 15.0;
												book.m_Value_3 = 0.0;
												book.m_Value_4 = 0.0;
												book.m_Value_5 = 0.0;
												book.m_Slayer_1 = 8;
												book.m_Slayer_2 = 15;
												book.m_Owner = pmi.Mobile;
												book.m_Extra = "of the Serpent";
												book.m_FromWho = "From the Great Earth Serpent";
												book.m_HowGiven = "Gifted to";
												book.m_Points = 300;
												book.m_Hue = 0xB96;
												pmi.Mobile.AddToBackpack( book );

											BankCheck check = new BankCheck(5000);
												check.Name = "Reward from the Great Earth Serpent";
												pmi.Mobile.AddToBackpack ( check );

											pmi.Mobile.SendMessage("Two items have appeared in your backpack!");
											pmi.Mobile.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
											pmi.Mobile.PlaySound( 0x1F7 );
										}
									}
								}
								else
								{
									ManualOfItems book = new ManualOfItems();
										book.Hue = 0xB96;
										book.Name = "Tome of Magical Serpent Relics";
										book.m_Charges = 1;
										book.m_Skill_1 = 99;
										book.m_Skill_2 = 40;
										book.m_Skill_3 = 0;
										book.m_Skill_4 = 0;
										book.m_Skill_5 = 0;
										book.m_Value_1 = 10.0;
										book.m_Value_2 = 15.0;
										book.m_Value_3 = 0.0;
										book.m_Value_4 = 0.0;
										book.m_Value_5 = 0.0;
										book.m_Slayer_1 = 8;
										book.m_Slayer_2 = 15;
										book.m_Owner = m;
										book.m_Extra = "of the Serpent";
										book.m_FromWho = "From the Great Earth Serpent";
										book.m_HowGiven = "Gifted to";
										book.m_Points = 300;
										book.m_Hue = 0xB96;
										m.AddToBackpack( book );

									BankCheck check = new BankCheck(10000);
										check.Name = "Reward from the Great Earth Serpent";
										m.AddToBackpack ( check );

									m.SendMessage("Two items have appeared in your backpack!");
									m.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
									m.PlaySound( 0x1F7 );
								}
							}
						}

						LoggingFunctions.LogGenericQuest( m, "has maintained the balance between order and chaos" );
					}
				}
				else if ( m.Region.IsPartOf( "Dungeon Rock" ) ) // EXODUS - FORGE OF VIRTUE
				{
					if ( m.Backpack.FindItemByType( typeof ( DarkCoreExodus ) ) != null )
					{
						Item core = m.Backpack.FindItemByType( typeof ( DarkCoreExodus ) );

						ArrayList targets = new ArrayList();
						foreach ( Item item in World.Items.Values )
						if ( item is DarkCoreExodus )
						{
							targets.Add( item );
						}
						for ( int i = 0; i < targets.Count; ++i )
						{
							Item item = ( Item )targets[ i ];
							item.Delete();
						}

						Point3D fire = new Point3D( 710, 2209, -17 );
						Effects.SendLocationEffect( fire, m.Map, 0x3709, 30, 10 );
						m.PlaySound( 0x208 );
						m.SendMessage( "You destroy the dark core of Exodus, unleashing the power." );

						for ( int x = -8; x <= 8; ++x )
						{
							for ( int y = -8; y <= 8; ++y )
							{
								double dist = Math.Sqrt(x*x+y*y);

								if ( dist <= 8 )
									new Server.Misc.SummonQuests.GoodiesTimer( m.Map, m.X + x, m.Y + y ).Start();
							}
						}

						Point3D blast = new Point3D( 711, 2210, -5 );

						Effects.PlaySound(blast, m.Map, 0x307);
						Effects.SendLocationEffect(blast, m.Map, 0x36B0, 9, 10, 0, 0);

						int Change = 0;

						foreach ( Item enchant in m.GetItemsInRange( 20 ) )
						{
							if ( enchant.X>=704 && enchant.Y>=2208 && enchant.X<=705 && enchant.Y<=2209 && Change == 0 )
							{
								int min = 50;
								int max = 200;

								int props = 5 + Utility.RandomMinMax( 0, 10 );

								if ( enchant is BaseWeapon )
								{
									Change++;
									BaseRunicTool.ApplyAttributesTo( (BaseWeapon)enchant, false, m.Luck, props, min, max );
								}
								else if ( enchant is BaseArmor )
								{
									Change++;
									BaseRunicTool.ApplyAttributesTo( (BaseArmor)enchant, false, m.Luck, props, min, max );
								}
								else if ( enchant is BaseJewel )
								{
									Change++;
									BaseRunicTool.ApplyAttributesTo( (BaseJewel)enchant, false, m.Luck, props, min, max );
								}
								else if ( enchant is BaseClothing )
								{
									Change++;
									BaseRunicTool.ApplyAttributesTo( (BaseClothing)enchant, false, m.Luck, props, min, max );
								}

								if ( Change == 1 )
								{
									enchant.Hue = 1072;

									Effects.SendLocationParticles( EffectItem.Create( enchant.Location, enchant.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
									Effects.PlaySound( enchant.Location, enchant.Map, 0x1ED );

									string checkItem = enchant.GetType().ToString();  
									int index = checkItem.LastIndexOf(".");
									string[] getItem = new string[] {checkItem.Substring(0, index), checkItem.Substring(index + 1)};

									Type itemType = ScriptCompiler.FindTypeByName( getItem[1] );

									if ( itemType != null )
									{
										Item newItem = (Item)Activator.CreateInstance( itemType );

										string nameItem = newItem.Name;

										if ( nameItem == null ){ nameItem = MorphingItem.AddSpacesToSentence( (newItem.GetType()).Name ); }

										enchant.Name = LootPackEntry.MagicItemAdj( "start", Server.Misc.GetPlayerInfo.OrientalPlay( m ), Server.Misc.GetPlayerInfo.EvilPlay( m ), enchant.ItemID ) + " " + enchant.Name;

										enchant.Name = nameItem + " of Exodus";

										newItem.Delete();
									}
								}
							}
						}



						Party p = Engines.PartySystem.Party.Get( m );
						if ( p != null )
						{
							foreach ( PartyMemberInfo pmi in p.Members )
							{
								if ( pmi.Mobile is PlayerMobile && pmi.Mobile.InRange(this.Location, 20) )
								{
									LoggingFunctions.LogGenericQuest( pmi.Mobile, "has destroyed the dark core of Exodus" );
									Titles.AwardFame( pmi.Mobile, 300, true );
									if ( ((PlayerMobile)(pmi.Mobile)).KarmaLocked == true ){ Titles.AwardKarma( pmi.Mobile, -300, true ); }
									else { Titles.AwardKarma( pmi.Mobile, 300, true ); }

									ManualOfItems book = new ManualOfItems();
										book.Hue = 0x835;
										book.Name = "Tome of Exodus Relics";
										book.m_Charges = 1;
										book.m_Skill_1 = 99;
										book.m_Skill_2 = 32;
										book.m_Skill_3 = 0;
										book.m_Skill_4 = 0;
										book.m_Skill_5 = 0;
										book.m_Value_1 = 15.0;
										book.m_Value_2 = 10.0;
										book.m_Value_3 = 0.0;
										book.m_Value_4 = 0.0;
										book.m_Value_5 = 0.0;
										book.m_Slayer_1 = 32;
										book.m_Slayer_2 = 13;
										book.m_Owner = pmi.Mobile;
										book.m_Extra = "of Exodus";
										book.m_FromWho = "From the Destruction of the Dark Core";
										book.m_HowGiven = "Acquired by";
										book.m_Points = 300;
										book.m_Hue = 0x835;
										pmi.Mobile.AddToBackpack( book );

									pmi.Mobile.SendMessage("An item has appeared in your backpack!");
								}
							}
						}
						else
						{
							LoggingFunctions.LogGenericQuest( m, "has destroyed the dark core of Exodus" );
							Titles.AwardFame( m, 300, true );
							if ( ((PlayerMobile)m).KarmaLocked == true ){ Titles.AwardKarma( m, -300, true ); }
							else { Titles.AwardKarma( m, 300, true ); }

							ManualOfItems book = new ManualOfItems();
								book.Hue = 0x835;
								book.Name = "Tome of Exodus Relics";
								book.m_Charges = 1;
								book.m_Skill_1 = 99;
								book.m_Skill_2 = 32;
								book.m_Skill_3 = 0;
								book.m_Skill_4 = 0;
								book.m_Skill_5 = 0;
								book.m_Value_1 = 15.0;
								book.m_Value_2 = 10.0;
								book.m_Value_3 = 0.0;
								book.m_Value_4 = 0.0;
								book.m_Value_5 = 0.0;
								book.m_Slayer_1 = 32;
								book.m_Slayer_2 = 13;
								book.m_Owner = m;
								book.m_Extra = "of Exodus";
								book.m_FromWho = "From the Destruction of the Dark Core";
								book.m_HowGiven = "Acquired by";
								book.m_Points = 300;
								book.m_Hue = 0x835;
								m.AddToBackpack( book );
								m.SendMessage("An item has appeared in your backpack!");
						}
					}
				}
				else if ( m.Region.IsPartOf( "the Lost Graveyard" ) ) // GEM OF IMMORTALITY
				{
					if ( m.Backpack.FindItemByType( typeof ( ShardOfFalsehood ) ) != null && m.Backpack.FindItemByType( typeof ( ShardOfCowardice ) ) != null && m.Backpack.FindItemByType( typeof ( ShardOfHatred ) ) != null )
					{
						Item shard1 = m.Backpack.FindItemByType( typeof ( ShardOfFalsehood ) );
						Item shard2 = m.Backpack.FindItemByType( typeof ( ShardOfCowardice ) );
						Item shard3 = m.Backpack.FindItemByType( typeof ( ShardOfHatred ) );

						m.AddToBackpack ( new GemImmortality() );
						m.SendMessage( "The shards magically form the Gem of Immortality." );
						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
						Effects.PlaySound( m.Location, m.Map, 0x653 );

						LoggingFunctions.LogGenericQuest( m, "has created the gem of immortality" );

						ArrayList targets = new ArrayList();
						foreach ( Item item in World.Items.Values )
						if ( item is ShardOfHatred || item is ShardOfFalsehood || item is ShardOfCowardice )
						{
							targets.Add( item );
						}
						for ( int i = 0; i < targets.Count; ++i )
						{
							Item item = ( Item )targets[ i ];
							item.Delete();
						}
					}
				}
				else if ( m.Region.IsPartOf( "the Tomb of Kas the Bloody Handed" ) ) // DEATH KNIGHT TOMB
				{
					ArrayList targets = new ArrayList();
					foreach ( Item item in World.Items.Values )
					{
						if ( item is SoulLantern )
						{
							SoulLantern lantern = (SoulLantern)item;
							if ( lantern.owner == m )
							{
								targets.Add( item );
							}
						}
						else if ( item is DeathKnightSpellbook )
						{
							DeathKnightSpellbook book = (DeathKnightSpellbook)item;
							if ( book.owner == m )
							{
								targets.Add( item );
							}
						}
					}
					for ( int i = 0; i < targets.Count; ++i )
					{
						Item item = ( Item )targets[ i ];
						item.Delete();
					}

					if ( m.Karma < 0 && m.Skills[SkillName.Chivalry].Base > 0 )
					{
						m.AddToBackpack ( new SoulLantern( m ) );
						DeathKnightSpellbook book = new DeathKnightSpellbook( (ulong)0, m );
						Server.Misc.BookProperties.GetBookProperties( book );
						m.AddToBackpack ( book );
						m.SendMessage( "Kas has granted you your wish." );

						LoggingFunctions.LogGenericQuest( m, "has become a death knight" );

						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
						Effects.PlaySound( m.Location, m.Map, 0x1ED );
					}
				}
				else if ( m.Region.IsPartOf( "the Tomb of Malak the Syth Lord" ) ) // SYTH TOMB
				{
					ArrayList targets = new ArrayList();
					foreach ( Item item in World.Items.Values )
					{
						if ( item is SythSpellbook )
						{
							SythSpellbook book = (SythSpellbook)item;
							if ( book.owner == m )
							{
								targets.Add( item );
							}
						}
					}
					for ( int i = 0; i < targets.Count; ++i )
					{
						Item item = ( Item )targets[ i ];
						item.Delete();
					}

					if ( m.Karma < 0 && m.Skills[SkillName.EvalInt].Base > 0 )
					{
						SythSpellbook book = new SythSpellbook( (ulong)0, m );
						m.AddToBackpack ( book );
						m.SendMessage( "You have obtained Malak's Datacron of Syth Knowledge." );

						LoggingFunctions.LogGenericQuest( m, "has obtained Malak's Datacron of Syth Knowledge" );

						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3789, 10, 32, 5032 );
						Effects.PlaySound( m.Location, m.Map, 0x1F8 );
					}
				}
				
				
				else if ( m.Region.IsPartOf( "the Tomb of Zoda the Jedi Master" ) ) // JEDI TOMB
				{
					ArrayList targets = new ArrayList();
					foreach ( Item item in World.Items.Values )
					{
						if ( item is JediSpellbook )
						{
							JediSpellbook book = (JediSpellbook)item;
							if ( book.owner == m )
							{
								targets.Add( item );
							}
						}
					}
					for ( int i = 0; i < targets.Count; ++i )
					{
						Item item = ( Item )targets[ i ];
						item.Delete();
					}

					if ( m.Karma >= 2500 && m.Skills[SkillName.EvalInt].Base >= 25 )
					{
						JediSpellbook book = new JediSpellbook( (ulong)0, m );
						m.AddToBackpack ( book );
						m.SendMessage( "You have obtained Zoda's Datacron of Jedi Wisdom." );

						LoggingFunctions.LogGenericQuest( m, "has obtained Zoda's Datacron of Jedi Wisdom" );

						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3789, 10, 32, 5032 );
						Effects.PlaySound( m.Location, m.Map, 0x1FF );
					}
				}
				
				
				else if ( m.Region.IsPartOf( "the Altar of Golden Rangers" ) || m.Region.IsPartOf( "the Ranger Outpost" ) ) // GOLDEN RANGER
				{
					if ( m.Skills[SkillName.Camping].Base >= 90 || m.Skills[SkillName.Tracking].Base >= 90 )
					{
						if ( m.Backpack.FindItemByType( typeof ( GoldenFeathers ) ) != null )
						{
							Item feath = m.Backpack.FindItemByType( typeof ( GoldenFeathers ) );

							int RidOf = 0;

							GoldenFeathers goldfeather = (GoldenFeathers)feath;
							if ( goldfeather.owner == m )
							{
								foreach ( Item enchant in m.GetItemsInRange( 20 ) )
								{
									if ( enchant.X>=5203 && enchant.Y>=1301 && enchant.X<=5205 && enchant.Y<=1305 )
									{
										if ( enchant is BaseWeapon )
										{
											BaseWeapon weapon = (BaseWeapon)enchant;
											if (	Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) || 
													Server.Misc.MaterialInfo.IsAnyKindOfClothItem( enchant ) || 
													Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( enchant ) )
											{
												MorphingItem.MorphMyItem( weapon, "Blessed by the Rangers", "Golden Ranger", "IGNORED", MorphingTemplates.TemplateRanger("weapons") );
												Effects.SendLocationParticles( EffectItem.Create( weapon.Location, weapon.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
												Effects.PlaySound( weapon.Location, weapon.Map, 0x1ED );
											}

											RidOf = 1;
										}
										else if ( enchant is BaseArmor )
										{
											BaseArmor armor = (BaseArmor)enchant;
											if (	Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) || 
													Server.Misc.MaterialInfo.IsAnyKindOfClothItem( enchant ) || 
													Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( enchant ) )
											{
												MorphingItem.MorphMyItem( armor, "IGNORED", "Golden Ranger", "IGNORED", MorphingTemplates.TemplateRanger("armors") );
												Effects.SendLocationParticles( EffectItem.Create( armor.Location, armor.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
												Effects.PlaySound( armor.Location, armor.Map, 0x1ED );
											}

											RidOf = 1;
										}
									}
								}

								if ( RidOf > 0 ){ feath.Delete(); }
							}
							else
							{
								m.SendMessage( "You need your own golden feathers to receive the ranger blessing." );
							}
						}
						else
						{
							m.SendMessage( "You need golden feathers to receive the ranger blessing." );
						}
					}
					else
					{
						m.SendMessage( "Only a master explorer or ranger can be blessed here." );
					}
				}
				else if ( m.Region.IsPartOf( "the Moon's Core" ) && m.X == 5689 && m.Y == 1912 ) // MOON CORE
				{
					if ( m.Skills[SkillName.Magery].Base >= 80 || m.Skills[SkillName.Necromancy].Base >= 80 )
					{
						if ( 	m.Backpack.FindItemByType( typeof ( StaffPartVenom ) ) != null && 
								m.Backpack.FindItemByType( typeof ( StaffPartCaddellite ) ) != null && 
								m.Backpack.FindItemByType( typeof ( StaffPartFire ) ) != null && 
								m.Backpack.FindItemByType( typeof ( StaffPartLight ) ) != null && 
								m.Backpack.FindItemByType( typeof ( StaffPartEnergy ) ) != null 
						)
						{
							Item piece1 = m.Backpack.FindItemByType( typeof ( StaffPartVenom ) );
							Item piece2 = m.Backpack.FindItemByType( typeof ( StaffPartCaddellite ) );
							Item piece3 = m.Backpack.FindItemByType( typeof ( StaffPartFire ) );
							Item piece4 = m.Backpack.FindItemByType( typeof ( StaffPartLight ) );
							Item piece5 = m.Backpack.FindItemByType( typeof ( StaffPartEnergy ) );

							piece1.Delete(); piece2.Delete(); piece3.Delete(); piece4.Delete(); piece5.Delete();

							LoggingFunctions.LogGenericQuest( m, "has assembled the staff of ultimate power" );

							int magic = 0;
								if ( m.Skills[SkillName.Necromancy].Base > m.Skills[SkillName.Magery].Base ){ magic = 1; }

							StaffFiveParts staff = new StaffFiveParts( m, magic ); 
							staff.MoveToWorld (new Point3D(5693, 1913, 2), Map.Trammel);
							Effects.SendLocationParticles( EffectItem.Create( staff.Location, staff.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
							Effects.PlaySound( staff.Location, staff.Map, 0x1ED );
						}
					}
				}
				else if ( m.Map == Map.TerMur ) // POISON FORGE
				{
					foreach ( Item enchant in m.GetItemsInRange( 20 ) )
					{
						if ( enchant.X>=1064 && enchant.Y>=1167 && enchant.X<=1067 && enchant.Y<=1169 )
						{
							if ( enchant is BaseWeapon )
							{
								BaseWeapon weapon = (BaseWeapon)enchant;
								if ( weapon.AosElementDamages.Poison < 100 && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) )
								{
									Effects.SendLocationParticles( EffectItem.Create( weapon.Location, weapon.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
									Effects.PlaySound( weapon.Location, weapon.Map, 0x1ED );
									weapon.AosElementDamages.Poison = 100;
									weapon.AosElementDamages.Physical = 0;
									weapon.AosElementDamages.Fire = 0;
									weapon.AosElementDamages.Cold = 0;
									weapon.AosElementDamages.Energy = 0;
									weapon.EngravedText = "Poison Forged";
									weapon.Identified = true;
									if (weapon.HitPoints > 0 ){ weapon.HitPoints = weapon.HitPoints - 1; }
									if (weapon.MaxHitPoints > 0 ){ weapon.MaxHitPoints = weapon.MaxHitPoints - 1; }
									if (weapon.HitPoints > weapon.MaxHitPoints ){ weapon.HitPoints = weapon.MaxHitPoints; }
									if (Utility.RandomMinMax( 1, 20 ) == 1 ){ weapon.WeaponAttributes.HitPoisonArea = Utility.RandomMinMax( 1, 100 ); }
									enchant.Hue = Utility.RandomList( 0x48F, 0x4F3, 0x4F4, 0x4F5, 0x4F6, 0x4F7, 0x4F8, 0x557, 0x558, 0x559, 0x55A, 0x55B, 0x55C, 0x7D1, 0x7D2, 0x7D3, 0x7D4, 0x7D5, 0x7D6 );
								}
							}
							else if ( enchant is BaseArmor )
							{
								BaseArmor armor = (BaseArmor)enchant;

								if ( armor.PoisonBonus < 5 && armor is BaseShield && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) )
								{
									Effects.SendLocationParticles( EffectItem.Create( armor.Location, armor.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
									Effects.PlaySound( armor.Location, armor.Map, 0x1ED );
									armor.PoisonBonus = Utility.RandomMinMax( 1, 5 );
									armor.PhysicalBonus = 0;
									armor.FireBonus = 0;
									armor.ColdBonus = 0;
									armor.EnergyBonus = 0;
									armor.Identified = true;
									if (armor.HitPoints > 0 ){ armor.HitPoints = armor.HitPoints - 1; }
									if (armor.MaxHitPoints > 0 ){ armor.MaxHitPoints = armor.MaxHitPoints - 1; }
									if (armor.HitPoints > armor.MaxHitPoints ){ armor.HitPoints = armor.MaxHitPoints; }
									enchant.Hue = Utility.RandomList( 0x48F, 0x4F3, 0x4F4, 0x4F5, 0x4F6, 0x4F7, 0x4F8, 0x557, 0x558, 0x559, 0x55A, 0x55B, 0x55C, 0x7D1, 0x7D2, 0x7D3, 0x7D4, 0x7D5, 0x7D6 );
								}
								else if ( armor.PoisonBonus < 10 && armor is BaseArmor && !(armor is BaseShield) && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) )
								{
									Effects.SendLocationParticles( EffectItem.Create( armor.Location, armor.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
									Effects.PlaySound( armor.Location, armor.Map, 0x1ED );
									armor.PoisonBonus = Utility.RandomMinMax( 5, 15 );
									armor.PhysicalBonus = 0;
									armor.FireBonus = 0;
									armor.ColdBonus = 0;
									armor.EnergyBonus = 0;
									armor.Identified = true;
									if (armor.MaxHitPoints > 0 ){ armor.MaxHitPoints = armor.MaxHitPoints - 1; }
									if (armor.HitPoints > 0 ){ armor.HitPoints = armor.HitPoints - 1; }
									if (armor.HitPoints > armor.MaxHitPoints ){ armor.HitPoints = armor.MaxHitPoints; }
									enchant.Hue = Utility.RandomList( 0x48F, 0x4F3, 0x4F4, 0x4F5, 0x4F6, 0x4F7, 0x4F8, 0x557, 0x558, 0x559, 0x55A, 0x55B, 0x55C, 0x7D1, 0x7D2, 0x7D3, 0x7D4, 0x7D5, 0x7D6 );
								}
							}
						}
					}
				}
				else if ( m.Map == Map.Felucca ) // COLD FORGE
				{
					foreach ( Item enchant in m.GetItemsInRange( 20 ) )
					{
						if ( enchant.X>=3979 && enchant.Y>=736 && enchant.X<=3982 && enchant.Y<=738 )
						{
							if ( enchant is BaseWeapon )
							{
								BaseWeapon weapon = (BaseWeapon)enchant;
								if ( weapon.AosElementDamages.Cold < 100 && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) )
								{
									Effects.SendLocationParticles( EffectItem.Create( weapon.Location, weapon.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
									Effects.PlaySound( weapon.Location, weapon.Map, 0x1ED );
									weapon.AosElementDamages.Poison = 0;
									weapon.AosElementDamages.Physical = 0;
									weapon.AosElementDamages.Fire = 0;
									weapon.AosElementDamages.Cold = 100;
									weapon.AosElementDamages.Energy = 0;
									weapon.EngravedText = "Frost Forged";
									weapon.Identified = true;
									if (weapon.HitPoints > 0 ){ weapon.HitPoints = weapon.HitPoints - 1; }
									if (weapon.MaxHitPoints > 0 ){ weapon.MaxHitPoints = weapon.MaxHitPoints - 1; }
									if (weapon.HitPoints > weapon.MaxHitPoints ){ weapon.HitPoints = weapon.MaxHitPoints; }
									if (Utility.RandomMinMax( 1, 20 ) == 1 ){ weapon.WeaponAttributes.HitColdArea = Utility.RandomMinMax( 1, 100 ); }
									enchant.Hue = Utility.RandomList( 0x47E, 0x480, 0x481, 0x482, 0x48D, 0x4F8, 0x4ED, 0x4EE, 0x4EF, 0x4F0, 0x4F1, 0x4F2, 0x551, 0x552, 0x553, 0x554, 0x555, 0x556 );
								}
							}
							else if ( enchant is BaseArmor )
							{
								BaseArmor armor = (BaseArmor)enchant;

								if ( armor.ColdBonus < 5 && armor is BaseShield && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) )
								{
									Effects.SendLocationParticles( EffectItem.Create( armor.Location, armor.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
									Effects.PlaySound( armor.Location, armor.Map, 0x1ED );
									armor.PoisonBonus = 0;
									armor.PhysicalBonus = 0;
									armor.FireBonus = 0;
									armor.ColdBonus = Utility.RandomMinMax( 1, 5 );
									armor.EnergyBonus = 0;
									armor.Identified = true;
									if (armor.HitPoints > 0 ){ armor.HitPoints = armor.HitPoints - 1; }
									if (armor.MaxHitPoints > 0 ){ armor.MaxHitPoints = armor.MaxHitPoints - 1; }
									if (armor.HitPoints > armor.MaxHitPoints ){ armor.HitPoints = armor.MaxHitPoints; }
									enchant.Hue = Utility.RandomList( 0x47E, 0x480, 0x481, 0x482, 0x48D, 0x4F8, 0x4ED, 0x4EE, 0x4EF, 0x4F0, 0x4F1, 0x4F2, 0x551, 0x552, 0x553, 0x554, 0x555, 0x556 );
								}
								else if ( armor.ColdBonus < 10 && armor is BaseArmor && !(armor is BaseShield) && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) )
								{
									Effects.SendLocationParticles( EffectItem.Create( armor.Location, armor.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
									Effects.PlaySound( armor.Location, armor.Map, 0x1ED );
									armor.PoisonBonus = 0;
									armor.PhysicalBonus = 0;
									armor.FireBonus = 0;
									armor.ColdBonus = Utility.RandomMinMax( 5, 15 );
									armor.EnergyBonus = 0;
									armor.Identified = true;
									if (armor.MaxHitPoints > 0 ){ armor.MaxHitPoints = armor.MaxHitPoints - 1; }
									if (armor.HitPoints > 0 ){ armor.HitPoints = armor.HitPoints - 1; }
									if (armor.HitPoints > armor.MaxHitPoints ){ armor.HitPoints = armor.MaxHitPoints; }
									enchant.Hue = Utility.RandomList( 0x47E, 0x480, 0x481, 0x482, 0x48D, 0x4F8, 0x4ED, 0x4EE, 0x4EF, 0x4F0, 0x4F1, 0x4F2, 0x551, 0x552, 0x553, 0x554, 0x555, 0x556 );
								}
							}
						}
					}
				}
				else if ( m.Map == Map.Trammel ) // ENERGY FORGE
				{
					foreach ( Item enchant in m.GetItemsInRange( 20 ) )
					{
						if ( enchant.X>=5331 && enchant.Y>=3203 && enchant.X<=5334 && enchant.Y<=3207 )
						{
							if ( enchant is BaseWeapon )
							{
								BaseWeapon weapon = (BaseWeapon)enchant;
								if ( weapon.AosElementDamages.Energy < 100 && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) )
								{
									Effects.SendLocationParticles( EffectItem.Create( weapon.Location, weapon.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
									Effects.PlaySound( weapon.Location, weapon.Map, 0x1ED );
									weapon.AosElementDamages.Poison = 0;
									weapon.AosElementDamages.Physical = 0;
									weapon.AosElementDamages.Fire = 0;
									weapon.AosElementDamages.Cold = 0;
									weapon.AosElementDamages.Energy = 100;
									weapon.EngravedText = "Energy Forged";
									weapon.Identified = true;
									if (weapon.HitPoints > 0 ){ weapon.HitPoints = weapon.HitPoints - 1; }
									if (weapon.MaxHitPoints > 0 ){ weapon.MaxHitPoints = weapon.MaxHitPoints - 1; }
									if (weapon.HitPoints > weapon.MaxHitPoints ){ weapon.HitPoints = weapon.MaxHitPoints; }
									if (Utility.RandomMinMax( 1, 20 ) == 1 ){ weapon.WeaponAttributes.HitEnergyArea = Utility.RandomMinMax( 1, 100 ); }
									enchant.Hue = Utility.RandomList( 0x490, 0x4F9, 0x4FA, 0x4FB, 0x4FC, 0x4FD, 0x4FE, 0x55D, 0x55E, 0x55F, 0x560, 0x561, 0x562 );
								}
							}
							else if ( enchant is BaseArmor )
							{
								BaseArmor armor = (BaseArmor)enchant;

								if ( armor.EnergyBonus < 5 && armor is BaseShield && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) )
								{
									Effects.SendLocationParticles( EffectItem.Create( armor.Location, armor.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
									Effects.PlaySound( armor.Location, armor.Map, 0x1ED );
									armor.PoisonBonus = 0;
									armor.PhysicalBonus = 0;
									armor.FireBonus = 0;
									armor.ColdBonus = 0;
									armor.EnergyBonus = Utility.RandomMinMax( 1, 5 );
									armor.Identified = true;
									if (armor.HitPoints > 0 ){ armor.HitPoints = armor.HitPoints - 1; }
									if (armor.MaxHitPoints > 0 ){ armor.MaxHitPoints = armor.MaxHitPoints - 1; }
									if (armor.HitPoints > armor.MaxHitPoints ){ armor.HitPoints = armor.MaxHitPoints; }
									enchant.Hue = Utility.RandomList( 0x490, 0x4F9, 0x4FA, 0x4FB, 0x4FC, 0x4FD, 0x4FE, 0x55D, 0x55E, 0x55F, 0x560, 0x561, 0x562 );
								}
								else if ( armor.EnergyBonus < 10 && armor is BaseArmor && !(armor is BaseShield) && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) )
								{
									Effects.SendLocationParticles( EffectItem.Create( armor.Location, armor.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
									Effects.PlaySound( armor.Location, armor.Map, 0x1ED );
									armor.PoisonBonus = 0;
									armor.PhysicalBonus = 0;
									armor.FireBonus = 0;
									armor.ColdBonus = 0;
									armor.EnergyBonus = Utility.RandomMinMax( 5, 15 );
									armor.Identified = true;
									if (armor.MaxHitPoints > 0 ){ armor.MaxHitPoints = armor.MaxHitPoints - 1; }
									if (armor.HitPoints > 0 ){ armor.HitPoints = armor.HitPoints - 1; }
									if (armor.HitPoints > armor.MaxHitPoints ){ armor.HitPoints = armor.MaxHitPoints; }
									enchant.Hue = Utility.RandomList( 0x490, 0x4F9, 0x4FA, 0x4FB, 0x4FC, 0x4FD, 0x4FE, 0x55D, 0x55E, 0x55F, 0x560, 0x561, 0x562 );
								}
							}
						}
					}
				}
				else if ( m.Map == Map.Malas ) // FIRE FORGE
				{
					foreach ( Item enchant in m.GetItemsInRange( 20 ) )
					{
						if ( enchant.X>=798 && enchant.Y>=1125 && enchant.X<=801 && enchant.Y<=1126 )
						{
							if ( enchant is BaseWeapon )
							{
								BaseWeapon weapon = (BaseWeapon)enchant;
								if ( weapon.AosElementDamages.Fire < 100 && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) )
								{
									Effects.SendLocationParticles( EffectItem.Create( weapon.Location, weapon.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
									Effects.PlaySound( weapon.Location, weapon.Map, 0x1ED );
									weapon.AosElementDamages.Poison = 0;
									weapon.AosElementDamages.Physical = 0;
									weapon.AosElementDamages.Fire = 100;
									weapon.AosElementDamages.Cold = 0;
									weapon.AosElementDamages.Energy = 0;
									weapon.EngravedText = "Fire Forged";
									weapon.Identified = true;
									if (weapon.HitPoints > 0 ){ weapon.HitPoints = weapon.HitPoints - 1; }
									if (weapon.MaxHitPoints > 0 ){ weapon.MaxHitPoints = weapon.MaxHitPoints - 1; }
									if (weapon.HitPoints > weapon.MaxHitPoints ){ weapon.HitPoints = weapon.MaxHitPoints; }
									if (Utility.RandomMinMax( 1, 20 ) == 1 ){ weapon.WeaponAttributes.HitFireArea = Utility.RandomMinMax( 1, 100 ); }
									enchant.Hue = Utility.RandomList( 0x489, 0x4E7, 0x4E8, 0x4E9, 0x4EA, 0x4EB, 0x4EC, 0x54B, 0x54C, 0x54D, 0x54E, 0x54F, 0x550 );
								}
							}
							else if ( enchant is BaseArmor )
							{
								BaseArmor armor = (BaseArmor)enchant;

								if ( armor.FireBonus < 5 && armor is BaseShield && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) )
								{
									Effects.SendLocationParticles( EffectItem.Create( armor.Location, armor.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
									Effects.PlaySound( armor.Location, armor.Map, 0x1ED );
									armor.PoisonBonus = 0;
									armor.PhysicalBonus = 0;
									armor.FireBonus = Utility.RandomMinMax( 1, 5 );
									armor.ColdBonus = 0;
									armor.EnergyBonus = 0;
									armor.Identified = true;
									if (armor.HitPoints > 0 ){ armor.HitPoints = armor.HitPoints - 1; }
									if (armor.MaxHitPoints > 0 ){ armor.MaxHitPoints = armor.MaxHitPoints - 1; }
									if (armor.HitPoints > armor.MaxHitPoints ){ armor.HitPoints = armor.MaxHitPoints; }
									enchant.Hue = Utility.RandomList( 0x489, 0x4E7, 0x4E8, 0x4E9, 0x4EA, 0x4EB, 0x4EC, 0x54B, 0x54C, 0x54D, 0x54E, 0x54F, 0x550 );
								}
								else if ( armor.FireBonus < 10 && armor is BaseArmor && !(armor is BaseShield) && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( enchant ) )
								{
									Effects.SendLocationParticles( EffectItem.Create( armor.Location, armor.Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5008 );
									Effects.PlaySound( armor.Location, armor.Map, 0x1ED );
									armor.PoisonBonus = 0;
									armor.PhysicalBonus = 0;
									armor.FireBonus = Utility.RandomMinMax( 5, 15 );
									armor.ColdBonus = 0;
									armor.EnergyBonus = 0;
									armor.Identified = true;
									if (armor.MaxHitPoints > 0 ){ armor.MaxHitPoints = armor.MaxHitPoints - 1; }
									if (armor.HitPoints > 0 ){ armor.HitPoints = armor.HitPoints - 1; }
									if (armor.HitPoints > armor.MaxHitPoints ){ armor.HitPoints = armor.MaxHitPoints; }
									enchant.Hue = Utility.RandomList( 0x489, 0x4E7, 0x4E8, 0x4E9, 0x4EA, 0x4EB, 0x4EC, 0x54B, 0x54C, 0x54D, 0x54E, 0x54F, 0x550 );
								}
							}
						}
					}
				}
			}
		}

		[Constructable]
		public MagicForges() : base( 0x1BC3 )
		{
			Name = "Magic Forge Trigger";
			Visible = false;
		}

		public MagicForges( Serial serial ) : base( serial )
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