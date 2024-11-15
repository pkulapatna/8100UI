using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8100UI.Services
{
    public class ClsConveyor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Maxlength { get; set; }
        public int Count { get; set; }
        public int FirstbaleID { get; set; }
        public int LastBaleID { get; set; }
        public int ProcessingLine { get; set; }
        public string Type { get; set; }
        

        public ClsConveyor(int id, string name, int maxlength, int count, int firstbaleID, int lastBaleID, int processingLine, string type)
        {
            this.ID = id;
            this.Name = name;
            this.Maxlength = maxlength;
            this.Count = count;
            this.FirstbaleID = firstbaleID;
            this.LastBaleID = lastBaleID;
            this.ProcessingLine = processingLine;
            this.Type = type;
           
        }
    }
}
