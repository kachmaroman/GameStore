using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Domain.Entities
{
    public class Requirements
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Game Game { get; set; }

        public Guid GameId { get; set; }

        // Windows
        [Display(Name = "Windows OS")]
        public string WinOS { get; set; }

        [Display(Name = "Windows CPU")]
        public string WinCPU { get; set; }

        [Display(Name = "Windows RAM")]
        public string WinRAM { get; set; }

        [Display(Name = "Windows GPU")]
        public string WinGPU { get; set; }

        [Display(Name = "Windows Storage")]
        public string WinStorage { get; set; }

        // Linux
        [Display(Name = "Linux OS")]
        public string LinOS { get; set; }

        [Display(Name = "Linux CPU")]
        public string LinCPU { get; set; }

        [Display(Name = "Linux RAM")]
        public string LinRAM { get; set; }

        [Display(Name = "Linux GPU")]
        public string LinGPU { get; set; }

        [Display(Name = "Linux Storage")]
        public string LinStorage { get; set; }

        // Mac
        [Display(Name = "Mac OS")]
        public string MacOS { get; set; }

        [Display(Name = "Mac CPU")]
        public string MacCPU { get; set; }

        [Display(Name = "Mac RAM")]
        public string MacRAM { get; set; }

        [Display(Name = "Mac GPU")]
        public string MacGPU { get; set; }

        [Display(Name = "Mac Storage")]
        public string MacStorage { get; set; }
    }
}
