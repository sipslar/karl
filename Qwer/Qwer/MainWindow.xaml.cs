using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Numerics;
namespace Qwer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string alphabetSt = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ 0123456789";

        public MainWindow()
        {
            InitializeComponent();
        }
        void Cor(out BigInteger  p, out BigInteger q, out BigInteger e1, out BigInteger d, out BigInteger n, out BigInteger Fn)
        {
            if (!BigInteger.TryParse(text_p.Text, out p))
            {
                throw new Exception("Неправильне число ");
            }
            if (!BigInteger.TryParse(text_q.Text, out q))
            {
                throw new Exception("Неправильне число ");
            }
            if (!BigInteger.TryParse(text_e.Text, out e1))
            {
                throw new Exception("Неправильне число ");
            }

            if (!BigInteger.TryParse(text_d.Text, out d))
            {
                d = 0;
            }
             Fn = ((p - 1) * (q - 1));
             n = p * q;
            if (e1 < 0 || e1 > n || BigInteger.GreatestCommonDivisor(e1, Fn) != 1)
            {
                throw new Exception("Неправильне число e");
            }
            if (d == 0)
            {
                Generation(p, q, e1, ref d, Fn);
                text_d.Text = d.ToString();
            }
        }
        private void Cr_but_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BigInteger p, q, e1, d = 0,n,Fn;
                Cor(out  p, out  q, out  e1, out  d, out  n, out  Fn);
                int[] text = St2Ints(text_in.Text);
                var rez = Encryption(text, n, e1);
                text_out.Text = string.Join(" ", rez);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void De_but_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BigInteger p, q, e1, d = 0, n, Fn;
                Cor(out p, out q, out e1, out d, out n, out Fn);
                int[] text = St2Ints(text_in.Text);
                var rez = Decryption(text, n, d);
                text_out.Text = string.Join(" ", rez);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Generation(BigInteger p, BigInteger q, BigInteger e, ref BigInteger d, BigInteger Fn)
        {
            BigInteger dn;
            int k = 0;
            do
            {
                k++;
                dn = (k * Fn + 1) % e;

            } while (dn != 0);
            d = (k * Fn + 1) / e;
        }
        public static int[] Encryption(int[] mas, BigInteger n, BigInteger e)
        {
            int[] CT = new int[mas.Length];
            for (int i = 0; i < CT.Length; i++)
            {
                CT[i] = (int)BigInteger.ModPow(mas[i], e, n);
            }
            return CT;
        }
        public static int[] Decryption(int[] mas, BigInteger n, BigInteger d)
        {
            int[] CT = new int[mas.Length];
            for (int i = 0; i < CT.Length; i++)
            {
                CT[i] = (int)BigInteger.ModPow(mas[i], d, n);
            }
            return CT;
        }
        static int[] St2Ints(string text)
        {
            string[] mas_text = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int[] mas = new int[mas_text.Length];
            for (int i = 0; i < mas.Length; i++)
            {
                mas[i] = int.Parse(mas_text[i]);
            }
            return mas;
        }
        static int[] Convert(string text)
        {
            string KEY_U = text.ToUpper().Replace("_", " ");
            int Length = text.Length;
            int[] mas = new int[Length];
            for (int i = 0; i < Length; i++)
            {
                mas[i] = alphabetSt.IndexOf(KEY_U[i]) + 1;
            }
            return mas;
        }
        static char[] Convert(int[] text)
        {
            int Length = text.Length;
            char[] mas = new char[Length];
            for (int i = 0; i < Length; i++)
            {
                if (text[i] - 1 >= alphabetSt.Length)
                {
                    throw new Exception("Помилка");
                }
                mas[i] = alphabetSt[text[i] - 1];
            }
            return mas;
        }
        static BigInteger Euler(BigInteger p, BigInteger q)
        {
            BigInteger result = ((p - 1) * (q - 1));
            return result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var rez = Convert(text_in.Text);
            text_in.Text = string.Join(" ", rez);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var rez = St2Ints(text_in.Text);
            var text = Convert(rez);
            text_in.Text = new string(text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var rez = Convert(text_out.Text);
            text_out.Text = string.Join(" ", rez);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var rez = St2Ints(text_out.Text);
            var text = Convert(rez);
            text_out.Text = new string(text);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            BigInteger a = BigInteger.Parse(text_e_a.Text);
            BigInteger b = BigInteger.Parse(text_e_b.Text);
            BigInteger rez = Euler(a, b);
            text_e_out.Text = rez.ToString();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            BigInteger a = BigInteger.Parse(t1.Text);
            BigInteger b = BigInteger.Parse(t2.Text);
            BigInteger rez = a% b;
            tv.Text = rez.ToString();
        }

        private void Ha_but_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BigInteger p, q, e1, d = 0, n, Fn;
                Cor(out p, out q, out e1, out d, out n, out Fn);
                int[] text = St2Ints(text_in.Text);
                BigInteger H = BigInteger.Parse(text_h.Text);
                var Hash = Hash_cr( n,text,H);
                var S = BigInteger.ModPow(Hash, d, n);
                
                text_out.Text = string.Format("Hash - {0}\n S - {1}", Hash,S);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static BigInteger Hash_cr( BigInteger n, int[] m, BigInteger H)
        {
           
            int length = m.Length;
            for (int i = 0; i < length; i++)
            {
                H = BigInteger.ModPow(H + m[i], 2, n);
            }
            return H;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            BigInteger ch = BigInteger.Parse(ch1.Text);
            BigInteger st = BigInteger.Parse(st1.Text);
            BigInteger mod = BigInteger.Parse(md1.Text);
            BigInteger rez = BigInteger.ModPow(ch, st, mod);
            te_r.Text = rez.ToString();
        }
        public static int[] HamEncryption(int[] text, int[] gamma)
        {
            int[] mas = new int[text.Length];
            int length = mas.Length;
            int N = alphabetSt.Length;
            int L = gamma.Length;
            for (int i = 0; i < length; i++)
            {
                mas[i] = (text[i] + gamma[i % L]) % N;
                if (mas[i] == 0)
                {
                    mas[i] = N;
                }
            }
            return mas;
        }
        public static int[] HamDecryption(int[] text, int[] gamma)
        {
            int[] mas = new int[text.Length];
            int length = mas.Length;
            int N = alphabetSt.Length;
            int L = gamma.Length;
            for (int i = 0; i < length; i++)
            {
                mas[i] = (text[i] - gamma[i % L] + N) % N;
                if (mas[i] == 0)
                {
                    mas[i] = N;
                }
            }
            return mas;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            string text = in_ham.Text;
            string key =text_ham.Text;
            var rez = HamEncryption(Convert(text), Convert(key));
            out_ham.Text =new string( Convert(rez));
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            string text = in_ham.Text;
            string key = text_ham.Text;
            var rez = HamDecryption(Convert(text), Convert(key));
            out_ham.Text = new string(Convert(rez));
        }    
    }
}
