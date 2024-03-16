using System;

namespace MiliSoftware.SqlLite
{
    /// <summary>
    /// Esta clase permite definir atributos para crear un campo en sqlLiter o leer datos de un campo 
    /// </summary>
    public class SqlField : Attribute
    {
        /// <summary>
        /// Obtiene el tipo del campo actual
        /// </summary>        
        public SqlFieldType FieldType { private set; get; }

        /// <summary>
        /// Obtiene el tamaño del campo actual
        /// </summary>
        public int Length { private set; get; }

        /// <summary>
        /// Obtiene el nombre del campo actual
        /// </summary>
        public string Name { private set; get; }

        /// <summary>
        /// Contructor de la clase SqlField
        /// </summary>
        /// <param name="fieldType"></param>
        /// <param name="length"></param>
        public SqlField(SqlFieldType fieldType, int length = -1)
        {
            FieldType = fieldType;
            Length = length;
            Name = null;
        }

        /// <summary>
        /// Contructor de la clase SqlField
        /// </summary>
        /// <param name="fieldType"></param>
        /// <param name="length"></param>
        public SqlField(SqlFieldType fieldType,string name, int length = -1)
        {
            FieldType = fieldType;
            Length = length;
            Name = name;
        }
    }
}
