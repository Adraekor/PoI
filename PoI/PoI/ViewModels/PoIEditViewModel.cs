﻿using LiteDB;
using PoI.Model;
using PoI.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;

namespace PoI.ViewModels
{
    public class PoIEditViewModel : ViewModelBase
	{
        private IPoIService PoIService;
        private PointOfInterest PoI;
        public DelegateCommand DelegateSave { get; private set; }

        private IPageDialogService _dialogService;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _tag;
        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        public PoIEditViewModel(INavigationService navigationService, IPoIService poiService, IPageDialogService pageDialog)
            : base(navigationService)
        {
            PoIService = poiService;
            _dialogService = pageDialog;
            DelegateSave = new DelegateCommand(UpdateAndExit, CanEditPoI).ObservesProperty(() => Name).ObservesProperty(() => Tag);
        }

        public async void UpdateAndExit()
        {
            var answer = await _dialogService.DisplayAlertAsync("Modification", "Êtes vous sûr de vouloir modifier ce PoI", "Non", "Oui");

            if (!answer)
            {
                PoI.Name = Name;
                PoI.MiniName = Name.Substring(0, 5) + "...";
                PoI.Description = Description;
                PoI.MiniDesc = Description.Substring(0, 10) + "...";
                PoI.Tag = Tag;
                PoI.MiniTag = Tag.Substring(0, 5) + "...";

                var param = new NavigationParameters()
                {
                    {"poi", PoI }
                };

                using (var db = new LiteDatabase(AppConstante.dbPath))
                {
                    var PoIs = db.GetCollection<PointOfInterest>("poi");
                    PoIs.Update(PoI);
                }

                await NavigationService.GoBackAsync(param);
            }
        }

        private bool CanEditPoI()
        {
            if (string.IsNullOrEmpty(Name))
                return false;

            if (string.IsNullOrEmpty(Tag))
                return false;

            return true;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            var poi = parameters.GetValue<PointOfInterest>("poi");
            PoI = poi;
            Name = poi.Name;
            Description = poi.Description;
            Tag = poi.Tag;
        }
    }
}
