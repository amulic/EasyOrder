using AutoMapper;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using EasyOrder.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private ISupplierRepository _supplierRepository;
        private IMapper _mapper;

        public SupplierController(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        [HttpGet("{supplierId}")]
        [ProducesResponseType(200, Type = typeof(Supplier))]
        [ProducesResponseType(400)]
        public IActionResult GetSupplier(Guid supplierId)
        {
            if (!_supplierRepository.SupplierExists(supplierId))
                return NotFound();

            var supplier = _mapper.Map<SupplierDto>(_supplierRepository.GetSupplier(supplierId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(supplier);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetBills()
        {
            var suppliers = _mapper.Map<List<SupplierDto>>(_supplierRepository.GetSuppliers());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(suppliers);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBill([FromBody] SupplierDto supplierCreate)
        {
            if (supplierCreate == null)
                return BadRequest();

            var supplier = _supplierRepository.GetSuppliers().Where(c => c.Id == supplierCreate.Id).FirstOrDefault();

            if (supplier != null)
            {
                ModelState.AddModelError("", "Bill already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest();

            var supplierMap = _mapper.Map<Supplier>(supplierCreate);

            if (!_supplierRepository.CreateSupplier(supplierMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
