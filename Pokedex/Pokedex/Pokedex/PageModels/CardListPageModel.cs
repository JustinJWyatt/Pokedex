using FreshMvvm;
using Pokedex.Client;
using Pokedex.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Pokedex.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class CardListPageModel : FreshBasePageModel
    {
        private readonly ITrelloAPIClient _trelloAPIClient;

        private TrelloCard _selectedCard;
        public bool IsLoading { get; private set; }

        public CardListPageModel(ITrelloAPIClient trelloAPIClient)
        {
            _trelloAPIClient = trelloAPIClient;
        }

        public List<TrelloCard> TrelloCards { get; set; }
        public TrelloCard NewCard { get; set; }

        private string _trelloListID;

        public override async void Init(object initData)
        {
            if (initData != null && initData is string listId)
            {
                _trelloListID = listId;

                NewCard = new TrelloCard(_trelloListID);

                TrelloCards = await _trelloAPIClient.GetCardsAsync(listId);
            }

            base.Init(initData);
        }

        public TrelloCard SelectedCard
        {
            get
            {
                return _selectedCard;
            }
            set
            {
                _selectedCard = value;
                if (value == null) return;
                ShowCardDetailCommand.Execute(_selectedCard.Id);
                SelectedCard = null;
            }
        }

        public Command ShowCardDetailCommand => new Command<string>(async (card) =>
        {
            await CoreMethods.PushPageModel<CardPageModel>(card, true, true);
        });

        public Command Foo => new Command((a) => { });

        public Command CreateCardCommand => new Command(async (a) =>
        {
            if (NewCard.Name.Length < 5)
            {
                await CurrentPage.DisplayAlert("Error", "Length must be 5 characters or more", "OK");
                return;
            }

            if (string.IsNullOrEmpty(NewCard.Desc))
            {
                await CurrentPage.DisplayAlert("Error", "Description required", "OK");
                return;
            }

            try
            {
                IsLoading = true;

                var response = await _trelloAPIClient.PostCardAsync(NewCard);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    IsLoading = false;

                    await CurrentPage.DisplayAlert("Success!", "Card created", "OK");
                    TrelloCards.Add(NewCard);
                    NewCard = new TrelloCard(_trelloListID);
                }
                else 
                {
                    await CurrentPage.DisplayAlert("Error", "Request unsuccessful", "OK");
                    return;
                }
            }
            catch (Exception)
            {
                IsLoading = false;
            }
        }, canExecute: x => !string.IsNullOrEmpty(NewCard.Name) && !string.IsNullOrEmpty(NewCard.Desc));
    }
}
