﻿using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    public class UpdateHasRegisteredTimes : IActionObject
    {
        private readonly DBConnection _conn;
        private readonly string _dni;
        private readonly long _finish;
        private readonly long _id;
        private readonly long _initial;

        public UpdateHasRegisteredTimes(ref DBConnection conn, string dni, long id, long initial, long finish) {
            _conn = conn;
            _dni = dni;
            _id = id;
            _initial = initial;
            _finish = finish;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_FINISH_COMPETITION, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _id);
                    command.ExecuteNonQuery();
                }

                using (var command = new SQLiteCommand(Resources.SQL_INSERT_TIMES, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _id);
                    command.Parameters.AddWithValue("@INITIAL", _initial);
                    command.Parameters.AddWithValue("@FINISH", _finish);
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException e) {
                if (SQLiteErrorCode.Constraint_PrimaryKey.Equals(e.ErrorCode) ||
                    SQLiteErrorCode.Constraint_Unique.Equals(e.ErrorCode) || e.ErrorCode == 19) {
                    using (var command = new SQLiteCommand(Resources.SQL_UPDATE_TIMES, _conn.DbConnection)) {
                        command.Parameters.AddWithValue("@DNI", _dni);
                        command.Parameters.AddWithValue("@COMPETITION_ID", _id);
                        command.Parameters.AddWithValue("@INITIAL", _initial);
                        command.Parameters.AddWithValue("@FINISH", _finish);
                        command.ExecuteNonQuery();
                    }
                }
                else {
                    _conn.DbConnection?.Close();
                    throw;
                }
            }
        }
    }
}
