using System.Collections.Generic;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using Loyalty.API.Models;
using MvvmCross.ViewModels;

namespace Loyalty.Core.ViewModels.Profile.Items
{
    public class AchievementItemVm : MvxViewModel
    {
        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value, nameof(ImageUrl));
        }

        public List<ITransformation> Transformations => new List<ITransformation> { new CircleTransformation() };

        public AchievementItemVm(Achievement model)
        {
            ImageUrl = model.ImageUrl;
        }
    }
}
