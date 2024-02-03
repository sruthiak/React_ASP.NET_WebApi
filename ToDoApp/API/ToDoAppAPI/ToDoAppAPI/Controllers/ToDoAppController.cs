using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoAppAPI.Interfaces;

namespace ToDoAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoAppController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly INotesService notesService;

        public ToDoAppController(IConfiguration configuration,INotesService notesService)
        {
            this.configuration = configuration;
            this.notesService = notesService;
        }
        [HttpGet]
        [Route("GetNotes")]
        public async Task<JsonResult> GetNotes()
        {
            var result = await notesService.GetNotes(configuration.GetConnectionString("ToDoAppConString"));
            if (result.IsSuccess)
            {
                //var json = JsonConvert.SerializeObject(result.Notes, Formatting.Indented);
                return new JsonResult(result.Notes);
            }
            return new JsonResult("Not Found");
        }

        [HttpPost]
        [Route("AddNotes")]
        public JsonResult AddNotes([FromForm] string newNote)
        {
            var result =  notesService.AddNotes(configuration.GetConnectionString("ToDoAppConString"),newNote);
            if (result.IsSuccess)
            {
                //var json = JsonConvert.SerializeObject(result.Notes, Formatting.Indented);
                return new JsonResult("Notes added succesfully");
            }
            return new JsonResult(result.ErrorMessage);
        }
    }
}
