namespace REPOSITORY_API.Controllers
{
    using AutoMapper;
    using Contracts;
    using Entities.DataTransferObjects;
    using Entities.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="OwnerController" />.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILoggerManager _logger;

        /// <summary>
        /// Defines the _repository.
        /// </summary>
        private readonly IRepositoryWrapper _repository;

        //map
        /// <summary>
        /// Defines the _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerController"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILoggerManager"/>.</param>
        /// <param name="repository">The repository<see cref="IRepositoryWrapper"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// The GetAllOwners.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            try
            {

                var owners =await _repository.Owner.GetAllOwners();
                _logger.LogInfo($"Returned all owners from database.");

                var ownersResult = _mapper.Map<IEnumerable<OwnerDto>>(owners);

                return Ok(ownersResult);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// The GetOwnerById.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="ActionResult{Owner}"/>.</returns>
        [HttpGet("{id}", Name = "OwnerById")]
        public async Task<IActionResult> GetOwnerById(Guid id)
        {
            try
            {
                var getOwner = await _repository.Owner.GetOwnerById(id);
                if (getOwner == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with id: {id}");
                    var ownerResult = _mapper.Map<Owner>(getOwner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// The GetOwnerWithDetails.
        /// </summary>
        /// <param name="guid">The guid<see cref="Guid"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpGet("{guid}/account")]
        public async Task<IActionResult> GetOwnerWithDetails(Guid guid)
        {
            try
            {
                var owner_detail = await _repository.Owner.GetOwnerWithDetails(guid);
                if (owner_detail == null)
                {
                    _logger.LogError($"Owner with id: {guid}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with details for id: {guid}");
                    var ownerResult = _mapper.Map<OwnerDto>(owner_detail);
                    return Ok(ownerResult);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// The CreateOwner.
        /// </summary>
        /// <param name="ownerDto">The ownerDto<see cref="OwnerForCreationDto"/>.</param>
        /// <returns>The <see cref="ActionResult{OwnerDto}"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateOwner(OwnerForCreationDto ownerDto)
        {
            try
            {
                if (ownerDto == null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var owner = _mapper.Map<Owner>(ownerDto);
                _repository.Owner.CreateOwner(owner);
               await _repository.Save();

                var createdOwner = _mapper.Map<OwnerDto>(owner);
                return CreatedAtRoute("OwnerById", new { id = createdOwner.Id }, createdOwner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// The UpdateOwner.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="ownerDto">The ownerDto<see cref="OwnerForUpdateDto"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwner(Guid id, OwnerForUpdateDto ownerDto)
        {
            try
            {
                if (ownerDto == null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var owner = await _repository.Owner.GetOwnerById(id);
                if (owner == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(ownerDto, owner);
                _repository.Owner.UpdateOwner(owner);
               await _repository.Save();
                return NoContent();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// The DeleteOwner.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(Guid id)
        {
            try
            {
                var owner =await _repository.Owner.GetOwnerById(id);
                if (owner == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Owner.DeleteOwner(owner);
              await _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                if (_repository.Account.AccountsByOwner(id).Any())
                {
                    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");
                    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                }
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
