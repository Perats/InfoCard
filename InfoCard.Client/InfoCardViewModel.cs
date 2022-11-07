using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace InfoCard.Client
{
    public class InfoCardViewModel
    {
        private int id;
        private string name;
        private byte[] imageData;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        public string Name  
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public byte[] ImageData
        {
            get { return imageData; }
            set
            {
                imageData = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
