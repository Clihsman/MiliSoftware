/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 30/03/2023          *
 * Assembly : MiliDatabase             *
 * *************************************/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MiliSoftware.SqlLite
{
    public class SqlSchema
    {
        public SqlTable SqlTable { get; private set; }
        public SqlField[] Fields { get; private set; }
        private Dictionary<SqlField, object> FieldInfos { get; set; }

        public SqlSchema(SqlTable sqlTable, SqlField[] sqlFields)
        {          
            SqlTable = sqlTable;
            Fields = sqlFields;
        }

        public SqlSchema(SqlTable sqlTable, SqlField[] sqlFields, Dictionary<SqlField, object> fieldInfos)
        {
            SqlTable = sqlTable;
            Fields = sqlFields;
            FieldInfos = fieldInfos;
        }

        public object[] GetDataArray(object instance)
        {
            Type type = instance.GetType();
            object[] values = new object[Fields.Length];

            for (int i = 0; i < values.Length; i++)
            {
                SqlField sqlField = Fields[i];
                var field = type.GetField(sqlField.Name);
                if (field != null)
                {
                    values[i] = field.GetValue(instance);
                }
                else
                { 
                    var property = type.GetProperty(sqlField.Name);
                    if(!(property is null)) values[i] = property.GetValue(instance);
                }
            }

            return values;
        }

        public string GetCreateTable()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("CREATE TABLE IF NOT EXISTS {0}", SqlTable.TableName));
            stringBuilder.Append(GetFields());
            return stringBuilder.ToString();
        }

        private string GetFields() {
            List<string> fields = new List<string>();

            foreach (SqlField sqlField in Fields)
            {
                fields.Add(string.Format("\"{0}\" {1}", sqlField.Name, sqlField.FieldType.ToString().ToUpper()));
            }

            if (!fields.Contains(string.Format("\"{0}\" {1}", "_id", "TEXT")))
                fields.Insert(0, string.Format("\"{0}\" {1}", "_id", "TEXT"));

            return string.Format("({0})", string.Join(",", fields));
        }

        private string GetFieldNames()
        {
            List<string> fields = new List<string>();

            foreach (SqlField sqlField in Fields)
            {
                fields.Add(string.Format("\"{0}\"", sqlField.Name));
            }

            if(!fields.Contains(string.Format("\"{0}\"", "_id")))
                fields.Insert(0, string.Format("\"{0}\"", "_id"));

            return string.Format("({0})", string.Join(",", fields));
        }

        private string GetFieldParams()
        {
            List<string> fields = new List<string>();

            foreach (SqlField sqlField in Fields)
            {
                fields.Add(string.Format("@{0}", sqlField.Name));
            }

            if(!fields.Contains("@_id"))
                fields.Insert(0, "@_id");

            return string.Format("({0})", string.Join(",", fields));
        }

        private string GetDeleteParams(object[] data)
        {
            List<string> fields = new List<string>();

            for (int i = 0; i < Fields.Length; i++)
            {
                if (Fields[i].Name == "_id" && data[i] != null) continue;
                fields.Add(string.Format("{0} = @{0}", Fields[i].Name));
            }

            return string.Format("{0}", string.Join(" AND ", fields));
        }

        private string GetEditParams(object[] data)
        {
            List<string> fields = new List<string>();

            for (int i = 0; i < Fields.Length; i++)
            {
                if (Fields[i].Name == "_id") continue;
                fields.Add(string.Format("{0} = @{0}", Fields[i].Name));
            }

            return string.Format("{0}", string.Join(",", fields));
        }

        public void Save(SqlLiteDatabase database, params object[] data)
        {
            InsertSqlTableRef(database, data);
            database.ExecuteNomQueryID(string.Format("INSERT INTO {0}{1} VALUES {2}"
                , SqlTable.TableName, GetFieldNames(), GetFieldParams()), GetInsertData(database, data));
        }

        public void Save(string _id, SqlLiteDatabase database, params object[] data)
        {
            InsertSqlTableRef(database, data);
            Dictionary<string, object> insertData = GetInsertData(database, data);
            insertData["@_id"] = _id;

            database.ExecuteNomQueryID(string.Format("INSERT INTO {0}{1} VALUES {2}"
                , SqlTable.TableName, GetFieldNames(), GetFieldParams()), insertData);
        }

        public void Delete(SqlLiteDatabase database, params object[] data)
        {
            InsertSqlTableRef(database, data);
            database.ExecuteNomQueryID(string.Format("DELETE FROM {0} WHERE {2}"
                , SqlTable.TableName, GetFieldNames(), GetDeleteParams(data)), GetInsertData(database, data));
            Console.WriteLine(string.Format("DELETE FROM {0} WHERE {2}"
                , SqlTable.TableName, GetFieldNames(), GetDeleteParams(data)), GetInsertData(database, data));
        }

        public void Edit(SqlLiteDatabase database, params object[] data)
        {
            InsertSqlTableRef(database, data);
            database.ExecuteNomQueryID(string.Format("UPDATE {0} SET {1} WHERE _id=@_id"
                , SqlTable.TableName, GetEditParams(data)), GetInsertData(database, data));
        }

 

        private object[] InsertSqlTableRef(SqlLiteDatabase database, object[] data)
        {
            SqlLoader loader = new SqlLoader();

            if (data == null) return data;

            for (int i = 0; i < Fields.Length; i++)
            {
                if (Fields[i] is SqlTableRef)
                {
                   // Console.WriteLine("Name: {0} IsArray : {1}", ((SqlTableRef)Fields[i]).Name, data[i] is null);
                    if (data[i] != null && data[i].GetType().IsArray && ((SqlTableRef)Fields[i]).SqlTableRefType == SqlTableRefType.Array)
                    {
                        SqlSchema sqlSchema = loader.LoadSqlSchema(((Array)data[i]).GetValue(0).GetType());
                        if (sqlSchema == null) continue;
                        string _id = IdGenerator.GenerateId(database.GetRowCount(sqlSchema.SqlTable.TableName));
                        foreach (object dataTableRef in (Array)data[i])
                        {
                            data[i] = _id;
                            sqlSchema.Save(_id, database, sqlSchema.GetDataArray(dataTableRef));
                        }
                    }
                    else if (data[i] != null && !data[i].GetType().IsArray && ((SqlTableRef)Fields[i]).SqlTableRefType == SqlTableRefType.Field)
                    {
                        SqlSchema sqlSchema = loader.LoadSqlSchema(data[i].GetType());
                        if (sqlSchema == null) continue;
                        string _id = IdGenerator.GenerateId(database.GetRowCount(sqlSchema.SqlTable.TableName));
                        sqlSchema.Save(_id, database, sqlSchema.GetDataArray(data[i]));
                        data[i] = _id;
                    }
                }
            }

            return data;
        }

        private Dictionary<string, object> GetInsertData(SqlLiteDatabase database, object[] data) {
            Dictionary<string, object> insertData = new Dictionary<string, object>();

            for (int i = 0; i < Fields.Length; i++)
            {
                if (Fields[i].Name == "_id")
                    Console.WriteLine(data[i]);
                insertData.Add(string.Format("@{0}", Fields[i].Name), data[i]);
            }

            if (!insertData.ContainsKey("@_id"))
                insertData.Add("@_id", IdGenerator.GenerateId(database.GetRowCount(SqlTable.TableName)));
            else if (insertData["@_id"] == null)
                insertData["@_id"] = IdGenerator.GenerateId(database.GetRowCount(SqlTable.TableName));

            return insertData;
        }

        private Dictionary<string, object> GetDeleteData(SqlLiteDatabase database, object[] data)
        {
            Dictionary<string, object> deleteData = new Dictionary<string, object>();

            for (int i = 0; i < Fields.Length; i++)
            {
                if (Fields[i].Name == "_id" && data[i] != null) continue;
                deleteData.Add(string.Format("@{0}", Fields[i].Name), data[i]);
            }

            return deleteData;
        }

        public T FindOne<T>(SqlLiteDatabase database, string _id) where T : class
        {
            T instance = Activator.CreateInstance<T>();
            Dictionary<string,object>[] values = database.ExecuteQueryD(string.Format("SELECT * FROM {0} WHERE _id=@_id", SqlTable.TableName),
                new Dictionary<string, object>() { { "@_id", _id } });

            foreach (Dictionary<string, object> value in values)
            {
                SetDataArray(database, instance, value);
                break;
            }

            return instance;
        }

        public object FindOne(SqlLiteDatabase database,Type type, string _id)
        {
            object instance = Activator.CreateInstance(type);
            Dictionary<string, object>[] values = database.ExecuteQueryD(string.Format("SELECT * FROM {0} WHERE _id=@_id", SqlTable.TableName),
                new Dictionary<string, object>() { { "@_id", _id } });

            foreach (Dictionary<string, object> value in values)
            {
                SetDataArray(database, instance, value);
                break;
            }

            return instance;
        }

        public T[] Find<T>(SqlLiteDatabase database, string _id) where T : class
        {   
            List<T> data = new List<T>();
            Dictionary<string, object>[] values = database.ExecuteQueryD(string.Format("SELECT * FROM {0} WHERE _id=@_id", SqlTable.TableName),
                new Dictionary<string, object>() { { "@_id", _id } });

            foreach (Dictionary<string, object> value in values)
            {
                T instance = Activator.CreateInstance<T>();
                SetDataArray(database, instance, value);
                data.Add(instance);
            }

            return data.ToArray();
        }

        public T[] Find<T>(SqlLiteDatabase database) where T : class
        {
            List<T> data = new List<T>();
            Dictionary<string, object>[] values = database.ExecuteQueryD(string.Format("SELECT * FROM {0}", SqlTable.TableName));

            foreach (Dictionary<string, object> value in values)
            {
                T instance = Activator.CreateInstance<T>();
                SetDataArray(database, instance, value);
                data.Add(instance);
            }

            return data.ToArray();
        }

        public object[] Find(SqlLiteDatabase database,Type type, string _id)
        {
            List<object> data = new List<object>();
            Dictionary<string, object>[] values = database.ExecuteQueryD(string.Format("SELECT * FROM {0} WHERE _id=@_id", SqlTable.TableName),
                new Dictionary<string, object>() { { "@_id", _id } });

            foreach (Dictionary<string, object> value in values)
            {
                object instance = Activator.CreateInstance(type);
                SetDataArray(database, instance, value);
                data.Add(instance);
            }

            return data.ToArray();
        }

        private Array FindArray(SqlLiteDatabase database, Type type, string _id)
        {
            Dictionary<string, object>[] values = database.ExecuteQueryD(string.Format("SELECT * FROM {0} WHERE _id=@_id", SqlTable.TableName),
                new Dictionary<string, object>() { { "@_id", _id } });

            Array data = Array.CreateInstance(type, values.Length);

            int pointer = 0;
            foreach (Dictionary<string, object> value in values)
            {
                object instance = Activator.CreateInstance(type);
                SetDataArray(database, instance, value);
                data.SetValue(instance, pointer++);
            }

            return data;
        }

        private void SetDataArray(SqlLiteDatabase database,object instance, Dictionary<string, object> data)
        {
            Type type = instance.GetType();

            for (int i = 0; i < Fields.Length; i++)
            {
                SqlField sqlField = Fields[i];
                object _menber = FieldInfos[sqlField];

                if (sqlField is SqlTableRef)
                {
                    SqlTableRef sqlTableRef = (SqlTableRef)sqlField;
                    if (sqlTableRef.SqlTableRefType == SqlTableRefType.Array)
                    {
                        if (_menber is FieldInfo && ((FieldInfo)_menber).FieldType.IsArray)
                        {
                            var field = (FieldInfo)_menber;

                        }
                        else if (_menber is PropertyInfo && ((PropertyInfo)_menber).PropertyType.IsArray)
                        {
                            var property = (PropertyInfo)_menber;
                            string fullName = property.PropertyType.FullName.Substring(0, property.PropertyType.FullName.Length - 2);
                            Type refType = Type.GetType(string.Format("{0},{1}", fullName, property.PropertyType.Assembly.GetName().Name));

                            SqlSchema sqlSchema = (new SqlLoader()).LoadSqlSchema(refType);
                            if (sqlSchema == null) throw new NotImplementedException();

                            if (sqlTableRef != null && data[sqlTableRef.Name] is string)
                            {
                                string href = (string)data[sqlTableRef.Name];
                                Array array = sqlSchema.FindArray(database, refType, href);
                                property.SetValue(instance, array);
                            }
                        }

                    }
                }
                else
                {
                    if (_menber is FieldInfo)
                    {
                        var field = (FieldInfo)_menber;
                        object value = Convert.ChangeType(data[sqlField.Name], field.FieldType);
                        field.SetValue(instance, value);
                    }
                    else if (_menber is PropertyInfo)
                    {
                        var property = (PropertyInfo)_menber;
                        object value = Convert.ChangeType(data[sqlField.Name], property.PropertyType);
                        property.SetValue(instance, value);
                    }
                }
            }
        }
    }
}
