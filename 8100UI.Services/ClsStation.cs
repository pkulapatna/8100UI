using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8100UI.Services
{
    public class ClsStation
    {
        public int StationId { get; set; }
        public string StationName { get; set; }
        public int IntConveyor { get; set; }
        public int Line { get; set; }
        public int OutPutCnv { get; set; }
        public int InputCnv { get; set; }


        public ClsStation(int statId, string staName, int intconv,int line, int outPutCnv, int inputCnv)
        {
            this.StationId = statId;
            this.StationName = staName;
            this.IntConveyor = intconv;
            this.Line = line;
            this.OutPutCnv = outPutCnv;
            this.InputCnv = inputCnv;
        }

    }
}
