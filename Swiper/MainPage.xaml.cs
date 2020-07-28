using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swiper.Controls;
using Xamarin.Forms;

namespace Swiper
{
    public partial class MainPage : ContentPage
    {
        private int _likeCount;
        private int _denyCount;
        private void UpdateGui()
        {
            likeLabel.Text = _likeCount.ToString();
            denyLabel.Text = _denyCount.ToString();
        }
        
        private void Handle_OnLike(object sender, EventArgs e)
        {
            _likeCount++;
            InsertPhoto();
            UpdateGui();
        }
        private void Handle_OnDeny(object sender, EventArgs e)
        {
            _denyCount++;
            InsertPhoto();
            UpdateGui();
        }
        
        public MainPage()
        {
            InitializeComponent();
            AddInitialPhotos();
        }
        
        private void AddInitialPhotos()
        {
            for (int i = 0; i < 10; i++)
            {
                InsertPhoto();
            }
        }
        
        private void InsertPhoto()
        {
            var photo = new SwiperControl();
            this.MainGrid.Children.Insert(0, photo);
            photo.OnDeny += Handle_OnDeny;
            photo.OnLike += Handle_OnLike;
        }
    }
}