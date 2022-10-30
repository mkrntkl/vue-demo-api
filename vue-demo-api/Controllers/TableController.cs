using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using vui_demo_api.Models;

namespace vui_demo_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ILogger<TableController> _logger;
        private readonly AppDbContext _appDbContext;

        public TableController(ILogger<TableController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        [SwaggerOperation(Summary = "Get all saved tables")]
        [HttpGet()]
        public async Task<Table[]> Get()
        {
            List<Table> tables = await _appDbContext.Tables.OrderBy(table => table.CreatedAt).ToListAsync();
            return tables.ToArray();
        }

        [SwaggerOperation(Summary = "Get a table by id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> Get(Guid id)
        {
            Table? table = await _appDbContext.Tables.FirstOrDefaultAsync(table => table.Id == id);

            if (table == null)
            {
                return NotFound(new
                {
                    Id = id,
                    Error = "No table found"
                });
            }

            table.Headers = await _appDbContext.TableHeaders.Where(header => header.TableId.Equals(id)).ToListAsync();
            table.Values = await _appDbContext.TableValues.Where(value => value.TableId.Equals(id)).ToListAsync();

            return table;
        }

        [SwaggerOperation(Summary = "Create a new table")]
        [HttpPost()]
        public async Task<ActionResult<Table>> Post(Table table)
        {
            try
            {
                _appDbContext.Tables.Add(table);
                _appDbContext.TableHeaders.AddRange(table.Headers);
                _appDbContext.TableValues.AddRange(table.Values);
                _appDbContext.SaveChanges();

                Table? newTable = await _appDbContext.Tables.FirstOrDefaultAsync(t => t.Id == table.Id);

                return newTable;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                return BadRequest();
            }
        }

        //TODO: updating existing tables
        //[SwaggerOperation(Summary = "Update an existing table")]
        //[HttpPut("{id}")]
        //public async Task<Table> Put(Guid id, Table table)
        //{
        //    Table? existingTable = await _appDbContext.Tables.FirstOrDefaultAsync(table => table.Id == id);

        //    if (existingTable != null)
        //    {
        //        List<TableHeader> tableHeaders = await _appDbContext.TableHeaders
        //            .Where(tableHeader => existingTable.Id == tableHeader.TableId)
        //            .ToListAsync();
        //    }

        //    return null;
        //}
    }
}