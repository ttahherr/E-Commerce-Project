using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Project.Models
{
    public class Movie
    {
        public int Id { get; set; }


        [DisplayName("Movie Name")]
        [Required(ErrorMessage ="This field is required")]
        public string Name { get; set; }


		[DisplayName("Movie Description")]
		[Required(ErrorMessage = "This field is required")]
		public string Description { get; set; }


		[DisplayName("Start Date")]
		[Required(ErrorMessage = "This field is required")]
		[DataType(DataType.DateTime)]
		public DateTime StartDate { get; set; }


		[DisplayName("End Date")]
		[Required(ErrorMessage = "This field is required")]
		[DataType(DataType.DateTime)]
		public DateTime EndDate { get; set; }

		[DisplayName("Movie gener")]
		[Required(ErrorMessage = "This field is required")]
		public string Category { get; set; }

		[ValidateNever]
		public string? ImagePath { get; set; }

		[ValidateNever]
		[NotMapped]
		public IFormFile ImageFile { get; set; }

		[ValidateNever]
        public ICollection<Actor> _Actors{ get; set; }







    }
}
