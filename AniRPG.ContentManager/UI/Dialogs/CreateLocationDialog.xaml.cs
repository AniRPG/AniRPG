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
using System.Windows.Shapes;

namespace AniRPG.ContentManager.UI.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для CreateLocationDialog.xaml
    /// </summary>
    public partial class CreateLocationDialog : Window
    {
        public string LocationName => LocationNameTextBox.Text;

        public CreateLocationDialog()
        {
            InitializeComponent();
        }

        private void AcceptButtonClickedHandler(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
