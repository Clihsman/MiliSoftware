/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 18/05/2023          *
 * Assembly : MiliDataExport           *
 * *************************************/

namespace MiliSoftware.Data
{
    /// <summary>
    /// Clase abtracta que contiene todas las funciones nesesarias para exportar
    /// documentos
    /// </summary>
    public abstract class MiliDE
    {
        /// <summary>
        /// Inicia los recursos requeridos para exportar
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Exporta los datos correspondientes
        /// </summary>
        public abstract void Export();

        /// <summary>
        /// Aborta la exportación
        /// </summary>
        public abstract void Abort();

        /// <summary>
        /// Cierra y libera los recursos usados
        /// </summary>
        public abstract void Close();
    }
}
