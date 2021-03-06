﻿using System.Collections.Generic;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Json;

namespace Logic.Db.ActionObjects.CategoriesLogic
{
    internal class SelectCategoriesPredefinied : IActionObject
    {
        private readonly DBConnection _conn;
        public IEnumerable<AbsoluteCategory> List;

        public SelectCategoriesPredefinied(ref DBConnection conn) {
            _conn = conn;
        }

        public void Execute() {
            try {
                var defaultDeserialize =
                    new JsonDeserialize<AbsoluteCategory>(JsonDeserialize<AbsoluteCategory>.DefaultCategoriesFilename);
                List = defaultDeserialize.ListJson();
                //using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ALL_CATEGORIES, _conn.DbConnection))

                //using (SQLiteDataReader reader = command.ExecuteReader())
                //{

                //    while (reader.Read())
                //    {
                //        CategoryDto cat = new CategoryDto()
                //        {

                //            Name = reader.GetString(0),
                //            //Min_Age = reader.GetInt16(1),
                //            //Max_Age = reader.GetInt16(1)
                //    };

                //        list.Add(cat);
                //    }
                //}
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
