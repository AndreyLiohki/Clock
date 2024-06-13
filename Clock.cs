using System;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Timers;

class Anal
{
    static void Main(string[] args)
    {
        int Radius = 15;
        int height = (2 * Radius) + 1;
        int width = (2 * Radius) + 1;
        char[,] Clock = new char[height, width];
        var timer = new System.Timers.Timer(500);
        timer.Elapsed += (sender, e) => {
            ClearClock(Clock, Radius, height, width);
            Console.Clear();
            var timerState = DateTime.Now;
            double q = -((timerState.Second + timerState.Millisecond / 1000.0) * Math.PI) / 30 + (Math.PI / 2);
            int u = (int)(Radius * Math.Sin(q)), v = (int)(Radius * Math.Cos(q));
            int x = v + 14, y = 15 - u;
            line(15, x, 15, y, ref Clock, timerState.Second, height, width, "*");
            q = -((timerState.Minute) * Math.PI) / 30 + (Math.PI / 2);
            u = (int)(Radius * Math.Sin(q) * 0.8);
            v = (int)(Radius * Math.Cos(q) * 0.8);
            x = v + 14;
            y = 15 - u;
            line(15, x, 15, y, ref Clock, timerState.Minute, height, width, "+");
            q = -((timerState.Hour) * Math.PI) / 6 + (Math.PI / 2);
            u = (int)(Radius * Math.Sin(q) * 0.6);
            v = (int)(Radius * Math.Cos(q) * 0.6);
            x = v + 14;
            y = 15 - u;
            line(15, x, 15, y, ref Clock, timerState.Minute, height, width, "#");
            PrintClock(Clock, Radius);
        };
        timer.AutoReset = true;
        timer.Enabled = true;
        while (Console.Read() != 'q') ;
    }   
    static void line(int x0, int x1, int y0, int y1, ref char[,] Clock, int time, int height, int width, string elem)
    {
        
        bool flag = Math.Abs(y1-y0) >= Math.Abs(x1-x0);
        if (flag)
        {
            (x0, y0) = (y0, x0);
            (x1, y1) = (y1, x1);

        }
        if (x0>x1)
        {
            (x0, x1) = (x1, x0);
            (y0, y1) = (y1, y0);
        }

        int deltaX = Math.Abs(x1-x0);
        int deltaY = Math.Abs(y1-y0);
        int error = 0;
        int deltaErr = deltaY + 1;
        int y = y0;
        int diry = ((y1-y0) > 0 ? 1 : -1);
        for(int i = x0; i<=x1; ++i)
        {
            
            if (flag)
            {
                Fill(Clock, i, y, elem, height, width);
            }
            else
            {
                Fill(Clock, y, i, elem, height, width);
            }
            error = error + deltaErr;
            if(error >= deltaX + 1)
            {
                y += diry;
                error -= (deltaX + 1);
            }
        }


    }
    static void ClearClock(char[,] Clock, int Radius, int height, int width)
    {
        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                double Checker = Math.Pow((Radius - 1 - i), 2) + Math.Pow((Radius - 1 - j), 2);
                if (Checker < Radius * Radius)
                {
                    Clock[i, j] = '.';
                }
                else
                {
                    Clock[i, j] = ' ';
                }
            }
        }
    }
    static void Fill(char[,] Clock, int i, int j, string t, int height, int width)
    {

        for(int q = 0; q< t.Length; ++q)
        {
            if(j+q<width && j+q>=0 && i < height)
                Clock[i, j + q] = t[q];
        }

    }
    static void PrintClock(char[,] Clock, int Radius)
    {
        for(int i = 0; i< (2* Radius) + 1; ++i)
        {
            for(int j = 0; j< (2 * Radius) + 1; ++j)
            {
                Console.Write(Clock[i, j]);
            }
            Console.WriteLine();
        }
    }

}
