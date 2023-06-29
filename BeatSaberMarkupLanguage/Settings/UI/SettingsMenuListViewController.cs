﻿using System;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using UnityEngine;

namespace BeatSaberMarkupLanguage.Settings.UI.ViewControllers
{
    [ViewDefinition("BeatSaberMarkupLanguage.Views.settings-list.bsml")]
    internal class SettingsMenuListViewController : BSMLAutomaticViewController
    {
        [UIComponent("list")]
        public CustomListTableData list;

        public Action<SettingsMenu> clickedMenu;

        public override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);

            if (firstActivation)
            {
                rectTransform.sizeDelta = new Vector2(35, 0);
                rectTransform.anchorMin = new Vector2(0.5f, 0);
                rectTransform.anchorMax = new Vector2(0.5f, 1);
            }

            list.data = BSMLSettings.instance.settingsMenus;

            if (list.tableView != null)
            {
                list.tableView.ReloadData();
            }
        }

        [UIAction("settings-click")]
        private void SettingsClick(TableView tableView, int index)
        {
            SettingsMenu settingsMenu = (SettingsMenu)list.data[index];
            clickedMenu?.Invoke(settingsMenu);
        }
    }
}
