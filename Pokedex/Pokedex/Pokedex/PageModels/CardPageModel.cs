using FreshMvvm;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Pokedex.Client;
using Pokedex.Models;
using PropertyChanged;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pokedex.PageModels
{
    [AddINotifyPropertyChangedInterface]

    public class CardPageModel : FreshBasePageModel
    {
        private readonly ITrelloAPIClient _trelloAPIClient;

        public CardPageModel(ITrelloAPIClient trelloAPIClient)
        {
            _trelloAPIClient = trelloAPIClient;
        }

        public TrelloCard Card { get; set; }

        public bool IsLoading { get; private set; }
        public int AttachmentCount { get; set; }

        public override async void Init(object initData)
        {
            if (initData != null && initData is string cardId)
            {
                Card = await _trelloAPIClient.GetCardAsync(cardId, attachments: true);

                AttachmentCount = Card.Attachments.Count;
            }

            base.Init(initData);
        }

        private byte[] ImageToByteArray(MediaFile media)
        {
            byte[] bytes;

            using (var memoryStream = new MemoryStream())
            {
                media.GetStream().CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            return bytes;
        }

        public Command PickPhotoCommand => new Command(async () =>
        {
            var media = await CrossMedia.Current.PickPhotoAsync();
            var bytes = File.ReadAllBytes(media.Path);
            await AddAttachmentAsync(bytes);
        });

        public Command CapturePhotoCommand => new Command(async (s) =>
        {
            var cameraMediaOptions = new StoreCameraMediaOptions
            {
                DefaultCamera = CameraDevice.Rear,

                // Set the value to true if you want to save the photo to your public storage.
                SaveToAlbum = false,

                // Give the name of the folder you want to save to
                Directory = "MyAppName",

                // Set the compression quality
                // 0 = Maximum compression but worse quality
                // 100 = Minimum compression but best quality
                CompressionQuality = 100
            };

            var media = await CrossMedia.Current.TakePhotoAsync(cameraMediaOptions);

            await AddAttachmentAsync(ImageToByteArray(media));
        });

        private async Task AddAttachmentAsync(byte[] file)
        {
            try
            {
                var response = await _trelloAPIClient.PostCardAttachmentAsync(Card.Id, file);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await CurrentPage.DisplayAlert("Success!", "Attachment was added", "OK");

                    AttachmentCount += 1;
                }
                else
                {
                    await CurrentPage.DisplayAlert("Error", "Bad Request", "OK");
                }
            }
            catch (Exception)
            {
                await CurrentPage.DisplayAlert("Error", "An exception occured", "OK");
            }
        }
    }
}
