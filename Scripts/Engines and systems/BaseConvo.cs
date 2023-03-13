using System;
using System.Collections; 
using System.Collections.Generic;
using System.IO;
using Server;
using Server.Gumps;
using Server.Items;
using System.Text;
using System.Text.RegularExpressions;

namespace Server.Mobiles
{
	public interface IStringList
	{
		string GetString( BaseConvo npc, Mobile pc );
	}

	public class AttitudeList : IStringList
	{
		private Attitude[] m_Atts;
		private IStringList[] m_Strings;

		public AttitudeList( FileStrBuff file )
		{
			string tok;
			ArrayList att = new ArrayList( 5 );
			while ( !file.Eof() && (tok=file.GetNextToken()) != "{" )
			{
				try
				{
					att.Add( Enum.Parse( typeof( Attitude ), tok, true ) );
				}
				catch
				{
				}
			}

			if ( file.Eof() )
				return;

			file.Seek( -1 );
			m_Strings = KeywordCollection.MakeList( file );
			if ( att.Count > 0 )
				m_Atts = (Attitude[])att.ToArray( typeof( Attitude ) );
			else
				m_Strings = null;
		}

		public string GetString( BaseConvo npc, Mobile pc )
		{
			Attitude test = npc.Attitude;
			while ( true )
			{
				for(int i=0;i<m_Atts.Length;i++)
				{
					if ( m_Atts[i] == test )
					{
						string str = null;
						for(int s=0;s<m_Strings.Length && str == null;s++)
							str = m_Strings[s].GetString( npc, pc );
						return str;
					}
				}

				if ( test < Attitude.Neutral )
					test = (Attitude)( ((int)test) + 1 );
				else if ( test > Attitude.Neutral )
					test = (Attitude)( ((int)test) - 1 );
				else
					break;
			}
			return null;
		}
	}

	public class NotorietyList : IStringList
	{
		private IStringList[] m_Strings;
		private NotoVal[] m_Notos;

		public NotorietyList( FileStrBuff file )
		{
			string tok;
			ArrayList noto = new ArrayList( 5 );
			while ( !file.Eof() && (tok=file.GetNextToken()) != "{" )
			{
				try
				{
					noto.Add( Enum.Parse( typeof( NotoVal ), tok, true ) );
				}
				catch
				{
				}
			}

			if ( file.Eof() )
				return;

			file.Seek( -1 );
			m_Strings = KeywordCollection.MakeList( file );
			if ( noto.Count > 0 )
				m_Notos = (NotoVal[])noto.ToArray( typeof( NotoVal ) );
			else
				m_Strings = null;
		}

		private NotoVal GetNotoValFor( Mobile pc )
		{
			int val = (int)((pc.Karma + 128.0)/52.0);
			if ( val <= 0 )
				return NotoVal.Infamous;
			else if ( val >= 4 )
				return NotoVal.Famous;
			else
				return (NotoVal)val;
		}

		public string GetString( BaseConvo npc, Mobile pc )
		{
			NotoVal noto = GetNotoValFor( pc );
			for(int i=0;i<m_Notos.Length;i++)
			{
				if ( m_Notos[i] == noto || m_Notos[i] == noto+1 || m_Notos[i] == noto-1 )
				{
					string str = null;
					for(int s=0;s<m_Strings.Length && str == null;s++)
						str = m_Strings[s].GetString( npc, pc );
					return str;
				}
			}
			return null;
		}
	}

	public class PhraseList : IStringList
	{
		private ArrayList m_Strings;
		public PhraseList()
		{
			m_Strings = new ArrayList();
		}

		public string GetString( BaseConvo npc, Mobile pc )
		{
			if ( m_Strings != null && m_Strings.Count > 0  )
			{
				object obj = m_Strings[Utility.Random( m_Strings.Count )];
				if ( obj is string )
					return (string)obj;
				else if ( obj is string[] )
					return ((string[])obj)[pc.Female ? 1 : 0];
				else
					return null;
			}
			else
			{
				return null;
			}
		}

