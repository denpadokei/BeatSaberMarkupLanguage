using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage.Components;
using UnityEngine;
using UnityEngine.ResourceManagement.Util;

namespace BeatSaberMarkupLanguage
{
    public abstract class PersistentSingleton<T> : ComponentSingleton<T> where T : ComponentSingleton<T>
    {
        public static T instance => Instance;
    }
}
