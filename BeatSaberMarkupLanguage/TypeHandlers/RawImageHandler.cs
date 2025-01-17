﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatSaberMarkupLanguage.TypeHandlers
{
    [ComponentHandler(typeof(RawImage))]
    internal class RawImageHandler : TypeHandler<RawImage>
    {
        public override Dictionary<string, string[]> Props => new()
        {
            { "image", new[] { "source", "src" } },
        };

        public override Dictionary<string, Action<RawImage, string>> Setters => new()
        {
            { "image", new Action<RawImage, string>(SetImage) },
        };

        public void SetImage(RawImage image, string imagePath)
        {
            if (imagePath.Length > 1 && imagePath[0] == '#')
            {
                string imgName = imagePath.Substring(1);

                image.texture = Utilities.FindTextureCached(imgName);
                if (image.texture == null)
                {
                    Logger.Log.Error($"Could not find {nameof(Texture)} with image name '{imgName}'");
                }
            }
            else
            {
                Utilities.GetData(imagePath, (byte[] data) =>
                {
                    image.texture = Utilities.LoadTextureRaw(data);
                });
            }
        }
    }
}
