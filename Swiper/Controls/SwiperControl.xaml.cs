﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Swiper.Utils;

namespace Swiper.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SwiperControl : ContentView
	{
        private readonly double _initialRotation;
        private static readonly Random _random = new Random();
        private double _screenWidth = -1;
        private const double DeadZone = 0.4d;
        private const double DecisionThreshold = 0.4d;

        public event EventHandler OnLike;
        public event EventHandler OnDeny;

        public SwiperControl ()
		{
            InitializeComponent();
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            this.GestureRecognizers.Add(panGesture);
            _initialRotation = _random.Next(-10, 10);
            photo.RotateTo(_initialRotation, 100, Easing.SinOut);
            
            var picture = new Picture();
            descriptionLabel.Text = picture.Description;
            image.Source = new UriImageSource() { Uri = picture.Uri };
            loadingLabel.SetBinding(IsVisibleProperty, "IsLoading");
            loadingLabel.BindingContext = image;
        }
        
        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    PanStarted();
                    break;

                case GestureStatus.Running:
                    PanRunning(e);
                    break;

                case GestureStatus.Completed:
                    PanCompleted();
                    break;
            }
        }

        private void PanStarted()
        {
            photo.ScaleTo(1.1, 100);
        }

        private void PanRunning(PanUpdatedEventArgs e)
        {
            photo.TranslationX = e.TotalX;
            photo.TranslationY = e.TotalY;
            photo.Rotation = _initialRotation + (photo.TranslationX / 25);
        }

        private void PanCompleted()
        {
            photo.TranslateTo(0, 0, 250, Easing.SpringOut);
            photo.RotateTo(_initialRotation, 250, Easing.SpringOut);
            photo.ScaleTo(1, 250);
        }
    }
}