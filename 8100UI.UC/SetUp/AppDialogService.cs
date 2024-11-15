using System;
using System.Collections.Generic;

namespace SetUp
{
    public interface IAppDialogService
    {
        void ShowDialog(string name, Action<string> callback);

        void ShowDialog<ViewModel>(Action<string> callback);

    }


    public class AppDialogService : IAppDialogService
    {
        public AppDialogService()
        {
           
        }
        static Dictionary<Type, Type> _mapping = new Dictionary<Type, Type>();

        public static void RegisterDialog<TView, TViewModel>()
        {
            _mapping.Add(typeof(TViewModel), typeof(TView));
        }

        public void ShowDialog(string name, Action<string> callback)
        {
            var type = Type.GetType($"SetUp.Views.{name}");
            ShowDialogInternal(type, callback);
        }

        public void ShowDialog<TViewModel>(Action<string> callback)
        {
            var type = _mapping[typeof(TViewModel)];
            ShowDialogInternal(type, callback);
        }

        private static void ShowDialogInternal(Type type, Action<string> callback)
        {
            var dialog = new SetUpWindow();

            void closeEventHandler(object s, EventArgs e)
            {
                callback(dialog.DialogResult.ToString());
                dialog.Closed -= closeEventHandler;
            }

            dialog.Closed += closeEventHandler;
            dialog.Content = Activator.CreateInstance(type);
            dialog.ShowDialog();
        }
    }
}
