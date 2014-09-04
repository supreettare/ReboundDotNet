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

namespace ReboundTest
{
    [Activity(Label = "ChatBubbleActivity", Icon = "@drawable/icon")]
    public class ChatBubbleActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SpringListenerViewActivity _view = new SpringListenerViewActivity(this);
            

            //LinearLayout.LayoutParams parms = new LinearLayout.LayoutParams(200, 200); //Width, Height
            //_view.LayoutParameters = parms;

            // Set our view from the "chat bubble" layout resource
            SetContentView(_view);

            //root relative layou
            //RelativeLayout relativeLayoutRoot = FindViewById<RelativeLayout>(Resource.Id.relativeLayoutRoot);

            //relativeLayoutRoot.AddView(_view);

            


        }
    }
}