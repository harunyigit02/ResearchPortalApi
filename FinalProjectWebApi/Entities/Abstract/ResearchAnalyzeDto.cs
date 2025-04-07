namespace FinalProjectWebApi.Entities.Abstract
{
    public class ResearchAnalyzeDto
    {
        public int ResearchId { get; set; }
        public List<OptionFilterDto> FilterDtos { get; set; }
    }
}
