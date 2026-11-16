using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{

    public class TaskData
    {
        public int TaskID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimeReq { get; set; }
        public int Progress { get; set; }
        public int RewardID { get; set; }
        public int RewardAmt { get; set; }
    }

    public class AkashicDAL
	{
		private static string connectionString;

        public List<TaskData> GetRecord()
		{
            var records = new List<TaskData>();


            return records;
        }

		public void InsertRecord(TaskData task)
        {

        }

        public void UpdateRecord(TaskData task, int newProgress)
        {

        }
    }
}
