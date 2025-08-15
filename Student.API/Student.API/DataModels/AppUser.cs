namespace Student.API.DataModels
{
	public class AppUser
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
	}

}
