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
    /// Логика взаимодействия для PageGril.xaml
    /// </summary>
    public partial class PageGril : Page
    {
        public PageGril()
        {
            InitializeComponent();
            DtGridGril.ItemsSource = PR14Entities.GetContext().GRIL.ToList();
        }

        

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
                var tovarForRemoning = DtGridGril.SelectedItems.Cast<GRIL>().ToList();
                if (MessageBox.Show($"Вы точно хотите удалить следующее {tovarForRemoning.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        PR14Entities.GetContext().GRIL.RemoveRange(tovarForRemoning);
                        PR14Entities.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены");

                        DtGridGril.ItemsSource = PR14Entities.GetContext().GRIL.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageGrilAdd(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.Navigate(new PageGrilAdd((sender as Button).DataContext as GRIL));
        }
    }
    }
