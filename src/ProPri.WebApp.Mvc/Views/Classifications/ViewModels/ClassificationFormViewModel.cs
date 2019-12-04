using System;
using System.Collections.Generic;

namespace ProPri.WebApp.Mvc.Views.Classifications.ViewModels
{
    public class ClassificationFormViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ClassificationIndexViewModel Parent { get; set; }
        public List<ClassificationIndexViewModel> Children { get; set; }
    }
}