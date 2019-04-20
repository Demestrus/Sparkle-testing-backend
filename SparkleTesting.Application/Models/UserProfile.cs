using SparkleTesting.Domain.Entities;

namespace SparkleTesting.Application.Models
{
    public class UserProfile
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string StudyYear { get; set; }
        public byte[] Photo { get; set; }

        public UserProfile()
        {

        }
        public UserProfile(User user)
        {
            Surname = user.Surname;
            Name = user.Name;
            Patronymic = user.Patronymic;
            StudyYear = user.StudyYear;
            Photo = user.Photo;
        }
    }
}