		private object Parse( string str )
		{
			StringBuilder male = new StringBuilder( str.Length );
			StringBuilder female = null;
			const int BOTH = 0, MALEONLY = 1, FEMALEONLY = 2;
			int gender = 0; // 0=both, 1=male,2=female

			for(int i=0;i<str.Length;i++)
			{
				char p = str[i];
				switch ( p )
				{
					case '$': // $milord/milady$
						if ( gender == FEMALEONLY )
						{
							gender = BOTH;
						}
						else
						{
							gender = MALEONLY;
							if ( female == null )
								female = new StringBuilder( male.ToString() );
						}
						break;

					case '/':// $milord/milady$
						if ( gender == MALEONLY )
						{
							gender = FEMALEONLY;
						}
						else
						{
							male.Append( p );
							if ( female != null )
								female.Append( p );
						}
						break;

					case '_': // _Name_, _MyName_, _Town_, _Job_
						i++;
						if ( i >= str.Length )
							break;
						p = str[i];

						switch ( p )
						{
							case 'N':
							case 'n':
								male.Append( @"{0}" );
								if ( female != null )
									female.Append( @"{0}" );
								break;
							case 'M':
							case 'm':
								male.Append( @"{1}" );
								if ( female != null )
									female.Append( @"{1}" );
								break;
							case 'J':
							case 'j':
								male.Append( @"{2}" );
								if ( female != null )
									female.Append( @"{2}" );
								break;
							case 'T':
							case 't':
								male.Append( @"{3}" );
								if ( female != null )
									female.Append( @"{3}" );
								break;
						}

						while ( i < str.Length && p != '_' )
						{
							i++;
							p = str[i];
						}
						break;

					case '[': // [Attack] [Leave] etc
						while ( i < str.Length && p != ']' )
						{
							i++;
							p = str[i];
						}
						break;

					case '%':// %0
						i++; 
						
						male.Append( @"{4}" );
						if ( female != null )
							female.Append( @"{4}" );
						break;

					default:
						if ( gender != FEMALEONLY )
							male.Append( p );
						if ( female != null && gender != MALEONLY )
							female.Append( p );
						break;
				}
			}

			if ( female != null )
				return new string[]{ male.ToString(), female.ToString() };
			else
				return male.ToString();
		}

		public void Add( string str )
		{
			m_Strings.Add( Parse( str ) );
		}
	}

	public class Keyword
	{
		private Regex m_Keywords;
		public readonly IStringList[] Lists;

		public Keyword( string keyexp, IStringList[] lists )
		{
			Lists = lists;
			m_Keywords = new Regex( keyexp, RegexOptions.Compiled|RegexOptions.Singleline|RegexOptions.IgnoreCase );
		}

		public bool Match( string said )
		{
			return m_Keywords != null && m_Keywords.IsMatch( said );
		}
	}

	public class KeywordCollection
	{
		private ArrayList m_Keys;

		public KeywordCollection( FileStrBuff file )
		{
			m_Keys = new ArrayList();

			file.SkipUntil( '{' );
			file.NextChar();

			int brace = 1;
			while ( !file.Eof() && brace > 0 )
			{
				string tok = file.GetNextToken();
				switch ( tok[0] )
				{
					case '#':
					{
						if ( tok.ToLower() != "#key" )
						{
							//Console.WriteLine( "KeywordCollection : Unknown command '{0}'", tok );
							continue;
						}

						StringBuilder keyExp = new StringBuilder();
						int count = 0;
						while ( !file.Eof() && (tok=file.GetNextToken()) != "{" )
						{
							if ( tok[0] == '@' )
							{
								if ( tok == "@InternalGreeting" )
								{
									if ( count > 0 )
										keyExp.Append( "|" );
									count+=7;
									keyExp.Append( "(*hi*)|(*hello*)|(*hail*)|(*greeting*)|(*how*((*you*)|(*thou*)))|(*good*see*thee*)" );
								}
					
								continue;
							}
							else
							{
								count++;
								if ( count > 1 )
									keyExp.Append( "|" );
								keyExp.AppendFormat( "({0})", tok );
							}
						}

						if ( !file.Eof() )
						{
							file.Seek( -1 ); // leave the { in the input

							IStringList[] lists = MakeList( file );
							//Console.WriteLine( "KC '{0}' loaded {1} sub-lists...", exp, lists.Length );
							if ( lists != null && keyExp.Length > 0 )
							{
								keyExp.Replace( "*", ".*" );
								string exp = keyExp.ToString();
								if ( exp != null && exp.Length > 0 )
									m_Keys.Add( new Keyword( exp, lists ) );
							}
						}
						break;
					}
					case '{':
						brace++;
						break;
					case '}':
						brace--;
						break;
					default:
						//Console.WriteLine( "KeywordCollection : Unknown token '{0}'", tok );
						break;
				}
			}
		}

