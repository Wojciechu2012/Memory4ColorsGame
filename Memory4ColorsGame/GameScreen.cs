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
using System.Threading.Tasks;
using Android.Media;

namespace Memory4ColorsGame
{
    [Activity(Label = "GameScreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class GameScreen : Activity
        {
        

        int level;
        ImageButton musiconoff;
        Button[] buttonlist = new Button[4];
        Button startlevelbutton;
        Queue<int> pressingorder = new Queue<int>();
        Random rand;
        TextView leveltext;
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;

        public TextView Leveltext
        {
            get
            {
                return leveltext;
            }

            set
            {
                leveltext = value;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.GameScreen);
            Leveltext = FindViewById<TextView>(Resource.Id.leveltext);
            musiconoff = FindViewById<ImageButton>(Resource.Id.onoffmusic);
            startlevelbutton = FindViewById<Button>(Resource.Id.startlevel);
            buttonlist[0] = FindViewById<Button>(Resource.Id.buttonred);
            buttonlist[1] = FindViewById<Button>(Resource.Id.buttongreen);
            buttonlist[2] = FindViewById<Button>(Resource.Id.buttonyellow);
            buttonlist[3] = FindViewById<Button>(Resource.Id.buttonblue);
            blockbuttons();

            for (int i = 0; i < buttonlist.Length; i++)
            {
                Button b = buttonlist[i];
                b.Tag = i;
                b.Click += B_Click;
            }


            prefs = Application.Context.GetSharedPreferences("level", FileCreationMode.Private);
            editor = prefs.Edit();
            
            level = prefs.GetInt("level", 1);


            Leveltext.Text = "Level " + level;

            musiconoff.Click += Musiconoff_Click;

            startlevelbutton.Click += (e, o) =>
            {
                Leveltext.Text = "Level " + level;
                pressing_order();
                ShowButton(pressingorder);
            };
           
        }

        private void Musiconoff_Click(object sender, EventArgs e)
        {
            ImageButton ib = (ImageButton)sender;

            if (MusicMediaPlayer.mediaplayer.IsPlaying)
            {
                MusicMediaPlayer.mediaplayer.Pause();
                ib.SetImageResource(Android.Resource.Drawable.IcMediaPause);
            }
            else { MusicMediaPlayer.mediaplayer.Start();
                ib.SetImageResource(Android.Resource.Drawable.IcMediaPlay);
            }
        }

        private void B_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (!(button.Tag.Equals(pressingorder.Dequeue())))
            {
                startlevelbutton.Enabled = true;
                blockbuttons();
                leveltext.Text = "Level " + level + "\r\nTry again";
                return;
            }

             if (pressingorder.Count == 0)
            {
                level++;
                startlevelbutton.Enabled = true;
                    blockbuttons();
               editor.PutInt("level", level);
                editor.Apply();
                
                leveltext.Text = "Level " + level;
                return;
                }
            leveltext.Text = "Level " + level + "\r\nLeft " + pressingorder.Count;
        }

        private async void buttonChangeColor(Button button)
        {
            button.Pressed = true;
            await Task.Delay(1500);
            button.Pressed = false;
        }

        private void pressing_order()
        {
            rand = new Random();
            pressingorder.Clear();
            for (int i = 0; i < level+1; i++)
            {
                pressingorder.Enqueue(rand.Next(0, 4));
            }

        }


        private async void ShowButton(Queue<int> queue)
        {
            blockbuttons();
            startlevelbutton.Enabled = false;
            Queue<int> newque = new Queue<int>();

            foreach (int number in queue)
            {
                newque.Enqueue(number);
            }

            while (newque.Count > 0)
            {
                int button = newque.Dequeue();
                buttonChangeColor(buttonlist[button]);
                await Task.Delay(1600);
            }
            enablebuttons();
        }



        private void blockbuttons()
        {
            foreach (Button b in buttonlist)
            {
                b.Enabled = false;
                b.Clickable = false;
            }

        }
        private void enablebuttons()
        {
            foreach (Button b in buttonlist)
            {
                b.Enabled = true;
                b.Clickable = true;
            }

        }
    }
}