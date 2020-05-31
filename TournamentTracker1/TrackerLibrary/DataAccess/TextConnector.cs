using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModel.csv";

        public PrizeModel CreatePrize(PrizeModel model)
        {
            List<PrizeModel> prizes = PrizesFile.fullFilePath().LoadFile().ConvertToPrizeModels();

            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;
            currentId += 1;

            prizes.Add(model);

            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }
    }
}

