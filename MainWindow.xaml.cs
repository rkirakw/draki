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
using System.Text.Json;
using System.IO;

namespace gdm_n0cl1p_check_ml9_th1s_m0m3nt_d3aD {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    
    public class Question {
        public string question { get; set; }

        public string yes { get; set; }
        public Question postY { get; set; }
        
        public string no { get; set; }
        public Question postN { get; set; }
        
        public string maybe { get; set; }
        public Question postM { get; set; }
    }



    public partial class MainWindow : Window {
        Question start;
        Question current;
        Question prev;

        public MainWindow() {
            InitializeComponent();

            start = Init();
            SetScene(start);
        }


        public void SetScene(Question que){
            text.Text = que.question;
            yButton.Content = que.yes;
            yButton.Visibility = que.yes == null ? Visibility.Hidden : Visibility.Visible;

            nButton.Content = que.no;
            nButton.Visibility = que.no == null ? Visibility.Hidden : Visibility.Visible;

            mButton.Content = que.maybe;
            mButton.Visibility = que.maybe == null ? Visibility.Hidden : Visibility.Visible;

            prev = current;
            current = que;
        }

        public Question Init() {
            string json = File.ReadAllText("text.json");
            Question list = JsonSerializer.Deserialize<Question>(json);
            return list;
        }

        public void SelectButton(object sender, RoutedEventArgs e) {
            string button = (e.OriginalSource as Button).Name;
            Question next = button == "yButton" ? current.postY : button == "nButton" ? current.postN : current.postM;

            if (next.question.Contains("<back>")) {
                SetScene(prev);
                return;
            }
            else if (next.question.Contains("<start>")) {
                SetScene(start);
                return;
            }

            SetScene(next);
        }
    }
}
