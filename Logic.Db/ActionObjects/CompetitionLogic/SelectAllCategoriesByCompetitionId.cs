﻿using Logic.Db.Connection;
using Logic.Db.Dto;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Util.Services;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    public class SelectAllCategoriesByCompetitionId : IActionObject
    {
        private readonly DBConnection _conn;
        private readonly CompetitionDto _competitionDto;
        public IList<AbsoluteCategory> Categories;

        public SelectAllCategoriesByCompetitionId(ref DBConnection conn, CompetitionDto comp)
        {
            _conn = conn;
            _competitionDto = comp;
        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Properties.Resources.SQL_SELECT_CATEGORY_BY_COMPETITION, _conn.DbConnection))
                {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competitionDto.ID);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        Categories = new List<AbsoluteCategory>();
                        while (reader.Read()) {
                            int[] id = {reader.GetInt32(2), reader.GetInt32(3)};
                            IEnumerable<CategoryDto> childCategory = new CompetitionService().SelectCategoryByAbsoluteCategories(id);

                            AbsoluteCategory category = new AbsoluteCategory {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                CategoryM = childCategory.ElementAt(0),
                                CategoryF = childCategory.ElementAt(1),
                            };

                            Categories.Add(category);
                        }
                    }
                }
            }
            catch (SQLiteException)
            {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
