using FinalProjectWebApi.Entities.Concrete;

namespace FinalProjectWebApi.Entities.Abstract
{
    public class UserAnswerDto
    {
        public int UserId { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
