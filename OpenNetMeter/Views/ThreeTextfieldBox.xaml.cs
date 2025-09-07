using System;
using System.Windows;

namespace OpenNetMeter.Views;

public partial class ThreeTextfieldBox : Window
{
    public string ProcessOriginal =>TextBox1.Text;
    public string ProcessName =>TextBox2.Text;
    public string ProcessPreferedName=>TextBox3.Text;

    public ThreeTextfieldBox(string id, string name, string PreferedName)
    {
        InitializeComponent();

        //original name
        TextBox1.Text = name;
        //PreferedName
        TextBox2.Text = PreferedName;

        //New PreferedName
        TextBox3.Text = PreferedName;
    }

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
        
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
