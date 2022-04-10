using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Nami.DXP.Domain
{
    [Table("FACELOOK_MAIN_bak_102720")]
    public partial class FacelookMainBak102720
    {
        [Required]
        [Column("EMP_ID")]
        [StringLength(50)]
        public string EmpId { get; set; }
        [Column("NAME_FIRST")]
        [StringLength(100)]
        public string NameFirst { get; set; }
        [Column("NAME_MIDDLE")]
        [StringLength(100)]
        public string NameMiddle { get; set; }
        [Column("NAME_LAST")]
        [StringLength(100)]
        public string NameLast { get; set; }
        [Column("NAME_SUFFIX")]
        [StringLength(50)]
        public string NameSuffix { get; set; }
        [Column("NAME_ALIAS")]
        [StringLength(100)]
        public string NameAlias { get; set; }
        [Column("EMP_DEPT")]
        [StringLength(150)]
        public string EmpDept { get; set; }
        [Column("EMP_TITLE")]
        [StringLength(150)]
        public string EmpTitle { get; set; }
        [Column("EMP_LEVEL")]
        [StringLength(10)]
        public string EmpLevel { get; set; }
        [Column("EMP_PRI_LAT")]
        [StringLength(30)]
        public string EmpPriLat { get; set; }
        [Column("EMP_SHIFT")]
        [StringLength(10)]
        public string EmpShift { get; set; }
        [Column("EMP_STATUS")]
        [StringLength(30)]
        public string EmpStatus { get; set; }
        [Column("EMP_FULL_PART")]
        [StringLength(30)]
        public string EmpFullPart { get; set; }
        [Column("EMP_TYPE")]
        [StringLength(30)]
        public string EmpType { get; set; }
        [Column("EMP_PLANT")]
        [StringLength(30)]
        public string EmpPlant { get; set; }
        [Column("EMP_GENDER")]
        [StringLength(10)]
        public string EmpGender { get; set; }
        [Column("DATE_HIRED", TypeName = "date")]
        public DateTime? DateHired { get; set; }
        [Column("DATE_TERM", TypeName = "date")]
        public DateTime? DateTerm { get; set; }
        [Column("CERT_FIRST_AID")]
        [StringLength(10)]
        public string CertFirstAid { get; set; }
        [Column("CERT_EVACUATION")]
        [StringLength(10)]
        public string CertEvacuation { get; set; }
        [Column("CERT_SPILL_RESPONSE")]
        [StringLength(10)]
        public string CertSpillResponse { get; set; }
        [Column("CERT_FORKLIFT")]
        [StringLength(10)]
        public string CertForklift { get; set; }
        [Column("CERT_SCISSOR_LIFT")]
        [StringLength(10)]
        public string CertScissorLift { get; set; }
        [Column("EMP_IMAGE")]
        public string EmpImage { get; set; }
        [Column("EMP_BADGE")]
        [StringLength(10)]
        public string EmpBadge { get; set; }
    }
}
