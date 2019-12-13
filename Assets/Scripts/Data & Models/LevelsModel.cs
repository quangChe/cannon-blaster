using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database
{
    public class LevelsModel
    {
        public int _levelNumber; // Also the level number
        public int _starsCollected;
        public int _successCount;
        public int _totalCount;
        public string _date = "";

        public LevelsModel(int levelNumber, int starsCollected, int successCount, int totalCount)
        {
            _levelNumber = levelNumber;
            _starsCollected = starsCollected;
            _successCount = successCount;
            _totalCount = totalCount;
        }

        public LevelsModel(int lvl, int stars, int success, int total, string date)
            : this(lvl, stars, success, total)
        {
            _date = date;
        }
    }
}

