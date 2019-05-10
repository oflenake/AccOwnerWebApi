using System;
using System.Linq;
using Contracts;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccOwnerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        #region Fields
        private string _component;
        private string _process;
        private string _message;
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        #endregion

        #region Constructor
        /// <summary>
        /// Inject the logger and repository parameter services inside the constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _component = "OwnerController";
            _process = string.Empty;
            _message = string.Empty;
            _logger = logger;
            _repository = repository;
        }
        #endregion

        #region Public Methods
        // Decorate 'GetAllAction' action method with '[HttpGet]' attribute to map 
        // the action to the 'GET' request. Use both injected parameters to 
        // log the messages and to get the data from the repository class.

        // No route attribute right above the action method, then the route 
        // will be: api/owner (http://localhost:5050/api/owner).

        // GET: api/owner - Get All Owners
        [HttpGet]
        public IActionResult GetAllAction()
        {
            try
            {
                _process = "GetAllAction";
                var owners = _repository.Owner.GetAllData();

                _message = "Returned all owners from database";
                _logger.LogInfo($"{string.Format("[{0}.{1}] - {2}.", _component, _process, _message)}");

                return Ok(owners); // Ok status code is: 200
            }
            catch (Exception ex)
            {
                _message = "An exception occured in 'GetAllAction' action method";
                _logger.LogError($"{string.Format("[{0}.{1}] - {2}:", _component, _process, _message)} {ex.Message}");

                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/owner/1234-abcd-5678-efgh - Get Owner by id
        [HttpGet("{id}", Name = "GetByIDOwner")]
        public IActionResult GetByIDAction(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetByIDData(id);

                if (owner.IsEmptyObject())
                {
                    _logger.LogError($"[OwnerController.GetByIDAction] - Owner with id: {id}, hasn't been found in db.");
                    return NotFound("Owner not found");
                }
                else
                {
                    _logger.LogInfo($"[OwnerController.GetByIDAction] - Returned owner with id: {id}.");
                    return Ok(owner); // Ok status code is: 200
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[OwnerController.GetByIDAction] - An exception occured in " +
                                 $"'GetByIDAction' action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/owner/1234-abcd-5678-efgh/account - Get all Accounts for an Owner
        [HttpGet("{id}/account")]
        public IActionResult GetByIDRelatedAction(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetByIDRelatedData(id);

                if (owner.IsEmptyObject())
                {
                    _logger.LogError($"[OwnerController.GetByIDRelatedAction] - Owner with id: {id}, hasn't been found in db.");
                    return NotFound("Owner not found");
                }
                else
                {
                    _logger.LogInfo($"[OwnerController.GetByIDRelatedAction] - Returned owner with id: {id}, related details.");
                    return Ok(owner); // Ok status code is: 200
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[OwnerController.GetByIDRelatedAction] - An exception occured in " +
                                 $"'GetByIDRelatedAction' action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/owner - Add Owner
        [HttpPost]
        public IActionResult PostCreateAction([FromBody]Owner owner)
        {
            try
            {
                if (owner.IsObjectNull())
                {
                    _logger.LogError("[OwnerController.PostCreateAction] - Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("[OwnerController.PostCreateAction] - Invalid model owner object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Owner.PostCreateData(owner);
                _logger.LogInfo($"[OwnerController.PostCreateAction] - New owner with id: {owner.ID}, " +
                                $"created at route: 'GetByIDOwner'.");
                // CreatedAtRoute will return a status code 201, which stands for Created and also 
                // it populates the body of the response with the new owner object, as well as the 
                // location attribute within the response header with the address to retrieve 
                // the new owner and its address Url.
                return CreatedAtRoute("GetByIDOwner", new { id = owner.ID }, owner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[OwnerController.PostCreateAction] - An exception occured in " +
                                 $"'PostCreateAction' action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/owner/1234-abcd-5678-efgh - Update Owner
        [HttpPut("{id}")]
        public IActionResult PutUpdateAction(Guid id, [FromBody]Owner owner)
        {
            try
            {
                if (owner.IsObjectNull())
                {
                    _logger.LogError("[OwnerController.PutUpdateAction] - Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("[OwnerController.PutUpdateAction] - Invalid model owner object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbOwner = _repository.Owner.GetByIDData(id);

                if (dbOwner.IsEmptyObject())
                {
                    _logger.LogError($"[OwnerController.PutUpdateAction] - Owner with id: {id}, hasn't been found in db.");
                    return NotFound("Owner not found");
                }

                _repository.Owner.PutUpdateData(dbOwner, owner);
                _logger.LogInfo($"[OwnerController.PutUpdateAction] - Owner with id: {owner.ID}, " +
                                $"updated successfully.");

                return NoContent(); // Stands for status code 204
            }
            catch (Exception ex)
            {
                _logger.LogError($"[OwnerController.PutUpdateAction] - An exception occured in " +
                                 $"'PutUpdateAction' action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/owner/1234-abcd-5678-efgh - Delete Owner
        [HttpDelete("{id}")]
        public IActionResult DeleteByIDAction(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetByIDData(id);

                if (owner.IsEmptyObject())
                {
                    _logger.LogError($"[OwnerController.DeleteByIDAction] - Owner with id: {id}, hasn't been found in db.");
                    return NotFound("Owner not found");
                }

                if (_repository.Account.GetByIDData(id).Any())
                {
                    _logger.LogWarn($"[OwnerController.DeleteByIDAction] - Cannot delete owner with id: {id}. " +
                                     $"It has related accounts. Delete those accounts first.");
                    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                }

                _repository.Owner.DeleteByIDData(owner);
                _logger.LogInfo($"[OwnerController.DeleteByIDAction] - Owner with id: {id}, " +
                                $"deleted successfully.");

                return NoContent(); // Stands for status code 204
            }
            catch (Exception ex)
            {
                _logger.LogError($"[OwnerController.DeleteByIDAction] - An exception occured in " +
                                 $"'DeleteByIDAction' action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion
    }
}
