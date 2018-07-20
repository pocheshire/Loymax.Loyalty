using System;
using System.Globalization;
using System.Linq;
using Android.App;
using MvvmCross.Converters;
using Realms;

namespace Loyalty.Droid.Converters
{
    public class EmptyImageUrlValueConverter : MvxValueConverter<string, string>
    {
        protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            var userId = (string)parameter;

            if (string.IsNullOrEmpty(value))
            {
                var realm = Realm.GetInstance();
                var userImages = realm.All<UserImage>();

                var userImage = userImages?.FirstOrDefault(x => x.Id == userId);
                if (userImage != null)
                    return userImage.ImagePath;
                else
                {
                    int imageNumber = 1;
                    var imageName = "ic_funny_avatar";
                    var lastUserImage = userImages?.LastOrDefault();
                    if (lastUserImage != null)
                    {
                        var imageNameSliceWithNumberAndExtention = lastUserImage.ImagePath.Split('_').Last();
                        var imageNameNumberString = imageNameSliceWithNumberAndExtention.Split('.').First();
                        var previousImageNumber = int.Parse(imageNameNumberString);

                        var fullImageName = $"{imageName}_{previousImageNumber + 1}";
                        if (Application.Context.Resources.GetIdentifier(fullImageName, "drawable", Application.Context.PackageName) != 0)
                            imageNumber = previousImageNumber + 1;
                    }

                    var imagePath = $"res:{imageName}_{imageNumber}.png";

                    realm.Write(() => realm.Add(new UserImage { Id = userId, ImagePath = imagePath }));

                    return imagePath;
                }
            }
            else
            {
                var realm = Realm.GetInstance();
                var userImage = realm.Find<UserImage>(userId);
                if (userImage != null)
                    realm.Remove(userImage);
            }

            return value;
        }

        public class UserImage : RealmObject
        {
            [PrimaryKey]
            public string Id { get; set; }

            public string ImagePath { get; set; }
        }
    }
}
