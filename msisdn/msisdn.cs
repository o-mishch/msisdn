using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace msisdn
{
    class  Msisdn
    {
        //private List<string> array = new List<string>();
        public FileInfo OpenFileInfo { get; private set; }
        public FileInfo SaveFileInfo { get; private set; }

        public List<string> Massif { get; private set; }
        public List<string> Left { get; private set; }
        public List<string> Kit { get; private set; }
        public List<string> Sequence { get; private set; }

        public bool Open(OpenFileDialog openDialog)
        {
            OpenFileInfo = new FileInfo(openDialog.FileName);
            string[] temp = File.ReadAllLines(openDialog.FileName);
            Massif = new List<string>();
            int count =0;
            bool success = true;
            foreach (string s in temp)
            {
                foreach (char c in s)
                {
                    if (!char.IsDigit(c))
                    {
                        //if (c < '0' || c > '9')
                        //return false;
                        success = false;
                        break;
                    }
                }
                if (success)
                {
                    s.Trim();
                    if (s.Length > 9 && s.Length < 13)
                        Massif.Add(s);
                }
                else
                {
                    count++;
                    success = true;
                }
            }
            if (Massif.Count > 0)
                return true;
            else
                return false;
        }

        public bool Write(SaveFileDialog saveDialog, List<string> arr)
        {
            SaveFileInfo = new FileInfo(saveDialog.FileName);
            File.WriteAllLines(saveDialog.FileName, arr);
            return true;
        }

        private List<string> SuffixSort(List<string> arr)
        {
            List<KeyValuePair<string, string>> myList = new List<KeyValuePair<string, string>>();
            List<string> temp = new List<string>();
            foreach (string value in arr)
                myList.Add(new KeyValuePair<string, string>(value.Substring(0, 10), value.Substring(10)));
            myList.Sort(delegate(KeyValuePair<string, string> x, KeyValuePair<string, string> y)
            {   //return x.Value.CompareTo(y.Value);
                if (Convert.ToInt64(x.Value) < Convert.ToInt64(y.Value))
                    return -1;
                if (Convert.ToInt64(x.Value) > Convert.ToInt64(y.Value))
                    return 1;
                if (Convert.ToInt64(x.Key) < Convert.ToInt64(y.Key))
                    return -1;
                if (Convert.ToInt64(x.Key) > Convert.ToInt64(y.Key))
                    return 1;
                return 0;
            });
            for (int i = 0; i < myList.Count; i++)
                temp.Add(myList[i].Key + myList[i].Value);
            return temp;
        }

        public void sortArray(int typeOfSort)
        {
            switch (typeOfSort)
            {
                case 1:
                    Massif.Sort();
                    break;
                case 2:
                    Massif = SuffixSort(Massif);
                    break;
            }
        }
        public void Uniq()
        {
            Massif = Massif.Distinct().ToList();
        }

        public List<string> Range(int numeric)
        {
            List<decimal> arr = new List<decimal>();
            List<string> temp = new List<string>();
            Sequence = new List<string>();
            arr = Massif.Select(x => decimal.Parse(x)).ToList();
            arr.Sort();
            arr = arr.Distinct().ToList();
            bool begin = true;
            int a;
            for (int i = 0; i < arr.Count; i++)
            {
                for (a = 1; ((a + i < arr.Count) && (arr[i] + a == arr[i + a])); a++)
                {
                    if (begin)
                    {
                        temp.Add(arr[i].ToString());
                        begin = false;
                    }
                    temp.Add(arr[i + a].ToString());
                }
                if (a >= numeric)
                    Sequence.AddRange(temp);
                begin = true;
                temp.Clear();
            }
            //Sequence = Sequence.Distinct().ToList();
            return Sequence;
        }

        public void Kits(int numeric)
        {
            List<string> arr = new List<string>();
            List<KeyValuePair<string, int>> myList = new List<KeyValuePair<string, int>>();
            Kit = new List<string>();
            Left = new List<string>();
            arr = SuffixSort(Massif).Distinct().ToList();
            string suffix = arr[0].ToString().Substring(10);
            int count, a, max;
            int[] s = new int[arr.Count];
            foreach (string value in arr)
                myList.Add(new KeyValuePair<string, int>(value.Substring(0, 10), Convert.ToInt32(value.Substring(10))));
            for (int i = 0; i < myList.Count; i++)
                s[i]=(myList[i].Value);
            max = s.Max();
            bool stop = false;
            myList = null;
            s = null;
            if (numeric > arr.Count && arr.Count > 100) numeric = arr.Count/100;
            for (int i = 0; i < arr.Count;i++)
            {
                if ((arr[i].Substring(10) == suffix) && (stop == false))// && (count < arr.Count))
                {
                    count = i + numeric / 100;
                    while ((i < count) && (i < arr.Count))
                    {
                        Kit.Add(arr[i]);
                        i++;
                    }
                    i--;
                    a = 1;
                    if ((i + a) < arr.Count)
                    {
                        while (Convert.ToInt32(arr[i + a].Substring(10)) <= Convert.ToInt32(suffix))
                        {
                            if (Convert.ToInt32(arr[i + a].Substring(10)) == max)
                            {
                                stop = true;
                                break;
                            }
                            a++;
                        }
                        suffix = string.Format("{0:00}", arr[i + a].Substring(10));
                    }
                }
                else
                {
                    Left.Add(arr[i]);
                }
            }
            Left = SuffixSort(Left);
            Kit = SuffixSort(Kit);
        }
    }
}
