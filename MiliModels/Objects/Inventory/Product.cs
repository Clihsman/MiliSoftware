/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 09/02/2023          *
 * Assembly : MiliController           *
 * *************************************/

/*
    Es esta clase se crea los datos del producto
    Los atributos SqlField y SqlTableRef permiten 
    que la base de datos local se cree sin nesesidad 
    de escribir el sql y tambien permite 
    (Guardar, Eliminar, Editar, Actualizar) los datos
    la interfaz IJsonObject da los metodos para convertir
    la clase en un Json y cargar de un Json la clase
*/

using Newtonsoft.Json.Linq;
using MiliSoftware.SqlLite;

namespace MiliSoftware
{
    /// <summary>
    /// Esta clase contiene todos la información del producto
    /// </summary>
    [SqlTable("Products")]
    public class Product : IJsonObject
    {
        // product data
        [SqlField(SqlFieldType.Text)]
        public string _id { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Code { get; set; }
        [SqlField(SqlFieldType.Integer)]
        public dynamic Type { get; set; }
        [SqlTableRef("InventoryGroup", SqlTableRefType.Field)]
        public dynamic InventoryGroup { get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Name { get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Barcode { get; protected set; }
        [SqlField(SqlFieldType.Integer)]
        public dynamic UnitOfMeasurement { get; protected set; }
        [SqlField(SqlFieldType.Integer)]
        public dynamic TaxClassification { get; protected set; }
        [SqlField(SqlFieldType.Integer)]
        public dynamic Store { get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Picture { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Description { get; protected set; }
        [SqlField(SqlFieldType.Integer)]
        public bool SaveImage { get; protected set; }
        // key value group
        [SqlField(SqlFieldType.Text)]
        public string Key0 { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Value0 { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Key1 { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Value1 { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Key2 { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Value2 { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Key3 { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Value3 { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Key4 { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Value4 { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Key5 { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Value5 { get; protected set; }
        // equivalent products
        [SqlTableRef("EquivalentProducts", SqlTableRefType.Array)]
        public EquivalentProduct[] EquivalentProducts { get; protected set; }
        // product components
        [SqlTableRef("ProductComponents", SqlTableRefType.Array)]
        public ProductComponent[] ProductComponents { get; set; }

        /// <summary>
        /// Constructor de la clase Product
        /// </summary>
        /// <param name="Code">Codigo del producto</param>
        /// <param name="Type">Tipo de producto</param>
        /// <param name="Group">Grupo en que se encuentra el producto</param>
        /// <param name="Name">Nombre del producto</param>
        /// <param name="Barcode">Codigo de barras del producto</param>
        /// <param name="UnitOfMeasurement">Unidad de medida del producto</param>
        /// <param name="TaxClassification">Clasificacion tributaria del producto</param>
        /// <param name="Store">Bodega en que se encuentra el producto </param>
        /// <param name="Picture">Imagen del productos</param>
        /// <param name="Description">Descripcion del producto</param>
        /// <param name="SaveImage">Guardar imagen en el servidor?</param>
        /// <param name="Key0">Tipo definido por el Usuario</param>
        /// <param name="Value0">Tipo definido por el Usuario</param>
        /// <param name="Key1">Tipo definido por el Usuario</param>
        /// <param name="Value1">Tipo definido por el Usuario</param>
        /// <param name="Key2">Tipo definido por el Usuario</param>
        /// <param name="Value2">Tipo definido por el Usuario</param>
        /// <param name="Key3">Tipo definido por el Usuario</param>
        /// <param name="Value3">Tipo definido por el Usuario</param>
        /// <param name="Key4">Tipo definido por el Usuario</param>
        /// <param name="Value4">Tipo definido por el Usuario</param>
        /// <param name="Key5">Tipo definido por el Usuario</param>
        /// <param name="Value5">Tipo definido por el Usuario</param>
        /// <param name="EquivalentProducts">Productos equivalentes</param>
        /// <param name="ProductComponents">Componentes del producto</param>
        public Product(string Code, int Type, InventoryGroup InventoryGroup, string Name, string Barcode, int UnitOfMeasurement, int TaxClassification,
         int Store, string Picture, string Description, bool SaveImage, string Key0, string Value0, string Key1, string Value1, string Key2,
         string Value2, string Key3,  string Value3, string Key4, string Value4,string Key5, string Value5, 
         EquivalentProduct[] EquivalentProducts, ProductComponent[] ProductComponents)
        {
            this.Code = Code;
            this.Type = Type;
            this.InventoryGroup = InventoryGroup;
            this.Name = Name;
            this.Barcode = Barcode;
            this.UnitOfMeasurement = UnitOfMeasurement;
            this.TaxClassification = TaxClassification;
            this.Store = Store;
            this.Picture = Picture;
            this.Description = Description;
            this.SaveImage = SaveImage;
            // key value group
            this.Key0 = Key0;
            this.Value0 = Value0;
            this.Key1 = Key1;
            this.Value1 = Value1;
            this.Key2 = Key2;
            this.Value2 = Value2;
            this.Key3 = Key3;
            this.Value3 = Value3;
            this.Key4 = Key4;
            this.Value4 = Value4;
            this.Key5 = Key5;
            this.Value5 = Value5;
            // equivalent products
            this.EquivalentProducts = EquivalentProducts;
            // product components
            this.ProductComponents = ProductComponents;
        }

        public Product(){}

        /// <summary>
        /// Carga de un Json los datos de la clase
        /// </summary>
        /// <param name="json"></param>
        public void FromJson(string json)
        {
            JObject jObject = JObject.Parse(json);
            _id = jObject.Value<string>(nameof(_id));
            Code = jObject.Value<string>(nameof(Code));
            Type = jObject.Value<int>(nameof(Type));
           // Group = jObject.Value<int>(nameof(Group));
            Name = jObject.Value<string>(nameof(Name));
            Barcode = jObject.Value<string>(nameof(Barcode));
            UnitOfMeasurement = jObject.Value<int>(nameof(UnitOfMeasurement));
            TaxClassification = jObject.Value<int>(nameof(TaxClassification));
            Store = jObject.Value<int>(nameof(Store));
            Picture = jObject.Value<string>(nameof(Picture));
            Description = jObject.Value<string>(nameof(Description));
            SaveImage = jObject.Value<bool>(nameof(SaveImage));

            // key value group
            Key0 = jObject.Value<string>(nameof(Key0));
            Value0 = jObject.Value<string>(nameof(Value0));
            Key1 = jObject.Value<string>(nameof(Key1));
            Value1 = jObject.Value<string>(nameof(Value1));
            Key2 = jObject.Value<string>(nameof(Key2));
            Value2 = jObject.Value<string>(nameof(Value2));
            Key3 = jObject.Value<string>(nameof(Key3));
            Value3 = jObject.Value<string>(nameof(Value3));
            Key4 = jObject.Value<string>(nameof(Key4));
            Value4 = jObject.Value<string>(nameof(Value4));
            Key5 = jObject.Value<string>(nameof(Key5));
            Value5 = jObject.Value<string>(nameof(Value5));

            // equivalent products 
            if (jObject.Value<JArray>(nameof(EquivalentProduct)) == null) EquivalentProducts = null;
            else
            {
                JArray equivalens = jObject.Value<JArray>(nameof(EquivalentProducts));
                EquivalentProducts = new EquivalentProduct[equivalens.Count];

                for (int i = 0; i < EquivalentProducts.Length; i++)
                {
                    JObject equivalent = (JObject)equivalens[i];
                    EquivalentProducts[i] = new EquivalentProduct(
                        equivalent.Value<int>(nameof(EquivalentProduct.id)),
                        equivalent.Value<string>(nameof(EquivalentProduct.Code)),
                        equivalent.Value<string>(nameof(EquivalentProduct.Name)));
                }
            }

            // product components
            if (jObject.Value<JArray>(nameof(ProductComponents)) == null) ProductComponents = null;
            else
            {
                JArray productComponents = jObject.Value<JArray>(nameof(ProductComponents));
                ProductComponents = new ProductComponent[productComponents.Count];

                for (int i = 0; i < ProductComponents.Length; i++)
                {
                    JObject productComponent = (JObject)productComponents[i];
                    ProductComponents[i] = new ProductComponent(
                        productComponent.Value<int>(nameof(ProductComponent.id)),
                        productComponent.Value<string>(nameof(ProductComponent.Code)),
                        productComponent.Value<string>(nameof(EquivalentProduct.Name)),
                        productComponent.Value<int>(nameof(ProductComponent.Amount)));
                }
            }
        }

        /// <summary>
        /// Convierte la clase en un Json
        /// </summary>
        /// <returns>JSON</returns>
        public string ToJson()
        {
            JObject jObject = new JObject
            {
                { nameof(Code), Code },
                { nameof(Type), Type },

                //jObject.Add(nameof(Group), Group);
                { nameof(InventoryGroup), InventoryGroup?.GetJObject() },

                { nameof(Name), Name },
                { nameof(Barcode), Barcode },
                { nameof(UnitOfMeasurement), UnitOfMeasurement },
                { nameof(TaxClassification), TaxClassification },
                { nameof(Store), Store },
                { nameof(Picture), Picture },
                { nameof(Description), Description },
                { nameof(SaveImage), SaveImage },

                // key value group
                { nameof(Key0), Key0 },
                { nameof(Value0), Value0 },
                { nameof(Key1), Key1 },
                { nameof(Value1), Value1 },
                { nameof(Key2), Key2 },
                { nameof(Value2), Value2 },
                { nameof(Key3), Key3 },
                { nameof(Value3), Value3 },
                { nameof(Key4), Key4 },
                { nameof(Value4), Value4 },
                { nameof(Key5), Key5 },
                { nameof(Value5), Value5 }
            };

            // equivalent products 
            if (EquivalentProducts == null) jObject.Add(nameof(EquivalentProducts), null);
            else
            {
                JArray equivalens = new JArray();
                foreach (EquivalentProduct equivalent in EquivalentProducts)
                {
                    JObject o_equivalent = new JObject
                    {
                        { nameof(EquivalentProduct.id), equivalent.id },
                        { nameof(EquivalentProduct.Code), equivalent.Code },
                        { nameof(EquivalentProduct.Name), equivalent.Name }
                    };
                    equivalens.Add(o_equivalent);
                }
                jObject.Add(nameof(EquivalentProducts), equivalens);
            }
            // product components
            if (ProductComponents == null) jObject.Add(nameof(ProductComponents), null);
            else
            {
                JArray components = new JArray();
                foreach (ProductComponent productComponent in ProductComponents)
                {
                    JObject o_productComponent = new JObject
                    {
                        { nameof(ProductComponent.id), productComponent.id },
                        { nameof(ProductComponent.Code), productComponent.Code },
                        { nameof(ProductComponent.Name), productComponent.Name },
                        { nameof(ProductComponent.Amount), productComponent.Amount }
                    };
                    components.Add(o_productComponent);
                }
                jObject.Add(nameof(ProductComponents), components);
            }

            return jObject.ToString();
        }
    }
}
