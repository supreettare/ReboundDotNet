using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Facebook.Rebound;

namespace ReboundTest
{
    [Activity(Label = "ReboundTest", Icon = "@drawable/icon", MainLauncher = true)]
    public class MainActivity : Activity
    {
        int count = 1;
       
        private SpringConfig SPRING_CONFIG;//= SpringConfig.FromOrigamiTensionAndFriction(40, 3);
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            EditText tension = FindViewById<EditText>(Resource.Id.editTextTension);
            EditText friction = FindViewById<EditText>(Resource.Id.editTextFriction);

           
            //Spring Dynamics 
            SpringSystem _springSystem = SpringSystem.Create();

            //Click
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            Spring _spring = _springSystem.CreateSpring();
            _spring.AddListener(new SimpleSpringListener(button, "click"));
            button.Click += delegate {

                SPRING_CONFIG = SpringConfig.FromOrigamiTensionAndFriction(100, 12);//Best Values: 100,12
                _spring.SetSpringConfig(SPRING_CONFIG);
                _spring.SetCurrentValue(1);
                _spring.SetEndValue(0.2);
                _spring.SetOvershootClampingEnabled(true);
            };


            //Tilt
            Button buttonTilt = FindViewById<Button>(Resource.Id.buttonTilt);
            Spring _spring2 = _springSystem.CreateSpring();
            _spring2.AddListener(new SimpleSpringListener(buttonTilt, "tilt"));
            buttonTilt.Click += delegate
            {
                SPRING_CONFIG = SpringConfig.FromOrigamiTensionAndFriction(100, 12);//Best Values: 100,12
                _spring2.SetSpringConfig(SPRING_CONFIG);
                _spring2.SetCurrentValue(0);
                _spring2.SetEndValue(0.2);
            };


            //BounceIn
            Button buttonBounceIn = FindViewById<Button>(Resource.Id.buttonBounceIn);
            Spring _spring3 = _springSystem.CreateSpring();
            _spring3.AddListener(new SimpleSpringListener(buttonBounceIn, "bouncein"));
            buttonBounceIn.Click += delegate
            {
                SPRING_CONFIG = SpringConfig.FromOrigamiTensionAndFriction(Convert.ToDouble(tension.Text), Convert.ToDouble(friction.Text));//Best Values: 10,2
                _spring3.SetSpringConfig(SPRING_CONFIG);
                _spring3.SetCurrentValue(1);
                _spring3.SetEndValue(0);
            };

            //BounceOut
            Button buttonBounceOut = FindViewById<Button>(Resource.Id.buttonBounceOut);
            Spring _spring4 = _springSystem.CreateSpring();
            _spring4.AddListener(new SimpleSpringListener(buttonBounceOut, "bounceout"));
            buttonBounceOut.Click += delegate
            {
                SPRING_CONFIG = SpringConfig.FromOrigamiTensionAndFriction(Convert.ToDouble(tension.Text), Convert.ToDouble(friction.Text));//Best Values: 10,2
                _spring4.SetSpringConfig(SPRING_CONFIG);
                _spring4.SetCurrentValue(0);
                _spring4.SetEndValue(1);
            };

            //BounceFadeIn
            Button buttonBounceFadeIn = FindViewById<Button>(Resource.Id.buttonBounceFadeIn);

            //Spring _spring5 = _springSystem.CreateSpring();
            //_spring5.AddListener(new SimpleSpringListener(buttonBounceFadeIn, "bouncefadein"));
            buttonBounceFadeIn.Click += delegate
            {
                //SPRING_CONFIG = SpringConfig.FromOrigamiTensionAndFriction(Convert.ToDouble(tension.Text), Convert.ToDouble(friction.Text));//Best Values: 10,2
                //_spring5.SetSpringConfig(SPRING_CONFIG);
                //_spring5.SetCurrentValue(1);
                //_spring5.SetEndValue(0);
                StartActivity(typeof(ChatBubbleActivity));
            };

            Button buttonReset = FindViewById<Button>(Resource.Id.buttonReset);
            //buttonReset.Visibility = ViewStates.Gone;
            buttonReset.Click += (s, e) =>
            {
                _spring4.SetEndValue(0);
                _spring4.SetCurrentValue(0);
                buttonBounceFadeIn.Background.SetAlpha(256);
            };


            //ImageView img = FindViewById<ImageView>(Resource.Id.imageView1);
            // //Spring _spring6 = _springSystem.CreateSpring();
            // // _spring6.AddListener(new SpringListenerViewActivity(this, img));
            
            //img.SetOnDragListener(new SpringListenerViewActivity(this));
            
        }
    }

    public class SimpleSpringListener : Java.Lang.Object, ISpringListener
    {
        private View button;
        float scalex = 0;
        float scaley = 0;
        string effect;

        public SimpleSpringListener(View button, string effect)
        {
            // TODO: Complete member initialization
            this.button = button;
            this.scalex = button.ScaleX;
            this.scaley = button.ScaleY;
            this.effect = effect;
        }
        public void OnSpringActivate(Spring p0)
        {
            //throw new NotImplementedException();
        }

        public void OnSpringAtRest(Spring spring)
        {
            //throw new NotImplementedException();
            //spring.SetEndValue(0);

            //float value = (float)spring.CurrentValue;
            //float scale = 1f + (value * 0.5f);
            if (effect == "click" || effect == "tilt")
            {
                button.ScaleX = scalex;
                button.ScaleY = scaley;
                button.RotationY = 0;
            }
           

            if (effect == "bouncein")
            {
                spring.SetEndValue(0);
            }

            if (effect == "bounceout")
            {
                spring.SetEndValue(1);
            }

            if (effect == "bouncefadein")
            {
                button.Background.SetAlpha(256);
            }
        }

        public void OnSpringEndStateChange(Spring p0)
        {
            //throw new NotImplementedException();
        }

        public void OnSpringUpdate(Spring spring)
        {
            if (effect=="click")
            {
                float value = (float)spring.CurrentValue;
                float scale = 1f - (value * 0.2f);
                button.ScaleX = scale;
                button.ScaleY = scale;
            }

            if (effect == "tilt")
            {
                button.RotationY = -2;
            }

            if (effect == "bouncein")
            {
                float value = (float)spring.CurrentValue;
                float scale = 1f - (value * 0.8f);
                button.ScaleX = scale;
                button.ScaleY = scale;
            }

            if (effect == "bounceout")
            {
                float value = (float)spring.CurrentValue;
                float scale = 1f - (value * 0.8f);
                button.ScaleX = scale;
                button.ScaleY = scale;
            }

            if (effect == "bouncefadein")
            {
                float value = (float)spring.CurrentValue;
                float scale = 1f - (value * 0.5f);
                button.ScaleX = scale;
                button.ScaleY = scale;
                button.Background.SetAlpha(Convert.ToInt16(scale * 100));
            }
            
            if (effect == "bouncefadeout")
            {
                float value = (float)spring.CurrentValue;
                float scale = 1f - (value * 0.8f);
                button.ScaleX = scale;
                button.ScaleY = scale;
            }
        }

        //public IntPtr Handle
        //{
        //    get { return button.; }
        //}

        //public void Dispose()
        //{
        //    //throw new NotImplementedException();
        //}
    }
}

