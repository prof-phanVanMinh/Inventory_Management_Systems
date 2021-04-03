using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.SqlServer;

namespace Domain
{
    public class UserModel
    {
        UserDao dao = new UserDao();
        public bool LoginUser(string user, string password)
        {
            return dao.Login(user, password);
        }
        public string recoverPasswword(string userRequesting)
        {
            return dao.recoverPassword(userRequesting);     
        }
    }
}
