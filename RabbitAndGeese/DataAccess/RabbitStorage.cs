using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using RabbitAndGeese.Models;

namespace RabbitAndGeese.DataAccess
{
    public class RabbitStorage
    {
        static List<Rabbit> _hutch = new List<Rabbit>();
        private readonly string ConnectionString;

        public  RabbitStorage(IConfiguration config)
        {
            ConnectionString = config.GetSection("ConnectionString").Value;
        }

        public bool Add(Rabbit rabbit)
        {
            //rabbit.Id = _hutch.Any() ? _hutch.Max(r => r.Id) + 1 : 1;
            //_hutch.Add(rabbit);

            using (var db = new SqlConnection(ConnectionString))
            {
                db.Open();

                var result = db.Execute(@"INSERT INTO [dbo].[Rabbit]([Name],[Color],[MaxFeetPerSecond],[Size],[Sex])
                                        VALUES (@Name,@Color,@MaxFeetPerSecond,@Size,@Sex)", rabbit);

                return result == 1;
            }

        }

        public List<Rabbit> GetAllRabbits()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var result = connection.Query<Rabbit>("select * from Rabbit");

                return result.ToList();
            }
        }

        public Rabbit GetById(int rabbitId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var result = connection.QueryFirst<Rabbit>(@"select *
                                    from Rabbit
                                    where Id = @id", new { id = rabbitId});

                var result2 = connection.Query<Rabbit>("select * from Rabbit");

                return result;
            }
        }

        public bool DeleteById(int rabbitId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                db.Open();

                var result = db.Execute("Delete From Rabbit Where id = @id", new { id = rabbitId });

                return result == 1;
            }
        }
    }
}
