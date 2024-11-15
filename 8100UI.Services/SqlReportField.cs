using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8100UI.Services
{
    public class SqlReportField
    {
        private string _dbField;
        public string DbField
        {
            get { return _dbField; }
            set { _dbField = value; }
        }

        private string _fieldExp;
        public string FieldExp
        {
            get { return _fieldExp; }
            set { _fieldExp = value; }
        }


        private string _fieldName;
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        public string _format;
        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }


        public SqlReportField(string item1, string item2, string item3, string item4)
        {
            this.DbField = item1;
            this.FieldExp = item2;
            this.FieldName = item3;
            this.Format = item4;
        }
    }
}
