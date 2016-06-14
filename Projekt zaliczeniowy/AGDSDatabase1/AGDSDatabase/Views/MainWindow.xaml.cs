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
using AGDSDatabase.ViewModels;
using System.Collections.ObjectModel;
using System.IO;
using QuickGraph;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Web;
using AGDSDatabase.Models;
using AGDSDatabase;
using AGDSDatabase.Models.AGDS;
using System.Data.OleDb;
using System.Data;
using System.ComponentModel;

namespace AGDSDatabase.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DatabaseViewModel databaseViewModel;
        public static IBidirectionalGraph<object, IEdge<object>> graphToVisualize;
        public static string path;

        public IBidirectionalGraph<object, IEdge<object>> GraphToVisualize
        {
            get
            {
                return graphToVisualize;
            }
        }
        public MainWindow()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.Filter = "ACCDB Files (*.accdb)|*.accdb|all Files (*.)|*";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            string filename = dlg.FileName;
            // Get the selected file name and display in a TextBox 
            if (filename.EndsWith(".accdb"))
            {
                // Open document 
                
                databaseViewModel = new DatabaseViewModel(filename);
                if (filename.EndsWith("BazaStudenci.accdb"))
                    CreateGraphToVisualize(databaseViewModel);
                path = "Studenci";
                InitializeComponent();
                TreeViewCreator(databaseViewModel);
                ErrorInfo.DataContext = databaseViewModel;
                
            }
            else
            {
                MessageBox.Show("Wrong database format.");
                Application.Current.Shutdown(110);
            }
        }

        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.Filter = "ACCDB Files (*.accdb)|*.accdb|all Files (*.)|*";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            string filename = dlg.FileName;

            // Get the selected file name and display in a TextBox 
            if (filename.EndsWith(".accdb"))
            {
                Graph.Visibility = Visibility.Visible;
                databaseViewModel = new DatabaseViewModel(filename);
                if(!filename.EndsWith("BazaStudenci.accdb"))
                {
                    path = "Orders";
                    Graph.Visibility = Visibility.Hidden;  
                }
                
                TreeViewCreator(databaseViewModel);
                ErrorInfo.DataContext = databaseViewModel;
            }
            else
            {
                MessageBox.Show("Wrong database format.");
                Application.Current.Shutdown(110);
            }
        }

        private void TreeViewCreator(DatabaseViewModel databaseViewModel)
        {
            treeView.Items.Clear();

            foreach (var tab in databaseViewModel.agdsModel.GetTables())
            {
                MenuItem root = new MenuItem() { Title = tab.TableName };
                foreach (var attr in databaseViewModel.agdsModel.GetAttributes(tab))
                {
                    MenuItem childItem1 = new MenuItem() { Title = attr.ColumnName };
                    foreach (var attrVal in databaseViewModel.agdsModel.GetAttributeValues(attr))
                    {
                        MenuItem childItem2 = new MenuItem() { Title = attrVal.Value.ToString() };
                        childItem1.Items.Add(childItem2);
                        foreach(var item in databaseViewModel.agdsModel.GetItems(attrVal))
                        {
                            MenuItem childItem3 = new MenuItem() { Title = item.Print(databaseViewModel.databaseModel) };
                            childItem2.Items.Add(childItem3);
                            foreach(var it in databaseViewModel.agdsModel.GetAssociatedItems(item))
                            {
                                MenuItem childItem4 = new MenuItem() { Title = it.Print(databaseViewModel.databaseModel) };
                                childItem3.Items.Add(childItem4);
                            }
                        }
                    }
                    root.Items.Add(childItem1);
                }
                treeView.Items.Add(root);
            }
        }

        private void CreateGraphToVisualize(DatabaseViewModel databaseViewModel)
        {
            var g = new BidirectionalGraph<object, IEdge<object>>();

            foreach (var tab in databaseViewModel.agdsModel.GetTables())
            {
                g.AddVertex(tab.TableName);
                foreach (var attr in databaseViewModel.agdsModel.GetAttributes(tab))
                {
                    g.AddVertex(attr.ColumnName);
                    g.AddEdge(new Edge<object>(tab.TableName, attr.ColumnName));
                    foreach (var attrVal in databaseViewModel.agdsModel.GetAttributeValues(attr))
                    {
                        g.AddVertex(attrVal.Value.ToString());
                        g.AddEdge(new Edge<object>(attr.ColumnName, attrVal.Value.ToString()));
                        foreach (var item in databaseViewModel.agdsModel.GetItems(attrVal))
                        {
                            g.AddVertex(item.Print());
                            g.AddEdge(new Edge<object>(attrVal.Value.ToString(), item.Print()));
                            foreach (var it in databaseViewModel.agdsModel.GetAssociatedItems(item))
                            {
                                g.AddVertex(it.Print());
                                g.AddEdge(new Edge<object>(item.Print(), it.Print()));
                                g.AddEdge(new Edge<object>(it.Print(), item.Print()));
                            }
                        }
                    }
                }
            }
            graphToVisualize = g;

        }


        public class MenuItem
        {
            public MenuItem()
            {
                this.Items = new ObservableCollection<MenuItem>();
            }

            public string Title { get; set; }

            public ObservableCollection<MenuItem> Items { get; set; }
        }

        private void MenuExecute_Click(object sender, RoutedEventArgs e)
        {
            //We'll use ! for not, & for and, | for or and remove whitespace

            if(String.IsNullOrWhiteSpace(QueryTBox.Text))
            {
                ErrorInfo.Content = "Wrong query";
            }
            else
            {
                List<Token> tokens = new List<Token>();
                StringReader reader = new StringReader(QueryTBox.Text);
                //Tokenize the expression
                Token t = null;
                do
                {
                    t = new Token(reader);
                    tokens.Add(t);
                } while (t.type != Token.TokenType.EXPR_END);

                //Use a minimal version of the Shunting Yard algorithm to transform the token list to polish notation
                List<Token> polishNotation = Util.TransformToPolishNotation(tokens);

                var enumerator = polishNotation.GetEnumerator();
                enumerator.MoveNext();
                BoolExpr root = Util.Make(ref enumerator, databaseViewModel.agdsModel);
                //Request boolean values for all literal operands
                Dictionary<string, List<Item>> dict = new Dictionary<string, List<Item>>();
                var watch = System.Diagnostics.Stopwatch.StartNew();
                try
                {
                    foreach (Token tok in polishNotation.Where(token => token.type == Token.TokenType.LITERAL))
                    {

                        dict[tok.value] = databaseViewModel.agdsModel.GetItemsByExpr(tok.value);
                    }
                    watch.Stop();
                    var elapsedMs = watch.Elapsed.TotalMilliseconds;
                    string agdsMS = elapsedMs.ToString();
                    watch = System.Diagnostics.Stopwatch.StartNew();
                    //DB
                    DataSet ds = new DataSet();
                    ds.Tables.Add("test");
                    OleDbCommand myAccessCommand = new OleDbCommand("SELECT * FROM "+path+" WHERE "+QueryTBox, databaseViewModel.databaseModel.MyAccessConnection);
                    OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);
                    watch.Stop();
                    var elapsedMsDB = watch.Elapsed.TotalMilliseconds;
                    string s = "Zapytanie: " + QueryTBox.Text + "\tAGDS (ms): " + agdsMS + "\tDatabase (ms): " + elapsedMsDB;
                    ErrorInfo.Content = "AGDS: "+ agdsMS+" / Database: "+ elapsedMsDB;
                    File.AppendAllText(@"D:\Studia\VIII Semestr\Metody Inżynierii Wiedzy\Projekt zaliczeniowy\AGDSDatabase\AGDSDatabase\bin\Debug\Logs.txt", s+Environment.NewLine);
                }
                catch
                {
                    ErrorInfo.Content = "Wrong query";
                }

                //DB
                finally
                {
                    string tbx = "";
                    List<Item> ret = new List<Item>();
                    foreach (var item in Util.Eval(root))
                    {
                        tbx += item.Print(databaseViewModel.databaseModel) + "\n";
                    }
                    Output.Text = tbx;
                }

            }
  
        }

        }
    }