		public string GetString( BaseConvo npc, Mobile pc, string said )
		{
			for(int i=0;i<m_Keys.Count;i++)
			{
				Keyword k = (Keyword)m_Keys[i];
				if ( k.Match( said ) )
				{
					string str = null;
					for(int s=0;s<k.Lists.Length && str == null;s++)
						str = k.Lists[s].GetString( npc, pc );
					return str;
				}
			}

			return null;
		}

		public static IStringList[] MakeList( FileStrBuff file )
		{
			file.SkipUntil( '{' );
			file.NextChar();
			int brace = 1;

			ArrayList list = new ArrayList();
			while ( !file.Eof() && brace > 0 )
			{
				string tok = file.GetNextToken();
				switch ( tok[0] )
				{
					case '{':
						brace++;
						break;
					case '}':
						brace--;
						break;
					case '#':
					{
						string lwr = tok.ToLower();
						if ( lwr == "#attitude" )
							list.Add( new AttitudeList( file ) );
						else if ( lwr == "#notoriety" )
							list.Add( new NotorietyList( file ) );
						//else
						//	Console.WriteLine( "MakeList : Unknown token '{0}'", lwr );
						break;
					}
					default: 
					{
						PhraseList pl = new PhraseList();
						pl.Add( tok );
						//if ( brace <= 0 )
						//	Console.WriteLine( "MakeList : Warning, no opening brace for PhraseList." );
						while ( !file.Eof() && brace > 0 )
						{
							tok = file.GetNextToken();
							if ( tok == "{" )
								brace++;
							else if ( tok == "}" )
								brace--;
							else
								pl.Add( tok );
						}
						list.Add( pl );
						break;
					}
				}
			}//while

			if ( list.Count > 0 )
				return (IStringList[])list.ToArray( typeof( IStringList ) );
			else
				return null;
		}
	}

	public class Fragment
	{
		private KeywordCollection[] m_Collections;

		public Fragment( FileStrBuff file )
		{
			m_Collections = new KeywordCollection[3];
			while ( !file.Eof() )
			{
				string tok = file.GetNextToken().ToLower();
				if ( tok == null || tok.Length <= 0 )
					continue;

				if ( tok == "#sophistication" )
				{
					Sophistication s;
					string level = file.GetNextToken();
					try
					{
						s = (Sophistication)Enum.Parse( typeof(Sophistication), level, true );
					}
					catch
					{
						//Console.WriteLine( "Fragment : Error, invalid Sophistication {0}", level );
						continue;
					}

					m_Collections[(int)s] = new KeywordCollection( file );
				}
				else if ( tok == "#fragment" )
				{
					while ( !file.Eof() )
					{
						if ( file.GetNextToken() == "{" )
							break;
					}
				}
				else if ( tok != "{" && tok != "}" )
				{
					//Console.WriteLine( "Fragment : Unknown token '{0}'", tok );
				}
			}
		}

		public string GetString( BaseConvo npc, Mobile pc, string said )
		{
			int soph = (int)npc.Sophistication;
			string str = null;

			str = m_Collections[soph].GetString( npc, pc, said );
			if ( str == null )
			{
				if ( soph == (int)Sophistication.High )
					soph --;
				else if ( soph == (int)Sophistication.Low )
					soph++;
				else
					return null;
				str = m_Collections[soph].GetString( npc, pc, said );
			}
			return str;
		}
	}

