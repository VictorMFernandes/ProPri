using System.Collections.Generic;

namespace Rise.WebApp.Mvc.Views.Classifications.ViewModels
{
    public class ClassificationIndexViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }

        public bool Expanded { get; set; }
        public bool Selected { get; set; }
        public bool HasChild { get; set; }
        public List<ClassificationIndexViewModel> Children { get; set; }
    }
}