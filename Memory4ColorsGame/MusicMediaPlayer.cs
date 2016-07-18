using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;

namespace Memory4ColorsGame
{
    class MusicMediaPlayer
    {
        public static MediaPlayer mediaplayer;
        
        

        public void createMusic(Context con,int raw)
        {
            mediaplayer = MediaPlayer.Create(con, raw);
            mediaplayer.Looping = true;
            mediaplayer.Start();
        }
    }
}