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
using RecieptGenerator.DataModels;

namespace RecieptGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bill bill = new Bill();
        int clickCount = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            if (clickCount == 0)
            {
                bill.startBinding(txtName.Text.ToString(), txtMobile.Text.ToString());
                txtName.IsEnabled = false;
                txtMobile.IsEnabled = false;
                clickCount++;
            }
            try
            {
                bill.addItem(txtItem.Text.ToString(), int.Parse(txtPrice.Text), int.Parse(txtQuantity.Text));
                dataGridItems.Items.Add(bill.itemList.Last());
                txtItem.Text = "";
                txtPrice.Text = "";
                txtQuantity.Text = "";
            }
            catch
            {
                MessageBox.Show("Quantity and price should be a number");
            }
            

        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            bill.generatePDF();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var srno = new DataGridTextColumn();
            var item = new DataGridTextColumn();
            var price = new DataGridTextColumn();
            var quantity = new DataGridTextColumn();
            var amount = new DataGridTextColumn();
            srno.Header = "Srno.";
            srno.Binding = new Binding("Key");
            item.Header = "Item Name";
            item.Binding = new Binding("Value.name");
            price.Header = "Price";
            price.Binding = new Binding("Value.price");
            quantity.Header = "Quantity";
            quantity.Binding = new Binding("Value.quantity");
            amount.Header = "Amount";
            amount.Binding = new Binding("Value.amount");
            dataGridItems.Columns.Add(srno);
            dataGridItems.Columns.Add(item);
            dataGridItems.Columns.Add(price);
            dataGridItems.Columns.Add(quantity);
            dataGridItems.Columns.Add(amount);

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtName.IsEnabled = true;
            txtMobile.IsEnabled = true;
            txtName.Text = "";
            txtMobile.Text = "";
            txtItem.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
            dataGridItems.Items.Clear();
        }
    }
}
