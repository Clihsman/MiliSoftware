/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 10/05/2023          *
 * Assembly : MiliModels               *
 * *************************************/

using Newtonsoft.Json.Linq;

namespace MiliSoftware
{
    public class User : IJsonObject
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public void FromJson(string json)
        {
            throw new System.NotImplementedException();
        }

        public string ToJson()
        {
            return new JObject
            {
                { "email", Email },
                { "password", Password }
            }
            .ToString();
        }
    }
}
