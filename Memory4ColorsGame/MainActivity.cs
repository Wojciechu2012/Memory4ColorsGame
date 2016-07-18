using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;

namespace Memory4ColorsGame
{
    [Activity(Label = "Memory4ColorsGame", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Main);

            MusicMediaPlayer musicplayer = new MusicMediaPlayer();
            musicplayer.createMusic(this, Resource.Raw.ElevatorMusic);

            Button button = FindViewById<Button>(Resource.Id.MyButton);
            
            button.Click += delegate { StartActivity(typeof(GameScreen)); };
        }
    }
}

