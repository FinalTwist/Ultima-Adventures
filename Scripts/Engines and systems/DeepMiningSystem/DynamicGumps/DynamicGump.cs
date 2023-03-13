/*
 * 
 * By Gargouille
 * Date: 17/03/2013
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Prompts;
using Server.Targeting;

namespace DynamicGumps
{
	public enum DisplayMode { Blank, LabelLeft, Label, LabelData, List, ListNoButton, Previous, Close, LabelRight, ButtonLabel, LabelButton, ButtonLabelData}
	public enum IsClosable { True, False }
	public enum Profile { Paper, Stone, Craft, Craft2 }
	
	#region Attributes
	public abstract class CallBackAttribute : System.Attribute
	{
		public readonly string CallBack = "CallBack_";
		
		public CallBackAttribute()
		{
		}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class GumpHeaderAttribute : System.Attribute
	{
		public readonly Profile Profile;
		public readonly IsClosable IsClosable;
		public readonly string Title;
		
		public GumpHeaderAttribute(Profile profile, IsClosable closable, string title )
		{
			Profile = profile;
			IsClosable = closable;
			Title = title;
		}
	}

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class GumpElementAttribute : CallBackAttribute
	{
		public readonly string CheckActive = "IsActive_";
		public readonly string CheckTrue = "IsTrue_";
		public readonly string CheckData = "GetData_";
		public readonly string Text;
		public readonly string FalseText;
		public readonly DisplayMode Mode;
		
		public GumpElementAttribute(DisplayMode mode)
		{
			Mode = mode;
		}
		
		public GumpElementAttribute(DisplayMode mode, string text, string falsetext)
		{
			Mode = mode;
			Text = text;
			FalseText = falsetext;
		}
		
		public GumpElementAttribute(DisplayMode mode, string text)
		{
			Mode = mode;
			Text = text;
		}
	}
	
	#endregion

	public class GumpProfile
	{
		public GumpProfile(int b, int but, int but2, int butb, int butb2, int butc, int butc2, int prev, int prev2, int up, int up2, int down, int down2, int updownscale, int colortitre, int color1, int color2, int colorlabel, int colorlist)
		{
			m_BackGround = b;
			m_But = but;
			m_But2 = but2;
			m_Butb = butb;
			m_Butb2 = butb2;
			m_Butc = butc;
			m_Butc2 = butc2;
			m_Prev = prev;
			m_Prev2 = prev2;
			m_Up = up;
			m_Up2 = up2;
			m_Down = down;
			m_Down2 = down2;
			m_UpDownScale = updownscale;
			m_ColorTitre = colortitre;
			m_Color1 = color1;
			m_Color2 = color2;
			m_ColorLabel = colorlabel;
			m_ColorList = colorlist;
		}
		
		private int m_BackGround ; public int BackGround { get { return m_BackGround;} }
		private int m_But ; public int But { get { return m_But;}  }
		private int m_But2 ; public int But2 { get { return m_But2;}  }
		private int m_Butb ; public int Butb { get { return m_Butb;}  }
		private int m_Butb2 ; public int Butb2 { get { return m_Butb2;}  }
		private int m_Butc ; public int Butc { get { return m_Butc;}  }
		private int m_Butc2 ; public int Butc2 { get { return m_Butc2;}  }
		private int m_Prev ; public int Prev { get { return m_Prev;}  }
		private int m_Prev2 ; public int Prev2 { get { return m_Prev2;}  }
		private int m_Up ; public int Up { get { return m_Up;}  }
		private int m_Up2 ; public int Up2 { get { return m_Up2;}  }
		private int m_Down ; public int Down { get { return m_Down;}  }
		private int m_Down2 ; public int Down2 { get { return m_Down2;}  }
		private int m_UpDownScale ; public int UpDownScale { get { return m_UpDownScale;}  }
		private int m_ColorTitre ; public int ColorTitre { get { return m_ColorTitre;}  }
		private int m_Color1 ; public int Color1 { get { return m_Color1;} }
		private int m_Color2 ; public int Color2 { get { return m_Color2;}  }
		private int m_ColorLabel ; public int ColorLabel { get { return m_ColorLabel;}  }
		private int m_ColorList ; public int ColorList { get { return m_ColorList;}  }
		
		public static GumpProfile[] Profiles = new GumpProfile[]
		{
			new GumpProfile(0xbb8,0x15e1,0x15e5,0x845,0x846,0x26af,0x26b0,0x15a2,0x15a3,0x983,0x984,0x985,0x986,4,0x481,0x30,0x3fb,0xf3,0x0),//Paper
			new GumpProfile(2600,4005,4006,0xd0,0xd1,4005,4006,0xfb0,0xfb0,0x983,0x984,0x985,0x986,4,0x0,0x0,0x0,0x0,0x0),//Stone
			new GumpProfile(0xe10,0xfa5,0xfa6,0xfa5,0xfa6,0x868,0x86a,0xfb4,0xfb5,0xa5a,0xa5b,0xa58,0xa57,8,0x38c,0x3b0,0x3b0,0x3bb,0x3df),//Craft
			new GumpProfile(0xe10,0xfa5,0xfa6,0xfa5,0xfa6,0x868,0x86a,0xfae,0xfaf,0xa90,0xa91,0xa92,0xa93,8,0x38c,0x3b0,0x3b0,0x3bb,0x3df)//Craft2
				
		};
	}
	
	public class DynamicGump : Gump
	{
		private Mobile m_From;
		private object m_Parent;
		private List<MemberInfo> m_Members = new List<MemberInfo>();
		
		private new int X = 50;
		private new int Y = 50;
		private int Width = 200;
		private int Height;
		
		private const int XTab = 35;
		private int XText { get { return XButton + XTab; } }
		private const int YTab = 60;
		private int YText { get { return Y + YTab; } }
		private const int BR = 30;
		private int XButton { get { return X + XTab; } }
		
		private GumpProfile m_Profile;
		private int BackGround { get { return m_Profile.BackGround;}}
		private int button { get { return m_Profile.But;}}
		private int button2 { get { return m_Profile.But2;}}
		private int boolButton { get { return m_Profile.Butb;}}
		private int boolButton2 { get { return m_Profile.Butb2;}}
		private int buttonCategory { get { return m_Profile.Butc;}}
		private int buttonCategory2 { get { return m_Profile.Butc2;}}
		private int buttonPrevious { get { return m_Profile.Prev; }}
		private int buttonPrevious2 { get { return m_Profile.Prev2; }}
		private int Up { get { return m_Profile.Up; }}
		private int Up2 { get { return m_Profile.Up2; }}
		private int Down { get { return m_Profile.Down; }}
		private int Down2 { get { return m_Profile.Down2; }}
		private int UpDownScale { get {return m_Profile.UpDownScale; }}
		private int ColorTitre { get { return m_Profile.ColorTitre;}}
		private int Color1 { get { return m_Profile.Color1;}}
		private int Color2 { get { return m_Profile.Color2;}}
		private int ColorLabel { get { return m_Profile.ColorLabel;}}
		private int ColorList { get { return m_Profile.ColorList;}}
		
		private int MinWidth = 250;
		private int LonguestLine = 0;
		private int pixchar = 6;
		
		private int index = 0;
		
		public DynamicGump(Mobile from, object parent): base( 0, 0 )
		{
			m_From = from;
			m_Parent = parent;
			
			RenderBackGround();
		}
		
		private void RenderBackGround()
		{
			GetBackGroundInfo();
			
			AddLines();
		}
		
		private void GetBackGroundInfo()
		{
			GumpHeaderAttribute header = (GumpHeaderAttribute)m_Parent.GetType().GetCustomAttributes(typeof(GumpHeaderAttribute),false)[0];
			LonguestLine = header.Title.Length;
			
			GetList();
			
			Closable = header.IsClosable==IsClosable.True;
			
			m_Profile = GumpProfile.Profiles[(int)(header.Profile)];
			
			Width = ScaleWidth(Width);
			
			Height = YTab+(BR*(m_Members.Count+1));
			
			AddBackground( X, Y, Width, Height, BackGround );
			
			AddLabel( XText, Y+(YTab/3),ColorTitre, header.Title.ToUpper());
		}
		
		private void GetList()
		{
			foreach (MemberInfo member in  (m_Parent.GetType()).GetMembers())
			{
				object[] o = member.GetCustomAttributes(typeof(GumpElementAttribute),false);
				
				bool toAdd = false;
				
				if(o.Length>0)
				{
					if(IsValid(((GumpElementAttribute)o[0]).CheckActive+member.Name))
					{
						if((bool)(m_Parent.GetType().InvokeMember(((GumpElementAttribute)o[0]).CheckActive+member.Name, BindingFlags.InvokeMethod, null, m_Parent, new object[]{})))
							toAdd = true;
					}
					else
						toAdd = true;
				}
				
				if(toAdd)
				{
					m_Members.Add(member);
					
					CheckLength(member);
				}
			}
		}
		
		private void AddLines()
		{
			foreach (MemberInfo member in m_Members)
			{
				AddLine(member);
			}
		}
		
		private void AddLine(MemberInfo member)
		{
			string txt = "";
			int butt = button;
			int butt2 = button2;
			int tabtext = 0;
			int tabbutt = 0;
			int color = Color1;
			
			GumpElementAttribute attr =  Attr(member);
			
			if(attr.Mode!=DisplayMode.Blank)
			{
				if(IsValid(attr.CheckTrue+member.Name) && attr.FalseText!=null)
				{
					try
					{
						if((bool)(m_Parent.GetType().InvokeMember(attr.CheckTrue+member.Name, BindingFlags.InvokeMethod, null, m_Parent, new object[]{})))
							txt = attr.FalseText;
						
						butt = boolButton;
						butt2 = boolButton2;
					}catch{Console.WriteLine("Invoke error => CheckTrue_"+member.Name);}
				}
				
				if(txt=="")
					txt = attr.Text;
				
				if(attr.Mode==DisplayMode.ButtonLabelData | attr.Mode==DisplayMode.LabelData | attr.Mode==DisplayMode.List | attr.Mode==DisplayMode.ListNoButton )
				{
					if(IsValid(attr.CheckData+member.Name))
					{
						txt+=(string)(m_Parent.GetType().InvokeMember(attr.CheckData+member.Name, BindingFlags.InvokeMethod, null, m_Parent, new object[]{}));
					}
					
					if(attr.Mode==DisplayMode.List | attr.Mode==DisplayMode.ListNoButton)
					{
						tabtext+=XTab;
						color = ColorList;
					}
				}
				
				if(attr.Mode== DisplayMode.LabelButton )
				{
					tabbutt = Width-(XTab*2);
					tabtext = (Width-XText)-(attr.Text.Length*pixchar);
					butt = buttonCategory;
					butt2 = buttonCategory2;
					color = Color2;
				}
				else if (attr.Mode==DisplayMode.LabelLeft | attr.Mode==DisplayMode.LabelData)
				{
					tabtext = -XTab;
				}
				else if (attr.Mode==DisplayMode.LabelRight)
				{
					tabtext = (Width-XText)-(attr.Text.Length*pixchar);
				}
				
				if(attr.Mode<=DisplayMode.LabelData)
					color = ColorLabel;
				
				AddLabel(XText+tabtext, YText+(index*BR),color, txt);
				
				if(attr.Mode==DisplayMode.Previous)
				{
					butt = buttonPrevious;
					butt2 =buttonPrevious2;
				}
				
				if(attr.Mode >= DisplayMode.Previous)
					AddButton( XButton+tabbutt, YText+(index*BR), butt, butt2, m_Members.IndexOf(member)+1, GumpButtonType.Reply, 0);
				
				if(attr.Mode==DisplayMode.List | attr.Mode==DisplayMode.ListNoButton)
				{
					AddButton( XButton+XTab, YText+(index*BR)-UpDownScale, Up, Up2, m_Members.IndexOf(member)+1+1000, GumpButtonType.Reply, 0);
					AddButton( XButton+XTab, YText+(index*BR)+UpDownScale, Down, Down2, m_Members.IndexOf(member)+1+2000, GumpButtonType.Reply, 0);
					if(attr.Mode==DisplayMode.List)
						AddButton( XButton + Width-(XTab*2), YText+(index*BR), buttonCategory, buttonCategory2, m_Members.IndexOf(member)+1, GumpButtonType.Reply, 0);
				}
			}
			index++;
		}
		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int buttonID = info.ButtonID;
			
			string callback;
			
			if(buttonID==0)
			{
				callback = "CallBack_OnClosed";
				
				if(IsValid(callback))
					try
				{
					m_Parent.GetType().InvokeMember(callback, BindingFlags.InvokeMethod , null, m_Parent, new object[]{m_From});
				}
				catch{}
				
				return;
			}
			else buttonID--;//TODO bug if list is the first member
			
			bool up = false;
			bool down = false;
			if(buttonID>2000)
			{
				down = true;
				buttonID-=2000;
			}
			else if(buttonID>1000)
			{
				up = true;
				buttonID-=1000;
			}
			
			MemberInfo member = m_Members[buttonID];
			
			GumpElementAttribute attr = Attr(member);
			
			if(attr.Mode==DisplayMode.Close)return;
			
			callback = attr.CallBack+member.Name;
			
			if(up)callback+="_Up";
			if(down)callback+="_Down";
			
			if(IsValid(callback))
				m_Parent.GetType().InvokeMember(callback, BindingFlags.InvokeMethod , null, m_Parent, new object[]{m_From});
		}
		
		private int ScaleWidth(int width)
		{
			if(width < MinWidth)
				width = MinWidth;
			
			int scale = 0;
			if(LonguestLine > 23)
			{
				scale = pixchar*(LonguestLine-22);
			}
			return width+scale;
		}
		
		bool yetalist = false;
		private void CheckLength(MemberInfo member)
		{
			GumpElementAttribute attr = Attr(member);
			
			if(attr.Mode==DisplayMode.Blank)return;
			
			string text = attr.Text;
			string falsetext = attr.FalseText;
			
			if(text!=null && text.Length>LonguestLine)
				LonguestLine = text.Length;
			
			if(falsetext!=null && falsetext.Length>LonguestLine)
				LonguestLine = falsetext.Length;
			
			if(attr.Mode==DisplayMode.ButtonLabelData || attr.Mode==DisplayMode.LabelData || attr.Mode==DisplayMode.List)
			{
				if(IsValid(attr.CheckData+member.Name))
				{
					try
					{
						text+=(string)(m_Parent.GetType().InvokeMember(attr.CheckData+member.Name, BindingFlags.InvokeMethod, null, m_Parent, new object[]{}));
						if(text.Length > LonguestLine)
							LonguestLine = text.Length;
					}catch{Console.WriteLine("Invoke error => GetData_"+member.Name);}
				}
			}
			
			if(attr.Mode==DisplayMode.List && !yetalist)
			{
				yetalist = true;
				LonguestLine+=10;
			}
		}
		
		private bool IsValid(string method)
		{
			try
			{
				MethodInfo info = m_Parent.GetType().GetMethod(method);
				return info!=null;
			}
			catch{Console.WriteLine("Not Found Method => "+method);return false;}
		}
		
		public static GumpElementAttribute Attr(MemberInfo member)
		{
			return (GumpElementAttribute)(member.GetCustomAttributes(typeof(GumpElementAttribute),false)[0]);
		}
	}
	
	public class CallBackPrompt : Prompt
	{
		private object m_Sender;
		private string m_CallBack;
		private string m_CallBack2;
		
		public CallBackPrompt( object sender, string callback )
		{
			m_Sender = sender;
			m_CallBack = callback;
		}
		
		public CallBackPrompt( object sender, string callback, string callback2 )
		{
			m_Sender = sender;
			m_CallBack = callback;
			m_CallBack2 = callback2;
		}

		public override void OnResponse( Mobile m, string text )
		{
			try{m_Sender.GetType().InvokeMember(m_CallBack, BindingFlags.InvokeMethod, null, m_Sender, new object[]{m,text});}
			catch{Console.WriteLine("Method not found => "+m_CallBack);}
		}
		
		public override void OnCancel( Mobile m )
		{
			try{m_Sender.GetType().InvokeMember(m_CallBack2, BindingFlags.InvokeMethod, null, m_Sender, new object[]{m});}
			catch{Console.WriteLine("Method not found => "+m_CallBack2);}
		}
	}
	
	public class CallBackTarget : Target
	{
		private object m_Sender;
		private string m_CallBack;
		private object[] m_Data;

		public CallBackTarget( object sender, string callback, object[] data) : base( 20, true, TargetFlags.None )
		{
			m_Sender = sender;
			m_CallBack = callback;
			m_Data = data;
		}

		protected override void OnTarget( Mobile m, object o )
		{
			IPoint3D point = (IPoint3D)o;
			
			m_Sender.GetType().InvokeMember(m_CallBack, BindingFlags.InvokeMethod, null, m_Sender, new object[]{m,point,m_Data});
		}
	}
}