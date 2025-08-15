using System.Reflection.Metadata.Ecma335;

namespace Student.API.DataModels
{
	public class Address
	{
		public Guid Id { get; set; }
		public string PhysicalAdress { get; set; }
		public string PostalAdress { get; set; }

		public Guid StudentId { get; set; }

	}
}
