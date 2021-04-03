using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Common.Cache;

namespace DataAccess.SqlServer
{
  public class UserDao:ConnectionToSQL
    {
        public bool Login(string user, string password)
        {
            using(var connection = GetConnection())
            {
                connection.Open();
                using(var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from users where loginname = @user and password = @password";
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@password", password);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            UserLoginCache.UserId = reader.GetInt32(0);
                            UserLoginCache.FirstName = reader.GetString(3);
                            UserLoginCache.LastName = reader.GetString(4);
                            UserLoginCache.Position = reader.GetString(5);
                            UserLoginCache.Email = reader.GetString(6);
                        }
                        return true;
                    }
                        
                    else
                        return false;

                }
            }
        }
        public string recoverPassword(string userRequesting)
        {
            using(var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from users where LoginName =  @user or Email = @email";
                    command.Parameters.AddWithValue("@user", userRequesting);
                    command.Parameters.AddWithValue("@email", userRequesting);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string userName = reader.GetString(3) + ", " + reader.GetString(4);
                        string userEmail = reader.GetString(6);
                        string password = reader.GetString(2);
                        var mailService = new MailServices.SystemSupportMail();
                        mailService.sendMail(
                            subject: "SYSTEM: Password recovery request ",
                            body: "Hi, " + userName + "you request to recover your password.\n" + "your current password is:  " + password +
                            "\n However, we ask that you change your password imediately once you enter the system.",
                            recipientMail: new List<string> { userEmail }
                            );
                        return "Hi, " + userName + " You request to recover your password.\n" +
                               "please check your email:" + userEmail;
                    }
                    else
                        return "Sorry, You do not have account with that mail or username";
                }
            }
        }
    }
}
