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
using System.Windows.Threading;

namespace MemoriaJatek
{
	/// <summary>
	/// Interaction logic for Jatek.xaml
	/// </summary>
	public partial class Jatek : Window
	{
		public int numberOfRows = MainWindow.numOfRows;
		public List<Button> buttons = new List<Button>();
		public List<Button> buttons2 = new List<Button>();
		public List<int> numbers = new List<int>();
		public int[] checkNumbers = new int[2];
		public List<Button> hiddenButtons = new List<Button>();
		public int index = 0;
		public Random rnd = new Random();
		public TextBlock tblock = new TextBlock();
		public List<int> buttonContents = new List<int>();
		public int points = 0;
		DispatcherTimer timer = new DispatcherTimer();
		public int counter = 0;
		
		

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
			SecondLayerButtonSetting(numOfRows);
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
					btn.Background = Brushes.Cyan;
					Grid.SetRow(btn, i);
					Grid.SetColumn(btn, j);
					grd.Children.Add(btn);
					buttons.Add(btn);
				}
			}
		}

		public void SecondLayerButtonSetting(int numOfRows)
		{
			for (int row = 0; row < numOfRows; row++)
			{
				for (int col = 0; col < numOfRows; col++)
				{
					Button btn2 = new Button();
					btn2.FontSize = 30;
					btn2.Click += Btn_Click;
					btn2.Content = "";
					Grid.SetRow(btn2, row);
					Grid.SetColumn(btn2, col);
					grd.Children.Add(btn2);
					buttons2.Add(btn2);

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
			button.Visibility = Visibility.Hidden;
			hiddenButtons.Add(button);
			int row = Grid.GetRow(button);
			int col = Grid.GetColumn(button);

			Button btnContent = buttons.Find(btn => Grid.GetRow(btn) == row && Grid.GetColumn(btn) == col);
			int number = int.Parse(btnContent.Content.ToString());

			checkNumbers[index] = number;
			if (index < 2)
			{
				if (hiddenButtons.Count == 2)
				{
					foreach (Button item1 in hiddenButtons)
					{
						foreach (Button item2 in buttons2)
						{
							if (item1 != item2)
							{
								item2.IsEnabled = false;
							}
						}
					}
				}


				if (checkNumbers[0] != 0 && checkNumbers[1] != 0)
				{
					timer.Interval = TimeSpan.FromSeconds(1);
					timer.Tick += (s, args) =>
					{
						counter++;
						if (checkNumbers[0] != checkNumbers[1])
						{
							if (counter == 1)
							{
								timer.Stop();
								foreach (Button btn in hiddenButtons)
								{
									btn.Visibility = Visibility.Visible;

								}
								foreach (Button btn in buttons2)
								{
									btn.IsEnabled = true;
								}
								hiddenButtons.Clear();
								counter = 0;
							}
						}
						else
						{
							timer.Stop();
							foreach (Button btn in hiddenButtons)
							{
								foreach (Button btn2 in buttons2)
								{
									if (btn == btn2)
									{
										buttons2.Remove(btn);
										points++;
										tblock.Text = $"\n\tPontok: {points / 2}";
										return;
									}
								}
							}
							foreach (Button btn in buttons2)
							{
								btn.IsEnabled = true;
							}

							hiddenButtons.Clear();
							counter = 0;
						}
					};
					timer.Start();
				}

				index++;
			}

			if (index == 2)
			{
				index = 0;
			}


		}
		
	}
}
