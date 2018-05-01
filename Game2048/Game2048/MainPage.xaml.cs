using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Game2048
{
    class _2048
    {
        public int[,] arrNumbers { get; set; }
        public int sizeBoard { get; set; }


        Random Rnd;
        public _2048()
        {
            Rnd = new Random();
            sizeBoard = 4;
            arrNumbers = new int[sizeBoard, sizeBoard];
        }

        private void ClearArr()
        {
            Array.Clear(arrNumbers, 0, arrNumbers.Length);
        }

        public void StartGame()
        {
            ClearArr();

            arrNumbers = new int[sizeBoard, sizeBoard];

            arrNumbers[Rnd.Next(arrNumbers.Length / sizeBoard), Rnd.Next(arrNumbers.Length / sizeBoard)] = 2;
            arrNumbers[Rnd.Next(arrNumbers.Length / sizeBoard), Rnd.Next(arrNumbers.Length / sizeBoard)] = 4;
            arrNumbers[Rnd.Next(arrNumbers.Length / sizeBoard), Rnd.Next(arrNumbers.Length / sizeBoard)] = 2;
        }
        //  //  //  //  //  //  //
        private void MoveRight()
        {
            for (int i = 0; i < sizeBoard; i++)
            {
                for (int j = 0; j < sizeBoard; j++)
                {
                    if (arrNumbers[i, j] != 0 && j + 1 < sizeBoard && arrNumbers[i, j + 1] == 0)
                    {
                        arrNumbers[i, j + 1] = arrNumbers[i, j];
                        arrNumbers[i, j] = 0;
                    }
                }
            }
        }
    }

    public partial class MainPage : ContentPage
    {
        public Label[,] arrLabels { get; set; }

        _2048 game2048;

        public MainPage()
        {
            InitializeComponent();
            game2048 = new _2048();

            getSizeBoard.SelectedIndex = 1;
        }

        private void sizeBoard_Click(object sender, EventArgs e)
        {
            int temp = GetSizeBoard();
            game2048.sizeBoard = temp;
            game2048.StartGame();
            DrawBoard(temp);
        }

        private void Btn_NewGame_Click(object sender, EventArgs e)
        {
            int temp = GetSizeBoard();
            game2048.sizeBoard = temp;
            game2048.StartGame();
            DrawBoard(temp);
        }

        private int GetSizeBoard()
        {
            switch (getSizeBoard.Items[getSizeBoard.SelectedIndex])
            {
                case "3 x 3": return 3;
                case "4 x 4": return 4;
                case "5 x 5": return 5;
                default: return 0;
            }
        }


        private void DrawBoard(int sizeBoard)
        {
            ClearBoard();
            arrLabels = new Label[sizeBoard, sizeBoard];
            for (int row = 0; row < sizeBoard; row++)
            {
                for (int collumn = 0; collumn < sizeBoard; collumn++)
                {
                    arrLabels[row, collumn] = new Label();

                    arrLabels[row, collumn].VerticalTextAlignment = TextAlignment.Center;
                    arrLabels[row, collumn].HorizontalTextAlignment = TextAlignment.Center;
                    arrLabels[row, collumn].FontSize = 30;
                    arrLabels[row, collumn].TextColor = Color.FromHex("#3F51B5");

                    if (game2048.arrNumbers[row, collumn] != 0)
                    {
                        arrLabels[row, collumn].BackgroundColor = Color.FromHex("#8BC34A");
                        arrLabels[row, collumn].Text = game2048.arrNumbers[row, collumn].ToString();

                        if (game2048.arrNumbers[row, collumn] > 2000)
                        {
                            arrLabels[row, collumn].BackgroundColor = Color.FromHex("#FFEB3B");
                            arrLabels[row, collumn].Text = game2048.arrNumbers[row, collumn].ToString("00\n000");
                            arrLabels[row, collumn].FontSize = 25;
                            arrLabels[row, collumn].TextColor = Color.FromHex("#ff3d00");
                            if (game2048.arrNumbers[row, collumn] > 100)
                            {
                                arrLabels[row, collumn].FontSize = 27;
                                if (game2048.arrNumbers[row, collumn] > 9999)
                                    arrLabels[row, collumn].FontSize = 24;
                            }
                        }
                    }

                    else
                        arrLabels[row, collumn].BackgroundColor = Color.FromHex("#607D8B");

                    Grid.SetRow(arrLabels[row, collumn], row);
                    Grid.SetColumn(arrLabels[row, collumn], collumn);
                    MyGrid.Children.Add(arrLabels[row, collumn]);
                }
            }
        }

        private void ClearBoard()
        {
            MyGrid.Children.Clear();
        }


        ///////////////////////////////////////////////
        private void OnContainerSizeChanged(object sender, EventArgs e)
        {
            View container = (View)sender;
            double width = container.Width;
            double height = container.Height;

            if (width < height)
            {
                MyStackLayout.Orientation = StackOrientation.Vertical;

                MyFrame.HeightRequest = width;
                MyFrame.WidthRequest = width;

                MyFrame.VerticalOptions = LayoutOptions.EndAndExpand;
                MyFrame.HorizontalOptions = LayoutOptions.CenterAndExpand;

                MenuFrame.HeightRequest = height - width;
            }
            else
            {
                MyStackLayout.Orientation = StackOrientation.Horizontal;

                MyFrame.HeightRequest = height;
                MyFrame.WidthRequest = height;

                MyFrame.VerticalOptions = LayoutOptions.CenterAndExpand;
                MyFrame.HorizontalOptions = LayoutOptions.EndAndExpand;

                MenuFrame.WidthRequest = width - height;
            }

            //MyStackLayout.Orientation = (width < height) ? StackOrientation.Vertical : StackOrientation.Horizontal;
        }

    }
}
