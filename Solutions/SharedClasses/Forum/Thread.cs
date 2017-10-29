using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SharedClasses.Forum
{
    class Thread
    {

        [Key]
        public int ThreadID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ForumID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int AuthorID { get; set; }

        [Required]
        [Display(Name = "Subject")]
        [MaxLength(400)]
        public string ThreadSubject { get; set; }

        [Required]
        [MaxLength(2000)]
        [Display(Name = "Body")]
        public int Body { get; set; }

        [Display(Name = "Posted")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PostedDate { get; set; }
    }
}
