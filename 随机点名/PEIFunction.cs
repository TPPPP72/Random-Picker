using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace 随机点名
{
    public static class PEIFunction
    {
        public static bool Isnumber(string s)
        {
            foreach (var item in s)
                if (char.IsDigit(item) == false)
                    return false;
            return true;
        }

        public static ImageBrush GetBrush(string s)
        {
            ImageBrush imageBrush = new()
            {
                ImageSource = new BitmapImage(new Uri(s))
            };
            return imageBrush;
        }

        public static List<Tuple<int, int>> GetRanges(int size)
        {
            List<Tuple<int, int>> result = [];
            List<int> prob = [20, 30];
            int start = 0;
            for (int i = 0; i < 2; i++)
            {
                int step = size * prob[i] / 100 - 1;
                result.Add(new Tuple<int, int>(start, start + step));
                start += step + 1;
            }
            result.Add(new Tuple<int, int>(start, size - 1));
            return result;
        }

        public static int QueryQuality(int n, List<Tuple<int, int>> ranges)
        {
            for (int i = 0; i < 3; i++)
                if (n >= ranges[i].Item1 && n <= ranges[i].Item2)
                    return i;
            return 0;
        }

        public static int GetUser(List<Userdata> datas)
        {
            int Maxtime = datas.Last().TIME;
            List<Tuple<int, int>> pranges = [];
            int start = 1;
            foreach (var i in datas)
            {
                int step = (Maxtime - i.TIME + 1) * (Maxtime - i.TIME + 1);
                pranges.Add(new Tuple<int, int>(start, start + step));
                start += step + 1;
            }
            Random rand = new();
            int index = rand.Next(1, start);
            for (int i = 0; i < pranges.Count; i++)
                if (index >= pranges[i].Item1 && index <= pranges[i].Item2)
                    return i;
            return 0;
        }
    }
}
