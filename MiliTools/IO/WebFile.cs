/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 24/10/2022          *
 * Assembly : MiliTools                *
 * *************************************/
 
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Net;

namespace MiliTools.IO
{
    /// <summary>
    /// Clase para manejar archivos locales y en la nube
    /// </summary>
    public sealed class WebFile
    {
        /// <summary>
        /// Ruta del archivo
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Constructor de la clase WebFile
        /// </summary>
        /// <param name="fileName">Ruta del archivo</param>
        public WebFile(string fileName) {
            FileName = fileName;
        }

        /// <summary>
        /// Metodo para Obtener la clase Stream del archivo
        /// </summary>
        /// <returns></returns>
        public Stream GetStream() {
            Stream stream = null;

            if (IsWeb(FileName))
            {
                Uri uri = new Uri(FileName);
                WebClient M_WebClient = new WebClient();
                stream = new MemoryStream(M_WebClient.DownloadData(uri));
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
            else if(IsLocal(FileName))
            {
                stream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            }
            else throw new FileNotFoundException("The specific file does not exist, please verify that the path entered is valid", FileName);

            return stream;
        }

        /// <summary>
        /// Metodo para guardar el archivo
        /// </summary>
        /// <param name="fileName">Ruta del archivo</param>
        public void Save(string fileName)
        {
            using (Stream stream = GetStream())
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                File.WriteAllBytes(fileName, buffer);
            }
        }

        /// <summary>
        /// Metodo para guardar el archivo en la carpeta temporal del sistema
        /// </summary>
        /// <returns>Ruta del archivo</returns>
        public string SaveTemp()
        {
            string tempName = Path.GetTempPath() + Guid.NewGuid().ToString();
            using (Stream stream = GetStream())
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                File.WriteAllBytes(tempName, buffer);
            }
            return tempName;
        }

        /// <summary>
        /// Metodo para verificar si la ruta del archivo es valida
        /// </summary>
        /// <param name="fileName">Ruta del archivo</param>
        /// <returns></returns>
        public static bool IsValidFile(string fileName) {
            return IsLocal(fileName) || IsWeb(fileName);
        }

        /// <summary>
        /// Metodo para verificar si la ruta del archivo es local
        /// </summary>
        /// <param name="fileName">Ruta del archivo</param>
        /// <returns></returns>
        public static bool IsLocal(string fileName) {
            return File.Exists(fileName);
        }

        /// <summary>
        /// Metodo para verificar si la ruta del archivo es el la nube
        /// </summary>
        /// <param name="fileName">Ruta del archivo</param>
        /// <returns></returns>
        public static bool IsWeb(string fileName) {
            string regex = @"^(http|ftp|https|www)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?$";
            return Regex.IsMatch(fileName, regex);
        }
    }
}
