using System;

namespace ProPri.WebApp.Mvc.Views.Palavras.ViewModels
{
    public class PalavraFormViewModel
    {
        public Guid Id { get; set; }
        public string TextoPortugues { get; set; }
        public string TextoIngles { get; set; }
        public string ImagemUpload { get; set; }
    }
}