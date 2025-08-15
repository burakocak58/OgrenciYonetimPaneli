using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Student.API.Repositories;

namespace Student.API.Controllers
{
	[ApiController]
	public class GendersController : Controller
	{
		private readonly IStudentRepository studentRepository;
		private readonly IMapper mapper;
		public GendersController(IStudentRepository studentRepository, IMapper mapper)
		{
			this.studentRepository = studentRepository;
			this.mapper = mapper;
		}
		[HttpGet]
		[Route("[controller]")]
		public async Task<IActionResult> GetAllGendersAsync()
		{
			var GenderList = await studentRepository.GetGendersAsync();
			if (GenderList == null || !GenderList.Any())
			{
				return NotFound();
			}
			return Ok(mapper.Map<List<DomainModels.Gender>>(GenderList));
		}
	}
}
