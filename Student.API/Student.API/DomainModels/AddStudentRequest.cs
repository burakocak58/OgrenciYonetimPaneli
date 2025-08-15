namespace Student.API.DomainModels
{
	public class AddStudentRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }

		public Guid GenderId { get; set; }
		public string PhysicalAdress { get; set; }
		public string PostalAdress { get; set; }
	}
}
