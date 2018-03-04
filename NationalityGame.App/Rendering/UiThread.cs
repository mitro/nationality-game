using System;
using System.Windows;

namespace NationalityGame.App.Rendering
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