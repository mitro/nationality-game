using System;
using System.Windows;

namespace NationalityGame.App.Utils
{
    public static class UiThread
    {
        public static void Dispatch(Action action)
        {
            if (Application.Current == null)
            {
                return;
            }

            Application.Current.Dispatcher.Invoke(action);
        }
    }
}