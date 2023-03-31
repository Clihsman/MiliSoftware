using MiliFileEngine.src.IDS;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using MiliEngine.src.Attributes;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace MiliFileEngine.src.Resources
{
    public class MiliResources
    {
        public const int classID = ClassIDS.MILIRESOURCES;

        public static class Externs
        {
            public const int classID = ClassIDS.MILIRESOURCES_EXTERNS;

            #region GetData 7AE138E4-E40F1AF4-EF35538F-9C87B6D8

            [MiliHash("7515280E-23291792-23A3E3AC-EE3C675F")]
            public static T GetObject<T>(string hash)
            {
                string json = GetString(hash);
                return JsonConvert.DeserializeObject<T>(json);
            }

            [MiliHash("85736601-563BDDA5-5B4EB498-93BA2A0F")]
            public static Bitmap GetBitmap(string hash) {
                return new Bitmap(GetStream(hash));
            }

            [MiliHash("58442BE3-9A4F4E19-76757696-C3D52376")]
            public static string GetString(string hash)
            {
                StreamReader reader = new StreamReader(GetStream(hash));
                string @string = reader.ReadToEnd();
                reader.Close();
                return @string;
            }

            [MiliHash("6194E391-FD5134C2-764EA7C5-2DFCB1C5")]
            public static Stream GetStream(string hash)
            {
                string filename = GetFileName(hash);
                FileStream fileStream = new FileStream(filename, FileMode.Open,FileAccess.Read);
                GZipStream gZipStream = new GZipStream(fileStream, CompressionMode.Decompress);
                return gZipStream;
            }

            #endregion

            #region SetData F1243641-B8643D21-B6D53461-7A4167A

            [MiliHash("444C0A11-C33A139-D7947100-543B4A26")]
            public static void SetObject<T>(string hash,T obj)
            {
                string json = JsonConvert.SerializeObject(obj);
                SetString(hash, json);
            }

            [MiliHash("D19A7F8B-D5A30883-5ED69A4B-A46CFD1")]
            public static void SetString(string hash,string @string) {
                MemoryStream memoryStream = new MemoryStream();
                byte[] buffer = Encoding.UTF8.GetBytes(@string);

                memoryStream.Write(buffer, 0, buffer.Length);
                memoryStream.Seek(0,SeekOrigin.Begin);

                SetStream(hash, memoryStream);
                memoryStream.Close();
            }

            [MiliHash("D36A437-2058625A-33450513-E4EB1B29")]
            public static void SetStream(string hash,Stream stream)
            {
                string filename = GetFileName(hash, true);

                FileStream fileStream = new FileStream(filename,FileMode.Create, FileAccess.Write);
                GZipStream gZipStream = new GZipStream(fileStream, CompressionMode.Compress);

                CopyStream(stream, gZipStream);

                gZipStream.Flush();
                gZipStream.Close();
                fileStream.Close();
                stream.Seek(0, SeekOrigin.Begin);
            }

            [MiliHash("D205E98C-1FD871E6-178FFF15-37B73")]
            public static void SetBitmap(string hash,Bitmap bitmap) {
                MemoryStream memoryStream = new MemoryStream();
                bitmap.Save(memoryStream,System.Drawing.Imaging.ImageFormat.Png);
                SetStream(hash, memoryStream);
                memoryStream.Close();
            }

            #endregion

            #region Tools 4E769EA3-6691460-8C21D7F9-33D6B91

            [MiliHash("24FFF40C-E02C6FEC-F6EBDF3C-6B875829")]
            public static string GetFileName(string hash,bool createFolder = false)
            {
                string directory = string.Format("assets/objects/{0}", hash.Substring(0, 2));

                if (!Directory.Exists(directory) && createFolder)
                    Directory.CreateDirectory(directory);

                return string.Format("{0}/{1}", directory, hash);
            }

            [MiliHash("401E8892-4DE2EEFA-A332878-381A08D6")]
            public static FileStream CreateFile(string hash) {
                string filename = GetFileName(hash);
                return File.Create(filename);
            }

            [MiliHash("A52E1369-6EDF917F-CB99E133-132C091A")]
            public static bool ExistFile(string hash)
            {
                string filename = GetFileName(hash);
                return File.Exists(filename);
            }

            [MiliHash("C3D16F19-F6997B64-76274366-FAFF98DD")]
            private static void CopyStream(Stream input, Stream output)
            {
                input.CopyTo(output);
            }

            #endregion
        }

        public static class Interns
        {
            public const int classID = ClassIDS.MILIRESOURCES_INTERNS;


        }

        public static class Utils
        {
            public static string GetHashToName(string str)
            {
                SHA1 sha1 = SHA1.Create();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                return sb.ToString();
            }

            public static string GetHashToStream(Stream str)
            {
                SHA1 sha1 = SHA1.Create();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = sha1.ComputeHash(str);
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                str.Seek(0, SeekOrigin.Begin);
                return sb.ToString();
            }
        }
    }
}
