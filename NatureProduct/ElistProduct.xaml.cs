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



namespace NatureProduct
{
    public partial class ElistProduct : PhoneApplicationPage
    {
        Dictionary<string, string> harmCategory;
        Good ecompObj;

        private void createDict(){
            harmCategory = new Dictionary<string,string>();
            harmCategory.Add("safe", "#06F349");
            harmCategory.Add("securesafe", "#FDFA2D");
            harmCategory.Add("condsafe", "#FFFFC700");
            harmCategory.Add("notsafe", "#F37906");
            harmCategory.Add("harmful", "#FF0000");   
        }

        public ElistProduct()
        {
            InitializeComponent();
            createDict();

            var obj = App.Current as App;
            ecompObj = obj.sharEObj;
            ProdName.Title = ecompObj.name;
        }

        string getColor(int severity) {
            String color = "";
            switch (severity)
            {
                case 1:
                    color = harmCategory["safe"];
                    break;
                case 2:
                    color = harmCategory["securesafe"];
                    break;
                case 3:
                    color = harmCategory["condsafe"];
                    break;
                case 4:
                    color = harmCategory["notsafe"];
                    break;
                case 5:
                    color = harmCategory["harmful"];
                    break;
            }
            return color;
        }
        
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {        

            base.OnNavigatedTo(e);
             
            
            if (GroupedEList.ItemsSource == null)
            {
                List<EName> source = new List<EName>();
                ecompObj.additives.ForEach(delegate(EComponent eComp) { 
                    String listName = string.Join(", ", eComp.names.ToArray());
                    source.Add(new EName {Id = 1, NameE = eComp.id, Name = listName, Color = getColor(eComp.severity)});
                });
                source.Add(new EName() { Id = 1, NameE = "E123", Name = "кармин", Color = "#06F349" });
                source.Add(new EName() { Id = 1, NameE = "E133", Name = "бензонат натрия", Color = "#FF0000" });
                GroupedEList.ItemsSource = source;
            }

            if (GroupedDescribeList.ItemsSource == null)
            {
                List<DescribeCateg> source = new List<DescribeCateg>();                
                source.Add(new DescribeCateg() { Id = 1, Name = "Безопасна", Color = harmCategory["safe"] });
                source.Add(new DescribeCateg() { Id = 2, Name = "небезопасна для определённой категории лиц", Color = harmCategory["securesafe"] });
                source.Add(new DescribeCateg() { Id = 3, Name = "побочные явления не представляют угрозу жизни", Color = harmCategory["condsafe"] });
                source.Add(new DescribeCateg() { Id = 4, Name = "побочные явления представляют  угрозу жизни", Color = harmCategory["notsafe"] });
                source.Add(new DescribeCateg() { Id = 5, Name = "запрещена", Color = harmCategory["harmful"] });
                GroupedDescribeList.ItemsSource = source;
            }
            if (GroupedExtenList.ItemsSource == null) {
                List<EExtenDescribe> source = new List<EExtenDescribe>();
                ecompObj.additives.ForEach(delegate(EComponent eComp)
                {
                    String listName = string.Join(", ", eComp.names.ToArray());
                    source.Add(new EExtenDescribe() { Id = 1, NameE = eComp.id, Describe = eComp.category + ". " + eComp.comment, Color = getColor(eComp.severity) });
                });
                source.Add(new EExtenDescribe() { Id = 1, NameE = "E123", Describe = "Describe", Color = harmCategory["safe"] });
            }
        }       
    }
}