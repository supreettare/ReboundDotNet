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
using Com.Facebook.Rebound;
using Android.Graphics;
using Android.Graphics.Drawables;


//Credit Goes to following StackOverflow post for this implementation 
//http://stackoverflow.com/questions/23976343/adding-natural-dragging-effect-to-imageview-same-as-facebook-massanger-chat-head
namespace ReboundTest
{
    public class SpringListenerViewActivity : View, ISpringListener
    {
        private static int NUM_ELEMS = 4;
        private Spring[] mXSprings = new Spring[NUM_ELEMS];
        private Spring[] mYSprings = new Spring[NUM_ELEMS];
        private Paint mPaint = new Paint();
        private Bitmap mBitmap;
        private View _view;

        public SpringListenerViewActivity(Context context): base(context)
        {
            mBitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.Icon);
            SpringSystem ss = SpringSystem.Create();
            Spring s;
            for (int i = 0; i < NUM_ELEMS; i++)
            {
                s = ss.CreateSpring();
                s.SetSpringConfig(new MySpringConfig(200, i == 0 ? 8 : 15 + i * 2, i, true));
                s.AddListener(this);
                mXSprings[i] = s;

                s = ss.CreateSpring();
                s.SetSpringConfig(new MySpringConfig(200, i == 0 ? 8 : 15 + i * 2, i, false));
                s.AddListener(this);
                mYSprings[i] = s;
            }
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            mXSprings[0].SetCurrentValue(w / 2);
            mYSprings[0].SetCurrentValue(0);

            mXSprings[0].SetEndValue(w / 2);
            mYSprings[0].SetEndValue(h / 2);
        }


        public void OnSpringActivate(Spring p0)
        {
            
        }

        public void OnSpringAtRest(Spring p0)
        {
           
        }

        public void OnSpringEndStateChange(Spring p0)
        {
            
        }

        public void OnSpringUpdate(Spring s)
        {
            MySpringConfig cfg = (MySpringConfig)s.SpringConfig;
            if (cfg.index < NUM_ELEMS -1)
            {
                Spring[] springs = cfg.horizontal ? mXSprings : mYSprings;
                springs[cfg.index + 1].SetEndValue(s.CurrentValue);
            }
            if (cfg.index == 0)
            {
                this.Invalidate();
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            mXSprings[0].SetEndValue(e.GetX());
            mYSprings[0].SetEndValue(e.GetY());
            return true;
        }

        protected override void OnDraw(Canvas canvas)
        {
            for (int i = NUM_ELEMS - 1; i >= 0; i--)
            {
                mPaint.Alpha= (i == 0 ? 255 : 192 - i * 128 / NUM_ELEMS);
                canvas.DrawBitmap(mBitmap,
                        (float)mXSprings[i].CurrentValue - mBitmap.Width / 2,
                        (float)mYSprings[i].CurrentValue - mBitmap.Height / 2,
                        mPaint);
                //canvas.DrawBitmap(mBitmap, 0, 0, mPaint);
            }
        }

        //public bool OnTouch(View v, MotionEvent e)
        //{
        //    mXSprings[0].SetEndValue(e.GetX());
        //    mYSprings[0].SetEndValue(e.GetY());
        //    return true;
        //}

        //public bool OnDrag(View v, DragEvent e)
        //{
        //    mXSprings[0].SetEndValue(e.GetX());
        //    mYSprings[0].SetEndValue(e.GetY());
        //    return true;
        //}
    }

    public class MySpringConfig : SpringConfig {
        public int index;
        public bool horizontal;
        public MySpringConfig(double tension, double friction, int index, bool horizontal) :
            base(tension, friction)
        {
            this.index = index;
            this.horizontal = horizontal;
        }
    }
}