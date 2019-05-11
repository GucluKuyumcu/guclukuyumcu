using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FormsProject.Dto
{
    public class FormDetailsModel
    {
        public int FormID { get; set; }

        [Required(ErrorMessage = "Lütfen Adı Giriniz")]
        [Display(Name = "Adı")]
        [StringLength(50, ErrorMessage = "Adı Soyadı En Fazla 50 karakter olmalı!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen Açıklama Giriniz!")]
        [Display(Name = "Açıklama")]       
        public string Description { get; set; }

        public string CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string Ara { get; set; }
    }
}