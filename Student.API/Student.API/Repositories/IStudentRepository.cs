namespace Student.API.Repositories
{
	public interface IStudentRepository
	{
		Task<List<DataModels.Student>> GetStudentsAsync();
		Task<DataModels.Student> GetStudentAsync(Guid studentId);

		Task<List<DataModels.Gender>> GetGendersAsync();
		Task<bool> Exists(Guid studentId);
		Task<DataModels.Student> UpdateStudent(Guid studentId, DataModels.Student student);
		Task<DataModels.Student> DeleteStudent(Guid studentId);
		Task<DataModels.Student> AddStudent( DataModels.Student student);
		Task<bool>UpdateProfileImage(Guid studentId, string profileImageUrl);
	}
}
