/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 22/05/2023          *
 * Assembly : MiliController           *
 * *************************************/

using GrapInterCom.Interfaces.Inventory;
using MiliSoftware.Errores;
using MiliSoftware.Inventario;
using MiliSoftware.Inventario.Modelos;
using MiliSoftware.SqlLite;
using MiliSoftware.WebServices;
using MiliWebService.WebServices;
using OpenRestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.Inventory
{
    public class InventoryGroupController : IController<InventoryGroup, InventoryGroup[], InventoryGroup, InventoryGroup>
    {
        private InventarioGrupoApi InventarioGrupoApi = new InventarioGrupoApi();

        public InventoryGroupController(IInventoryGroupGUI inventoryGroup) {
            inventoryGroup.controller = this;
            inventoryGroup.Show();
        }

        public bool Create(InventoryGroup data)
        {
            try
            {
                SqlLiteDatabase database = new SqlLiteDatabase();
                database.Open();
                SqlLoader loader = new SqlLoader();
                SqlSchema schema = loader.LoadSqlSchema(typeof(InventoryGroup));
                schema.Save(database, schema.GetDataArray(data));
                database.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Delete(InventoryGroup data)
        {
            try
            {/*
                SqlLiteDatabase database = new SqlLiteDatabase();
                database.Open();
                SqlLoader loader = new SqlLoader();
                SqlSchema schema = loader.LoadSqlSchema(typeof(InventoryGroup));
                schema.Delete(database, schema.GetDataArray(data));
                database.Close();
                */
            }
            catch
            {
                return false;
            }

            return true;
        }

        public InventoryGroup[] Read(InventoryGroup[] a)
        {
            WebService.InitServices("", "");
            Response response = WebService.GetString("api/productos");
            string json = null;

            if (response)
            {
                json = response.Body as string;
            }
            else {
                return null;
            }
         
            Console.WriteLine(json);
            /*
    

            SqlLiteDatabase database = new SqlLiteDatabase();
            database.Open();
            SqlLoader loader = new SqlLoader();
            SqlSchema schema = loader.LoadSqlSchema(typeof(InventoryGroup));
            InventoryGroup[] data = schema.Find<InventoryGroup>(database);

            Console.WriteLine(data[0].ToJson());

            database.Close();
            */
            return InventoryGroup.FromJsonArray(json);
        }

        public async Task<int> EliminarGrupo(params int[] ids)
        {
            return await InventarioGrupoApi.Delete(ids);
        }

        public async Task<List<InventoryGroup>> GetInventoryGroups()
        {
            RestResponse<InventarioGrupo[], AccesoError> repuesta = await InventarioGrupoApi.All();
            if (repuesta)
                return new List<InventoryGroup>(repuesta.Value0.Select(o => new InventoryGroup(o.Codigo, o.Nombre) {Id=o.Id}));
            return null;
        }

        public async Task<InventoryGroup> GuardarGrupo(InventoryGroup grupo) {
            InventarioGrupo inventarioGrupo = await InventarioGrupoApi.Save(new InventarioGrupo() { Id = grupo.Id, Codigo = grupo.Code, Nombre = grupo.Name });
            return new InventoryGroup {Id=inventarioGrupo.Id, Name= inventarioGrupo.Nombre, Code= inventarioGrupo.Codigo};
        }

        public async Task<int> ActualizarGrupo(InventoryGroup grupo)
        {
            return await InventarioGrupoApi.Update(new InventarioGrupo() { Id = grupo.Id, Codigo = grupo.Code, Nombre = grupo.Name }, grupo.Id);
        }

        public bool Update(InventoryGroup data)
        {
            try
            {
                SqlLiteDatabase database = new SqlLiteDatabase();
                database.Open();
                SqlLoader loader = new SqlLoader();
                SqlSchema schema = loader.LoadSqlSchema(typeof(InventoryGroup));
                schema.Edit(database, schema.GetDataArray(data));
                Console.WriteLine(data.ToJson());
                database.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }
    }
}
