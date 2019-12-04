using System;
using System.Collections.Generic;

namespace ProPri.WebApp.Mvc.Views.Classifications.ViewModels
{
    public class ClassificationIndexViewModel
    {
        public int Id;
        public int? ParentId;
        public string Name;

        public bool Expanded;
        public bool Selected;
        public bool HasChild;
        public List<ClassificationIndexViewModel> Children;
    }
}