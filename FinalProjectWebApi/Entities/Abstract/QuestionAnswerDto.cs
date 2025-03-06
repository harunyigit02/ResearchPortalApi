namespace FinalProjectWebApi.Entities.Abstract
{
    public class QuestionAnswerDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<OptionDTO> Options { get; set; } = new();
        public int SelectedOptionId { get; set; }
        public DateTime ParticipatedAt { get; set; }
    }
}
