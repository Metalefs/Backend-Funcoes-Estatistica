using System.ComponentModel.DataAnnotations;

namespace Estatistica101.Enums
{
    public enum ClassificacaoModa
    {
        [Display(Name= "Amodal")]
        Amodal,
        [Display(Name= "Unimodal")]
        Unimodal,
        [Display(Name= "Bimodal")]
        Bimodal,
        [Display(Name= "Trimodal")]
        Trimodal,
        [Display(Name= "Polimodal")]
        Polimodal
    }
}
