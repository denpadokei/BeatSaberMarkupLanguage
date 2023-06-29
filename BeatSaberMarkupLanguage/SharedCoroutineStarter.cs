using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.Util;

namespace BeatSaberMarkupLanguage
{
    public class SharedCoroutineStarter : ComponentSingleton<SharedCoroutineStarter>
    {
        public static SharedCoroutineStarter instance => Instance;
    }
}
