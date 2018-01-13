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

namespace IpPlanning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string ConvToBin(string s)
        {
            byte num = 0;
            
            
          
            
            try
            {
                if (s.Length == 0)
                {
                    num = 0;
                }
                num = Convert.ToByte(s);
            }
            catch (Exception)
            {
                if (s!="")
                {
                    MessageBox.Show("El...tad");
                }
                
                
               
                //throw new Exception("El...tad");
            }
            
            string result = "";
            while (num>0)
            {
                int remainder = num % 2;
                result = remainder.ToString() + result;
                
                num /= 2;
            }
            int l = result.Length;
            for (int i = 0; i < 8 - l; i++)
            {
                result = "0" + result;
            }
            return result;
        }
        public void TypeDef(string s)
        {
            string t;
            bool ok = false;
            string mask;
            ok = int.TryParse(ipOctet[0].Text, out int c);
            if (ok)
            {
                if (c >= 0 && c <= 127)
                {
                    t = "A";
                    mask = "255.0.0.0";
                }
                else if (c > 127 && c <= 191)
                {
                    t = "B";
                    mask = "255.255.0.0";
                }
                else
                {
                    t = "C";
                    mask = "255.255.255.0";
                }
                type.Content = $"A hálózat tipusa: {t}";
                defMask.Content = $"Alapértelmezett maszk: {mask}";
            }
        }
 
        /**************************************************************************************
         * Fields and properties
         * ************************************************************************************/
        List<TextBox> ipOctet;
        List<TextBox> maskOctet;
        List<Label> resIpBinLbl;
        List<Label> resMaskBinLbl;
        TextBox iptb;
        Label resultOfIp;

        public List<TextBox> IpOctet { get => ipOctet; set => ipOctet = value; }
        public List<TextBox> MaskOctet { get => maskOctet; set => maskOctet = value; }
        public TextBox Iptb { get => iptb; set => iptb = value; }
        public Label ResultOfIp { get => resultOfIp; set => resultOfIp = value; }
        public List<Label> ResIpBinLbl { get => resIpBinLbl; set => resIpBinLbl = value; }
        public List<Label> ResMaskBinLbl { get => resMaskBinLbl; set => resMaskBinLbl = value; }



        /******************************************************************************************
         * Constructor
         * ***************************************************************************************/

        public MainWindow()
        {
            InitializeComponent();
            mw.Title = "IP cím segédlet";
            ipOctet = new List<TextBox>();
            maskOctet = new List<TextBox>();
            resIpBinLbl = new List<Label>();
            resMaskBinLbl = new List<Label>();
            for (int i = 0; i < 4; i++)
            {
                

                iptb = new TextBox()
                {
                    
                    Height = 20,
                    Width = 30,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                    
                    Margin=new Thickness(IpInput.Width /2 -64 + 32*i , IpInput.Height / 2 - 10,0,0)
                    
                };
                iptb.TextChanged += Iptb_TextChanged;
                IpOctet.Add(iptb);
                TextBox masktb = new TextBox()
                {
                    Height=20,
                    Width=30,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(MaskInput.Width / 2 - 64 + 32 * i, MaskInput.Height / 2 - 10, 0, 0)
                };
                masktb.TextChanged += Masktb_TextChanged;
                MaskOctet.Add(masktb);
                Label resultOfIp = new Label()
                {
                    Background = new SolidColorBrush(Colors.LightGray),
                Content = "00000000",
                    Height = 30,
                    Width = 70,
                    //HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    
                    Margin = new Thickness(IPBinResult.Width / 2 - 144 + 72 * i, IPBinResult.Height / 2 - 10, 0, 0)
                };
                resIpBinLbl.Add(resultOfIp);
                Label resultOfMask = new Label()
                {
                    Content = "00000000",
                    Height = 30,
                    Width = 70,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    
                    Margin = new Thickness(MaskBinResult.Width / 2 - 144 + 72 * i, MaskBinResult.Height / 2 - 10, 0, 0)
                };
                resMaskBinLbl.Add(resultOfMask);
                IpInput.Children.Add(iptb);
                MaskInput.Children.Add(masktb);
                IPBinResult.Children.Add(resultOfIp);
                MaskBinResult.Children.Add(resultOfMask);
            }
            TextBox perInp = new TextBox()
            {
                Height = 20,
                Width = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(PerInput.Width / 2 - 15, PerInput.Height / 2 - 10, 0, 0)
            };
            PerInput.Children.Add(perInp);
                      
            


            
        }

        private void Masktb_TextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                string s = MaskOctet[i].Text;
                
                if (s != "")
                {
                    resMaskBinLbl[i].Content = ConvToBin(s);

                }
            }
            
        }

        private void Iptb_TextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                string s = IpOctet[i].Text;
                if (s != "")
                {
                    resIpBinLbl[i].Content = ConvToBin(s);
                    
                }
            }
            TypeDef(ipOctet[0].Text);

            
            
            
        }

        private void SelMask_Click(object sender, RoutedEventArgs e)
        {
            
            
                if (selPer.Visibility == Visibility.Visible)
                {
                    selPer.Visibility = Visibility.Hidden;
                    selMask.Visibility = Visibility.Visible;
                    PerInput.Visibility = Visibility.Collapsed;
                MaskInput.Visibility = Visibility.Visible;
                PerTitle.Visibility = Visibility.Collapsed;
                MaskTitle.Visibility = Visibility.Visible;
                }
                else
                {
                    selPer.Visibility = Visibility.Visible;
                    selMask.Visibility = Visibility.Hidden;
                PerInput.Visibility = Visibility.Visible;
                MaskInput.Visibility = Visibility.Collapsed;
                PerTitle.Visibility = Visibility.Visible;
                MaskTitle.Visibility = Visibility.Collapsed;
            }
            
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            List<int> a = new List<int>();
            string ip = "", mask = "", OR = "";
            int fdb = 0;
            int wdb = 0;
            for (int i = 0; i < 4; i++)
            {
                if (IpOctet[i].Text=="" || maskOctet[i].Text=="")
                {
                    
                    MessageBox.Show("Rossz cím!");
                    ok = false;
                }
                
            }
            for (int i = 0; i < 4; i++)
            {
                ip += resIpBinLbl[i].Content;
                mask += resMaskBinLbl[i].Content;
            }
            for (int i = 0; i < ip.Length; i++)
            {
                if (mask[i] == '1') fdb++;
            }


            while (mask[wdb] == '1')
            {
                wdb++;
            }
            if (fdb != wdb)
            {
                ok = false;
                MessageBox.Show("Rossz maszk!");
            }

            if (ok)
            {
                for (int i = 0; i < 4; i++)
                {
                    
                    a.Add(int.Parse(IpOctet[i].Text) & int.Parse(MaskOctet[i].Text));
                    OR += $"{a[i]}.";
                }

                NetAddress.Content = $"Hálózat címe: {OR}\bs";
                numOfIp.Content = $"Kiosztható: {Math.Pow(2, 32 - fdb)-2}";
                Br.Content = $"Szórási cím: {a[0]}.{a[1]}.{a[2]}.{Math.Pow(2, 32 - fdb)-1}";
                FirstAddress.Content= $"Első cím: {a[0]}.{a[1]}.{a[2]}.{(a[3]+1)}";
                LastAddress.Content= $"Utolsó cím: {a[0]}.{ a[1]}.{ a[2]}.{(a[3]+Math.Pow(2, 32 - fdb)-2)}";
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
        }
    }
}
