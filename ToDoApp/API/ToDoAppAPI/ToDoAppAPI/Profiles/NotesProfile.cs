namespace ToDoAppAPI.Profiles
{
    public class NotesProfile:AutoMapper.Profile
    {
        public NotesProfile()
        {
            CreateMap<Db.Note, Models.Note>();
        }
        
    }
}
