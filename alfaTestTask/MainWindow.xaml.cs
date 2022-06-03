using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using static alfaTestTask.ReadExcel;
using static alfaTestTask.SeleniumFirefox;
using static alfaTestTask.SeleniumEdge;
using static alfaTestTask.SeleniumChrome;

namespace alfaTestTask
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// It's special enum for InteractionWithData - we choose, what we wanna do with data - shows or use
        /// </summary>
        public enum UserChoose
        {
            CheckTable,
            /// <summary>
            /// Shows data of Excel in DataGrid
            /// </summary>
            CallFirefox,
            /// <summary>
            /// Call Firefox browser
            /// </summary>
            CallChrome,
            /// <summary>
            /// Call Google Chrome browser
            /// </summary>
            CallEdge
            /// <summary>
            /// Call Microsoft Edge browser
            /// </summary>
        }


        static public void InteractionWithData(DataGrid ExcelGrid, UserChoose choose)
        {
            MessageBox.Show("Пожалуйста, выберите скачанный файл .xls or .xlsx формата скачанного с https://rpachallenge.com.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information); //
            OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file
            if (file.ShowDialog() == true) //if there is a file chosen by the user
            {
                string fileExt = System.IO.Path.GetExtension(file.FileName); //get the file extension
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        ExcelFile rpaExcelFile = new ExcelFile(file.FileName, 11, 7);
                        if (choose == UserChoose.CheckTable) rpaExcelFile.SetData(rpaExcelFile.DataFromExcelFile, ExcelGrid);
                        else if (choose == UserChoose.CallFirefox) RPAchallengeFirefox(rpaExcelFile);
                        else if (choose == UserChoose.CallEdge) RPAchallengeEdge(rpaExcelFile);
                        else if (choose == UserChoose.CallChrome) RPAchallengeChrome(rpaExcelFile);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите скачанный файл .xls or .xlsx формата скачанного с https://rpachallenge.com!", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Error); //custom messageBox to show error
                }

            }
        }

        public MainWindow()
        {
            InitializeComponent();

        }

        private void firefoxButton_Click(object sender, RoutedEventArgs e)
        {

            InteractionWithData(ExcelGrid, UserChoose.CallFirefox);
        }


        private void openExcelFile_Click(object sender, RoutedEventArgs e)
        {
            InteractionWithData(ExcelGrid, UserChoose.CheckTable);

        }

        private void edgeButton_Click(object sender, RoutedEventArgs e)
        {
            InteractionWithData(ExcelGrid, UserChoose.CallEdge);
        }

        private void chromeButton_Click(object sender, RoutedEventArgs e)
        {
            InteractionWithData(ExcelGrid, UserChoose.CallChrome);
        }
    }
 }
