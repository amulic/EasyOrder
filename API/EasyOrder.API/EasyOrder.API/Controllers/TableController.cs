using AutoMapper;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using EasyOrder.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : Controller
    {
        private ITableRepository _tableRepository;
        private IMapper _mapper;

        public TableController(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        [HttpGet("{tableId}")]
        [ProducesResponseType(200, Type = typeof(Table))]
        [ProducesResponseType(400)]
        public IActionResult GetTable(int tableId)
        {
            if (!_tableRepository.TableExists(tableId))
                return NotFound();

            var table = _mapper.Map<BillDto>(_tableRepository.GetTable(tableId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(table);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetTables()
        {
            var tables = _mapper.Map<List<TableDto>>(_tableRepository.GetTables());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tables);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTable([FromBody] TableDto tableCreate)
        {
            if (tableCreate == null)
                return BadRequest();

            var table = _tableRepository.GetTables().Where(c => c.Id == tableCreate.Id).FirstOrDefault();

            if (table != null)
            {
                ModelState.AddModelError("", "Table already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest();

            var tableMap = _mapper.Map<Table>(tableCreate);

            if (!_tableRepository.CreateTable(tableMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
