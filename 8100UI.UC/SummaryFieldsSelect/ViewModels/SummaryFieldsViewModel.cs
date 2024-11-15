using _8100UI.Services;
using DateTimeModify.Views;
using FieldModify.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SummaryFieldsSelect.Model;
using SummaryFieldsSelect.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SummaryFieldsSelect.ViewModels
{
    public class SummaryFieldsViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private int ilottype;

        private Window ModWindow;
        private Window ModDtWindow;

        private string fldtoChange = string.Empty;
        private string fldval = string.Empty;
        
        private SummaryFieldsModel _summaryfieldModel;

        private ObservableCollection<string> _selectHdrItems;
        public ObservableCollection<string> SelectHdrItems
        {
            get { return _selectHdrItems; }
            set { SetProperty(ref _selectHdrItems, value); }
        }

        private ObservableCollection<string> _nameColumnList;
        public ObservableCollection<string> NameColumnList
        {
            get { return _nameColumnList; }
            set { SetProperty(ref _nameColumnList, value); }
        }

        private ObservableCollection<string> _wtMtColumnList;
        public ObservableCollection<string> WtMtColumnList
        {
            get { return _wtMtColumnList; }
            set { SetProperty(ref _wtMtColumnList, value); }
        }

        private ObservableCollection<SqlLotSumFields> _summaryFields;
        public ObservableCollection<SqlLotSumFields> SummaryFields
        {
            get { return _summaryFields; }
            set { SetProperty(ref _summaryFields, value); }
        }

        private string _selectedColName;
        public string SelectedColName
        {
            get { return _selectedColName; }
            set { SetProperty(ref _selectedColName, value); }
        }
        private int _colSelectedIndex;
        public int ColSelectedIndex
        {
            get { return _colSelectedIndex; }
            set { SetProperty(ref _colSelectedIndex, value); }
        }
        private List<Tuple<string, string>> _sqlFieldList;
        public List<Tuple<string, string>> SqlFieldList
        {
            get { return _sqlFieldList; }
            set { SetProperty(ref _sqlFieldList, value); }
        }

        //Name,SQLName,Format
        private List<Tuple<string, string, string, string>> _allFieldList;
        public List<Tuple<string, string, string, string>> AllFieldList
        {
            get { return _allFieldList; }
            set { SetProperty(ref _allFieldList, value); }
        }

        private List<Tuple<string, string, string, string>> _wtMtFieldList;
        public List<Tuple<string, string, string, string>> WtMtFieldList
        {
            get { return _wtMtFieldList; }
            set { SetProperty(ref _wtMtFieldList, value); }
        }

        private bool _selectTabOne;
        public bool SelectTabOne
        {
            get { return _selectTabOne; }
            set { SetProperty(ref _selectTabOne, value); }
        }
        private bool _bModify = false;
        public bool BModify
        {
            get { return _bModify; }
            set { SetProperty(ref _bModify, value); }
        }

        private int _selectHdrIndex = 0;
        public int SelectHdrIndex
        {
            get { return _selectHdrIndex; }
            set { SetProperty(ref _selectHdrIndex, value); }
        }

        private string _outPutColString;
        public string OutPutColString
        {
            get { return _outPutColString; }
            set { SetProperty(ref _outPutColString, value); }
        }

        private object _selectHdrItem;
        public object SelectHdrItem
        {
            get { return _selectHdrItem; }
            set { SetProperty(ref _selectHdrItem, value); }
        }

       
        public SummaryFieldsViewModel(IEventAggregator eventAggregator, int ilottype)
        {
            this._eventAggregator = eventAggregator;
            this.ilottype = ilottype;
            _summaryfieldModel = new SummaryFieldsModel(ilottype);

            _eventAggregator.GetEvent<ChangeFieldValue>().Subscribe(FormatValueChanged);
           
            LoadPage();
        }

      
        private void FormatValueChanged(string obj)
        {
            ObservableCollection<SqlLotSumFields> newlist = SummaryFields;

            switch (fldtoChange)
            {
                case "Format":
                    newlist[SelectHdrIndex].Format = obj;
                    break;
                case "Field Name":
                    newlist[SelectHdrIndex].FieldName = obj;
                    break;
            }

            SummaryFields = newlist;
            _summaryfieldModel.SaveFieldColumns(SummaryFields, ilottype);

            LoadPageExecute();
        }

        private void LoadPage()
        {

            if (NameColumnList != null) NameColumnList = null;
            NameColumnList = new ObservableCollection<string>();
            if (AllFieldList != null) AllFieldList = null;
            AllFieldList = new List<Tuple<string, string, string, string>>();

            AllFieldList = _summaryfieldModel.GetAllColumnList(ilottype);
            AllFieldList = AllFieldList.OrderBy(q => q).ToList();

            for (int i = 0; i < AllFieldList.Count; i++)
            {
                NameColumnList.Add(AllFieldList[i].Item1.ToString());
            }

            NameColumnList = NameColumnList;

            SummaryFields = new ObservableCollection<SqlLotSumFields>();
            SummaryFields = _summaryfieldModel.GetXMLReportList(ilottype);

            OutPutColString = string.Empty;
            OutPutColString = ClassCommon.ReportFields;

        }

        internal void Hdr_ClickEvents(string clickEvent)
        {
            fldtoChange = clickEvent;
            bool canMod = false;

            try
            {
                if (SelectHdrIndex > -1)
                {
                    switch (clickEvent)
                    {
                        case "Format":
                            fldval = SummaryFields[SelectHdrIndex].Format;
                            canMod = true;
                            break;

                        case "Field Name":
                            fldval = SummaryFields[SelectHdrIndex].FieldName;
                            if (fldval == "Weight") canMod = false;
                            else if (fldval == "Moisture") canMod = false;
                            else canMod = true;
                            break;

                        case "SQL Expression":
                            fldval = SummaryFields[SelectHdrIndex].FieldExp;
                            canMod = false;
                            break;
                    }

                    string fieldType = SummaryFields[SelectHdrIndex]._fieldType;

                    if (fieldType == "datetime")
                    {
                        Console.WriteLine($"SummaryFields[SelectHdrIndex].FieldName = {SummaryFields[SelectHdrIndex].FieldName}");

                        if (canMod)
                        {
                            if (ModDtWindow != null) ModDtWindow = null;
                            ModDtWindow = new Window
                            {
                                Title = "Field Modification " + SummaryFields[SelectHdrIndex].FieldName.ToString(),
                                Width = 550,
                                Height = 300,
                                Content = new DTModifyView(_eventAggregator, clickEvent, fldval)
                            };
                            ModDtWindow.ResizeMode = ResizeMode.NoResize;
                            ModDtWindow.ShowDialog();
                        }
                    }
                    else
                    {
                        if (canMod)
                        {
                            if (SummaryFields.Count > 0)
                            {
                                if (ModWindow != null) ModWindow = null;
                                ModWindow = new Window
                                {
                                    Title = "Field Modification " + SummaryFields[SelectHdrIndex].FieldName.ToString() + " => Field Expression",
                                    Width = 400,
                                    Height = 300,
                                    Content = new FieldModifyView(_eventAggregator, clickEvent, fldval, fieldType)
                                };
                                ModWindow.ResizeMode = ResizeMode.NoResize;
                                ModWindow.ShowDialog();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR in ListViewHdrClick {ex.Message}");
            }
        }


        private DelegateCommand _loadedPageICommand;
        public DelegateCommand LoadedPageICommand =>
        _loadedPageICommand ?? (_loadedPageICommand = new DelegateCommand(LoadPageExecute));
        private void LoadPageExecute()
        {
            SelectTabOne = true;
            LoadPage();
        }

        private DelegateCommand _settingsCommand;
        public DelegateCommand SettingsCommand =>
        _settingsCommand ?? (_settingsCommand = new DelegateCommand(SettingsCommandExecute));
        private void SettingsCommandExecute()
        {
            BModify = true;
        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand =>
        _cancelCommand ?? (_cancelCommand =
            new DelegateCommand(CancelCommandExecute).ObservesCanExecute(() => BModify));
        private void CancelCommandExecute()
        {
            BModify = false;
            //Not Save Data and Close!
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
        _saveCommand ?? (_saveCommand =
            new DelegateCommand(SaveCommandExecute).ObservesCanExecute(() => BModify));
        private void SaveCommandExecute()
        {
            BModify = false;
            _summaryfieldModel.SaveFieldColumns(SummaryFields, ilottype);

            //Save Data and Close!
            _eventAggregator.GetEvent<SaveModFieldEvent>().Publish(ilottype);
            LoadPage();
        }

        private DelegateCommand _insertItemCommand;
        public DelegateCommand InsertItemCommand =>
        _insertItemCommand ?? (_insertItemCommand =
            new DelegateCommand(InsertItemCommandExecute).ObservesCanExecute(() => BModify));

        private void InsertItemCommandExecute()
        {
            for (int i = 0; i < AllFieldList.Count; i++)
            {
                if (AllFieldList[i].Item1 == SelectedColName)
                    AddToReportField(i, AllFieldList);
            }
        }

        private DelegateCommand _removeitemCommand;
        public DelegateCommand RemoveitemCommand =>
        _removeitemCommand ?? (_removeitemCommand =
            new DelegateCommand(RemoveitemCommandExecute).ObservesCanExecute(() => BModify));
        private void RemoveitemCommandExecute()
        {
            ObservableCollection<SqlLotSumFields> newlist = (ObservableCollection<SqlLotSumFields>)SummaryFields;
            if (SelectHdrIndex > -1)
            {
                object selected = SelectHdrItem;
                newlist.Remove((SqlLotSumFields)selected);
                SummaryFields = newlist;
            }
        }

        private DelegateCommand _moveUpCommand;
        public DelegateCommand MoveUpCommand =>
        _moveUpCommand ?? (_moveUpCommand =
            new DelegateCommand(MoveUpCommandExecute).ObservesCanExecute(() => BModify));
        private void MoveUpCommandExecute()
        {
            if (SelectHdrIndex > 0)
            {
                ObservableCollection<SqlLotSumFields> newlist = (ObservableCollection<SqlLotSumFields>)SummaryFields;
                int NewIndex = SelectHdrIndex - 1;

                if ((NewIndex > -1) || (NewIndex >= SummaryFields.Count))
                {
                    object selected = SelectHdrItem;

                    // Removing removable element ItemsControl.ItemsSource
                    newlist.Remove((SqlLotSumFields)selected);
                    // Insert it in new position
                    newlist.Insert(NewIndex, (SqlLotSumFields)selected);
                    // Restore selection
                    SummaryFields = newlist;
                }
            }
        }

        private DelegateCommand _moveDownCommand;
        public DelegateCommand MoveDownCommand =>
        _moveDownCommand ?? (_moveDownCommand =
            new DelegateCommand(MoveDownCommandExecute).ObservesCanExecute(() => BModify));
        private void MoveDownCommandExecute()
        {
            if ((SelectHdrIndex > -1) & (SelectHdrIndex + 1 < SummaryFields.Count))
            {
                ObservableCollection<SqlLotSumFields> newlist = (ObservableCollection<SqlLotSumFields>)SummaryFields;
                int NewIndex = SelectHdrIndex + 1;
                object selected = SelectHdrItem;

                //Remove selected item
                newlist.Remove((SqlLotSumFields)selected);
                // Insert it in new position
                newlist.Insert(NewIndex, (SqlLotSumFields)selected);
                // Restore selection
                SummaryFields = newlist;
            }
        }

        private DelegateCommand _instructionsCommand;
        public DelegateCommand InstructionsCommand =>
        _instructionsCommand ?? (_instructionsCommand =
            new DelegateCommand(InstructionsCommandExecute));
        private void InstructionsCommandExecute()
        {
            GuideBox guidebox = new GuideBox()
            {
                Topmost = true,
            };
            guidebox.ResizeMode = ResizeMode.NoResize;
            guidebox.Show();
        }


        private void AddToReportField(int i, List<Tuple<string, string, string, string>> sqlFieldList)
        {
            SummaryFields.Add(new SqlLotSumFields(sqlFieldList[i].Item1, sqlFieldList[i].Item2, sqlFieldList[i].Item3, sqlFieldList[i].Item4));
        }
    }
}
