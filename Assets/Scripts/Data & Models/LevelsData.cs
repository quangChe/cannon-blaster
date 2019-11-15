using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database
{
    public class LevelsData : SqliteHelper
    {
        private const string TABLE_NAME = "Levels";
        private const string KEY_LEVEL = "level_number";
        private const string KEY_STARS = "stars_collected";
        private const string KEY_SUCCESS = "success_count";
        private const string KEY_TOTAL = "total_count";
        private const string KEY_DATE = "date";

        private string[] COLUMNS = {KEY_LEVEL, KEY_STARS, KEY_SUCCESS, KEY_TOTAL};

        public LevelsData() : base()
        {
            IDbCommand cmd = getDbCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME +
                "( id INTEGER PRIMARY KEY," +
                KEY_LEVEL + " INTEGER, " +
                KEY_STARS + " INTEGER, " +
                KEY_SUCCESS + " INTEGER, " +
                KEY_TOTAL + " INTEGER, " +
                KEY_DATE + " DATETIME DEFAULT CURRENT_TIMESTAMP );";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "CREATE UNIQUE INDEX IF NOT EXISTS idx_level_number ON " +
                TABLE_NAME + " (" + KEY_LEVEL + ");";
            cmd.ExecuteNonQuery();
        }

        public void addData(LevelsModel level)
        {
            IDbCommand cmd = getDbCommand();
            cmd.CommandText = "REPLACE INTO " + TABLE_NAME + " ( " +
                KEY_LEVEL + ", " +
                KEY_STARS + ", " +
                KEY_SUCCESS + ", " +
                KEY_TOTAL + " ) " +
                "VALUES ( " +
                level._levelNumber + ", " +
                level._starsCollected + ", " +
                level._successCount + ", " +
                level._totalCount + " );";
            cmd.ExecuteNonQuery();
        }

        public override IDataReader getAllData()
        {
            return base.getAllData(TABLE_NAME);
        }


    }
}

