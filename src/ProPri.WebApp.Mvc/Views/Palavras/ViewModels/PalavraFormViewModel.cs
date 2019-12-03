using System;
using System.ComponentModel;

namespace ProPri.WebApp.Mvc.Views.Palavras.ViewModels
{
    public class PalavraFormViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Texto Português")]
        public string TextoPortugues { get; set; }
        [DisplayName("Texto Inglês")]
        public string TextoIngles { get; set; }
        public string ImagemUpload { get; set; }
    }
}