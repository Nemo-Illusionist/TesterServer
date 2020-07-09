using System.Windows;

namespace TesterUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void login_ButtonClick(object sender, RoutedEventArgs e)
        {
         
            TestingWindow testingWindow = new TestingWindow();
            testingWindow.Show();
            EditorWindow editorWindow = new EditorWindow();
            editorWindow.Show();


        }

      
    }
}