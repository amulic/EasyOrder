using AutoMapper;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using EasyOrder.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : Controller
    {
        private IBillRepository _billRepository;
        private IMapper _mapper;

        public BillController(IBillRepository billRepository, IMapper mapper)
        {
            _billRepository = billRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetBills()
        {
            var bills = _mapper.Map<List<BillDto>>(_billRepository.GetBills());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bills);
        }

        [HttpGet("{billId}")]
        [ProducesResponseType(200, Type = typeof(Bill))]
        [ProducesResponseType(400)]
        public IActionResult GetBill(int billId) 
        {
            if (!_billRepository.BillExists(billId))
                return NotFound();

            var bill = _mapper.Map<BillDto>(_billRepository.GetBill(billId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bill);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBill([FromBody] BillDto billCreate)
        {
            if (billCreate == null)
                return BadRequest();

            var bill = _billRepository.GetBills().Where(c => c.Id == billCreate.Id).FirstOrDefault();

            if (bill != null)
            {
                ModelState.AddModelError("", "Bill already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest();

            var billMap = _mapper.Map<Bill>(billCreate);

            if (!_billRepository.CreateBill(billMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

    }
}
