using System;
using Npgsql;

namespace Driver
{
    public class AzurePostgresRead
    {
        // Obtain connection string information from the portal
        //
        private static string Host = "xxxxxx.postgres.database.azure.com";
        private static string User = "xxxxx@infypostgresql";
        private static string DBname = "xxxxx";
        private static string Password = "xxxxx";
        //private static string Port = "5432";
        
        static void Main(string[] args)
        {
            // Build connection string using parameters from portal
            //
            string connString =
                String.Format(
                    "Server={0}; User Id={1}; Database={2}; Password={3};SSLMode=Prefer",
                    Host,
                    User,
                    DBname,
                    //Port,
                    Password);

            using (var conn = new NpgsqlConnection(connString))
            {
                

                Console.Out.WriteLine("Opening connection");
                conn.Open();


                using (var command = new NpgsqlCommand("SELECT usename, datname,  state, query   FROM  pg_stat_activity where usename='rakesh'", conn))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            string.Format(
                                "Reading from table=({0}, {1}, {2}, {3})",
                                reader.GetString(0).ToString(),
                                reader.GetString(1).ToString(),
                                reader.GetString(2).ToString(),
                                reader.GetString(3).ToString()
                             
                                                                )
                            );
                    }
                    reader.Close();
                }
            }

            Console.WriteLine("Press RETURN to exit");
            Console.ReadLine();
        }
    }
}