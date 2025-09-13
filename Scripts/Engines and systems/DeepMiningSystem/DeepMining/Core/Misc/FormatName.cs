/*
 * Crée par SharpDevelop.
 * Gargouille 
 * Date: 03/10/2014
 */

using System;
using Server;

namespace Server.DeepMine
{
	public static class FormatName
	{
		public static string ToOre(string text)
		{
			return text.Remove(0,13);
		}
		
		public static string ToMobile(string text)
		{
			return text.Remove(0,15);
		}
	}
}