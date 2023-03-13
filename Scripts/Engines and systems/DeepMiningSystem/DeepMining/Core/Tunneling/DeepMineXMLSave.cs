/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 25/09/2014
 */

/// <summary>
/// All infos stored in DeepMineInfos are serialized here
/// 
/// Save is called by SaveWorld
///
/// Load is called by DeepMineInfos at Initialize
///
/// </summary>

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Server;

namespace Server.DeepMine
{
	public static class DeepMineXMLSave
	{
		public static void Configure()
		{
			EventSink.WorldSave += new WorldSaveEventHandler( Save );
		}
		
		private static string path = Path.Combine( Core.BaseDirectory, "Data/DeepMine/");
		private static string filepath = Path.Combine( path, String.Format( "{0}.XML", "DeepMine") );
		
		public static bool Load(MapRegister entry)
		{
			if(File.Exists(filepath))
			{
				XmlDocument xml = new XmlDocument();
				xml.Load(filepath);
				
				XmlNode allmap = xml.SelectSingleNode("AllMaps");
				
				foreach(XmlNode map in allmap.ChildNodes)
				{
					if(map.Name=="Map")
					{
						XmlNode mapid = map.SelectSingleNode("MapID");
						if(mapid!=null && entry.MapID==Convert.ToInt32(mapid.InnerText))
						{
							XmlNode mines = map.SelectSingleNode("Mines");
							if(entry.Mines!=Convert.ToInt32(mines.InnerText))
								return false;
							
							XmlNode levels = map.SelectSingleNode("Levels");
							if(entry.Levels!=Convert.ToInt32(levels.InnerText))
								return false;
							
							DeepMapInfo info = new DeepMapInfo();
							
							info.Deserialize(map);
							
							DeepMineInfos.MapDefs.Add(Map.Maps[entry.MapID],info);
							
							return true;
						}
					}
				}
				
				return false;
			}
			else return false;
		}
		
		public static void Save( WorldSaveEventArgs e )
		{
			if(!Directory.Exists(path))
				Directory.CreateDirectory(path);
			
			if(File.Exists(filepath))
				File.Delete(filepath);
			
			FileStream myFileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
			
			XmlTextWriter writer = new XmlTextWriter(myFileStream, Encoding.UTF8);
			
			writer.Formatting = Formatting.Indented;
			
			writer.WriteStartDocument(true);
			
			DeepMineInfos.Serialize(writer);
			
			writer.Close();
		}
	}
}