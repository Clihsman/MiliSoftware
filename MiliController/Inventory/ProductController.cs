﻿/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 17/12/2022          *
 * Assembly : MiliController           *
 * *************************************/

using GrapInterCom.Interfaces.Inventory;
using MiliSoftware.Model.WebServices;
using MiliSoftware.Objects.Inventory;
using MiliSoftware.WebServices;
using MiliWebService.WebServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.Inventory
{
    public class ProductController: IController<Product, string, string, string>
    {
        #region Constucts

        public ProductController(IProductGUI<Product,string,string,string> productGUI)
        {
            productGUI.controller = this;
            productGUI.Show();
        }

        #endregion

        #region CRUD

        /// <summary>
        /// Metodo Para Crear un Cliente
        /// </summary>
        /// <param name="data">Datos del cliente a crear</param>
        public bool Create(Product product)
        {
             WebPostService webPostService = new WebPostService("http://localhost/apirest/products");
             Console.WriteLine(webPostService.PostJson(product.ToJson()));      

            if (product.SaveImage)
            {
                webPostService = new WebPostService("http://localhost/apirest/files");
                string name = Path.GetFileName(product.Picture);
                MiliTools.IO.WebFile webFile = new MiliTools.IO.WebFile(product.Picture);
                Stream stream = webFile.GetStream();
                webPostService.PostStream(stream, name, WebFileFormat.PNG);
            }

            // Console.WriteLine(webPostService.PostStream(image, hash + ".png", WebFileFormat.PNG));
            //  throw new System.NotImplementedException();
            return true;
        }

        /// <summary>
        /// Metodo para Leer datos de un Cliente
        /// </summary>
        /// <param name="data">Datos del Cliente a Leer</param>
        public string Read(string data)
        {
            /*
            WebService.InitServices("", "");
            string json = WebService.GetString("apirest/clientes");
            Dictionary<string, object>[] customers = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(json);
            */

            //            return DObject.GetArray(clientes);
            return null;
        }

        /// <summary>
        /// MetodopPara Actualizar un cliente
        /// </summary>
        /// <param name="data"></param>
        public bool Update(string data)
        {
            //      WebService.InitServices("abc", "123");
            //     string response = WebService.PutString("apirest/clientes/agregar", data);
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Metodo para Eliminar un cliente
        /// </summary>
        /// <param name="data"></param>
        public bool Delete(string data)
        {
            //  WebService.InitServices("abc", "123");
            //string response = WebService.DeleteString("apirest/clientes/agregar", data);
            throw new System.NotImplementedException();
        }
        #endregion
    }
}