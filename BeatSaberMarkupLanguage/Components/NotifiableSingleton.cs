using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ResourceManagement.Util;

namespace BeatSaberMarkupLanguage.Components
{
    public class NotifiableSingleton<T> : PersistentSingleton<T>, INotifyPropertyChanged
        where T : ComponentSingleton<T>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Logger.Log?.Error($"Error Invoking PropertyChanged: {ex.Message}");
                Logger.Log?.Error(ex);
            }
        }
    }
}
