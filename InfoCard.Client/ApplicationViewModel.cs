using InfoCard.Di;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InfoCard.Client
{
    public class ApplicationViewModel
    {
        private InfoCardViewModel selectedCard;
        public ObservableCollection<InfoCardViewModel> InfoCardViewModels { get; set; }

        public RelayCommand AddCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        private async void AddCommandHandler(object obj)
        {
            var newCard = new InfoCardViewModel();
            InfoCardViewModels.Add(newCard);
            await Task.Yield();
            SelectedCard = newCard;
        }

        private async void SaveCommandHandler(object obj)
        {
            if (SelectedCard.Id != default)
            {
                await HttpHelper.Send<InfoCardViewModel, int>(HttpMethod.Put, $"Cards/{SelectedCard.Id}", SelectedCard);
            }
            else
            {
                var newCardId = await HttpHelper.Send<InfoCardViewModel, int>(HttpMethod.Post, "Cards" , SelectedCard);
                SelectedCard.Id = newCardId;
            }
            
        }

        public InfoCardViewModel SelectedCard
        {
            get { return selectedCard; }
            set
            {
                if (selectedCard != null && selectedCard.Id == default)
                {
                    InfoCardViewModels.Remove(selectedCard);
                }
                selectedCard = value;
                OnPropertyChanged();
            }
        }

        public ApplicationViewModel()
        {
            InfoCardViewModels = new ObservableCollection<InfoCardViewModel>() { };

            AddCommand = new RelayCommand(AddCommandHandler);
            SaveCommand = new RelayCommand(SaveCommandHandler);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public async Task LoadInfoCardsAsync()
        {
            var a = await HttpHelper.Get<List<InfoCardViewModel>>("Cards");

            foreach (var item in a)
            {
                InfoCardViewModels.Add(item);
            }
        }
    }
}
