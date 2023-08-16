using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace KeyboardTypingTrainer
{
   
    public partial class MainWindow : Window
    {
       
        int fails = 0; // количество ошибок
        Random rendStr = new Random();
        bool flagCaps = true; // sensitive
        bool flagBackspase = true;
        bool mesStop = false;
        bool levelFinished = false;
        bool info = false;
       
        DispatcherTimer total = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;

        int totaltemptimer = 0;
        int levelFinishedCount = 0;
        public MainWindow()
        {
            InitializeComponent();
        
            total.Tick += new EventHandler(totalTimer_Tick);
            total.Interval = TimeSpan.FromSeconds(1);
        }

        

        private void CapitalLetters() // заглавные буквы
        {
            this.Q.Content = "Q";
            this.W.Content = "W";
            this.E.Content = "E";
            this.R.Content = "R";
            this.T.Content = "T";
            this.Y.Content = "Y";
            this.U.Content = "U";
            this.I.Content = "I";
            this.O.Content = "O";
            this.P.Content = "P";
            this.A.Content = "A";
            this.S.Content = "S";
            this.D.Content = "D";
            this.F.Content = "F";
            this.G.Content = "G";
            this.H.Content = "H";
            this.J.Content = "J";
            this.K.Content = "K";
            this.L.Content = "L";
            this.Z.Content = "Z";
            this.X.Content = "X";
            this.C.Content = "C";
            this.V.Content = "V";
            this.B.Content = "B";
            this.N.Content = "N";
            this.M.Content = "M";
        }
        private void LoverLetters() //строчные буквы
        {
            this.Q.Content = "q";
            this.W.Content = "w";
            this.E.Content = "e";
            this.R.Content = "r";
            this.T.Content = "t";
            this.Y.Content = "y";
            this.U.Content = "u";
            this.I.Content = "i";
            this.O.Content = "o";
            this.P.Content = "p";
            this.A.Content = "a";
            this.S.Content = "s";
            this.D.Content = "d";
            this.F.Content = "f";
            this.G.Content = "g";
            this.H.Content = "h";
            this.J.Content = "j";
            this.K.Content = "k";
            this.L.Content = "l";
            this.Z.Content = "z";
            this.X.Content = "x";
            this.C.Content = "c";
            this.V.Content = "v";
            this.B.Content = "b";
            this.N.Content = "n";
            this.M.Content = "m";

        }

        private void CapitalSymbol()
        {
            this.apostrof.Content = "~";
            this.N1.Content = "!";
            this.N2.Content = "@";
            this.N3.Content = "#";
            this.N4.Content = "$";
            this.N5.Content = "%";
            this.N6.Content = "^";
            this.N7.Content = "&";
            this.N8.Content = "*";
            this.N9.Content = "(";
            this.N0.Content = ")";
            this.minus.Content = "_";
            this.ravno.Content = "+";
            this.OemOpenBrackets.Content = "{";
            this.OemCloseBrackets.Content = "}";
            this.Oem1.Content = ":";
            this.Oem1Quotes.Content = "\"";
            this.OemComma.Content = "<";
            this.OemPeriod.Content = ">";
            this.rightslash.Content = "?";
        }
        private void LoverSymbol()
        {
            this.apostrof.Content = "`";
            this.N1.Content = "1";
            this.N2.Content = "2";
            this.N3.Content = "3";
            this.N4.Content = "4";
            this.N5.Content = "5";
            this.N6.Content = "6";
            this.N7.Content = "7";
            this.N8.Content = "8";
            this.N9.Content = "9";
            this.N0.Content = "0";
            this.minus.Content = "-";
            this.ravno.Content = "=";
            this.OemOpenBrackets.Content = "[";
            this.OemCloseBrackets.Content = "]";
            this.Oem1.Content = ";";
            this.Oem1Quotes.Content = "'";
            this.OemComma.Content = ",";
            this.OemPeriod.Content = ".";
            this.rightslash.Content = "/";
        }

        void Speed() //скорость печати
        {  
            SpeedChar.Content = Math.Round(((double)InputedText.Text.Length / totaltemptimer) * 60).ToString();
        }

       

        private void totalTimer_Tick(object sender, EventArgs e)
        {

            totaltemptimer++;
            Speed();

            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                TotalTime.Content = currentTime;
            }

        }


        private void Start_Click(object sender, RoutedEventArgs e) //кнопка старта
        {
            if(levelFinishedCount==0)
            {
                MessageBox.Show("1 Уровень");
            }
            else if(levelFinishedCount==1)
            {
                MessageBox.Show("2 Уровень");
            }
            else if (levelFinishedCount==2)
            {
                MessageBox.Show("3 Уровень");
            }
            else
            {
                MessageBox.Show("Вы прошли все уровни!!");
                
            }
            total.Start();
            sw.Start();
            start.IsEnabled = false; //кнопка старт становится неактивной
            stop.IsEnabled = true; //кнопка стоп становится активной

            CreateText();
            InputedText.IsReadOnly = false;
            InputedText.IsEnabled = true;
            InputedText.Focus();
        }














        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if(InputedText.Text.Length < generatedText.Text.Length)
            {
                levelFinished = false;
                MessageBox.Show("Досрочное завершение\nТекст напечатан не полностью");
            }

            if (sw.IsRunning)
            {
                sw.Stop();
            }

            if (fails == 0 && InputedText.Text.Length==generatedText.Text.Length)
            {
                levelFinishedCount++;
                
            }
            else { MessageBox.Show("Чтобы перейти на следующий уровень, текст должен быть без ошибок");
                levelFinished = false;
            }


            //if(levelFinished==true)
            //{
            //    if(info == false)
            //    {
            //        MessageBox.Show($"Количество ошибок: {Fails.Content.ToString()}\nСкорость печати: {SpeedChar.Content.ToString()} сим\\мин\nПолное время: {TotalTime.Content.ToString()}");
            //    }
            //}
                
            
            sw.Reset();
            TotalTime.Content = "00:00";
            start.IsEnabled = true;
            stop.IsEnabled = false;
            InputedText.Text = "";
            generatedText.Text = "";
            Fails.Content = 0;
            SpeedChar.Content = 0;
            InputedText.IsReadOnly = true;
            InputedText.IsEnabled = false;
            fails = 0;

  
        }

        














        
        private void CreateText()
        {
            List<string> list = new List<string>();

            string firstLevel = "Сложнее всего начать действовать, все остальное зависит только от упорства...";
            string secondLevel = "Когда мне было 5 лет, мама всегда говорила, что главное в жизни – счастье. Когда я пошел в школу, на вопрос, кем я хочу быть, когда вырасту, я ответил “счастливым человеком”. Мне тогда сказали, что я не понимаю вопроса, а я ответил, что это они не понимают жизни...";
            string thirdLevel = "Если ты хочешь построить корабль, не надо созывать людей, планировать, делить работу, доставать инструменты. Надо заразить людей стремлением к бесконечному морю. Тогда они сами построят корабль...";

          




            if (levelFinishedCount == 0)
            {
                list.Add(firstLevel);
            }
            else if (levelFinishedCount == 1)
            {
                list.Add(secondLevel);
            }
            else if (levelFinishedCount == 2)
            {
                list.Add(thirdLevel);
            }
            else
            {
                MessageBox.Show("Вы прошли все уровни!!");
            }






            foreach (var item in list)
            {
                generatedText.Text += item;
            }



        }




        private void inputedText_TextChanged(object sender, TextChangedEventArgs e) // обработка изменения текста
        {
            int tempmaxinputedtextlenght = Convert.ToInt32(generatedText.Text.Length);

            if(InputedText.Text.Length<=tempmaxinputedtextlenght)
            {

                if (InputedText.Text.Length == generatedText.Text.Length && fails<20)
                {
                    string temptotaltimecontent = TotalTime.Content.ToString();
                    string tempfailscontent = Fails.Content.ToString();
                    string tempspeedcharcontent = SpeedChar.Content.ToString();
                    
                    levelFinished = true;
                    MessageBox.Show("Это победа");
                    if(info==false)
                    {
                        MessageBox.Show($"Количество ошибок: {tempfailscontent}\nСкорость печати: {tempspeedcharcontent} сим\\мин\nПолное время: {temptotaltimecontent}");
                    }
                    info = true;
                    sw.Reset();
                    TotalTime.Content = "00:00";
                    start.IsEnabled = true;
                    stop.IsEnabled = false;
                    InputedText.Text = "";
                    generatedText.Text = "";
                    Fails.Content = 0;
                    SpeedChar.Content = 0;
                    InputedText.IsReadOnly = true;
                    InputedText.IsEnabled = false;
                    fails = 0;

                }
                else if(InputedText.Text.Length == generatedText.Text.Length)
                {
                    levelFinished = false;
                    sw.Reset();
                    TotalTime.Content = "00:00";
                    start.IsEnabled = true;
                    stop.IsEnabled = false;
                    InputedText.Text = "";
                    generatedText.Text = "";
                    Fails.Content = 0;
                    SpeedChar.Content = 0;
                    InputedText.IsReadOnly = true;
                    InputedText.IsEnabled = false;
                    fails = 0;
                 
                }

                string str = generatedText.Text.Substring(0, InputedText.Text.Length);
                if (InputedText.Text.Equals(str))
                {
                    if (flagBackspase)
                    {
                        Speed();
                    }
                    InputedText.Foreground = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    if (flagBackspase)
                    {
                        fails++;
                    }
                    InputedText.Foreground = new SolidColorBrush(Colors.Red);
                    Fails.Content = fails;
                }

                if (InputedText.Text.Length == generatedText.Text.Length)
                {
                    if (fails == 0)
                    {
                        string temptotaltimecontent = TotalTime.Content.ToString();
                        string tempfailscontent = Fails.Content.ToString();
                        string tempspeedcharcontent = SpeedChar.Content.ToString();

                        sw.Reset();
                        TotalTime.Content = "00:00";
                        start.IsEnabled = true;
                        stop.IsEnabled = false;
                        InputedText.Text = "";
                        generatedText.Text = "";
                        Fails.Content = 0;
                        SpeedChar.Content = 0;
                        InputedText.IsReadOnly = true;
                        InputedText.IsEnabled = false;
                        fails = 0;
                        if(levelFinished==true)
                        {
                            string lvlname = "";
                            string templvl = "";

                            if (levelFinishedCount == 0)
                            {
                                templvl = "1";
                            }
                            else if (levelFinishedCount == 1)
                            {
                                templvl = "2";
                            }
                            else if (levelFinishedCount == 2)
                            {
                                templvl = "3";
                            }




                            MessageBox.Show($"Уровень: {templvl} пройден!");
                            if(info==false)
                            {
                                MessageBox.Show($"Количество ошибок: {tempfailscontent}\nСкорость печати: {tempspeedcharcontent} сим\\мин\nПолное время: {temptotaltimecontent}");

                            }
                            levelFinishedCount++;

                        }
                        else
                        {
                            MessageBox.Show("Пройдите уровень заново");
                        }

                    }
                }
            }
            else
            {
                sw.Reset();
                TotalTime.Content = "00:00";
                start.IsEnabled = true;
                stop.IsEnabled = false;
                InputedText.Text = "";
                generatedText.Text = "";
                Fails.Content = 0;
                SpeedChar.Content = 0;
                InputedText.IsReadOnly = true;
                InputedText.IsEnabled = false;
                fails = 0;

                MessageBox.Show("вы вышли из диапазона");
                
            }
                
        

            
        }



        private void InputedText_KeyDown(object sender, KeyEventArgs e)
        {
            List<UIElement> GetAllChildren(Panel c)
            {
                List<UIElement> list = c.Children.Cast<UIElement>().ToList();
                foreach (var elem in list.OfType<Grid>())
                    list.AddRange(GetAllChildren(elem));
                return list;
            }


            foreach (var item in GetAllChildren(buttonGrid).OfType<Button>())
            {
                if ((item as Button).Name == e.Key.ToString())
                {
                    if (e.Key.ToString() == "LeftShift" || e.Key.ToString() == "RightShift")
                    {
                        CapitalSymbol();
                        if (flagCaps)
                        {
                            CapitalLetters();
                        }
                        else
                        {
                            LoverLetters();
                        }
                    }
                    else if (e.Key.ToString() == "Capital")
                    {
                        if (flagCaps)
                        {
                            CapitalLetters();
                            flagCaps = false;
                        }
                        else
                        {
                            LoverLetters();
                            flagCaps = true;
                        }
                    }
                    else if (e.Key.ToString() == "Back")
                    {
                        flagBackspase = false;
                    }
                    else
                    {
                        flagBackspase = true;
                    }
                }
            }

        }
    
    }
}
