/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 28/04/2023          *
 * Assembly : MiliController           *
 * *************************************/

using MiliSoftware.SqlLite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MiliSoftware
{
    [SqlTable("Customers")]
    public class Client : IJsonObject
    {
        [SqlField(SqlFieldType.Text)]
        public string Code { get; set; }
        [SqlField(SqlFieldType.Integer)]
        public int Category { get; set; }
        [SqlField(SqlFieldType.Integer)]
        public int Group{ get; set; }
        [SqlField(SqlFieldType.Integer)]
        public int LineOfBusiness{ get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Name{ get; set; }
        [SqlField(SqlFieldType.Integer)]
        public int Type{ get; set; }
        [SqlField(SqlFieldType.Integer)]
        public int DocumentType{ get; set; }
        [SqlField(SqlFieldType.Integer)]
        public ulong Document{ get; set; }
        [SqlField(SqlFieldType.Text)]
        public string PictureSource{ get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Description{ get; set; }
        [SqlField(SqlFieldType.Integer)]
        public bool SaveImage{ get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Contact0{ get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Contact1{ get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Contact2{ get; set; }
        [SqlField(SqlFieldType.Text)]
        public string Email{ get; set; }
        [SqlField(SqlFieldType.Text)]
        public string BusinessRegistration{ get; set; }
        [SqlField(SqlFieldType.Integer)]
        public bool TaxIncluded{ get; set; }
        [SqlTableRef("Direction0", SqlTableRefType.Field)]
        public Address Direction0{ get; set; }
        [SqlTableRef("Direction1", SqlTableRefType.Field)]
        public Address Direction1{ get; set; }
        [SqlTableRef("Direction2", SqlTableRefType.Field)]
        public Address Direction2{ get; set; }

        public void FromJson(string json)
        {
            JObject jObject = JsonConvert.DeserializeObject<JObject>(json);
            if (!CheckObject(jObject)) throw new System.FormatException();
            FromJObject(jObject);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        private bool CheckObject(JObject @object)
        {
            if (!(@object.ContainsKey(nameof(Code)) && @object[nameof(Code)].Type == JTokenType.String))
                return false;

            if (!(@object.ContainsKey(nameof(Category)) && @object[nameof(Category)].Type == JTokenType.Integer))
                return false;

            if (!(@object.ContainsKey(nameof(Group)) && @object[nameof(Group)].Type == JTokenType.Integer))
                return false;

            if (!(@object.ContainsKey(nameof(LineOfBusiness)) && @object[nameof(LineOfBusiness)].Type == JTokenType.Integer))
                return false;

            if (!(@object.ContainsKey(nameof(Name)) && @object[nameof(Name)].Type == JTokenType.String))
                return false;

            if (!(@object.ContainsKey(nameof(Type)) && @object[nameof(Type)].Type == JTokenType.Integer))
                return false;

            if (!(@object.ContainsKey(nameof(DocumentType)) && @object[nameof(DocumentType)].Type == JTokenType.Integer))
                return false;

            if (!(@object.ContainsKey(nameof(Document)) && @object[nameof(Document)].Type == JTokenType.Integer))
                return false;

            if (!(@object.ContainsKey(nameof(PictureSource)) && @object[nameof(PictureSource)].Type == JTokenType.String))
                return false;

            if (!(@object.ContainsKey(nameof(SaveImage)) && @object[nameof(SaveImage)].Type == JTokenType.Boolean))
                return false;

            if (!(@object.ContainsKey(nameof(Contact0)) && @object[nameof(Contact0)].Type == JTokenType.String))
                return false;

            if (!(@object.ContainsKey(nameof(Contact1)) && @object[nameof(Contact1)].Type == JTokenType.String))
                return false;

            if (!(@object.ContainsKey(nameof(Contact2)) && @object[nameof(Contact2)].Type == JTokenType.String))
                return false;

            if (!(@object.ContainsKey(nameof(Email)) && @object[nameof(Email)].Type == JTokenType.String))
                return false;

            if (!(@object.ContainsKey(nameof(BusinessRegistration)) && @object[nameof(BusinessRegistration)].Type == JTokenType.String))
                return false;

            if (!(@object.ContainsKey(nameof(TaxIncluded)) && @object[nameof(TaxIncluded)].Type == JTokenType.Boolean))
                return false;

            if ((@object.ContainsKey(nameof(Direction0)) && @object[nameof(Direction0)].Type == JTokenType.Object))
            {
                JObject direction = ((JObject)@object[nameof(Direction0)]);

                if (!(direction.ContainsKey(nameof(Direction0.Country)) && direction[nameof(Direction0.Country)].Type == JTokenType.String))
                    return false;

                if (!(direction.ContainsKey(nameof(Direction0.City)) && direction[nameof(Direction0.City)].Type == JTokenType.String))
                    return false;

                if (!(direction.ContainsKey(nameof(Direction0.Condition)) && direction[nameof(Direction0.Condition)].Type == JTokenType.String))
                    return false;

                if (!(direction.ContainsKey(nameof(Direction0.Direction)) && direction[nameof(Direction0.Direction)].Type == JTokenType.String))
                    return false;

                if (!(direction.ContainsKey(nameof(Direction0.PostalCode)) && direction[nameof(Direction0.PostalCode)].Type == JTokenType.String))
                    return false;
            }
            else return false;

            if ((@object.ContainsKey(nameof(Direction1)) && @object[nameof(Direction1)].Type == JTokenType.Object))
            {
                JObject direction = ((JObject)@object[nameof(Direction1)]);

                if (!(direction.ContainsKey(nameof(Direction1.Country)) && direction[nameof(Direction1.Country)].Type == JTokenType.String))
                    return false;

                if (!(direction.ContainsKey(nameof(Direction1.City)) && direction[nameof(Direction1.City)].Type == JTokenType.String))
                    return false;

                if (!(direction.ContainsKey(nameof(Direction1.Condition)) && direction[nameof(Direction1.Condition)].Type == JTokenType.String))
                    return false;

                if (!(direction.ContainsKey(nameof(Direction1.Direction)) && direction[nameof(Direction1.Direction)].Type == JTokenType.String))
                    return false;

                if (!(direction.ContainsKey(nameof(Direction1.PostalCode)) && direction[nameof(Direction1.PostalCode)].Type == JTokenType.String))
                    return false;
            }
            else return false;

            if ((@object.ContainsKey(nameof(Direction2)) && @object[nameof(Direction2)].Type == JTokenType.Object))
            {
                JObject direction = ((JObject)@object[nameof(Direction2)]);

                if (!(direction.ContainsKey(nameof(Direction2.Country)) && direction[nameof(Direction2.Country)].Type == JTokenType.String))
                    return false;

                if (!(direction.ContainsKey(nameof(Direction2.City)) && direction[nameof(Direction2.City)].Type == JTokenType.String))
                    return false;

                if (!(direction.ContainsKey(nameof(Direction2.Condition)) && direction[nameof(Direction2.Condition)].Type == JTokenType.String))
                    return false;

                if (!(direction.ContainsKey(nameof(Direction2.Direction)) && direction[nameof(Direction2.Direction)].Type == JTokenType.String))
                    return false;

                if (!(direction.ContainsKey(nameof(Direction2.PostalCode)) && direction[nameof(Direction2.PostalCode)].Type == JTokenType.String))
                    return false;
            }
            else return false;

            return true;
        }

        private void FromJObject(JObject jObject)
        {
            Code = jObject[nameof(Code)].ToObject<string>();
            Category = jObject[nameof(Category)].ToObject<int>();
            Group = jObject[nameof(Group)].ToObject<int>();
            LineOfBusiness = jObject[nameof(LineOfBusiness)].ToObject<int>();
            Name = jObject[nameof(Name)].ToObject<string>();
            Type = jObject[nameof(Type)].ToObject<int>();
            DocumentType = jObject[nameof(DocumentType)].ToObject<int>();
            Document = jObject[nameof(Document)].ToObject<ulong>();
            PictureSource = jObject[nameof(PictureSource)].ToObject<string>();
            Description = jObject[nameof(Description)].ToObject<string>();
            SaveImage = jObject[nameof(SaveImage)].ToObject<bool>();
            Contact0 = jObject[nameof(Contact0)].ToObject<string>();
            Contact1 = jObject[nameof(Contact1)].ToObject<string>();
            Contact2 = jObject[nameof(Contact2)].ToObject<string>();
            Email = jObject[nameof(Email)].ToObject<string>();
            BusinessRegistration = jObject[nameof(BusinessRegistration)].ToObject<string>();
            TaxIncluded = jObject[nameof(TaxIncluded)].ToObject<bool>();
            Direction0 = jObject[nameof(Direction0)].ToObject<Address>();
            Direction1 = jObject[nameof(Direction1)].ToObject<Address>();
            Direction2 = jObject[nameof(Direction2)].ToObject<Address>();
        }
    }
}
