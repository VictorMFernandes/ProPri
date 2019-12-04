using ProPri.WebApp.Mvc.Views.Classifications.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ProPri.WebApp.Mvc.Views.Entries.ViewModels
{
    public class EntryFormViewModel
    {
        public Guid Id { get; set; }
        public string English { get; set; }
        public string Portuguese { get; set; }
        public string Image { get; set; }
        [DisplayName("Classification")]
        public Guid ClassificationId { get; set; }

        public IEnumerable<ClassificationIndexViewModel> Classifications { get; set; }
    }
}