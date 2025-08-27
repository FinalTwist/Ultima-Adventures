using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Server.Misc
{
    public static class Cliloc
    {
        private static readonly Dictionary<int, string> m_Entries;

        static Cliloc()
        {
            m_Entries = new Dictionary<int, string>();

            string path = Path.Combine(Core.BaseDirectory, "Files", "Cliloc.enu");
            if (!File.Exists(path))
                return;

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    // Consumir cabecera y versión (no se usan, pero hay que leerlas)
                    int header = reader.ReadInt32();
                    short version = reader.ReadInt16();

                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        int number = reader.ReadInt32();
                        byte flag = reader.ReadByte();
                        int length = reader.ReadUInt16(); // longitud sin signo

                        if (length <= 0)
                            continue;

                        long remaining = reader.BaseStream.Length - reader.BaseStream.Position;
                        if (length > remaining)
                            break; // archivo corrupto/truncado

                        byte[] buffer = reader.ReadBytes(length);
                        string text = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

                        int nullIdx = text.IndexOf('\0');
                        if (nullIdx >= 0)
                            text = text.Substring(0, nullIdx);

                        m_Entries[number] = text;
                    }
                }
            }
            catch
            {
                // Opcional: loguear el error
            }
        }

        public static string GetString(int number)
        {
            string text;
            return m_Entries.TryGetValue(number, out text) ? text : null;
        }

        // args tabulados en una sola cadena (p.ej. "uno\tdos\ttres")
        public static string GetString(int number, string args)
        {
            string text = GetString(number);
            if (text == null)
                return null;

            if (string.IsNullOrEmpty(args))
                return text;

            string[] parts = args.Split('\t');
            // Construir object[] sin LINQ:
            object[] boxed = Array.ConvertAll(parts, new Converter<string, object>(delegate (string s) { return (object)s; }));

            try
            {
                return string.Format(text, boxed);
            }
            catch (FormatException)
            {
                // Si no coinciden los marcadores, devolver el texto plano.
                return text;
            }
        }

        // Overload útil por si ya tienes los argumentos separados
        public static string GetString(int number, params string[] args)
        {
            string text = GetString(number);
            if (text == null)
                return null;

            if (args == null || args.Length == 0)
                return text;

            // Convertir string[] -> object[] sin LINQ
            object[] boxed = Array.ConvertAll(args, new Converter<string, object>(delegate (string s) { return (object)s; }));

            try
            {
                return string.Format(text, boxed);
            }
            catch (FormatException)
            {
                return text;
            }
        }
    }
}
