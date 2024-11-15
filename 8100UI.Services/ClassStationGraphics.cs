using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8100UI.Services
{
    public class ClassStationGraphics
    {
        public int StationId { get; set; } //UniqueID
        public string StationName { get; set; } //Name
        public bool BtnOneVisible { get; set; } //Btn1Visible
        public string BtnOneCaption { get; set; } //CapEnglish1
        public bool BtnTwoVisible { get; set; } //Btn2Visible
        public string BtnTwoCaption { get; set; } //CapEnglish2
        public bool BtnThreeVisible { get; set; } //Btn3Visible
        public string BtnThreeCaption { get; set; } //CapEnglish3


        public ClassStationGraphics(int stationId, string stationName, bool btnOneVis,
            string btnOneName, bool btnTwoVis, string btnTwoName, bool btnThreeVis, string btnThreeName)
        {
            this.StationId = stationId;
            this.StationName = stationName;
            this.BtnOneVisible = btnOneVis;
            this.BtnOneCaption = btnOneName;
            this.BtnTwoVisible = btnTwoVis;
            this.BtnTwoCaption = btnTwoName;
            this.BtnThreeVisible = btnThreeVis;
            this.BtnThreeCaption = btnThreeName;
        }
    }
}
