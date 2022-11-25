
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AliceSeleniumHelper
{
    public class ChromePosion
    {
        #region Global

        public static int Width = 51;//6;
        public static int Height = 600;
        public static string[] iPos = new string[GetCountPos()];
        public static string AppPath = System.AppDomain.CurrentDomain.BaseDirectory;
        #endregion




        #region Đưa Device vào vị trí trống trên màn hình
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public static Point GetLocationFromAvailablePos()
        {
            Point location = new Point();
            int AvailablePos = GetAvailablePos();

            if (AvailablePos < GetCountWidthPos())
            {

                location.Y = 0;
                location.X = Width * AvailablePos;


            }
            else
            {

                location.Y = Height;
                location.X = Width * (AvailablePos - GetCountWidthPos());
            }
            return location;
        }

        public static void SetAvailablePos(int i)
        {
            iPos[i] = "";
        }

        public static void SetAllAvailablePos()
        {
            for (int i = 0; i < iPos.Count(); i++)
            {
                iPos[i] = "";
            }
        }
        private static object _lock = new object();
        public static int GetAvailablePos()
        {
            int AvailablePos = -1;
            lock (_lock)
            {
                for (int i = 0; i < iPos.Count(); i++)
                {
                    if (String.IsNullOrEmpty(iPos[i]))
                    {
                        iPos[i] = "1";
                        AvailablePos = i;
                        break;
                    }

                }
                if (AvailablePos == -1)
                {
                    for (int i = 1; i < iPos.Count(); i++)
                    {
                        iPos[i] = "";
                    }
                    AvailablePos = 0;
                }
            }
            return AvailablePos;
        }

        public static void ResetAvailablePos()
        {
            for (int i = 0; i < iPos.Count(); i++)
            {
                iPos[i] = "";

            }

        }




        public static int GetCountWidthPos()
        {
            int a = 0;

            double Rong = SystemParameters.VirtualScreenWidth;

            int ngang = (int)(Rong / Width);
            a = ngang;
            return a;
        }

        public static int GetCountPos()
        {
            int a = 0;
            double Rong = SystemParameters.VirtualScreenWidth;
            double Cao = SystemParameters.PrimaryScreenHeight;
            int ngang = (int)(Rong / (double)Width);
            int doc = (int)(Cao / (double)Height);
            if (doc > 2)
            {
                doc = 2;
            }
            a = ngang * doc;
            return a;
        }
        #endregion

    }
}
