using _8100UI.Services;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace ProductSelection.ViewModels
{
    public class ProductViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private Sqlhandler _sqlHandler;

        private bool _userLogin = false;
        public bool UserLogin
        {
            get => _userLogin;
            set
            {
                SetProperty(ref _userLogin, value);
                ComboEnable = value;
            }
        }

        private bool _comboEnable;
        public bool ComboEnable
        {
            get => _comboEnable;
            set => SetProperty(ref _comboEnable, value);
        }


        private DataTable _prodListtable;
        public DataTable ProdListtable
        {
            get => _prodListtable;
            set => SetProperty(ref _prodListtable, value);
        }

        private DataTable _gradeListTable;
        public DataTable GradeListTable
        {
            get => _gradeListTable;
            set => SetProperty(ref _gradeListTable, value);
        }


        private int _productType;  //stock =0, next stock =  1, Grade =2, next Grade =3
        public int ProductType
        {
            get => _productType;
            set
            {
                SetProperty(ref _productType, value);
                SetProductType(value);
            }
        }

        private List<string> _comboList;
        public List<string> ComboList
        {
            get => _comboList;
            set => SetProperty(ref _comboList, value);
        }

        private string _labelName;
        public string LabelName
        {
            get => _labelName; 
            set => SetProperty(ref _labelName, value); 
        }

        public string SourceID { get; set; } = string.Empty;

        private string _selectProdVal;
        public string SelectProdVal
        {
            get { return _selectProdVal; }
            set
            {
                SetProperty(ref _selectProdVal, value);
                if (value != null)
                {
                    switch (ProductType)
                    {
                        case 0: // current Stock do nothing
                            if (ProdListtable != null)
                            {
                                for (int i = 0; i < ProdListtable.Rows.Count; i++)
                                {
                                    if (ProdListtable.Rows[i]["Name"].ToString() == value)
                                    {
                                        SourceID = "1"; 
                                        ClsPipeMessage.SendPipeMessage($"CP1;{value};{SourceID};");
                                        ClsSerilog.LogMessage(ClsSerilog.Info, $"Select Product {value}");
                                    }
                                }
                            }
                            break;

                        case 1: 
                            //In Next Stock, New Stock and Source ID send To Server!
                            
                            if(ProdListtable != null)
                            {
                                for (int i = 0; i < ProdListtable.Rows.Count; i++)
                                {
                                    if (ProdListtable.Rows[i]["Name"].ToString() == value)
                                    {
                                        SourceID = "1"; // ProdListtable.Rows[i]["SourceID"].ToString();
                                        ClsPipeMessage.SendPipeMessage($"NP1;{value};{SourceID};");
                                        ClsSerilog.LogMessage(ClsSerilog.Info, $"Select Product {value}");
                                    }
                                }
                            }
                            break;

                        case 2: // current Grade do nothing
                            if(GradeListTable != null)
                            {
                                for (int i = 0; i < GradeListTable.Rows.Count; i++)
                                {
                                    if (GradeListTable.Rows[i]["Name"].ToString() == value)
                                    {
                                        ClsPipeMessage.SendPipeMessage($"CG1;{value};");
                                        ClsSerilog.LogMessage(ClsSerilog.Info, $"Select Grade {value}");
                                    }
                                }
                            }
                            break;

                        case 3:
                            // Next Grade,  Grade and Line send to Server!
                            if (GradeListTable != null)
                            {
                                for (int i = 0; i < GradeListTable.Rows.Count; i++)
                                {
                                    if (GradeListTable.Rows[i]["Name"].ToString() == value)
                                    {
                                        ClsPipeMessage.SendPipeMessage($"NG1;{value};");
                                        ClsSerilog.LogMessage(ClsSerilog.Info, $"Select Grade {value}");
                                    }
                                }
                            }
                            break;
                    }       
                }
            }
        }

        public ProductViewModel(IEventAggregator eventAggregator, int productType)
        {
            this._eventAggregator = eventAggregator;
            this.ProductType = productType;

            UserLogin = ClassCommon.UserLogin;
            _eventAggregator.GetEvent<UserLogin>().Subscribe(UserLogonEvent);
        }

        private void UserLogonEvent(bool obj)
        {
            UserLogin = obj;
        }

        private void ProcessNamePipeMsg(string message)
        {
           
            message = message.TrimEnd(';');
            string[] words = message.Split(';');

            if (words[0] == "Logon")
                UserLogin = true;
            else if (words[0] == "Logoff")
                UserLogin = false;
        }

        private void SetProductType(int value)
        {
            ComboList = new List<string>();

            _sqlHandler = Sqlhandler.Instance;
           
            GetProductName();
            ProdListtable = GetListOfProduct();
            GradeListTable = GetListOfGrade();

            switch (value)
            {
                case 0: //Stock
                    LabelName = "Product";
                    
                    for (int i = 0; i < ProdListtable.Rows.Count; i++)
                    {
                        ComboList.Add(ProdListtable.Rows[i]["Name"].ToString());
                    }
                    break;

                case 1: //Next Stock
                    LabelName = "Next Prod.";
                    for (int i = 0; i < ProdListtable.Rows.Count; i++)
                    {
                        ComboList.Add(ProdListtable.Rows[i]["Name"].ToString());
                    }
                    break;

                case 2: // Grade
                    LabelName = "Grade";

                    for (int i = 0; i < GradeListTable.Rows.Count; i++)
                    {
                        ComboList.Add(GradeListTable.Rows[i]["Name"].ToString());
                    }
                    break;

                case 3: // Next Grade
                    LabelName = "Next Grade";
                    for (int i = 0; i < GradeListTable.Rows.Count; i++)
                    {
                        ComboList.Add(GradeListTable.Rows[i]["Name"].ToString());
                    }
                    break;

                case 10: //Stock
                    LabelName = "Product";

                    for (int i = 0; i < ProdListtable.Rows.Count; i++)
                    {
                        ComboList.Add(ProdListtable.Rows[i]["Name"].ToString());
                    }
                    break;

                case 11: //Next Stock
                    LabelName = "Next Prod.";
                    for (int i = 0; i < ProdListtable.Rows.Count; i++)
                    {
                        ComboList.Add(ProdListtable.Rows[i]["Name"].ToString());
                    }
                    break;

                case 12: // Grade
                    LabelName = "Grade";

                    for (int i = 0; i < GradeListTable.Rows.Count; i++)
                    {
                        ComboList.Add(GradeListTable.Rows[i]["Name"].ToString());
                    }
                    break;

                case 13: // Next Grade
                    LabelName = "Next Grade";
                    for (int i = 0; i < GradeListTable.Rows.Count; i++)
                    {
                        ComboList.Add(GradeListTable.Rows[i]["Name"].ToString());
                    }
                    break;

            }
        }

        private DataTable GetListOfGrade()
        {
            if (_sqlHandler == null) _sqlHandler = Sqlhandler.Instance;
            return _sqlHandler.GetStockSourcesTable(Sqlhandler.FS_GRADETABLE);
        }

        private DataTable GetListOfProduct()
        {
            if(_sqlHandler ==null) _sqlHandler = Sqlhandler.Instance;
            return _sqlHandler.GetStockSourcesTable(Sqlhandler.FS_STOCKTABLE);
        }


        /// <summary>
        /// From Sources table for stock and lines for Grade
        /// </summary>
        private void  GetProductName()
        {
            try
            {
               if (ProductType ==0)
                    SelectProdVal = _sqlHandler.GetProductName("Stock");  // Stock Combobox
                else if (ProductType == 2)
                    SelectProdVal = _sqlHandler.GetProductName("Grade"); // Grade combobox 
            }
            catch (Exception ex )
            {
                ClsSerilog.LogMessage(ClsSerilog.Error, $"Error in GetProductName -> { ex.Message}");
                MessageBox.Show($"Error in GetProductName -> { ex.Message}");
            }
        }
    }
}
