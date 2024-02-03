using AutoMapper;
using ToDoAppAPI.Db;
using ToDoAppAPI.Interfaces;

namespace ToDoAppAPI.Providers
{
    public class NotesProvider : INotesService
    {
        private readonly ILogger<NotesProvider> _logger;
        private readonly IMapper _mapper;
        public NotesProvider(ILogger<NotesProvider> logger,IMapper mapper)
        {
            this._logger = logger;
           this._mapper = mapper;
        }

        public (bool IsSuccess, string ErrorMessage) AddNotes(string connString, string newNote)
        {
            try
            {
                DbManager.AddNotes(connString, newNote);
                
                return (true, string.Empty);
                
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false,  ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Note> Notes, string ErrorMessage)> GetNotes(string connString)
        {
            try
            {
                List<Note> notes = DbManager.GetNotes(connString);
                //if (notes != null && notes.Any())
                //{
                    var result = _mapper.Map<IEnumerable<Note>, IEnumerable<Models.Note>>(notes);
                    if(result != null && result.Any())
                    {
                        return(true, result,string.Empty);
                    }
                    return (false, Enumerable.Empty<Models.Note>(), "Not found");
                //}
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, Enumerable.Empty<Models.Note>(), ex.Message);
            }
        }
    }
}
