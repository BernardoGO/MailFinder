using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace Spam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string mailListFile = "mailList.txt";
        MailListCtrl oList = new MailListCtrl(mailListFile);
        email_select email = new email_select();
        string caracteres = "<>óòôõéèêàáãâíÎîûúùü:&=+;*?!,/\\●()[]{}~^ \t¬$#\"\'´`|";
        string linha;
        int x = 0, y = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddMail_Click(object sender, RoutedEventArgs e)
        {
            //bool gravado = oList.AddToList(txtNewMail.Text);
            //if (gravado)
            //    MessageBox.Show("Gravado");
            //else MessageBox.Show("Já Existe");
            string _rtbContents = new TextRange(newDoc.ContentStart, newDoc.ContentEnd).Text;
            rtbContents = _rtbContents;
            Thread worker = new Thread(new ThreadStart(worker_method));
            worker.Start();
        }
        string rtbContents = "";
        delegate void changeStats(string stats);
        void changeTextStat(string stats)
        {
            label1.Content = stats;
        }

        private void worker_method()
        {
            this.Dispatcher.Invoke(new changeStats(changeTextStat), "Processando");
            oList.ReadList();
            int added = 0;
            int jaExists = 0;

            
            rtbContents = rtbContents.ToLower();
            rtbContents = email.RemoveCharacters(rtbContents, caracteres);
            rtbContents = email.RemoveSpaces(rtbContents);

            
            ParallelOptions ppl = new ParallelOptions();
            ppl.MaxDegreeOfParallelism = 3;
                    //foreach (string line in rtbContents.Split('\n'))
            Parallel.ForEach(rtbContents.Split('\n'), ppl, line =>
            {
                if (line.Trim(' ', '\r').Length != 0)
                {

                    linha = email.EmailCheck(line.Trim());
                    if (linha != "")
                    {
                        bool gravado = oList.AddToList(linha);
                        if (gravado)
                            added++;
                        else jaExists++;

                        this.Dispatcher.Invoke(new changeStats(changeTextStat), "Gravados: " + added.ToString() + "\nJa existe: " + jaExists.ToString());
                    }

                }
            });

                    oList.WriteList();
                    this.Dispatcher.Invoke(new changeStats(changeTextStat), "Terminado\nGravados: "+added.ToString() + "\nJa existe: "+jaExists.ToString());
            
        }
        FlowDocument newDoc = new FlowDocument();
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog().Value)
            {
                
                newDoc.Blocks.Add(new Paragraph(new Run( File.ReadAllText(openfile.FileName))));
                //txtText.Document = newDoc;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnClearBox_Click(object sender, RoutedEventArgs e)
        {
            //FlowDocument newDoc = new FlowDocument();
            //txtText.Document = newDoc;

            txtText.Document.Blocks.Clear();
        }

        private void btnCount_Click(object sender, RoutedEventArgs e)
        {
            label1.Content = "Numero de contatos: "+oList.ListCount().ToString();
        }

        private void chkTop_Most_Checked(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
        }

        private void chkTop_Most_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Topmost = !true;
        }
    }
}
