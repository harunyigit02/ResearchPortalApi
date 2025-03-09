namespace FinalProjectWebApi.Entities.Abstract
{
    public class OptionFilterDto
    {
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
        public int Count { get; set; }
        public string Percentage { get; set; }
    }
}
