using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace GUIProjekt
{
    class PROFESSORS
    {
        CONNECTION conection = new CONNECTION();

        public bool insertProfessors(String fname, String lname, String degree, String phone, String email)
        {

            MySqlCommand command = new MySqlCommand();
            String insertQuery = "INSERT INTO `professors`(`first_name`, `last_name`, `degree`, `phone`, `email`) VALUES (@fname,@lname,@degree,@phone,@email)";
            command.CommandText = insertQuery;
            command.Connection = conection.GetConnection();

            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@degree", MySqlDbType.VarChar).Value = degree;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;


            conection.openConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                conection.closeConnection();
                return true;
            }
            else
            {
                conection.closeConnection();
                return false;
            }


            
        }

    }
}
