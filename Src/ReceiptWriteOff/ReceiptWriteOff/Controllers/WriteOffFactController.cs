using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using ReceiptWriteOff.Application.Abstractions;
    using ReceiptWriteOff.Application.Contracts.WriteOffFact;
    using ReceiptWriteOff.Application.Implementations.Exceptions;
    using ReceiptWriteOff.Contracts.WriteOffFact;
    using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation.Exceptions;
    // ReSharper disable InconsistentNaming

    namespace ReceiptWriteOff.Controllers;
    
    [ApiController]
    [Route("api/v1/write-off-facts")]
    public class WriteOffFactController(IWriteOffFactService _writeOffFactService, IMapper _mapper)
        : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<WriteOffFactShortResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var writeOffFacts = (await _writeOffFactService.GetAllAsync(cancellationToken))
                .Select(_mapper.Map<WriteOffFactShortResponse>).ToList();
    
            return Ok(writeOffFacts);
        }
    
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WriteOffFactResponse>> GetAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var writeOffFact = await _writeOffFactService.GetAsync(id, cancellationToken);
                var writeOffFactResponse = _mapper.Map<WriteOffFactResponse>(writeOffFact);
                return Ok(writeOffFactResponse);
            }
            catch (EntityNotFoundException e)
            {
                Console.WriteLine(e);
                return NotFound($"No Write Off Fact with Id {id} found");
            }
        }
    
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<WriteOffFactResponse>> RegisterAsync([FromBody] RegisterWriteOffFactRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var registerWriteOffFactDto = _mapper.Map<RegisterWriteOffFactDto>(request);
                var writeOffFact = await _writeOffFactService.RegisterAsync(registerWriteOffFactDto, cancellationToken);
                var writeOffFactResponse = _mapper.Map<WriteOffFactResponse>(writeOffFact);
                return CreatedAtAction(nameof(GetAsync), new { id = writeOffFactResponse.Id }, writeOffFactResponse);
            }
            catch (AlreadyExistsException e)
            {
                Console.WriteLine(e);
                return Conflict(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                Console.WriteLine(e);
                return NotFound(e.Message);
            }
            catch (DateException e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditAsync(int id, [FromBody] RegisterWriteOffFactRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var registerWriteOffFactDto = _mapper.Map<RegisterWriteOffFactDto>(request);
                await _writeOffFactService.EditAsync(id, registerWriteOffFactDto, cancellationToken);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                Console.WriteLine(e);
                return NotFound($"No Write Off Fact with Id {id} found");
            }
        }
    
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _writeOffFactService.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                Console.WriteLine(e);
                return NotFound($"No Write Off Fact with Id {id} found");
            }
        }
    }