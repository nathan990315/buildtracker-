using System;
using System.ComponentModel.DataAnnotations;
using BuildFeed.Local;

namespace BuildFeed.ViewModel
{
    public class FrontPageBuild
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(VariantTerms), Name = nameof(VariantTerms.Model_MajorVersion))]
        public uint MajorVersion { get; set; }

        [Required]
        [Display(ResourceType = typeof(VariantTerms), Name = nameof(VariantTerms.Model_MinorVersion))]
        public uint MinorVersion { get; set; }

        [Required]
        [Display(ResourceType = typeof(VariantTerms), Name = nameof(VariantTerms.Model_BuildNumber))]
        public uint Number { get; set; }

        [Display(ResourceType = typeof(VariantTerms), Name = nameof(VariantTerms.Model_Revision))]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public uint? Revision { get; set; }

        [Display(ResourceType = typeof(VariantTerms), Name = nameof(VariantTerms.Model_LabString))]
        public string Lab { get; set; }

        [Display(ResourceType = typeof(VariantTerms), Name = nameof(VariantTerms.Model_BuildTime))]
        [DisplayFormat(ConvertEmptyStringToNull = true,
            ApplyFormatInEditMode = true,
            DataFormatString = "{0:yyMMdd-HHmm}")]
        public DateTime? BuildTime { get; set; }
    }
}