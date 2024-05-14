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

namespace MemoriaJatek
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static int numOfRows = 4;
		public MainWindow()
		{
			InitializeComponent();
			lboxInit();
		}
		public void lboxInit()
		{
			for (int i = 4; i < 9; i += 2)
			{
				lbox_rownumber.Items.Add(i);
			}
		}


		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Jatek jatek = new Jatek();
			jatek.Show();
			this.Hide();

		}

		private void lbox_rownumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			tbox_selected.Text = "Kiválasztott tábla: ";
			numOfRows = int.Parse(lbox_rownumber.SelectedItem.ToString());
			tbox_selected.Text += $"{numOfRows}x{numOfRows}";
		}
	}
}
