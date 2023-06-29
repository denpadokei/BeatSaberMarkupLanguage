﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HMUI;

namespace BeatSaberMarkupLanguage.ViewControllers
{
    public abstract class BSMLViewController : ViewController, INotifyPropertyChanged
    {
        public Action<bool, bool, bool> didActivate;

        public event PropertyChangedEventHandler PropertyChanged;

        public abstract string Content { get; }

        public virtual string FallbackContent => @"<bg>
                                                     <vertical child-control-height='false' child-control-width='true' child-align='UpperCenter' pref-width='110' pad-left='3' pad-right='3'>
                                                       <horizontal bg='panel-top' pad-left='10' pad-right='10' horizontal-fit='PreferredSize' vertical-fit='PreferredSize'>
                                                         <text text='Invalid BSML' font-size='10'/>
                                                       </horizontal>
                                                     </vertical>
                                                     <text-page text='{0}' anchor-min-x='0.1' anchor-max-x='0.9' anchor-max-y='0.8'/>
                                                   </bg>";

        protected internal void ClearContents()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        public override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                ParseWithFallback();
            }

            didActivate?.Invoke(firstActivation, addedToHierarchy, screenSystemEnabling);
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Logger.Log?.Error($"Error invoking PropertyChanged for property '{propertyName}' on View Controller {name}\n{ex}");
            }
        }

        protected void ParseWithFallback()
        {
            try
            {
                BSMLParser.instance.Parse(Content, gameObject, this);
            }
            catch (Exception ex)
            {
                Logger.Log.Error($"Error parsing BSML\n{ex}");
                ClearContents();
                BSMLParser.instance.Parse(string.Format(FallbackContent, Utilities.EscapeXml(ex.Message)), gameObject, this);
            }
        }
    }
}
