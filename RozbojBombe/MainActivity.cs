using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Media;
using Android.Content;




namespace RozbojBombe
{
    [Activity(Label = "RozbojBombe", MainLauncher = true)]
    public class MainActivity : Activity
    {
       private string bomb = new Random().Next(1, 4).ToString();
       private int scores = 0;
        Button button;
        TextView punkty;
        TextView.BufferType buffer;
        MediaPlayer bgMusic;
        MediaPlayer playExplosion;
        // MediaPlayer playYes;
        Vibrator vibrator;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
            SetContentView(Resource.Layout.Main);

            //Ustwienie przycisków 
            button = FindViewById<Button>(Resource.Id.button1);
            this.FindViewById<Button>(Resource.Id.button1).Click += ButtonClicked;
            button = FindViewById<Button>(Resource.Id.button2);
            this.FindViewById<Button>(Resource.Id.button2).Click += ButtonClicked;
            button = FindViewById<Button>(Resource.Id.button3);
            this.FindViewById<Button>(Resource.Id.button3).Click += ButtonClicked;

            //Ustwaienie wibracji
            vibrator = (Vibrator)this.ApplicationContext.GetSystemService(Context.VibratorService);

            punkty = FindViewById<TextView>(Resource.Id.textView1);
            bgMusic = MediaPlayer.Create(this, Resource.Raw.mission);
            bgMusic.Start();


        }

      

        //[Export("ButtonClicked")]
        public void ButtonClicked(object sender, EventArgs e)
        {

           // Button button = sender as Button;

            //Koniec gry
            if (button.Text == bomb)
            {
                PlayExplosion();
                vibrator.Vibrate(1500);
                
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("BOMBA WYBUCHŁA!!! Przeżyłeś " + scores + " razy");
                alert.SetMessage("KONIEC GRY!!!");
                alert.SetNegativeButton("KONIEC", (senderAlert, args)=> { FinishAndRemoveTask(); });
                alert.SetPositiveButton("Jeszcze Raz", (senderAlert, args) => {   });

                scores = 0;
                this.FindViewById<TextView>(Resource.Id.textView1).SetText("Punkty zdobyte: " + scores.ToString(), buffer);
                bomb = new Random().Next(1, 4).ToString();
                
                
                alert.Show();
            }
            else
            {

                //PlayYes();
                //vibrator.Vibrate(10);
                ++scores;
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Udało się!!!");
                alert.SetMessage("Zdobywasz punkt. Masz " + scores + " punktów");
                alert.SetNegativeButton("KONIEC", (senderAlert, args) => { FinishAndRemoveTask(); });
                alert.SetPositiveButton("Jeszcze Raz", (senderAlert, args) => { });
                this.FindViewById<TextView>(Resource.Id.textView1).SetText("Punkty zdobyte: " + scores.ToString(), buffer);
                bomb = new Random().Next(1, 4).ToString();
                //alert.Show();
            }

        }

        void PlayExplosion()
        {
            playExplosion = MediaPlayer.Create(this, Resource.Raw.explosion);
            playExplosion.Start();
        }

        //void PlayYes()
        //{
        //    playYes = MediaPlayer.Create(this, Resource.Raw.yes);
        //    playYes.Start();
        //}
        



    }
}

