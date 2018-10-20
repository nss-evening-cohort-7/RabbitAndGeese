using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using RabbitAndGeese.Models;

namespace RabbitAndGeese.DataAccess
{
    public class RabbitStorage
    {
        static List<Rabbit> _hutch = new List<Rabbit>();

        public void Add(Rabbit rabbit)
        {
            rabbit.Id = _hutch.Any() ? _hutch.Max(r => r.Id) + 1 : 1;
            _hutch.Add(rabbit);
        }

        public Rabbit GetById(int id)
        {
            using (var connection = new SqlConnection("Server=(local);Database=RabbitAndGeese;Trusted_Connection=True;"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $@"select *
                                    from Rabbit
                                    where Id = {id}";

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
    }
}
