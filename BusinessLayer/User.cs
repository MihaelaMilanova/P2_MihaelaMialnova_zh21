using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(20, ErrorMessage = "First Name cannot be more than 20 symbols")]
		public string FirstName { get; set; } = string.Empty;

        [Required]
		[MaxLength(20, ErrorMessage = "Last Name cannot be more than 20 symbols")]
		public string LastName { get; set; } = string.Empty;

        [Required]
		[Range(18, 81, ErrorMessage = "Age must be in the range [18;81]")]
		public int Age {  get; set; } 

        [Required]
		[MaxLength(20, ErrorMessage = "Username cannot be more than 20 symbols")]
		public string Username { get; set; } = string.Empty;

        [Required]
		[MaxLength(70, ErrorMessage = "Password cannot be more than 70 symbols")]
		public string Password { get; set; } = string.Empty;

        [Required]
		[MaxLength(20, ErrorMessage = "Email cannot be more than 20 symbols")]
		public string Email { get; set; } = string.Empty;

		
		public List<User> Friends { get; set; } = new();

		public List<Interest> Interests { get; set; } = new();

		private User()
		{

        }

		public User(int id, string firstName, string lastName, int age, string username, string password, string email)
		{
			FirstName = firstName;
			LastName = lastName;
			Age = age;
			Username = username;
			Password = password;
			Email = email;
			Friends = new List<User>();
			Interests = new List<Interest>();
		}
	}
}
