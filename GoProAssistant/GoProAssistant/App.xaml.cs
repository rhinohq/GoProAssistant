﻿using Xamarin.Forms;

using GoProAssistant.iOS;
using GoProAssistant.CameraInterface;
using GoProAssistant.Shared;

namespace GoProAssistant
{
    public partial class App : Xamarin.Forms.Application
    {
        private ILocationProvider locationProvider => DependencyService.Get<ILocationProvider>();

        public App()
        {
            InitializeComponent();

            DependencyService.Register<Camera>();
            DependencyService.Register<ILocationProvider, LocationManager>();
            DependencyService.Register<IRecordingStorage, RecordingStorage>();
            DependencyService.Register<IVideoEditor, VideoEditor>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            if (!GoProAssistant.IsInRecordingMode)
                locationProvider.StopLocationUpdates();
        }

        protected override void OnResume()
        {
            if (!GoProAssistant.IsInRecordingMode)
                locationProvider.StartLocationUpdates();
        }
    }
}
