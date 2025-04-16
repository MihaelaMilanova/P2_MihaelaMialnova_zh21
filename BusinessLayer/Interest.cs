using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
	public class Interest
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(20, ErrorMessage = "Name cannot be more than 20 symbols.")]
		public string Name { get; set; } = string.Empty;

        [ForeignKey("FieldId")]
        Field? FieldId { get; set; }

        public List<User>? Users { get; set; }
        public Field? FieldOfInterest { get; set; }


        private Interest()
		{
			
		}

		public Interest(int id, string name, Field? field)
		{
			Id = id;
			Name = name;
			FieldOfInterest = field;
			Users = new List<User>();
		}
	}
}
