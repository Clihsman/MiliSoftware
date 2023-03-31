/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 12/12/2022          *
 * Assembly : MiliController           *
 * *************************************/

using System;

namespace MiliSoftware
{
    /// <summary>
    /// Clase para manejo de excepciones del Controlador MiliSoftware
    /// </summary>
    public sealed class MiliConException : Exception
    {
        /// <summary>
        /// Codigo de Error
        /// </summary>
        public int ErrorCode { get; private set; }
        /// <summary>
        /// Estado del Error
        /// </summary>
        public int Status { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorCode">Codigo del Error</param>
        /// <param name="status">Estado del Error</param>
        /// <param name="message">Mensaje del Error</param>
        public MiliConException(int errorCode, int status, string message) : base(message)
        {
            ErrorCode = errorCode;
            Status = status;
        }
    }
}
