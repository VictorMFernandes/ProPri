using System;
using System.ComponentModel;

namespace ProPri.WebApp.Mvc.Views.Palavras.ViewModels
{
    public class PalavraIndexViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Texto Português")]
        public string TextoPortugues { get; set; }
        public string TextoIngles { get; set; }
    }
}