using FreshMvvm;
using Pokedex.Client;
using Pokedex.Models;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using System.ComponentModel.DataAnnotations;

namespace Pokedex.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class TrelloPageModel : FreshBasePageModel
    {
        private readonly ITrelloAPIClient _trelloAPIClient;

        private TrelloListItem _selectedTrelloListItem;

        private string _boardId;

        public TrelloPageModel(ITrelloAPIClient trelloAPIClient)
        {
            _trelloAPIClient = trelloAPIClient;
        }

        public TrelloListItem SelectedTrelloListItem
        {
            get
            {
                return _selectedTrelloListItem;
            }
            set
            {
                _selectedTrelloListItem = value;
                if (value == null) return;
                ShowCardListCommand.Execute(_selectedTrelloListItem.Id);
                SelectedTrelloListItem = null;
            }
        }

        public bool IsLoading { get; set; }

        public TrelloMember TrelloMemberFormData { get; set; }

        public string MemberEmail { get; set; }

        public Command ShowCardListCommand => new Command<string>(async (id) =>
        {
            await CoreMethods.PushPageModel<CardListPageModel>(id, true, true);
        });

        public ObservableCollection<TrelloListItem> TrelloList { get; set; }

        public override async void Init(object initData)
        {
            base.Init(initData);

            IsLoading = true;

            var boards = await _trelloAPIClient.GetBoardsAsync();

            _boardId = boards.FirstOrDefault().Id;

            var board = await _trelloAPIClient.GetBoardAsync(_boardId);

            board.Lists.ForEach(list =>
            {
                list.CardCount = board.Cards.Count(x => x.IdList == list.Id);
            });

            TrelloList = new ObservableCollection<TrelloListItem>(board.Lists);

            IsLoading = false;
        }

        public Command AddMemberCommand => new Command(async (a) =>
        {
            if (string.IsNullOrWhiteSpace(MemberEmail) || new EmailAddressAttribute().IsValid(MemberEmail) == false)
            {
                await this.CurrentPage.DisplayAlert("Error", "Email Invalid", "OK");
                return;
            }

            try
            {
                var response = await _trelloAPIClient.PutBoardMembersAsync(_boardId, MemberEmail);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await CurrentPage.DisplayAlert("Success!", "Member added", "OK");
                    MemberEmail = default;
                }
                else
                {
                    await this.CurrentPage.DisplayAlert("Error", "Request Failed", "OK");
                    return;
                }
            }
            catch (Exception)
            {
                await this.CurrentPage.DisplayAlert("Error", "Request threw an exception", "OK");
            }
        }, x => !string.IsNullOrEmpty(MemberEmail));
    }
}
