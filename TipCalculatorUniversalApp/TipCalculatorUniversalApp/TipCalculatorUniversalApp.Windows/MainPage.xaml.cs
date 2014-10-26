using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TipCalculatorUniversalApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Tip _tip;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void BillAmountTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            BillAmountTextBox.Text = _tip.BillAmount.ToString("C");
        }

        private void BillAmountTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            PeformCalculation();
        }

        private void BillAmountTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            BillAmountTextBox.Text = string.Empty;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            PeformCalculation();
        }

        private void PeformCalculation()
        {
            decimal amount;
            decimal.TryParse(BillAmountTextBox.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out amount);
            var percent = Convert.ToDouble(StackPanel.Children.OfType<RadioButton>().First(x => x.IsChecked == true).Tag);
            _tip = new Tip(amount, percent);
            ShowResult();
        }

        private void ShowResult()
        {
            TipAmountTextBlock.Text = _tip.TipAmount.ToString("C", CultureInfo.CurrentCulture);
            TotalAmountTextBlock.Text = _tip.TotalAmount.ToString("C", CultureInfo.CurrentCulture);
        }
    }
}
