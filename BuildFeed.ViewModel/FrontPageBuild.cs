using System;
using System.ComponentModel.DataAnnotations;
using BuildFeed.Local;

namespace BuildFeed.ViewModel
{
    public class FrontPageBuild
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(VariantTerms), Name = nameof(VariantTerms.Model_MajorVersion))]
        public uint MajorVersion { get; set; }

        [Display(ResourceType = typeof(VariantTerms), Name = nameof(VariantTerms.Model_MinorVersion))]
        public uint MinorVersion { get; set; }

        [Display(ResourceType = typeof(VariantTerms), Name = nameof(VariantTerms.Model_BuildNumber))]
        public uint Number { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [Display(ResourceType = typeof(VariantTerms), Name = nameof(VariantTerms.Model_Revision))]
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