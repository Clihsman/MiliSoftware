/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 10/05/2023          *
 * Assembly : MiliModels               *
 * *************************************/

namespace MiliSoftware
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string email,string password)
        {
            Email = email;
            Password = password;
        }
    }
}
