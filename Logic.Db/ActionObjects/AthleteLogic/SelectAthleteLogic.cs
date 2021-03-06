﻿using System.Collections.Generic;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic
{
    public class SelectAthleteLogic : IActionObject
    {
        private readonly DBConnection _conn;
        public readonly List<AthleteDto> List;

        public SelectAthleteLogic(ref DBConnection conn) {
            _conn = conn;
            List = new List<AthleteDto>();
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_ATHLETE, _conn.DbConnection)) {
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var athlete = new AthleteDto {
                                Dni = reader.GetString(0),
                                Name = reader.GetString(1),
                                Surname = reader.GetString(2),
                                BirthDate = reader.GetDateTime(3),
                                Gender = reader.GetString(4).ToCharArray()[0]
                            };
                            List.Add(athlete);
                        }
                    }
                }
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
