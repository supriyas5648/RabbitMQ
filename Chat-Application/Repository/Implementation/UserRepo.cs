using Npgsql;

namespace MVC;

public class UserRepo : IUserInterface
{
   private readonly NpgsqlConnection _conn;

   public UserRepo(NpgsqlConnection _conn)
    {
        this._conn = _conn;
    }
   public UserModel login(string email , string pass)
    {
        UserModel user = new();
       try
        {
           string query = @"SELECT * FROM t_User WHERE c_useremail = @email AND c_userpassword = @pass; ";

           var cmd = new NpgsqlCommand(query , _conn);
           cmd.Parameters.AddWithValue("@email" , email);
           cmd.Parameters.AddWithValue("@pass" , pass);

          _conn.OpenAsync();

           using (var reader = cmd.ExecuteReader())
            {
                user = new UserModel
                {
                   UserId  = Convert.ToInt32(reader["c_userid"]),
                   UserEmail = reader["c_useremail"].ToString(),
                   UserMobile = reader["c_usermobile"].ToString(),
                   UserPassword = reader["c_userpassword"].ToString(),
                   Dob = reader["c_userdob"].ToString()
                };
            }
            
        }catch(Exception e)
        {
            Console.WriteLine("login err :" +  e.Message);
        }finally
        {
         _conn.CloseAsync();
        }

        return user;
    } 
}