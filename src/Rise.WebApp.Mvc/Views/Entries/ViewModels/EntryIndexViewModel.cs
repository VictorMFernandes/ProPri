using System;

namespace Rise.WebApp.Mvc.Views.Entries.ViewModels
{
    public class EntryIndexViewModel
    {
        public Guid Id { get; set; }
        public string English { get; set; }
        public string Portuguese { get; set; }
        public string Classification { get; set; }
    }
}