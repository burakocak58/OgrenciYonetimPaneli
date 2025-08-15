using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Student.API.DataModels;
using Student.API.DomainModels;
using Student.API.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace Student.API.Controllers
{
	[ApiController]
	public class StudentsController : Controller
	{
		private readonly IStudentRepository studentRepository;
		private readonly IMapper mapper;
		private readonly IImageRepository imageRepository;
		public StudentsController(IStudentRepository studentRepository, IMapper mapper, IImageRepository imageRepository)

		{
			this.studentRepository = studentRepository;
			this.mapper = mapper;
			this.imageRepository = imageRepository;
		}
		[HttpGet]
		[Route("[controller]")]
		public async Task<IActionResult> GetAllStudentsAsync()
		{
			var students = await studentRepository.GetStudentsAsync();

			return Ok(mapper.Map<List<DomainModels.Student>>(students));
		}

		[HttpGet]
		[Route("[controller]/{studentId:guid}"),ActionName("GetStudentAsync")] 
		public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
		{
			var student = await studentRepository.GetStudentAsync(studentId);
			if (student == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<DomainModels.Student>(student));
		}
		[HttpPut]
		[Route("[controller]/{studentId:guid}")]
		public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request )
		{
			if (await studentRepository.Exists(studentId))
			{
				var uptatedStudent = await studentRepository.UpdateStudent(studentId, mapper.Map<DataModels.Student>(request));
				if (uptatedStudent != null)
				{
					return Ok(mapper.Map<DataModels.Student>(uptatedStudent));
				}
			}
			return NotFound();
		}
		[HttpDelete]
		[Route("[controller]/{studentId:guid}")]
		public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
		{
			if (await studentRepository.Exists(studentId))
			{
				var student = await studentRepository.DeleteStudent(studentId);
				if (student != null)
				{
					return Ok(mapper.Map<DataModels.Student>(student));
				}
			}
			return NotFound();
		}
		[HttpPost]
		[Route("[controller]/Add")]
		public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
		{
			var student = await studentRepository.AddStudent(mapper.Map<DataModels.Student>(request));
			return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id },mapper.Map<DataModels.Student>(student));
		}
		[HttpPost]
		[Route("[controller]/{studentId:guid}/upload-image")]
		public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
		{
			var validFileExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
			if (profileImage != null && profileImage.Length > 0)
			{
				var extension = Path.GetExtension(profileImage.FileName);
				if (validFileExtensions.Contains(extension))
				{
					if (await studentRepository.Exists(studentId))
					{
						var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
						var FileImagePath = await imageRepository.Upload(profileImage, fileName);
						if (await studentRepository.UpdateProfileImage(studentId, FileImagePath))
						{
							return Ok(FileImagePath);
						}
						return StatusCode(StatusCodes.Status500InternalServerError, "Error updating profile image");
					}
				}
				return BadRequest("Dosya Formatı Uygun Değil!");
			}
			return NotFound();
			
		}
	 
	}
}