	public class FileStrBuff
	{
		private string m_Data;
		private int m_Pos;

		public FileStrBuff( string fileName )
		{
			m_Pos = 0;
			using ( StreamReader reader = new StreamReader( fileName ) )
			{
				m_Data = reader.ReadToEnd();
			}
		}

		public int SkipUntil( char stop )
		{
			int start = m_Pos;
			while ( m_Pos < m_Data.Length && m_Data[m_Pos] != stop )
				m_Pos++;
			return m_Pos-start;
		}

		private static bool IsSkipChar( char c )
		{
			// include commas (used to seperate lists) as "whipespace"
			return c == ',' || c == '\t' || c == ' ' || c == '\n' || c == '\r'; //Char.IsWhiteSpace( c );
		}

		public char Peek()
		{
			if ( !Eof() )
				return m_Data[m_Pos];
			else 
				return '\x0';
		}

		public void NextChar()
		{
			m_Pos ++;
		}

		public void Seek( int amount )
		{
			m_Pos += amount;
		}

		public bool Eof()
		{
			return m_Pos >= m_Data.Length;
		}

		public string GetNextToken()
		{
			SkipWhitespace();
			if ( m_Pos >= m_Data.Length )
				return String.Empty;
			StringBuilder token = new StringBuilder();
			if ( m_Data[m_Pos] == '\"' )
			{
				m_Pos++; // skip the opening "
				while ( m_Pos < m_Data.Length && m_Data[m_Pos] != '\"' )
				{
					if ( m_Data[m_Pos] != '\n' )
						token.Append( m_Data[m_Pos] );
					m_Pos++;
				}
				m_Pos++; // skip the closing "
			}
			else
			{
				bool firstChar = true;
				while ( m_Pos < m_Data.Length && !IsSkipChar( m_Data[m_Pos] ) )
				{
					if ( m_Data[m_Pos] == '{' || m_Data[m_Pos] == '}' )
					{
						if ( firstChar )
						{
							token.Append( m_Data[m_Pos] );
							m_Pos++;
						}
						break;
					}
					else
					{
						firstChar = false;
						token.Append( m_Data[m_Pos] );
						m_Pos++;
					}
				}
			}
			return token.ToString();
		}

		private void SkipWhitespace()
		{
			if ( m_Pos >= m_Data.Length )
				return;

			bool newLine = m_Pos == 0 || m_Data[m_Pos] == '\n' || m_Data[m_Pos-1] == '\n';

			while ( m_Pos < m_Data.Length )
			{			
				if ( IsSkipChar( m_Data[m_Pos] ) )
				{
					if ( m_Data[m_Pos] == '\n' )
						newLine = true;
				}
				else if ( newLine && m_Pos+1 < m_Data.Length && m_Data[m_Pos] == '/' && m_Data[m_Pos+1] == '/' )
				{
					// its a comment, skip the whole line
					m_Pos+=2;
					while ( m_Pos < m_Data.Length && m_Data[m_Pos] != '\n' )
						m_Pos++;
					newLine = true;
				}
				else if ( m_Pos+1 < m_Data.Length && m_Data[m_Pos] == '/' && m_Data[m_Pos+1] == '*' )
				{
					// its a block comment, skip until its closed
					m_Pos+=2; // skip opener
					while ( m_Pos+1 < m_Data.Length && !( m_Data[m_Pos] == '*' && m_Data[m_Pos+1] == '/' ) )
						m_Pos++;
				}
				else
				{
					// found a non-whitespace, non comment. stop.
					break;
				}

				m_Pos++;
			}
		}
	}

	public class BaseConvo : BaseCreature
	{
		private const int EndFragment = 3;
		private const int GreetingFragment = 2;
		private const int BritanniaFragment = 1;
		private const int DefaultFragment = 0;

		private static Hashtable m_Frg = new Hashtable();

		private static Fragment LoadFrg( string name )
		{
			name = Path.Combine( Path.Combine( Core.BaseDirectory, "Data/convo" ), name );
			if ( !File.Exists( name ) )
				return null;
			
			//Console.WriteLine( "Loading convo fragment: {0}", name );
			return new Fragment( new FileStrBuff( name ) );
		}

