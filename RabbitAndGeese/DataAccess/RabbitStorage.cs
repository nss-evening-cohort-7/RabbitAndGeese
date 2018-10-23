using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using RabbitAndGeese.Models;

namespace RabbitAndGeese.DataAccess
{
    public class RabbitStorage
    {
        static List<Rabbit> _hutch = new List<Rabbit>();
        private const string ConnectionString = "Server=(local);Database=RabbitAndGeese;Trusted_Connection=True;";

        public bool Add(Rabbit rabbit)
        {
            //rabbit.Id = _hutch.Any() ? _hutch.Max(r => r.Id) + 1 : 1;
            //_hutch.Add(rabbit);

            using (var db = new SqlConnection(ConnectionString))
            {
                db.Open();

                var command = db.CreateCommand();
                command.CommandText = @"INSERT INTO [dbo].[Rabbit]([Name],[Color],[MaxFeetPerSecond],[Size],[Sex])
                                        VALUES (@Name,@Color,@MaxFeet,@Size,@Sex)";

                command.Parameters.AddWithValue("@name", rabbit.Name);
                command.Parameters.AddWithValue("@Color", rabbit.Color);
                command.Parameters.AddWithValue("@MaxFeet", rabbit.MaxFeetPerSecond);
                command.Parameters.AddWithValue("@Size", rabbit.Size);
                command.Parameters.AddWithValue("@Sex", rabbit.Sex);

                var result = command.ExecuteNonQuery();

                return result == 1;
            }

        }

        public Rabbit GetById(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"select *
                                    from Rabbit
                                    where Id = @id";
                
                //var idParameter = new SqlParameter("@id",SqlDbType.Int);
                //idParameter.Value = id;
                //command.Parameters.Add(idParameter);

                command.Parameters.AddWithValue("@id", id);

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var rabbit = new Rabbit
                    {
                        Id = (int) reader["Id"],
                        Name = reader["Name"].ToString(),
                        MaxFeetPerSecond = (int) reader["MaxFeetPerSecond"],
                        Size = Enum.Parse<Size>(reader["Size"].ToString())
                    };

                    return rabbit;
                }

                return null;
            }
        }
        public bool DeleteById(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                db.Open();

                var command = db.CreateCommand();
                command.CommandText = @"Delete From Rabbit Where id = @id";
                command.Parameters.AddWithValue("@id", id);

                var result = command.ExecuteNonQuery();

                return result == 1;
            }
        }
    }
}
