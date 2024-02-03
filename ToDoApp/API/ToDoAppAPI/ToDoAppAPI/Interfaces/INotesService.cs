using ToDoAppAPI.Db;

namespace ToDoAppAPI.Interfaces
{
    public interface INotesService
    {
        Task<(bool IsSuccess, IEnumerable<Models.Note> Notes, string ErrorMessage)> GetNotes(string connString);
        (bool IsSuccess,  string ErrorMessage) AddNotes(string connString, string newNote);
    }
}
