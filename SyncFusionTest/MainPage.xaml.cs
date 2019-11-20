using Syncfusion.ListView.XForms;
using System.Collections.Generic;
using System;
using Xamarin.Forms;

namespace SyncFusionTest
{
    public partial class MainPage : ContentPage
    {
        public class Register
        {
            public string name { get; set; }
            public string category { get; set; }
            public string qty { get; set; }
        }

        private int _index = -1;
        private Image _image = null;

        public MainPage()
        {
            InitializeComponent();
            listView.SwipeEnded += ListView_SwipeEnded;
            listView.SelectionChanged += ListView_SelectionChanged;

            // Registers generation
            string[][] words = {
                //      0 - name     1 - category
                new[] { "Orange",    "Fruit",     "10" },
                new[] { "Carrot",    "Vegetable", "12" },
                new[] { "Banana",    "Fruit",     "19" },
                new[] { "Chili",     "Vegetable", "11" },
                new[] { "Pineapple", "Fruit",     "15" },
                new[] { "Onion",     "Vegetable", "18" } };
            List<Register> registers = new List<Register>();
            foreach (string[] line in words)
            {
                Register register = new Register();
                register.name = line[0];
                register.category = line[1];
                register.qty = line[2];
                registers.Add(register);
            }
            listView.ItemsSource = registers;

            // Group info
            listView.DataSource.GroupDescriptors.Add(new Syncfusion.DataSource.GroupDescriptor()
            {
                PropertyName = "category",
                KeySelector = (object _object) => { return (_object as Register).category; }
            });
        }

        private void ListView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        { _index = e.ItemIndex; }

        void indexBindingContextChanged(object sender, EventArgs e)
        {
            if (_image != null) return;
            _image = sender as Image;
            (_image.Parent as View).GestureRecognizers.Add(new TapGestureRecognizer()
            { Command = new Command(showIndex) });
        }

        void showIndex()
        {
            if (_index < 0) return;
            var registers = listView.ItemsSource as List<Register>;
            var register = registers[_index];
            DisplayAlert("Index: " + _index, register.name, "Continue");
            listView.ResetSwipe();
        }

        private void ListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            var register = e.AddedItems[0] as Register;
            DisplayAlert("Category: " + register.category, register.name, "Continue");
            listView.ResetSwipe();
        }
    }
}