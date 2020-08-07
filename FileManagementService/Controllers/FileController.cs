using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedSky.FileManagement.Contracts.DataTransferObjects;
using RedSky.FileManagement.Contracts.Entities;
using RedSky.FileManagement.Contracts.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RedSky.FileManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private IServiceWrapper _services;
        private IMapper _mapper;

        public FileController(IServiceWrapper services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        /// <summary>
        /// Get information on uploaded files
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FileSummaryDto>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<FileSummaryDto>> Get()
        {
            //Project queryable to lazy load and avoid file blob in memory
            List<FileSummaryDto> files = _mapper.ProjectTo<FileSummaryDto>(_services.Files.GetAll()).ToList();
            return Ok(files);
        }

        /// <summary>
        /// Download file by ID
        /// </summary>
        /// <param name="id">ID of file to download</param>
        /// <returns></returns>
        [HttpGet("download/{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> Download(int id)
        {
            FileData file = _services.Files.Download(id);
            if (file == null) return NotFound();
            string encodedFile = file.File.ToString();
            return File(file.File, file.Type);
        }

        /// <summary>
        /// Upload file from single request form file
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(FileSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FileSummaryDto>> Upload()
        {
            IFormFile file = Request.Form.Files[0];
            if (file == null) return BadRequest();

            //Form the input into a collection entry
            FileData data = new FileData()
            {
                Name = file.FileName,
                Uploaded = DateTime.UtcNow,
                Type = file.ContentType
            };

            //Add the file as byte array
            using (MemoryStream stream = new MemoryStream())
            {
                file.CopyTo(stream);
                var bytes = stream.ToArray();
                data.File = bytes;
            }

            //Insert and return data with populated Id
            FileData updated = await _services.Files.Add(data);
            return Ok(_mapper.Map<FileSummaryDto>(updated));
        }

        /// <summary>
        /// Delete files by ID
        /// </summary>
        /// <param name="ids">IDs of files to delete</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<ActionResult> DeleteRange([FromBody]IEnumerable<int> ids)
        {
            await _services.Files.DeleteRange(ids);
            return Accepted();
        }

        /// <summary>
        /// Delete single file by ID
        /// </summary>
        /// <param name="id">ID of file to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<ActionResult> Delete(int id)
        {
            //This currently lacks validation for file presence
            await _services.Files.Delete(id);
            return Accepted();
        }
    }
}
