using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
	public class Field
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(20, ErrorMessage = "Name cannot be more than 20 symbols.")]
		public string Name { get; set; } = string.Empty;

		public List<User> Users { get; set; }

		private Field()
		{
            Users = new List<User>();
        }

		public Field(int id, string name )
		{
			Id = id;
			Name= name;
			Users = new List<User>();
		}
	}
}
