using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chess.Web.Models.GameModels
{
    public class HistoryModel
    {
        public string GameType { get; set; }

        public string GameDate { get; set; }

        public string OpponentNickname { get; set; }

        public string Result { get; set; }

        public string ResultType { get; set; }
    }
}
