/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 28/12/2022          *
 * Assembly : MiliController           *
 * *************************************/

using GrapInterCom.Interfaces.Suppliers;
using MiliFileEngine.src.Resources;
using MiliSoftware.Model.WebServices;
using MiliSoftware.WebServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace MiliSoftware.Suppliers
{
    public class SuppliersController : IController<string, object[], string, string>
    {
        private ISuppliersGUI suppliersGUI;

        public SuppliersController(ISuppliersGUI suppliersGUI)
        {
            this.suppliersGUI = suppliersGUI;
            suppliersGUI.controller = this;
            suppliersGUI.Show();
        }

        public bool Create(string data)
        {
            object[] supplierData = suppliersGUI.GetValues();
            int id = 0;

            Dictionary<string, object> values = new Dictionary<string, object> {
                {"code", supplierData[id++]},
                {"category", supplierData[id++]},
                {"group", supplierData[id++]},
                {"lineOfBusiness", supplierData[id++]},
                {"name", supplierData[id++]},
                {"type", supplierData[id++]},
                {"documentType", supplierData[id++]},
                {"document", supplierData[id++]},
                {"pictureSource", supplierData[id++]},
                {"description", supplierData[id++]},
                {"saveImage", supplierData[id++]},
                {"contact0", supplierData[id++]},
                {"contact1", supplierData[id++]},
                {"contact2", supplierData[id++]},
                {"email", supplierData[id++]},
                {"businessRegistration", supplierData[id++]},
                {"taxIncluded", supplierData[id++]},
                // direcction 0
                {"country0", supplierData[id++]},
                {"condition0", supplierData[id++]},
                {"city0", supplierData[id++]},
                {"postalCode0", supplierData[id++]},
                {"direction0", supplierData[id++]},
                  // direcction 1
                {"country1", supplierData[id++]},
                {"condition1", supplierData[id++]},
                {"city1", supplierData[id++]},
                {"postalCode1", supplierData[id++]},
                {"direction1", supplierData[id++]},
                  // direcction 2
                {"country2", supplierData[id++]},
                {"condition2", supplierData[id++]},
                {"city2", supplierData[id++]},
                {"postalCode2", supplierData[id++]},
                {"direction2", supplierData[id++]},
            };
            if ((byte)values["saveImage"] == 1)
            {
                string regex = @"^(http|ftp|https|www)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?$";
                string file = (string)values["pictureSource"];
                string extension = Path.GetExtension(file).ToUpper();
                bool aceptFile = (extension == ".PNG" || extension == ".JPG" || extension == ".JPEG" || extension == ".BMP");
                if(!aceptFile) throw new MiliConException(0x13, CodeException.FileFormatNotAccepted, "An attempt was made to upload a file with an unaccepted format");
                Stream image = null;

                if (File.Exists(file))
                {
                    FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                    Bitmap bitmap = new Bitmap(stream);
                    image = new MemoryStream();
                    bitmap.Save(image, System.Drawing.Imaging.ImageFormat.Png);
                    image.Seek(0, SeekOrigin.Begin);
                    stream.Close();
                    bitmap.Dispose();

                }
                else if (Regex.IsMatch(file, regex))
                {
                    if (WebService.IsConnectedToInternet())
                    {
                        try
                        {
                            WebService.InitServices("abc", "123");
                            using (MemoryStream stream = WebService.GetData(file))
                            {
                                Bitmap bitmap = new Bitmap(stream);
                                image = new MemoryStream();
                                bitmap.Save(image, System.Drawing.Imaging.ImageFormat.Png);
                                image.Seek(0, SeekOrigin.Begin);
                                bitmap.Dispose();
                            }
                        }
                        catch (WebException)
                        {
                            throw new MiliConException(0x13, CodeException.CouldNotConnectToServer, "Cannot connect to remote server");
                        }
                    }
                    else
                    {
                        throw new MiliConException(0x13, CodeException.NoInternetConnection, "An attempt was made to save an internet resource without an internet connection");
                    }
                }

                if (image != null)
                {
                    string hash = MiliResources.Utils.GetHashToStream(image);
                    MiliResources.Externs.SetStream(hash, image);
                    try
                    {
                        WebPostService webPostService = new WebPostService("http://localhost/apirest/files");
                        Console.WriteLine(webPostService.PostStream(image, hash + ".png", WebFileFormat.PNG));
                    }
                    catch (WebException)
                    {
                        image.Close();
                        throw new MiliConException(0x13, CodeException.CouldNotConnectToServer, "Cannot connect to remote server");
                    }

                    image.Close();
                }
            }

            values.Remove("saveImage");
            DObject dObject = new DObject(values);


            //  WebService.InitServices("abc", "123");
            // string response = WebService.PostString("apirest/suppliers", dObject.ToString());

            return true;
        }

        public bool Delete(string data)
        {
            throw new NotImplementedException();
        }

        public object[] Read(object[] data)
        {
            throw new NotImplementedException();
        }

        public bool Update(string data)
        {
            throw new NotImplementedException();
        }
    }
}
