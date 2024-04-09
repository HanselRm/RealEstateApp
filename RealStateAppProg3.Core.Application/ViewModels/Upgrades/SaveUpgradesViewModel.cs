

using System.ComponentModel.DataAnnotations;

namespace RealStateAppProg3.Core.Application.ViewModels.Upgrades
{
    public class SaveUpgradesViewModel
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
