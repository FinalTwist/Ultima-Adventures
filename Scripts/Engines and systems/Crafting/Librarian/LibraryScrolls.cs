using System;
using Server;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
	public class LibraryScroll1 : UnknownScroll
	{
		[Constructable]
		public LibraryScroll1()
		{
			if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ ScrollType = 3; } else { ScrollType = 2; } } else { ScrollType = 1; }
			ScrollLevel = 1;

			string scroll = "magery";
				if ( ScrollType == 2 ){ scroll = "magery"; }
				else if ( ScrollType == 3 ){ scroll = "bardic"; }

			string writing = "a plainly";
				if ( ScrollLevel == 2 ){ writing = "an expertly"; }
				else if ( ScrollLevel == 3 ){ writing = "an adeptly"; }
				else if ( ScrollLevel == 4 ){ writing = "a cleverly"; }
				else if ( ScrollLevel == 5 ){ writing = "a deviously"; }
				else if ( ScrollLevel == 6 ){ writing = "an ingeniously"; }

			ScrollOrigin = "may be " + writing + " written " + scroll + " scroll";
		}

		public LibraryScroll1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	public class LibraryScroll2 : UnknownScroll
	{
		[Constructable]
		public LibraryScroll2()
		{
			if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ ScrollType = 3; } else { ScrollType = 2; } } else { ScrollType = 1; }
			ScrollLevel = 1;

			string scroll = "magery";
				if ( ScrollType == 2 ){ scroll = "magery"; }
				else if ( ScrollType == 3 ){ scroll = "bardic"; }

			string writing = "a plainly";
				if ( ScrollLevel == 2 ){ writing = "an expertly"; }
				else if ( ScrollLevel == 3 ){ writing = "an adeptly"; }
				else if ( ScrollLevel == 4 ){ writing = "a cleverly"; }
				else if ( ScrollLevel == 5 ){ writing = "a deviously"; }
				else if ( ScrollLevel == 6 ){ writing = "an ingeniously"; }

			ScrollOrigin = "may be " + writing + " written " + scroll + " scroll";
		}

		public LibraryScroll2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	public class LibraryScroll3 : UnknownScroll
	{
		[Constructable]
		public LibraryScroll3()
		{
			if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ ScrollType = 3; } else { ScrollType = 2; } } else { ScrollType = 1; }
			ScrollLevel = 1;

			string scroll = "magery";
				if ( ScrollType == 2 ){ scroll = "magery"; }
				else if ( ScrollType == 3 ){ scroll = "bardic"; }

			string writing = "a plainly";
				if ( ScrollLevel == 2 ){ writing = "an expertly"; }
				else if ( ScrollLevel == 3 ){ writing = "an adeptly"; }
				else if ( ScrollLevel == 4 ){ writing = "a cleverly"; }
				else if ( ScrollLevel == 5 ){ writing = "a deviously"; }
				else if ( ScrollLevel == 6 ){ writing = "an ingeniously"; }

			ScrollOrigin = "may be " + writing + " written " + scroll + " scroll";
		}

		public LibraryScroll3( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	public class LibraryScroll4 : UnknownScroll
	{
		[Constructable]
		public LibraryScroll4()
		{
			if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ ScrollType = 3; } else { ScrollType = 2; } } else { ScrollType = 1; }
			ScrollLevel = 1;

			string scroll = "magery";
				if ( ScrollType == 2 ){ scroll = "magery"; }
				else if ( ScrollType == 3 ){ scroll = "bardic"; }

			string writing = "a plainly";
				if ( ScrollLevel == 2 ){ writing = "an expertly"; }
				else if ( ScrollLevel == 3 ){ writing = "an adeptly"; }
				else if ( ScrollLevel == 4 ){ writing = "a cleverly"; }
				else if ( ScrollLevel == 5 ){ writing = "a deviously"; }
				else if ( ScrollLevel == 6 ){ writing = "an ingeniously"; }

			ScrollOrigin = "may be " + writing + " written " + scroll + " scroll";
		}

		public LibraryScroll4( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	public class LibraryScroll5 : UnknownScroll
	{
		[Constructable]
		public LibraryScroll5()
		{
			if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ ScrollType = 3; } else { ScrollType = 2; } } else { ScrollType = 1; }
			ScrollLevel = 1;

			string scroll = "magery";
				if ( ScrollType == 2 ){ scroll = "magery"; }
				else if ( ScrollType == 3 ){ scroll = "bardic"; }

			string writing = "a plainly";
				if ( ScrollLevel == 2 ){ writing = "an expertly"; }
				else if ( ScrollLevel == 3 ){ writing = "an adeptly"; }
				else if ( ScrollLevel == 4 ){ writing = "a cleverly"; }
				else if ( ScrollLevel == 5 ){ writing = "a deviously"; }
				else if ( ScrollLevel == 6 ){ writing = "an ingeniously"; }

			ScrollOrigin = "may be " + writing + " written " + scroll + " scroll";
		}

		public LibraryScroll5( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	public class LibraryScroll6 : UnknownScroll
	{
		[Constructable]
		public LibraryScroll6()
		{
			if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ ScrollType = 3; } else { ScrollType = 2; } } else { ScrollType = 1; }
			ScrollLevel = 1;

			string scroll = "magery";
				if ( ScrollType == 2 ){ scroll = "magery"; }
				else if ( ScrollType == 3 ){ scroll = "bardic"; }

			string writing = "a plainly";
				if ( ScrollLevel == 2 ){ writing = "an expertly"; }
				else if ( ScrollLevel == 3 ){ writing = "an adeptly"; }
				else if ( ScrollLevel == 4 ){ writing = "a cleverly"; }
				else if ( ScrollLevel == 5 ){ writing = "a deviously"; }
				else if ( ScrollLevel == 6 ){ writing = "an ingeniously"; }

			ScrollOrigin = "may be " + writing + " written " + scroll + " scroll";
		}

		public LibraryScroll6( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
}