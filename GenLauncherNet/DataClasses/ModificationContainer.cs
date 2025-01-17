﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GenLauncherNet
{
    public class ModificationContainer
    {
        public GameModification ContainerModification { get; private set; }

        public ModificationVersion LatestVersion { get; private set; }
        public ModificationVersion SelectedVersion { get; private set; }
        
        public ModificationDownloader Downloader { get; set; }
        public GridControls _GridControls { get; private set; }
        public BitmapImage ImageSource { get; }

        public string NameInfo { get; private set; }
        public string LatestVersionInfo { get; private set; }
        public bool ReadyToRun { get; set; } = true;

        public ModificationContainer(GameModification modification)
        {
            ContainerModification = modification;

            UpdataModboxData();
        }

        public void UpdataModboxData()
        {
            NameInfo = ContainerModification.Name;

            if (ContainerModification.ModificationVersions != null && ContainerModification.ModificationVersions.Count > 0)
            {
                var ContainerVersions = ContainerModification.ModificationVersions;
                ContainerVersions.Sort();
                SelectedVersion = ContainerVersions.Where(t => t.IsSelected).FirstOrDefault();

                if (SelectedVersion == null)
                    SelectedVersion = ContainerVersions.OrderBy(m => m).LastOrDefault();

                SelectedVersion.IsSelected = true;

                LatestVersion = ContainerVersions.Last();
                LatestVersionInfo = "Latest version: " + LatestVersion.Version;
            }
        }

        private void SetImage()
        {
            var imageFileName = Path.Combine(EntryPoint.LauncherFolder, EntryPoint.LauncherImageSubFolder, ContainerModification.Name, LatestVersion.Version);
            if (!File.Exists(imageFileName))
                return;

            var stream = File.OpenRead(imageFileName);

            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                _GridControls._Image.Source = bitmap;
                stream.Close();
            }
            catch
            {
                try
                {
                    stream.Close();
                    if (File.Exists(imageFileName))
                        File.Delete(imageFileName);
                }
                catch
                {
                    //TODO logger
                }
            }
        }

        public void CancelDownload()
        {
            if (Downloader != null)
            {
                _GridControls._UpdateButton.IsEnabled = false;
                Downloader.CancelDownload();
                ClearDownloader();

                UpdataModboxData();

                if (ContainerModification.ModificationVersions.Where(m => m.Installed).Any())
                {
                    ReadyToRun = true;
                }
            }
            SetUnactiveProgressBar();
        }

        public void SetSelectedStatus()
        {
            ContainerModification.IsSelected = true;
            _GridControls._Name.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#baff0c");
            _GridControls._VersionTextBlock.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("White");
            _GridControls._Name.FontWeight = FontWeights.Bold;
            _GridControls._ComboBox.Visibility = System.Windows.Visibility.Visible;

            if (_GridControls._Image != null && _GridControls._ImageBorder != null)
            {
                if (_GridControls._Image.Source == null)
                    _GridControls._ImageBorder.BorderThickness = new Thickness(0, 0, 0, 0);
                else
                {
                    _GridControls._ImageBorder.BorderThickness = new Thickness(2, 2, 2, 2);
                    _GridControls._ImageBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#baff0c");
                }

            }
        }

        public void SetUnSelectedStatus()
        {
            //ContainerModification.IsSelected = false;
            _GridControls._Name.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("DarkGray");
            _GridControls._VersionTextBlock.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("DarkGray");
            _GridControls._Name.FontWeight = FontWeights.Normal;
            _GridControls._ComboBox.Visibility = System.Windows.Visibility.Hidden;


            if (_GridControls._Image != null && _GridControls._ImageBorder != null)
            {
                if (_GridControls._Image.Source == null)
                    _GridControls._ImageBorder.BorderThickness = new Thickness(0, 0, 0, 0);
                else
                {
                    _GridControls._ImageBorder.BorderThickness = new Thickness(2, 2, 2, 2);
                    _GridControls._ImageBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("DarkGray");
                }
            }

        }

        public void SetUnactiveProgressBar()
        {
            if (_GridControls != null && _GridControls._ProgressBar != null)
            {
                this._GridControls._ProgressBar.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#090502");
                this._GridControls._ProgressBar.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("DarkGray");
                this._GridControls._InfoTextBlock.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("White");
            }
        }

        public void SetUIElements(GridControls gridControls)
        {
            _GridControls = gridControls;
            UpdateUIelements();

            if (LatestVersion != null && LatestVersion.ModificationType == ModificationType.Mod)
                SetImage();

            SetUnSelectedStatus();
        }

        public void UpdateUIelements()
        {
            if (Downloader == null || Downloader.Result.Crashed)
            {
                Downloader = null;
                this._GridControls._ProgressBar.Value = 0;
                this._GridControls._InfoTextBlock.Text = String.Empty;
                this._GridControls._UpdateButton.Content = "UPDATE!";

                UpdataModboxData();
                UpdateComboBox();
                SelectItemInComboBox();
            }

            if (String.IsNullOrEmpty(ContainerModification.NewsLink))
                _GridControls._ChangeLogButton.Visibility = Visibility.Hidden;

            if (String.IsNullOrEmpty(ContainerModification.NetworkInfo))
                _GridControls._NetworkInfo.Visibility = Visibility.Hidden;
        }

        public void UpdateComboBox()
        {
            if (LatestVersion != null && LatestVersion.Installed)
            {
                this._GridControls._UpdateButton.IsEnabled = false;
            }
            else
            {
                this._GridControls._UpdateButton.IsEnabled = true;
                if (ContainerModification.ModificationType == ModificationType.Mod)
                    this._GridControls._UpdateButton.IsBlinking = true;
            }

            var versionListSource = new ObservableCollection<ComboBoxData>();

            foreach (var version in ContainerModification.ModificationVersions.Where(m => m.Installed))
            {
                versionListSource.Add(new ComboBoxData(this.ContainerModification, version.Version, this));
            }

            this._GridControls._ComboBox.ItemsSource = versionListSource;
        }

        public void SelectItemInComboBox()
        {
            //Case no VersionsInstalled
            if (ContainerModification.ModificationVersions.Count == 0)
            {
                _GridControls._ComboBox.IsEnabled = false;
                return;
            }

            //Case there is Repos version for mod
            if (ContainerModification.ModificationVersions.Count == 1 && !LatestVersion.Installed)
            {
                SetUIToInstallMode();
                return;
            }

            //Case there is installed Version in data, select it
            _GridControls._ComboBox.IsEnabled = true;
            if (ReadyToRun)
            {
                if (SelectedVersion == null || !LatestVersion.Installed)
                {
                    SelectedVersion = ContainerModification.ModificationVersions.Where(m => m.Installed).Last();
                }
                var versionString = SelectedVersion.Version;
                var ItemIndex = GetIndexOfItemByName(_GridControls._ComboBox, versionString);
                if (ItemIndex != -1)
                    _GridControls._ComboBox.SelectedItem = _GridControls._ComboBox.Items[ItemIndex];
            }

            //Case there was installation, select last version
            else
            {
                if (SelectedVersion != null)
                    SelectedVersion.IsSelected = false;

                SelectedVersion = ContainerModification.ModificationVersions.Where(m => m.Installed).Last();
                var versionString = SelectedVersion.Version;
                var ItemIndex = GetIndexOfItemByName(_GridControls._ComboBox, versionString);
                if (ItemIndex != -1)
                    _GridControls._ComboBox.SelectedItem = _GridControls._ComboBox.Items[ItemIndex];

                ReadyToRun = true;
            }
        }

        public void SetDownloader(ModificationDownloader downloader)
        {
            Downloader = downloader;
        }

        public void ClearDownloader()
        {
            Downloader = null;
        }

        public void PrepareControlsToDownloadMode()
        {
            this._GridControls._UpdateButton.Content = "STOP";
            this._GridControls._ComboBox.IsEnabled = false;
            this.ReadyToRun = false;

            this._GridControls._ProgressBar.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#2534ff");
            this._GridControls._ProgressBar.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#00e3ff");
            this._GridControls._InfoTextBlock.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("Black");
        }

        public void SetUIMessages(string message)
        {
            this._GridControls._InfoTextBlock.Text = message;
        }

        public void SetUIMessages(string message, int percentage)
        {
            this._GridControls._InfoTextBlock.Text = message;
            this._GridControls._ProgressBar.Value = percentage;
        }

        public void SetUIToInstallMode()
        {
            this._GridControls._ComboBox.IsEnabled = false;
            this._GridControls._UpdateButton.Content = "INSTALL!";
            this.ReadyToRun = false;
        }

        private static int GetIndexOfItemByName(System.Windows.Controls.ComboBox box, string itemName)
        {
            if (String.IsNullOrEmpty(itemName))
                return -1;

            var p = box.Items;
            for (var i = 0; i < box.Items.Count; i++)
            {
                if (((ComboBoxData)box.Items[i]).VersionName == itemName)
                    return i;
            }

            return -1;
        }
    }
}