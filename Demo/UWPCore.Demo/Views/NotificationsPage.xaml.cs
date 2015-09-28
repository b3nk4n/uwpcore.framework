using UWPCore.Framework.Controls;
using UWPCore.Framework.Notifications;
using UWPCore.Framework.Notifications.Models;
using UWPCore.Framework.UI;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Demo.Views
{
    /// <summary>
    /// Demo page for all notification services.
    /// </summary>
    public sealed partial class NotificationsPage : UniversalPage
    {
        private IToastService _toastService;
        private ITileService _tileService;
        private IDialogService _dialogService;

        public NotificationsPage()
        {
            InitializeComponent();
            _toastService = new ToastService();
            _tileService = new TileService();
            _dialogService = new DialogService();
        }

        #region Toast

        private void NotifyClicked(object sender, RoutedEventArgs e)
        {
            var toast = GetToastNotification();

            if (toast != null)
                _toastService.Show(toast);
        }

        private void ClearHistoryClicked(object sender, RoutedEventArgs e)
        {
            _toastService.ClearHistory();
        }

        private void RemoveToastByTagClicked(object sender, RoutedEventArgs e)
        {
            var tag = TagTextBox.Text;
            _toastService.RemoveFromHistory(tag);
        }

        private void RemoveToastByGroupClicked(object sender, RoutedEventArgs e)
        {
            var group = GroupTextBox.Text;
            _toastService.RemoveGroupeFromHistory(group);
        }

        private ToastNotification GetToastNotification()
        {
            var title = TitleTextBox.Text;
            var content1 = Content1TextBox.Text;
            var content2 = Content2TextBox.Text;
            var imageUri = ImageUriTextBox.Text;

            var selectedToastTemplate = (ToastTemplateComboBox.SelectedItem as ComboBoxItem).Content as string;
            ToastNotification toast = null;
            switch (selectedToastTemplate)
            {
                case "ToastText1":
                    toast = _toastService.Factory.CreateToastText01(content1);
                    break;
                case "ToastText2":
                    toast = _toastService.Factory.CreateToastText02(title, content1);
                    break;
                case "ToastText3":
                    toast = _toastService.Factory.CreateToastText03(title, content1);
                    break;
                case "ToastText4":
                    toast = _toastService.Factory.CreateToastText04(title, content1, content2);
                    break;
                case "ToastImageAndText1":
                    toast = _toastService.Factory.CreateToastImageAndText01(imageUri, content1);
                    break;
                case "ToastImageAndText2":
                    toast = _toastService.Factory.CreateToastImageAndText02(imageUri, title, content1);
                    break;
                case "ToastImageAndText3":
                    toast = _toastService.Factory.CreateToastImageAndText03(imageUri, title, content1);
                    break;
                case "ToastImageAndText4":
                    toast = _toastService.Factory.CreateToastImageAndText04(imageUri, title, content1, content2);
                    break;
            }

            return toast;
        }

        #endregion

        #region Tile

        private void PinUpdatePrimaryClicked(object sender, RoutedEventArgs e)
        {
            var tile = GetTileNotification();

            if (tile != null)
                _tileService.GetUpdaterForApplication().Update(tile);
        }

        private void PinUpdateSecondaryClicked(object sender, RoutedEventArgs e)
        {
            var tileId = TileIdTextBox.Text;
            var tile = GetTileNotification();

            if (tile != null)
                _tileService.GetUpdaterForSecondaryTile(tileId).Update(tile);
        }

        private async void RemoveTileClicked(object sender, RoutedEventArgs e)
        {
            var tileId = TileIdTextBox.Text;

            await _tileService.UnpinAsync(tileId);
        }

        private async void CheckTileExistsClicked(object sender, RoutedEventArgs e)
        {
            var tileId = TileIdTextBox.Text;

            var exists = _tileService.Exists(tileId);

            await _dialogService.ShowAsync(exists.ToString().ToUpper(), "Information");
        }

        private TileNotification GetTileNotification()
        {
            var title = TitleTextBox.Text;
            var content1 = Content1TextBox.Text;
            var content2 = Content2TextBox.Text;
            var content3 = Content3TextBox.Text;
            var content4 = Content4TextBox.Text;

            var selectedTileTemplate = (TileTemplateComboBox.SelectedItem as ComboBoxItem).Content as string;
            TileNotification tile = null;
            switch (selectedTileTemplate)
            {
                case "TileSquareBlock":
                    tile = _tileService.Factory.CreateTileSquareBlock(title, content1);
                    break;
                case "TileSquareText01":
                    tile = _tileService.Factory.CreateTileSquareText01(title, content1, content2, content3);
                    break;
                case "TileSquareText02":
                    tile = _tileService.Factory.CreateTileSquareText02(title, content1);
                    break;
                case "TileSquareText03":
                    tile = _tileService.Factory.CreateTileSquareText03(content1, content2, content3, content4);
                    break;
                case "TileSquareText04":
                    tile = _tileService.Factory.CreateTileSquareText04(content1);
                    break;
            }

            return tile;
        }

        #endregion

        #region Adaptive Toast

        private void AdaptiveToast1Clicked(object sender, RoutedEventArgs e)
        {
            var adaptiveToast = new AdaptiveToastModel()
            {
                Scenario = ToastScenario.Default,
                Launch = "Lauched by Adaptive toast 1",
                Duration = ToastDuration.Long,
                ActivationType = ToastActivationType.Foreground,
                Visual = new AdaptiveVisual()
                {
                    Bindings = {
                        new AdaptiveBinding()
                        {
                            Template = VisualTemplate.ToastGeneric,
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Content = "Header",
                                    HintStyle = TextStyle.Header,
                                    HintAlign = TextHintAlign.Center
                                },
                                new AdaptiveText()
                                {
                                    Content = "This is a sample content line",
                                    HintStyle = TextStyle.Body,
                                    HintWrap = true,
                                },
                                new AdaptiveText()
                                {
                                    Content = "This is a subheader line",
                                    HintStyle = TextStyle.Subheader,
                                    HintWrap = true,
                                },
                                new AdaptiveText()
                                {
                                    Content = "This is a subheader subtle line",
                                    HintStyle = TextStyle.SubheaderSubtle,
                                    HintWrap = true,
                                },
                                new AdaptiveText()
                                {
                                    Content = "Again a header?",
                                    HintStyle = TextStyle.Header,
                                    HintAlign = TextHintAlign.Right
                                },
                            }
                        }
                    }
                },
                Audio = new AdaptiveAudio()
                {
                    Loop = true,
                    Source = AdaptiveAudio.NOTIFICATION_LOOPING_CALL5 
                }
            };

            var toast = _toastService.AdaptiveFactory.Create(adaptiveToast);
            _toastService.Show(toast);
        }

        private void AdaptiveToast2Clicked(object sender, RoutedEventArgs e)
        {
            var adaptiveToast = new AdaptiveToastModel()
            {
                Launch = "Lauched by Adaptive toast 2",
                Visual = new AdaptiveVisual()
                {
                    Bindings = {
                        new AdaptiveBinding()
                        {
                            Template = VisualTemplate.ToastGeneric,
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Content = "Benjamin Sautermeister"
                                },
                                new AdaptiveText()
                                {
                                    Content = "Shall we meet up at 8?",
                                },
                                new AdaptiveImage()
                                {
                                    Placement = ImagePlacement.AppLogoOverride,
                                    Source = "/Assets/Images/bug.png"
                                }
                            }
                        }
                    }
                },
                Actions = new AdaptiveActions()
                {
                    Children =
                    {
                        new AdaptiveInput()
                        {
                            Id = "message",
                            Type = InputType.Text,
                            PlaceHolderContent = "reply here..."
                        },
                        new AdaptiveAction()
                        {
                            ActivationType = ToastActivationType.Background,
                            Content = "reply",
                            Arguments = "reply"
                        },
                        new AdaptiveAction()
                        {
                            ActivationType = ToastActivationType.Background,
                            Content = "video call",
                            Arguments = "video"
                        }
                    }
                }
            };

            var toast = _toastService.AdaptiveFactory.Create(adaptiveToast);
            _toastService.Show(toast);
        }

        private void AdaptiveToast3Clicked(object sender, RoutedEventArgs e)
        {
            var adaptiveToast = new AdaptiveToastModel()
            {
                Launch = "Lauched by Adaptive toast 3",
                Visual = new AdaptiveVisual()
                {
                    Bindings = {
                        new AdaptiveBinding()
                        {
                            Template = VisualTemplate.ToastGeneric,
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Content = "This is just a long bla bla text without any content, hopefully not bold like a header.",
                                    HintStyle = TextStyle.Body
                                },
                                new AdaptiveImage()
                                {
                                    Placement = ImagePlacement.AppLogoOverride,
                                    Source = "/Assets/Images/flash.png"
                                }
                            }
                        }
                    }
                },
                Actions = new AdaptiveActions()
                {
                    Children =
                    {
                        new AdaptiveInput()
                        {
                            Id = "time",
                            Type = InputType.Selection,
                            DefaultInput = "2",
                            Selections =
                            {
                                new AdaptiveSelection()
                                {
                                    Id = "1",
                                    Content = "First option"
                                },
                                new AdaptiveSelection()
                                {
                                    Id = "2",
                                    Content = "Second option (default)"
                                },
                                new AdaptiveSelection()
                                {
                                    Id = "3",
                                    Content = "Third option"
                                }
                            }
                        },
                        new AdaptiveAction()
                        {
                            Content = "Go!",
                            Arguments = "go"
                        },
                    }
                }
            };

            var toast = _toastService.AdaptiveFactory.Create(adaptiveToast);
            _toastService.Show(toast);
        }

        #endregion
    }
}
