using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NatureProduct.Models;
using NatureProduct.NetworkConnect;
using System.ComponentModel;

namespace NatureProduct
{
    public partial class ElistProduct : PhoneApplicationPage
    {
        Dictionary<string, string> harmCategory;
        Good ecompObj;
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void createDict(){
            harmCategory = new Dictionary<string,string>();
            harmCategory.Add("safe", "#9ACD32");
            harmCategory.Add("securesafe", "#4a9b1b");
            harmCategory.Add("condsafe", "#FFFFC700");
            harmCategory.Add("notsafe", "#F37906");
            harmCategory.Add("harmful", "#FF0000");   
        }

        public ElistProduct()
        {
            InitializeComponent();
            createDict();
        }

        string getColor(int severity) {
            String color = "";
            switch (severity)
            {
                case 0:
                    color = harmCategory["safe"];
                    break;
                case 1:
                    color = harmCategory["securesafe"];
                    break;
                case 2:
                    color = harmCategory["condsafe"];
                    break;
                case 3:
                    color = harmCategory["notsafe"];
                    break;
                case 4:
                    color = harmCategory["harmful"];
                    break;
            }
            return color;
        }
        
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {        

            base.OnNavigatedTo(e);
            var obj = App.Current as App;
            ecompObj = obj.sharEObj;
            ProdName.Title = ecompObj.name;
            
            if (GroupedEList.ItemsSource == null)
            {
                List<EName> source = new List<EName>();
                if (ecompObj.additives.Count == 0) 
                {
                    source.Add(new EName() { Id = 1, NameE = "Нет вредных добавок", Name = "Вам повезло", Color = harmCategory["safe"] });
                }
                else
                {
                    ecompObj.additives.ForEach(delegate(EComponent eComp)
                    {
                        String listName = string.Join(", ", eComp.names.Take(3).ToArray());
                        source.Add(new EName() { Id = 1, NameE = "E" + eComp.id, Name = listName, Color = getColor(eComp.severity) });
                    });   
                }                           
                GroupedEList.ItemsSource = source;
            }

            if (GroupedDescribeList.ItemsSource == null)
            {
                List<DescribeCateg> source = new List<DescribeCateg>();                
                source.Add(new DescribeCateg() { Id = 1, Name = "Безопасна", Color = harmCategory["safe"] });
                source.Add(new DescribeCateg() { Id = 2, Name = "небезопасна для определённой категории лиц", Color = harmCategory["securesafe"] });
                source.Add(new DescribeCateg() { Id = 3, Name = "побочные действия не представляют угрозу жизни", Color = harmCategory["condsafe"] });
                source.Add(new DescribeCateg() { Id = 4, Name = "побочные действия представляют  угрозу жизни", Color = harmCategory["notsafe"] });
                source.Add(new DescribeCateg() { Id = 5, Name = "запрещена", Color = harmCategory["harmful"] });
                GroupedDescribeList.ItemsSource = source;
            }
            if (GroupedExtenList.ItemsSource == null) {
                List<EExtenDescribe> source = new List<EExtenDescribe>();
                if (ecompObj.additives.Count == 0)
                {
                    source.Add(new EExtenDescribe() { Id = 1, NameE = "Нет вредных добавок", Describe = "Вам повезло", Color = harmCategory["safe"] });
                }
                else
                {
                    ecompObj.additives.ForEach(delegate(EComponent eComp)
                    {
                        String listName = string.Join(", ", eComp.names.Take(3).ToArray());
                        source.Add(new EExtenDescribe() { Id = 1, NameE = "E" + eComp.id, Describe = eComp.category + ". " + eComp.comment, Color = getColor(eComp.severity) });
                    });
                }
                GroupedExtenList.ItemsSource = source;
            }
        }      

        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var myItem = ((LongListSelector)sender).SelectedItem as Type;            
        }       
    }
}