		public static void Configure() 
		{
			Console.Write( "Loading convo fragments... " );

			m_Frg[DefaultFragment] = LoadFrg( "bdefault.frg" );
			m_Frg[BritanniaFragment] = LoadFrg( "britanni.frg" );
			m_Frg[GreetingFragment] = LoadFrg( "greetings.frg" );
			m_Frg[EndFragment] = LoadFrg( "convbye.frg" );

			for(int i=((int)RegionFragment._Offset)+1;i<(int)RegionFragment._End;i++)
				m_Frg[i] = LoadFrg( String.Format( "{0}.frg", ((RegionFragment)i).ToString() ) );

			for(int i=((int)JobFragment._Offset)+1;i<(int)JobFragment._End;i++)
				m_Frg[i] = LoadFrg( String.Format( "{0}.frg", ((JobFragment)i).ToString() ) );

			Console.WriteLine( "Done." );
		}

		public static bool Enabled { get { return true; } }

		private Sophistication m_Soph;
		private Attitude m_Mood;
		private JobFragment m_Job;
		private ArrayList m_ConvoList;

		[CommandProperty( AccessLevel.GameMaster )]
		public Sophistication Sophistication
		{
			get { return m_Soph; }
			set { m_Soph = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Attitude Attitude
		{
			get { return m_Mood; }
			set { m_Mood = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public JobFragment Job
		{
			get { return m_Job; }
			set { m_Job = value; }
		}

		private class ConvoTimer : Timer
		{
			private Mobile m_Mob;
			private BaseConvo m_Owner;
			public Mobile Mobile { get{ return m_Mob; } }

			public ConvoTimer( BaseConvo owner, Mobile m ) : base( TimeSpan.FromSeconds( 30.0 ) )
			{
				m_Owner = owner;
				m_Mob = m;
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if ( m_Owner.m_ConvoList != null )
					m_Owner.m_ConvoList.Remove( this );
				if ( m_Owner.FocusMob == m_Mob )
					m_Owner.FocusMob = null;
			}

			public void Refresh()
			{
				Stop();
				Start();
			}
		}

		private ConvoTimer StartConvo( Mobile m )
		{
			/* --from convinit.frg, seems unneeded w/proper use of greetings.frg
			Fragment f = (Fragment)m_Frg[InitFragment];
			if ( f != null )
			{
				string str = f.GetString( this, pc, "@InternalConvinit" );
				if ( str != null )
					Say( String.Format( str, m.Name, this.Name, "", "", "" ) );
			}
			*/

			if ( m_ConvoList == null )
				m_ConvoList = new ArrayList( 1 );
			ConvoTimer ct = new ConvoTimer( this, m );
			m_ConvoList.Add( ct );
			ct.Start();

			return ct;
		}

		private ConvoTimer GetConvo( Mobile m )
		{
			if ( m_ConvoList != null )
			{
				for (int i=0;i<m_ConvoList.Count;i++)
				{
					ConvoTimer ct = (ConvoTimer)m_ConvoList[i];
					if ( ct.Mobile == m )
						return ct;
				}
			}
			return null;
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete ();

			if ( m_ConvoList != null )
			{
				for(int i=0;i<m_ConvoList.Count;i++)
					((Timer)m_ConvoList[i]).Stop();
			}
		}

		public BaseConvo(AIType ai,	FightMode mode, int iRangePerception, int iRangeFight, double dActiveSpeed, double dPassiveSpeed) 
			: base(ai,mode,iRangePerception,iRangeFight,dActiveSpeed,dPassiveSpeed)
		{
			m_Job = JobFragment.None;
			if ( this.AlwaysMurderer )
				m_Mood = (Attitude)Utility.Random( 3 ); // make them never good tempered
			else
				m_Mood = (Attitude)Utility.Random( 5 );
			m_Soph = (Sophistication)Utility.Random( 3 );
		}

		public BaseConvo( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize (reader);

			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_Job = (JobFragment)reader.ReadShort();
					m_Mood = (Attitude)reader.ReadByte();
					m_Soph = (Sophistication)reader.ReadByte();
					break;
				}
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize (writer);

			writer.Write( (int)0 ); // version
			writer.Write( (short)m_Job );
			writer.Write( (byte)m_Mood );
			writer.Write( (byte)m_Soph );
		}

		protected virtual void GetConvoFragments( ArrayList list )
		{
			if ( this.Region is Regions.GuardedRegion )
			{
				Regions.GuardedRegion gr = (Regions.GuardedRegion)this.Region;
				//if ( gr.Fragment != RegionFragment.Wilderness )
				//	list.Add( (int)gr.Fragment );
			}
			list.Add( BritanniaFragment );
		}

		public override bool HandlesOnSpeech(Mobile from)
		{
			return base.HandlesOnSpeech(from) || ( Enabled && from != null && from.Player && from.Alive && (int)GetDistanceToSqrt( from ) < 4 && from != ControlMaster );
		}

		public virtual bool OnConvoStart( Mobile m )
		{
			return true;
		}

		private static ArrayList m_List = new ArrayList( 10 );
		public override void OnSpeech( SpeechEventArgs e )
		{
			Mobile pc = e.Mobile;
			if ( base.HandlesOnSpeech( pc ) )
				base.OnSpeech( e );
			
				string said = e.Speech;
				string str = null;
			
			if ( Enabled && !e.Handled && !e.Blocked && pc != null && pc.Player && pc.Alive && (int)GetDistanceToSqrt( pc ) < 4 && InLOS( pc ) && pc != ControlMaster )
			{
				if ( this is GauntletMaster && Insensitive.Contains(said, "scores"))
				{
					e.Mobile.CloseGump(typeof(MBRatingGump));
					e.Mobile.SendGump(new MBRatingGump(GauntletMaster.MBRating, (MonstersBashType)0));				
				}
				if ( this is GauntletMaster && Insensitive.Contains(said, "reset"))
				{
					if (pc is PlayerMobile)
					{
						PlayerMobile pm = (PlayerMobile)pc;
						pm.LastGauntletLevel = 1;
					}			
				}
				if ( this is BaseVendor && (Insensitive.Contains(said, "total") || Insensitive.Contains(said, "credits")))
				{
					if (pc is PlayerMobile && (this is Blacksmith || this is BlacksmithGuildmaster))
					{
						PlayerMobile pm = (PlayerMobile)pc;
						this.Say("You have a total of " + pm.BlacksmithBOD + " credits with the Guild.");
					}
					if (pc is PlayerMobile && (this is Tailor || this is TailorGuildmaster))
					{
						PlayerMobile pm = (PlayerMobile)pc;
						this.Say("You have a total of " + pm.TailorBOD + " credits with the Guild.");
					}			
				}
				else if ( this is BaseVendor && (this is Blacksmith || this is BlacksmithGuildmaster || this is Tailor || this is TailorGuildmaster) && (Insensitive.Contains(said, "claim") || Insensitive.Contains(said, "cash in")|| Insensitive.Contains(said, "redeem")))
				{
					String number = Regex.Match(e.Speech, @"\d+").Value;

					int amount = 0;
					if (number != null)
						int.TryParse(number, out amount);			
				
					((BaseVendor)this).ClaimCredits( pc, amount );
				}
	
				if ((Insensitive.StartsWith(said, "vendor" ) || Insensitive.StartsWith(said, this.Name)) && Insensitive.Contains(said, "explain"))
				{
				    if (this is Alchemist) { new Alchemist.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is AnimalTrainer) { new AnimalTrainer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Architect) { new Architect.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Armorer) { new Armorer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Artist) { new Artist.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Baker) { new Baker.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Banker) { new Banker.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Bard) { new Bard.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Barkeeper) { new Barkeeper.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Blacksmith) { new Blacksmith.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Bowyer) { new Bowyer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Cook) { new Cook.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Druid) { new Druid.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is DruidTree) { new DruidTree.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is DrunkenPirate) { new DrunkenPirate.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Enchanter) { new Enchanter.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Farmer) { new Farmer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Furtrader) { new Furtrader.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is GypsyLady) { new GypsyLady.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Herbalist) { new Herbalist.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is HolyMage) { new HolyMage.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is InnKeeper) { new InnKeeper.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Jester) { new Jester.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is KeeperOfChivalry) { new KeeperOfChivalry.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is LeatherWorker) { new LeatherWorker.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Mage) { new Mage.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Mapmaker) { new Mapmaker.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is NecroMage) { new NecroMage.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Necromancer) { new Necromancer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Provisioner) { new Provisioner.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Ranger) { new Ranger.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Sage) { new Sage.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Scribe) { new Scribe.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Shepherd) { new Shepherd.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Shipwright) { new Shipwright.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is StoneCrafter) { new StoneCrafter.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Tailor) { new Tailor.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Tanner) { new Tanner.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is TavernKeeper) { new TavernKeeper.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Thief) { new Thief.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Undertaker) { new Undertaker.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is VarietyDealer) { new VarietyDealer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Veterinarian) { new Veterinarian.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Weaponsmith) { new Weaponsmith.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Weaver) { new Weaver.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Witches) { new Witches.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is DeathKnightDemon) { new DeathKnightDemon.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is AlchemistGuildmaster) { new AlchemistGuildmaster.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is ArcherGuildmaster) { new ArcherGuildmaster.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is AssassinGuildmaster) { new AssassinGuildmaster.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is CartographersGuildmaster) { new CartographersGuildmaster.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is DruidGuildmaster) { new DruidGuildmaster.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is LibrarianGuildmaster) { new LibrarianGuildmaster.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is NecromancerGuildmaster) { new NecromancerGuildmaster.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is EvilHealer) { new EvilHealer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Healer) { new Healer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is WanderingHealer) { new WanderingHealer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Priest) { new Priest.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Courier) { new Courier.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Devon) { new Devon.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is EpicCharacter) { new EpicCharacter.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Garth) { new Garth.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Roscoe) { new Roscoe.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Xardok) { new Xardok.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is Citizens) { new Citizens.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is TownGuards) { new TownGuards.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is ChucklesJester) { new ChucklesJester.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is GodOfLegends) { new GodOfLegends.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is ExaltedDealer) { new ExaltedDealer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is LegendaryDealer) { new LegendaryDealer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is MythicalDealer) { new MythicalDealer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is PowerDealer) { new PowerDealer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }
				    else if (this is WonderousDealer) { new WonderousDealer.SpeechGumpEntry(e.Mobile, this).OnClick(); e.Handled = true; }

				}
				
				if ( this is BaseVendor && ( e.HasKeyword( 0x14D ) || e.HasKeyword( 0x3C ) || e.HasKeyword( 0x177 ) || e.HasKeyword( 0x171 ) ) )
				{
					if ( e.HasKeyword( 0x14D ) ) // *vendor sell*
					{
						e.Handled = true;
							
						((BaseVendor)this).VendorSell( pc );

					}
					else if ( e.HasKeyword( 0x3C ) ) // *vendor buy*
					{
						e.Handled = true;
						
						((BaseVendor)this).VendorBuy( pc );
					}
					else if ( e.HasKeyword( 0x177 ) ) // *sell*
					{
						e.Handled = true;
							
						((BaseVendor)this).VendorSell( pc );

					}
					else if ( e.HasKeyword( 0x171 ) ) // *buy*
					{
						e.Handled = true;
						
						((BaseVendor)this).VendorBuy( pc );
					}
					else
					{
						base.OnSpeech( e );
					}
				}				
				

				if ( said == null )
					return;

				ConvoTimer ct = GetConvo( pc );
				if ( ct == null )
				{
					bool convo = false;

					Fragment f = (Fragment)m_Frg[GreetingFragment];
					if ( f != null )
					{
						str = f.GetString( this, pc, said );
						convo = str != null && str.Length > 0;
					}

					if ( !convo && this.Name != null )
						convo = said.ToLower().IndexOf( this.Name.ToLower() ) != -1;

					if ( convo )
					{
						DebugSay( "They are talking to me!" );
						ct = StartConvo( pc );
						if ( str == null )
							str = "Hail, traveler.";
						if ( !OnConvoStart( pc ) )
							str = null;
					}
					else
					{
						DebugSay( "I dont think they are talking to me." );
					}
				}
				else
				{
					DebugSay( "I'm conversing with them!" );
					m_List.Clear();
					if ( m_Job != JobFragment.None )
						m_List.Add( (int)m_Job );
					GetConvoFragments( m_List );
					if ( m_List.Count > 0 )
					{
						for (int i=0;i<m_List.Count && str == null;i++)
						{
							Fragment f = (Fragment)m_Frg[(int)m_List[i]];
							if ( f != null )
								str = f.GetString( this, pc, said );
						}
					}

					if ( str == null )
					{
						Fragment f = (Fragment)m_Frg[EndFragment];
						if ( f != null )
							str = f.GetString( this, pc, said );

						if ( str == null )
						{
							f = (Fragment)m_Frg[DefaultFragment];
							if ( f != null && Utility.Random( 4 ) == 0 )
								str = f.GetString( this, pc, said );
						}
						else
						{
							DebugSay( "They ended the conversation" );
							if ( this.FocusMob == pc )
								this.FocusMob = null;
							ct.Stop();
							if ( m_ConvoList != null )
								m_ConvoList.Remove( ct );
						}
					}
					else if ( this.AIObject != null )
					{
						ct.Refresh();

						if ( this.AIObject.Action == ActionType.Wander || this.AIObject.Action == ActionType.Interact )
						{
							this.AIObject.Action = ActionType.Interact;
							this.FocusMob = pc;
						}
					}
				}

				if ( str != null && str.Length > 0 )
				{
					string town = this.Region.Name;// : "wilderness";
					if ( town == null || town.Length <= 1 )
						town = "great wide open";
					string job = this.Title != null && this.Title.StartsWith( "the" ) ? this.Title.Substring( 5 ) : m_Job.ToString();
					
					Say( String.Format( str, pc != null ? pc.Name : "someone", this.Name, job, town, "" ) );
					e.Handled = true;
				}
			}
		}
	}

	public enum RegionFragment
	{
		Wilderness = 0,
		_Offset = 100,

		britain,
		magincia,
		bucden,
		cove,
		jhelom,
		moonglow,
		nujelm,
		serphold,
		skara,
		trinsic,
		vesper,
		minoc,
		wind,
		yew,

		_End,
	}

	public enum JobFragment
	{
		None = 0,

		_Offset = 200,

		// real jobs:
		horse, 
		shopkeep,
		beggar,
		scholar,
		monk,
		sculptor,
		servant,
		shipwright,
		tailor,
		tinker,
		weaver,
		artist,
		architect,
		realtor,
		alchemist,
		baker,
		beekeeper,
		brigand,
		carpenter,
		cashual,
		cobbler,
		furtrader,
		gambler,
		glassblower,
		gypsy,
		herbalist,
		jailor,
		jeweler,
		master,
		mayor,
		judge,
		miner,
		rancher,
		animal,
		blacksmith,
		fighter,
		fisher,
		noble,
		bowyer,
		paladin,
		prisoner,
		sailor,
		pirate,
		waiter,
		weaponsmith,
		actor,
		armourer,
		healer,
		mage,
		bard,
		farmer,
		cook,
		guard,
		laborer,
		thief,
		innkeeper,
		tavkeep,
		minter,
		miller,
		vet,
		weaponstrainer,
		runner,
		priest,
		shepherd,
		ranger,
		tanner,
		mapmaker,
		banker,
		merchant,

		_End,
	}

	public enum Attitude { Wicked=0, Belligerent=1, Neutral=2, Kindly=3, Goodhearted=4 }
	public enum NotoVal { Infamous=0, Outlaw=1, Anonymous=2, Known=3 , Famous=4 }
	public enum Sophistication { Low=0, Medium=1, High=2 }
}

