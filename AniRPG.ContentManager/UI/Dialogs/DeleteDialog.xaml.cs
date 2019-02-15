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
    /// Логика взаимодействия для DeleteDialog.xaml
    /// </summary>
    public partial class DeleteDialog : Window
    {
        public DeleteDialog(string deleteWarningMessage = "Вы точно хотите удалить?")
        {
            InitializeComponent();

            MessageTextBox.Text = deleteWarningMessage;
        }

        private void AcceptButtonClickedHandler(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
