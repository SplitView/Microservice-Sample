using Book.Application.Author.Commands;
using Book.Application.Author.Queries;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book.API.Controllers
{
    public class AuthorController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> GetAllAuthors()
        {
            return Ok(await Mediator.Send(new GetAllAuthors()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthorById(Guid id)
        {
            var author = await Mediator.Send(new GetAuthorById(id));

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateAuthor(AddAuthorCommand command)
        {
            var author = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
        }
    }
}
