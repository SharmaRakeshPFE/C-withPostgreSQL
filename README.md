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
