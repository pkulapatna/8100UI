using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8100UI.Services
{
    public class SqlLotSumFields
    {
        private string _FieldName;
        public string FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }

        private string _fieldExp;
        public string FieldExp
        {
            get { return _fieldExp; }
            set { _fieldExp = value; }
        }
        public string _format;
        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }

        public string _fieldType;
        public string FieldType
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        public SqlLotSumFields(string item1, string item2, string item3, string item4)
        {
            this.FieldName = item1;
            this.FieldExp = item2;
            this.Format = item3;
            this.FieldType = item4;
        }
    }
}
