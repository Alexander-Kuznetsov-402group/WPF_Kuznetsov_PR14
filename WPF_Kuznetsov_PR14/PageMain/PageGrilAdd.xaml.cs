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
using WPF_Kuznetsov_PR14.ApplicationData;

namespace WPF_Kuznetsov_PR14.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageGrilAdd.xaml
    /// </summary>
    public partial class PageGrilAdd : Page
    {
        private GRIL _currentGril = new GRIL();

        public PageGrilAdd(GRIL selectedGril)
        {
            InitializeComponent();

            if (selectedGril != null)
                _currentGril = selectedGril;

            DataContext = _currentGril;
            ComboTip.ItemsSource = PR14Entities.GetContext().Tip.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentGril.naimenov))
                errors.AppendLine("Укажите название товара");
            if (_currentGril.kolichestvo <= 0)
                errors.AppendLine("Количество товара не может быть меньше или равно 0");
            if (_currentGril.cena <= 0)
                errors.AppendLine("Цена не может быть меньше или равно 0");
            if (_currentGril.Tip == null)
                errors.AppendLine("Выберите страну");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //добавление
            if (_currentGril.id == 0)
                PR14Entities.GetContext().GRIL.Add(_currentGril);

            //обработаем вариант выпада/записи данных
            try
            {
                PR14Entities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                AppFrame.FrameMain.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}