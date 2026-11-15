using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class AkashicBLL
    {
        public List<TaskData> GetRec(string userId)
        {
            AkashicDAL obj = new AkashicDAL();
            var records = new List<TaskData>();

            return records = obj.GetRecord();
        }

        public bool InsertRecord(string userId, int TaskID, string Name, string Description, int TimeReq, int Progress, int RewardID)
        {
            bool check = false;

            return check;
        }

        public bool UpdateRecord(string userId, string recordData)
        {
            bool check = false;

            return check;
        }
    }
}