#################################################################################
Monitoring Long Running Queries in Azure PostgreSQL using C# Console Application
#################################################################################
Step:- 1  Add refrence to using Npgsql
https://www.nuget.org/packages/Npgsql

Method used -
1>NpgsqlConnection
2>NpgsqlCommand

T-SQL --> SELECT   pid, now() - pg_stat_activity.query_start AS duration,   query, state ROM pg_stat_activity WHERE (now() - pg_stat_activity.query_start) > interval '5 minutes';

Note:-Change the query accordiblgy

###############################################################################//Code//#########################################################################################
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
