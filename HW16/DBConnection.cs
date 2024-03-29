﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW16
{
    public static class DBConnection
    {
        private static readonly string _host = "localhost";
        private static readonly string _user = "postgres";
        private static readonly string _dbName = "Shop";
        private static readonly string _port = "5432";
        public static string ConnectionString()
        {
            string password = "*****";
            string connString = string.Format
            (
              "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
              _host,
              _user,
              _dbName,
              _port,
              password);

            return connString;
        }
    }
}
