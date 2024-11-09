namespace AnimalAidPlatform.API.Controllers
{
    using AnimalAidPlatform.API.Models.DTO.Report;
    using AnimalAidPlatform.API.Models;
    using AnimalAidPlatform.API.Repositories.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using AnimalAidPlatform.API.Models.DTO;

    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        // GET: api/report
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            var reports = await _reportRepository.GetAllAsync();
            return Ok(reports);
        }

        // GET: api/report/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report == null) return NotFound();

            return Ok(report);
        }

        // POST: api/report
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReport([FromBody] ReportDTO reportDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var report = new Report
            {
                FeedPostId = reportDTO.FeedPostId,
                ReporterId = userId,
                Reason = reportDTO.Reason,
                ReportDate = DateTime.UtcNow,
                IsResolved = false
            };

            await _reportRepository.AddAsync(report);

            return CreatedAtAction(nameof(GetReport), new { id = report.Id }, report);
        }

        // DELETE: api/report/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            await _reportRepository.DeleteAsync(id);
            return NoContent();
        }

        // PUT: api/report/resolve/{id}
        [HttpPut("resolve/{id}")]
        [Authorize(Roles = "ADMIN")] 
        public async Task<IActionResult> ResolveReport(int id, [FromBody] ResolveReportDTO resolveReportDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _reportRepository.ResolveReportAsync(id, resolveReportDto);
            return NoContent();
        }

        // GET: api/report/feedpost/{feedPostId}
        [HttpGet("feedpost/{feedPostId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<IEnumerable<Report>>> GetReportsByFeedPostId(int feedPostId)
        {
            var reports = await _reportRepository.GetReportsByFeedPostIdAsync(feedPostId);

            if (reports == null || !reports.Any())
            {
                return NotFound(); 
            }

            return Ok(reports);
        }
    }

}
