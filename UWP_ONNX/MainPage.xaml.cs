using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_ONNX
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        JacketModel model;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var modelFolder=await appInstalledFolder.GetFolderAsync("Models");
            var modelFile = await modelFolder.GetFileAsync("jacket_model.onnx");
            model = await JacketModel.CreateModel(modelFile);
            base.OnNavigatedTo(e);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker=new FileOpenPicker();
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            var file = await picker.PickSingleFileAsync();
            if (file!=null)
            {
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    var decoder = await BitmapDecoder.CreateAsync(fileStream);
                    var software_bitmap = await decoder.GetSoftwareBitmapAsync();

                    WriteableBitmap bm =
                        new WriteableBitmap((int)decoder.PixelWidth,(int)decoder.PixelHeight);

                    software_bitmap.CopyToBuffer(bm.PixelBuffer);
                    imgFile.Source = bm;

                    var frame = VideoFrame.CreateWithSoftwareBitmap(software_bitmap);
                    var evalData = await model.EvaluateAsync(new JacketModelInput() { data = frame });
                    insulated_prob.Text = (100*evalData.loss["insulated"]).ToString("N2");
                    hardshell_prob.Text = (100*evalData.loss["hardshell"]).ToString("N2");
                }


            }
        }
    }
}
