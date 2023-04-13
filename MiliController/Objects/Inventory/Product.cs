/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 09/02/2023          *
 * Assembly : MiliController           *
 * *************************************/

using Newtonsoft.Json.Linq;
using MiliSoftware.SqlLite;

namespace MiliSoftware
{
    [SqlTable("Products")]
    public class Product : IJsonObject
    {
        // product data
        [SqlField(SqlFieldType.Text)]
        public string _id { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Code { get; protected set; }
        [SqlField(SqlFieldType.Integer)]
        public int Type { get; protected set; }
        [SqlField(SqlFieldType.Integer)]
        public int Group { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Name { get; protected set; }
        [SqlField(SqlFieldType.Text)]
        public string Barcode { get; protected set; }
        [SqlField(SqlFieldType.Integer)]
        public int UnitOfMeasurement { get; protected set; }
        [SqlField(SqlFieldType.Integer)]
        public int TaxClassification { get; protected set; }
        [SqlField(SqlFieldType.Integer)]
        public int Store { get; protected set; }
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
        public ProductComponent[] ProductComponents { get; protected set; }

        public Product(string Code, int Type, int Group, string Name, string Barcode, int UnitOfMeasurement, int TaxClassification,
         int Store, string Picture, string Description, bool SaveImage, string Key0, string Value0, string Key1, string Value1, string Key2,
         string Value2, string Key3,  string Value3, string Key4, string Value4,string Key5, string Value5, 
         EquivalentProduct[] EquivalentProducts, ProductComponent[] ProductComponents)
        {
            this.Code = Code;
            this.Type = Type;
            this.Group = Group;
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

        public void FromJson(string json)
        {
            JObject jObject = JObject.Parse(json);
            _id = jObject.Value<string>(nameof(_id));
            Code = jObject.Value<string>(nameof(Code));
            Type = jObject.Value<int>(nameof(Type));
            Group = jObject.Value<int>(nameof(Group));
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
                        equivalent.Value<string>(nameof(EquivalentProduct._id)),
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
                        productComponent.Value<string>(nameof(ProductComponent._id)),
                        productComponent.Value<string>(nameof(ProductComponent.Code)),
                        productComponent.Value<string>(nameof(EquivalentProduct.Name)),
                        productComponent.Value<int>(nameof(ProductComponent.Amount)));
                }
            }
        }

        public string ToJson()
        {
            JObject jObject = new JObject();
            jObject.Add(nameof(Code), Code);
            jObject.Add(nameof(Type), Type);
            jObject.Add(nameof(Group), Group);
            jObject.Add(nameof(Name), Name);
            jObject.Add(nameof(Barcode), Barcode);
            jObject.Add(nameof(UnitOfMeasurement), UnitOfMeasurement);
            jObject.Add(nameof(TaxClassification), TaxClassification);
            jObject.Add(nameof(Store), Store);
            jObject.Add(nameof(Picture), Picture);
            jObject.Add(nameof(Description), Description);
            jObject.Add(nameof(SaveImage), SaveImage);

            // key value group
            jObject.Add(nameof(Key0), Key0);
            jObject.Add(nameof(Value0), Value0);
            jObject.Add(nameof(Key1), Key1);
            jObject.Add(nameof(Value1), Value1);
            jObject.Add(nameof(Key2), Key2);
            jObject.Add(nameof(Value2), Value2);
            jObject.Add(nameof(Key3), Key3);
            jObject.Add(nameof(Value3), Value3);
            jObject.Add(nameof(Key4), Key4);
            jObject.Add(nameof(Value4), Value4);
            jObject.Add(nameof(Key5), Key5);
            jObject.Add(nameof(Value5), Value5);

            // equivalent products 
            if (EquivalentProducts == null) jObject.Add(nameof(EquivalentProducts), null);
            else
            {
                JArray equivalens = new JArray();
                foreach (EquivalentProduct equivalent in EquivalentProducts)
                {
                    JObject o_equivalent = new JObject();
                    o_equivalent.Add(nameof(EquivalentProduct._id), equivalent._id);
                    o_equivalent.Add(nameof(EquivalentProduct.Code), equivalent.Code);
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
                    JObject o_productComponent = new JObject();
                    o_productComponent.Add(nameof(ProductComponent._id), productComponent._id);
                    o_productComponent.Add(nameof(ProductComponent.Code), productComponent.Code);
                    o_productComponent.Add(nameof(ProductComponent.Amount), productComponent.Amount);
                    components.Add(o_productComponent);
                }
                jObject.Add(nameof(ProductComponents), components);
            }

            return jObject.ToString();
        }
    }
}
