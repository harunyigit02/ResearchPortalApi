namespace FinalProjectWebApi.Entities.Abstract
{
    public class OptionDTO
    {
        public int QuestionId { get; set; } // Hangi soruya ait olduğunu belirtir
        public string OptionText { get; set; } // Seçenek metni
    }
}
