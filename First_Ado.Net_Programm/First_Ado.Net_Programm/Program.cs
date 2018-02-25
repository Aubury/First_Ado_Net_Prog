using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace First_Ado.Net_Programm
{
    class Program
    {
        static void Main(string[] args)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["DefaltConnection"].ConnectionString;
            Console.WriteLine(connectionString);

            SqlConnection connection = new SqlConnection(connectionString);

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Строка подключения : {0}", connection.ConnectionString);
                Console.WriteLine("База данных : {0}", connection.Database);
                Console.WriteLine("Сервер : {0}", connection.DataSource);
                Console.WriteLine("Версия сервера : {0}", connection.ServerVersion);
                Console.WriteLine("Состояние : {0}", connection.State);
                Console.WriteLine("Workstationld : {0}", connection.WorkstationId);
            }
            Console.WriteLine("=====================================================");
            ========================================================================================================

            SqlDataReader reader = null;
            string sqlExpression = @"Select [EmployeeID],[LastName],[FirstName],[BirthDate],
                                      [City] from [dbo].[Employees]";
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                reader = command.ExecuteReader();
                int line = 0;

                while (reader.Read())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetName(i).ToString() + "\t || ");
                        }
                    }
                    Console.WriteLine();
                    line++;
                    Console.WriteLine("  " + reader[0] + " \t\t || " + reader[1] + " \t || " + reader[2] + "\t || " + reader[3] + "\t || " + reader[4]);

                }
                reader.Close();
            }
            ========================================================================================
            Console.WriteLine("=====================================================");

            string sqlExpression_2 = @"Insert into [dbo].[Territories] ([TerritoryID],[TerritoryDescription],[RegionID])
                                      values('12356','Dnepr',2)";

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression_2, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Insert {0} object into table Territories ", number);
            }

            Console.WriteLine("=====================================================");

            string sqlExpression_3 = @"Update [dbo].[Products] set [UnitPrice]=21.25 where [ProductID]=1";

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression_3, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Update {0} object into table Products ", number);
            }

            Console.WriteLine("=====================================================");

            string Terr_Id = "23456";
            string Terr_Desc = "Dnepr2";
            int regionId = 3;

            string sqlExpression_4 = $"Insert into [dbo].[Territories] ([TerritoryID],[TerritoryDescription],[RegionID])"
                                   + $"values(@Terr_Id,@Terr_Desc,@regionID)";
            using (connection = new SqlConnection(connectionString))
            {
                try
                {

                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlExpression_4, connection))
                    {

                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@Terr_Id";
                        param.SqlDbType = System.Data.SqlDbType.NVarChar;
                        param.Size = 20;
                        param.Value = Terr_Id;
                        command.Parameters.Add(param);


                        param = new SqlParameter
                        {
                            ParameterName = "@Terr_Desc",
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Size = 50,
                            Value = Terr_Desc
                        };
                        command.Parameters.Add(param);


                        param = new SqlParameter
                        {
                            ParameterName = "@regionId",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Value = regionId
                        };
                        command.Parameters.Add(param);

                        int number = command.ExecuteNonQuery();
                        Console.WriteLine("Insert {0} object into table Territories ", number);

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

            Console.WriteLine("=====================================================");

            string sqlExpression_4 = @"Delete from [dbo].[Territories] where [TerritoryDescription]= 'Dnepr2'";

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression_4, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Delete {0} object from table Territories ", number);
            }


            Console.Read();
        }
    }
}
