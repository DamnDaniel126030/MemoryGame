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

namespace MemoriaJatek
{
	/// <summary>
	/// Interaction logic for Jatek.xaml
	/// </summary>
	public partial class Jatek : Window
	{
		int numberOfRows = MainWindow.numOfRows;
		List<Button> buttons = new List<Button>();
		List<int> numbers = new List<int>();
		Random rnd = new Random();
		TextBlock tblock = new TextBlock();
		List<int> buttonContents = new List<int>();
		int points = 0;

		public Jatek()
		{
			InitializeComponent();
			AddNumbers();
			GridSetting(numberOfRows);
		}


		public void GridSetting(int numOfRows)
		{
			for (int i = 0; i < numOfRows; i++)
			{
				grd.RowDefinitions.Add(new RowDefinition());
			}
			grd.RowDefinitions.Add(new RowDefinition());
			tblock.FontSize = 15;
			tblock.Text = "\n\tPontok: ";
			Grid.SetRow(tblock, numOfRows + 1);
			grd.Children.Add(tblock);
			for (int i = 0; i < numOfRows; i++)
			{
				grd.ColumnDefinitions.Add(new ColumnDefinition());
			}
			ButtonSetting(numOfRows);
		}

		public void ButtonSetting(int numOfRows)
		{
			for(int i = 0; i < numOfRows; i++)
			{
				for (int j = 0; j < numOfRows; j++)
				{
					Button btn = new Button();
					btn.FontSize = 30;
					btn.Content = AddButtonContent();
					btn.Click += Btn_Click;
					Grid.SetRow(btn, i);
					Grid.SetColumn(btn, j);
					grd.Children.Add(btn);
					buttons.Add(btn);
				}
			}
		}

		public void AddNumbers()
		{
			int nums = numberOfRows * numberOfRows / 2;
			for (int i = 1; i <= nums; i++)
			{
				numbers.Add(i);
				numbers.Add(i);
			}
			numbers.OrderBy(i => rnd.Next()).ToList();
		}
		public int AddButtonContent()
		{
			int randomNumber = numbers[rnd.Next(0, numbers.Count)];
			numbers.Remove(randomNumber);
			return randomNumber;
		}

		public void Btn_Click(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;
			buttonContents.Add(int.Parse(button.Content.ToString()));	
			if (buttonContents.Count == 2)
			{
				if (buttonContents[0] == buttonContents[1])
				{
					points++;
					tblock.Text = $"\n\tPontok: {points}";
				}
				buttonContents.Clear();
			}

		}
		
	}
}
