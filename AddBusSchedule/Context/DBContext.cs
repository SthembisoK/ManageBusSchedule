﻿using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace AddBusSchedule.Context
{
    public class DBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");

        }
        public IDbConnection CreateConnection()=>new SqlConnection(_connectionString);
    }
}
