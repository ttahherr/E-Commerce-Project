using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Project.Models
{
    public class Actor
    {
        public int Id { get; set; }


		[DisplayName("Actor Name")]
		[Required(ErrorMessage = "This field is required")]
		public string Name { get; set; }


		[DisplayName("Actor Information")]
		[Required(ErrorMessage = "This field is required")]
		public string Info { get; set; }


		[DisplayName("Actor Nationality")]
		[Required(ErrorMessage = "This field is required")]
		public string Nationality { get; set; }


		[ValidateNever]
		public string? ImagePath { get; set; }



		[ValidateNever]
		[NotMapped]
		public IFormFile ImageFile { get; set; }

		[ValidateNever]
		[ForeignKey("Movie")]
        public int _MovieItemsID { get; set; }
				
		[ValidateNever]
		public Movie Movie { get; set; }


	}
}
